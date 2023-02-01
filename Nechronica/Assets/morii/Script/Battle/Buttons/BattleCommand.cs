using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.IO;

public class BattleCommand : MonoBehaviour
{
    protected const float COMMAND_SIZE = 90;

    [SerializeField]
    protected Doll_blu_Nor thisChara;                 // 自身を参照するための変数

    [SerializeField] protected List<CharaManeuver> maneuvers;

    private List<CharaManeuver> ActionManeuvers;   // 自身が持っているアクションマニューバを保存 
    private List<CharaManeuver> RapidManeuvers;    // 自身が持っているラピッドマニューバを保存
    private List<CharaManeuver> JudgeManeuvers;    // 自身が持っているジャッジマニューバを保存
    private List<CharaManeuver> DamageManeuvers;   // 自身が持っているダメージマニューバを保存

    private GameObject actSelect;                                   // アクションタイミングでの動きを決めるボタン
    public GameObject GetActSelect() => actSelect;
    
    [SerializeField] protected GameObject commands;
    [SerializeField] protected List<GameObject> parentCommand = new List<GameObject>();
    private List<GameObject> prefabObjList = new List<GameObject>();      // タイミングごとにわけられたマニューバの内容が格納されているリスト
    [SerializeField] protected GameObject originalParentObj;              // 上記プレハブの親Objの元となるオブジェクト
    [SerializeField] protected RectTransform backImg;
    [SerializeField] protected GameObject prefabButton;

    private List<GameObject> partsObjList = new List<GameObject>();     // 部位ごとに分けられたマニューバの内容が格納されているリスト

    private GameObject actionCommands;             // アクションタイミングのコマンドオブジェクト
    private GameObject rapidCommands;              // ラピッドタイミングのコマンドオブジェクト
    private GameObject judgCommands;               // ジャッジタイミングのコマンドオブジェクト
    private GameObject damageCommands;             // ダメージタイミングのコマンドオブジェクト

    public GameObject GetCommands() => commands;

    public GameObject GetActCommands() => actionCommands;
    public GameObject GetRpdCommands() => rapidCommands;
    public GameObject GetJdgCommands() => judgCommands;
    public GameObject GetDmgCommands() => damageCommands;

    private Button actionButton;                    // アクションのボタン
    private Button rapidButton;                     // ラピッドのボタン
    private Button standbyButton;                   // 待機のボタン



    private void Start()
    {
        // バトルシステムを取得
        thisChara = this.GetComponent<Doll_blu_Nor>();

        // ボタンを取得
        actionButton = this.transform.Find("Canvas/Act_select/Action").gameObject.GetComponent<Button>();
        rapidButton = this.transform.Find("Canvas/Act_select/Rapid").gameObject.GetComponent<Button>();
        standbyButton = this.transform.Find("Canvas/Act_select/Standby").gameObject.GetComponent<Button>();

        // ボタンにメソッドを加える
        actionButton.onClick.AddListener(OnClickAction);
        rapidButton.onClick.AddListener(OnClickRapid);
        standbyButton.onClick.AddListener(OnClickStandby);

        // アクション、ラピッド、待機を選ぶgameObjectを取得
        actSelect = this.transform.Find("Canvas/Act_select").gameObject;

        
    }

    protected virtual void InitCommand(int timing)
    {
        parentCommand.Add(BuildingParent(true, backImg));

        // パーツのマニューバとしての割り当てられているタイミングで分ける
        AddManeuver(thisChara.HeadParts, timing);
        AddManeuver(thisChara.ArmParts, timing);
        AddManeuver(thisChara.BodyParts, timing);
        AddManeuver(thisChara.LegParts, timing);

        // タイミングで分けられたマニューバ
        prefabObjList = BuildCommands(maneuvers, ref parentCommand, prefabObjList, backImg);

    }

    protected virtual void InitParts(int parts)
    {
        parentCommand.Add(BuildingParent(true, backImg));

        if(parts==DmgTimingProcess.HEAD)
        {
            partsObjList = BuildParts(thisChara.HeadParts, ref parentCommand, partsObjList, backImg, parts); 
        }
        else if (parts == DmgTimingProcess.ARM)
        {
            partsObjList = BuildParts(thisChara.ArmParts, ref parentCommand, partsObjList, backImg, parts);
        }
        else if (parts == DmgTimingProcess.BODY)
        {
            partsObjList = BuildParts(thisChara.BodyParts, ref parentCommand, partsObjList, backImg, parts);
        }
        else if (parts == DmgTimingProcess.LEG)
        {
            partsObjList = BuildParts(thisChara.LegParts, ref parentCommand, partsObjList, backImg, parts);
        }

        int a = 0;
        a++;
    }

