using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.IO;

public class BattleCommand : MonoBehaviour
{
    const float COMMAND_SIZE = 90;

    [SerializeField]
    private Doll_blu_Nor thisChara;                 // 自身を参照するための変数

    [SerializeField]
    private BattleCommand thisCharaCommand;         // 自己参照

    [SerializeField] private List<CharaManeuver> ActionManeuvers;    // 自身が持っているアクションマニューバを保存 
    [SerializeField] private List<CharaManeuver> RapidManeuvers;     // 自身が持っているラピッドマニューバを保存
    [SerializeField] private List<CharaManeuver> JudgeManeuvers;     // 自身が持っているジャッジマニューバを保存
    [SerializeField] private List<CharaManeuver> DamageManeuvers;    // 自身が持っているダメージマニューバを保存

    [SerializeField] private GameObject actionCommands;              // アクションタイミングのコマンドオブジェクト
    [SerializeField] private GameObject rapidCommands;               // ラピッドタイミングのコマンドオブジェクト
    [SerializeField] private GameObject judgCommands;               // ジャッジタイミングのコマンドオブジェクト
    [SerializeField] private GameObject damageCommands;              // ダメージタイミングのコマンドオブジェクト

    public GameObject GetActCommands() => actionCommands;
    public GameObject GetRpdCommands() => rapidCommands;
    public GameObject GetJdgCommands() => judgCommands;
    public GameObject GetDmgCommands() => damageCommands;

    [SerializeField] private Button actionButton;                    // アクションのボタン
    [SerializeField] private Button rapidButton;                     // ラピッドのボタン
    [SerializeField] private Button standbyButton;                   // 待機のボタン

    [SerializeField] private GameObject prefabActButton;                               // actionコマンドのプレハブ
    [SerializeField] private GameObject prefabRpdButton;                               // rapidコマンドのプレハブ
    [SerializeField] private GameObject prefabJdgButton;                               // judgeコマンドのプレハブ
    [SerializeField] private GameObject prefabDmgButton;                               // Damageコマンドのプレハブ

    [SerializeField] private List<GameObject> parentsActObj = new List<GameObject>();  // アクションコマンドの親オブジェクトリスト
    [SerializeField] private List<GameObject> parentsRpdObj = new List<GameObject>();  // ラピッドコマンドの親オブジェクト
    [SerializeField] private List<GameObject> parentsJdgObj = new List<GameObject>();  // ジャッジコマンドの親オブジェクト
    [SerializeField] private List<GameObject> parentsDmgObj = new List<GameObject>();  // ダメージコマンドの親オブジェクト

    private List<GameObject> prefabActObjList = new List<GameObject>();                // クローンしたアクションコマンドプレハブの保存先
    private List<GameObject> prefabRpdObjList = new List<GameObject>();                // クローンしたラピッドコマンドプレハブの保存先
    private List<GameObject> prefabJdgObjList = new List<GameObject>();                // クローンしたジャッジコマンドプレハブの保存先
    private List<GameObject> prefabDmgObjList = new List<GameObject>();                // クローンしたダメージコマンドプレハブの保存先


    private GameObject originalParentObj;              // 上記プレハブの親Objの元となるオブジェクト
    private RectTransform backActImg;                  // 上記変数の座標となるオブジェクト
    private RectTransform backRpdImg;                  // 上記変数の座標となるオブジェクト
    private RectTransform backJdgImg;                  // 上記変数の座標となるオブジェクト
    private RectTransform backDmgImg;                  // 上記変数の座標となるオブジェクト

    [SerializeField]
    private BattleSystem battleSystem;

    [SerializeField]
    private bool nowSelect;                         // 選択中かどうか
    public void SetNowSelect(bool select) => nowSelect = select;

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

        // コマンドを取得
        actionCommands = this.transform.Find("Canvas/Act_select/Action/ActionCommands").gameObject;
        rapidCommands = this.transform.Find("Canvas/Act_select/Rapid/RapidCommands").gameObject;
        judgCommands = this.transform.Find("Canvas/Judge/JudgeCommands").gameObject;
        damageCommands = this.transform.Find("Canvas/Damage/DamageCommands").gameObject;