    public void OnClickStandby()
    {
        // カウントを1減らして待機
        thisChara.NowCount = thisChara.NowCount - 1;
    }

    public void OnClickAction()
    {
        // アクションのコマンドを表示
        CommandAccessor.Instance.actCommands.GetCommands().SetActive(true);
    }

    public void OnClickRapid()
    {
        // ラピッドのコマンドを表示
        CommandAccessor.Instance.rpdCommands.GetCommands().SetActive(true);
    }

    /// <summary>
    /// キャラクターの各マニューバをそれぞれ分類わけする
    /// </summary>
    /// <param name="maneuvers"></param>
    public void AddManeuver(List<CharaManeuver> maneuvers)
    {
        for (int i = 0; i < maneuvers.Count; i++)
        {
            if (maneuvers[i].Timing == CharaBase.ACTION || maneuvers[i].Timing == CharaBase.MOVE)
            {
                ActionManeuvers.Add(maneuvers[i]);
            }
            else if (maneuvers[i].Timing == CharaBase.RAPID)
            {
                RapidManeuvers.Add(maneuvers[i]);
            }
            else if (maneuvers[i].Timing == CharaBase.JUDGE)
            {
                JudgeManeuvers.Add(maneuvers[i]);
            }
            else if (maneuvers[i].Timing == CharaBase.DAMAGE)
            {
                DamageManeuvers.Add(maneuvers[i]);
            }

        }
    }

    public void AddManeuver(List<CharaManeuver> _maneuvers, int timing)
    {
        for(int i=0;i<_maneuvers.Count;i++)
        {
            if(_maneuvers[i].Timing==timing)
            {
                maneuvers.Add(_maneuvers[i]);
            }
        }
    }

    /// <summary>
    /// コマンドを生成するメソッド
    /// </summary>
    /// <param name="maneuvers">マニューバの内容をクローンオブジェクトにする</param>
    /// <param name="prefabList">上記オブジェクトを格納し、管理するリスト</param>
    /// <param name="parentObj">上記オブジェクトリストを格納し、コマンド選択のページとしての扱いをする。</param>
    /// <returns><param name="prefabList"></returns>
    public List<GameObject> BuildCommands(List<CharaManeuver> maneuvers, ref List<GameObject> parentObj, List<GameObject> prefabList, RectTransform backImg)
    {
        GameObject Instance;
        // 親の数
        int countParent = 0;
        for (int i = 0; i < maneuvers.Count; i++)
        {
            Instance = Instantiate(prefabButton, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0));
            ButtonTexts clone = Instance.GetComponent<ButtonTexts>();
            clone.SetName(maneuvers[i].Name);
            clone.SetCost(maneuvers[i].Cost.ToString());

            // ここでボタンにオンクリックを追加。内容はマニューバ発動

            // 射程が複数存在する場合と、一か所にしか存在しない場合、もしくは自身に効果が及ぶ場合で処理を分ける
            if (maneuvers[i].MinRange == 10)
            {
                clone.SetRange("自身");
            }
            else if (maneuvers[i].MinRange != maneuvers[i].MaxRange)
            {
                clone.SetRange(maneuvers[i].MinRange.ToString() + "～" + maneuvers[i].MaxRange.ToString());
            }
            else
            {
                clone.SetRange(maneuvers[i].MinRange.ToString());
            }
            // 親を設定
            clone.transform.SetParent(parentObj[countParent].transform, false);
            AddFuncToButton(ref clone, maneuvers[i]);

            // コマンド5個区切りでコマンドの親オブジェクトを複製する。
            if ((i + 1) % 5 == 0)
            {
                countParent++;
                parentObj.Add(BuildingParent(false, backImg));
            }

            //リストに保存
            prefabList.Add(clone.gameObject);
        }

        return prefabList;
    }

    /// <summary>
    /// コマンドを生成するメソッド
    /// </summary>
    /// <param name="maneuvers">マニューバの内容をクローンオブジェクトにする</param>
    /// <param name="prefabList">上記オブジェクトを格納し、管理するリスト</param>
    /// <param name="parentObj">上記オブジェクトリストを格納し、コマンド選択のページとしての扱いをする。</param>
    /// <returns><param name="prefabList"></returns>
    public List<GameObject> BuildParts(List<CharaManeuver> maneuvers, ref List<GameObject> parentObj, List<GameObject> prefabList, RectTransform backImg, int parts)
    {
        GameObject Instance;
        // 親の数
        int countParent = 0;
        for (int i = 0; i < maneuvers.Count; i++)
        {
            Instance = Instantiate(prefabButton, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0));
            ButtonTexts clone = Instance.GetComponent<ButtonTexts>();
            clone.SetName(maneuvers[i].Name);
            clone.SetCost(maneuvers[i].Cost.ToString());

            // ここでボタンにオンクリックを追加。内容はマニューバ発動

            // 射程が複数存在する場合と、一か所にしか存在しない場合、もしくは自身に効果が及ぶ場合で処理を分ける
            if (maneuvers[i].MinRange == 10)
            {
                clone.SetRange("自身");
            }
            else if (maneuvers[i].MinRange != maneuvers[i].MaxRange)
            {
                clone.SetRange(maneuvers[i].MinRange.ToString() + "～" + maneuvers[i].MaxRange.ToString());
            }
            else
            {
                clone.SetRange(maneuvers[i].MinRange.ToString());
            }
            // 親を設定
            clone.transform.SetParent(parentObj[countParent].transform, false);
            ManeuverAndParts buff;
            buff.maneuver = maneuvers[i];
            buff.parts = parts;
            clone.GetComponent<Button>().onClick.AddListener(() => OnClickReflectDamage(buff));

            // コマンド5個区切りでコマンドの親オブジェクトを複製する。
            if ((i + 1) % 5 == 0)
            {
                countParent++;
                parentObj.Add(BuildingParent(false, backImg));
            }

            //リストに保存
            prefabList.Add(clone.gameObject);
        }

        return prefabList;
    }

    /// <summary>
    /// コマンドボタンを格納したオブジェクトリスト。
    /// </summary>
    /// <param name="isActive">アクティブ状態でクローンするかどうか</param>
    /// <returns></returns>
    protected GameObject BuildingParent(bool isActive, RectTransform backImg)
    {
        // サイズ調整----ゴリ押しなの気に食わん--------------------------
        // クローンした親オブジェクトのサイズ調整用
        Vector2 parentSize = backImg.sizeDelta;
        parentSize.y = parentSize.y - COMMAND_SIZE / 2;

        Vector3 parentPos = backImg.position;
        parentPos.y = parentPos.y + COMMAND_SIZE / 4;
        // --------------------------------------------------------------

        GameObject Instance;
        Instance = Instantiate(originalParentObj, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0), commands.transform);
        VerticalLayoutGroup parentClone = Instance.GetComponent<VerticalLayoutGroup>();
        // クローンしたオブジェクトの座標、サイズを調整する
        parentClone.GetComponent<RectTransform>().position = backImg.position;
        parentClone.GetComponent<RectTransform>().sizeDelta = backImg.sizeDelta;
        parentClone.gameObject.SetActive(isActive);
        parentClone.transform.SetParent(backImg.parent);
        return parentClone.gameObject;
    }

    protected void AddFuncToButton(ref ButtonTexts command, CharaManeuver maneuver)
    {
        if (maneuver.Timing == CharaBase.ACTION)
        {
            command.GetComponent<Button>().onClick.AddListener(() => OnClickActCommand(maneuver));
        }
        if (maneuver.Timing == CharaBase.MOVE)
        {
            command.GetComponent<Button>().onClick.AddListener(() => OnClickMoveCommand(maneuver));
        }
        if (maneuver.Timing == CharaBase.RAPID)
        {
            command.GetComponent<Button>().onClick.AddListener(() => OnClickRpdCommand(maneuver));
        }
        if (maneuver.Timing == CharaBase.JUDGE)
        {
            command.GetComponent<Button>().onClick.AddListener(() => OnClickJdgCommand(maneuver));
        }
        if (maneuver.Timing == CharaBase.DAMAGE)
        {
            command.GetComponent<Button>().onClick.AddListener(() => OnClickDmgCommand(maneuver));
        }
    }

    void OnClickActCommand(CharaManeuver maneuver)
    {
        // 必要な情報を送信
        ProcessAccessor.Instance.actTiming.SkillSelected = true;
        ProcessAccessor.Instance.actTiming.SetManeuver(maneuver);
        // 後のジャッジ、ダメージタイミングで攻撃を行うキャラが必要になるのでこの時点で行動するキャラを格納しておく
        ProcessAccessor.Instance.actTiming.ActingChara = thisChara;
        ProcessAccessor.Instance.actTiming.StandbyEnemySelect = true;
        // アルゴリズムメモ：攻撃しないアクションタイミングの為if文分けする
    }

    void OnClickJdgCommand(CharaManeuver maneuver)
    {
        ProcessAccessor.Instance.jdgTiming.SkillSelected = true;
        ProcessAccessor.Instance.jdgTiming.SetManeuver(maneuver);
        ProcessAccessor.Instance.jdgTiming.SetArea(thisChara.area);
        ProcessAccessor.Instance.jdgTiming.GetConfirmatButton().SetActive(true);
    }

    void OnClickRpdCommand(CharaManeuver maneuver)
    {
        ProcessAccessor.Instance.rpdTiming.SkillSelected = true;
        ProcessAccessor.Instance.rpdTiming.SetManeuver(maneuver);
        ProcessAccessor.Instance.rpdTiming.SetArea(thisChara.area);
        if(maneuver.MinRange==10)
        {
            // 移動マニューバの場合、移動する方向のボタンを表示
            if (maneuver.EffectNum.ContainsKey(EffNum.Move))
            {
                ProcessAccessor.Instance.actTimingMove.IsRapid = true;
                ProcessAccessor.Instance.actTimingMove.GetMoveButtons().SetActive(true);
            }
            else
            {
                ProcessAccessor.Instance.rpdTiming.GetConfirmatButton().SetActive(true);
            }
        }
        else
        {
            ProcessAccessor.Instance.rpdTiming.StandbyEnemySelect = true;
        }

        
    }

    void OnClickDmgCommand(CharaManeuver maneuver)
    {
        ProcessAccessor.Instance.dmgTiming.SkillSelected = true;
        ProcessAccessor.Instance.dmgTiming.SetManeuver(maneuver);
        ProcessAccessor.Instance.dmgTiming.SetArea(thisChara.area);
        ProcessAccessor.Instance.dmgTiming.GetConfirmatButton().SetActive(true);
    }

    void OnClickMoveCommand(CharaManeuver maneuver)
    {
        ProcessAccessor.Instance.actTimingMove.SkillSelected = true;
        ProcessAccessor.Instance.actTimingMove.ActingChara = thisChara;
        ProcessAccessor.Instance.actTimingMove.SetManeuver(maneuver);
    }

    void OnClickReflectDamage(ManeuverAndParts maneuver)
    {
        if(maneuver.parts==DmgTimingProcess.HEAD)
        {
            for (int i = 0; i < thisChara.HeadParts.Count; i++)
            {
                if(maneuver.maneuver.Name==thisChara.HeadParts[i].Name)
                {
                    thisChara.HeadParts[i].isDmage = true;
                }
            }
        }
        else if (maneuver.parts == DmgTimingProcess.ARM)
        {
            for (int i = 0; i < thisChara.ArmParts.Count; i++)
            {
                if (maneuver.maneuver.Name == thisChara.ArmParts[i].Name)
                {
                    thisChara.ArmParts[i].isDmage = true;
                }
            }
        }
        else if (maneuver.parts == DmgTimingProcess.BODY)
        {
            for (int i = 0; i < thisChara.BodyParts.Count; i++)
            {
                if (maneuver.maneuver.Name == thisChara.BodyParts[i].Name)
                {
                    thisChara.BodyParts[i].isDmage = true;
                }
            }
        }
        else if (maneuver.parts == DmgTimingProcess.LEG)
        {
            for (int i = 0; i < thisChara.LegParts.Count; i++)
            {
                if (maneuver.maneuver.Name == thisChara.LegParts[i].Name)
                {
                    thisChara.LegParts[i].isDmage = true;
                }
            }
        }
    }

    /// <summary>
    /// コマンドのネクストボタンを押されたときに反応するメソッド
    /// </summary>
    public void OnClickNext(List<GameObject> parentObj)
    {
        if(parentObj.Count >= 2)
        {

        }
    }

    /// <summary>
    /// コマンドのバックボタンを押されたときに反応するメソッド
    /// </summary>
    public void OnClickBack()
    {

    }
}

public struct ManeuverAndParts
{
    public CharaManeuver maneuver;
    public int parts;
}