        // バックイメージを取得
        backActImg = this.transform.Find("Canvas/Act_select/Action/ActionCommands/BackImg").GetComponent<RectTransform>();
        backRpdImg = this.transform.Find("Canvas/Act_select/Rapid/RapidCommands/BackImg").GetComponent<RectTransform>();
        backJdgImg = this.transform.Find("Canvas/Judge/JudgeCommands/BackImg").GetComponent<RectTransform>();
        backDmgImg = this.transform.Find("Canvas/Damage/DamageCommands/BackImg").GetComponent<RectTransform>();

        // 独自のプレハブフォルダからクローンオブジェクトを取得
        prefabActButton = NonResources.Load<GameObject>("Assets/morii/Prefab/Commands/ActionButton.prefab");
        prefabRpdButton = NonResources.Load<GameObject>("Assets/morii/Prefab/Commands/RapidButton.prefab");

        // 独自のプレハブフォルダから上記プレハブの親Objの元となるオブジェクトを取得
        originalParentObj = NonResources.Load<GameObject>("Assets/morii/Prefab/UIparent/VerticalParent.prefab");


        // 各親オブジェクトを1つずつあらかじめ作る
        parentsActObj.Add(BuildingParent(true, backActImg));
        parentsRpdObj.Add(BuildingParent(true, backRpdImg));
        parentsJdgObj.Add(BuildingParent(true, backJdgImg));
        parentsDmgObj.Add(BuildingParent(true, backDmgImg));

        // パーツのマニューバとしての割り当てられているタイミングで分ける
        AddManeuver(thisChara.GetHeadParts());
        AddManeuver(thisChara.GetArmParts());
        AddManeuver(thisChara.GetBodyParts());
        AddManeuver(thisChara.GetLegParts());

        // タイミングで分けられたマニューバ
        prefabActObjList = BuildCommands(ActionManeuvers, ref parentsActObj, prefabActObjList, backActImg);
        prefabRpdObjList = BuildCommands(RapidManeuvers, ref parentsRpdObj, prefabRpdObjList, backRpdImg);
        prefabJdgObjList = BuildCommands(JudgeManeuvers, ref parentsJdgObj, prefabJdgObjList, backJdgImg);
        prefabDmgObjList = BuildCommands(DamageManeuvers, ref parentsDmgObj, prefabDmgObjList, backDmgImg);
    }

    public void OnClickStandby()
    {
        // カウントを1減らして待機
        thisChara.NowCount = thisChara.NowCount - 1;
    }

    public void OnClickAction()
    {
        // アクションのコマンドを表示
        actionCommands.SetActive(true);
    }

    public void OnClickRapid()
    {
        // ラピッドのコマンドを表示
        rapidCommands.SetActive(true);
    }

    /// <summary>
    /// キャラクターの各マニューバをそれぞれ分類わけする
    /// </summary>
    /// <param name="maneuvers"></param>
    public void AddManeuver(List<CharaManeuver> maneuvers)
    {
        for (int i = 0; i < thisChara.GetLegParts().Count; i++)
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
            Instance = Instantiate(prefabActButton, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0));
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
    /// コマンドボタンを格納したオブジェクトリスト。
    /// </summary>
    /// <param name="isActive">アクティブ状態でクローンするかどうか</param>
    /// <returns></returns>
    GameObject BuildingParent(bool isActive, RectTransform backImg)
    {
        // サイズ調整----ゴリ押しなの気に食わん--------------------------
        // クローンした親オブジェクトのサイズ調整用
        Vector2 parentSize = backImg.sizeDelta;
        parentSize.y = parentSize.y - COMMAND_SIZE / 2;

        Vector3 parentPos = backImg.position;
        parentPos.y = parentPos.y + COMMAND_SIZE / 4;
        // --------------------------------------------------------------

        GameObject Instance;
        Instance = Instantiate(originalParentObj, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0), actionCommands.transform);
        VerticalLayoutGroup parentClone = Instance.GetComponent<VerticalLayoutGroup>();
        // クローンしたオブジェクトの座標、サイズを調整する
        parentClone.GetComponent<RectTransform>().position = backImg.position;
        parentClone.GetComponent<RectTransform>().sizeDelta = backImg.sizeDelta;
        parentClone.gameObject.SetActive(isActive);
        parentClone.transform.SetParent(backImg.parent);
        return parentClone.gameObject;
    }

    void AddFuncToButton(ref ButtonTexts command, CharaManeuver maneuver)
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
}

public struct ManerverAndArea
{
    public CharaManeuver maneuver;
    public int area;
}