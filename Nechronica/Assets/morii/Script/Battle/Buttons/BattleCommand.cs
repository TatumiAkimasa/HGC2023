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

    [SerializeField]
    private List<CharaManeuver> ActionManeuvers;    // 自身が持っているアクションマニューバを保存 
    [SerializeField]
    private List<CharaManeuver> RapidManeuvers;     // 自身が持っているラピッドマニューバを保存
    [SerializeField]
    private List<CharaManeuver> JudgeManeuvers;     // 自身が持っているジャッジマニューバを保存
    [SerializeField]
    private List<CharaManeuver> DamageManeuvers;    // 自身が持っているダメージマニューバを保存
                                      
    [SerializeField]
    private GameObject actionCommands;              // アクションタイミングのコマンドオブジェクト

    [SerializeField]
    private GameObject rapidCommands;                // ラピッドタイミングのコマンドオブジェクト

    [SerializeField]
    private Button actionButton;                    // アクションのボタン
    [SerializeField]
    private Button rapidButton;                     // ラピッドのボタン
    [SerializeField]
    private Button standbyButton;                   // 待機のボタン


    [SerializeField]
    private GameObject prefabActButton;             // actionコマンドのプレハブ
    [SerializeField]
    private List<GameObject> parentsActObj = new List<GameObject>();                // アクションコマンドの親オブジェクトリスト
    private List<GameObject> prefabActObjList = new List<GameObject>();             // クローンしたプレハブの保存先

    [SerializeField]
    private GameObject prefabRpdButton;             // rapidコマンドのプレハブ
    [SerializeField]
    private List<GameObject> parentsRpdObj = new List<GameObject>();                // ラピッドコマンドの親オブジェクト
    private List<GameObject> prefabRpdObjList = new List<GameObject>();             // クローンしたプレハブの保存先

    [SerializeField]
    private GameObject prefabJdgButton;             // judgeコマンドのプレハブ
    [SerializeField]
    private List<GameObject> parentsJdgObj = new List<GameObject>();                // ラピッドコマンドの親オブジェクト
    private List<GameObject> prefabJdgObjList = new List<GameObject>();             // クローンしたプレハブの保存先

    [SerializeField]
    private GameObject prefabDmgButton;             // Damageコマンドのプレハブ
    [SerializeField]
    private List<GameObject> parentsDmgObj = new List<GameObject>();                // ラピッドコマンドの親オブジェクト
    private List<GameObject> prefabDmgObjList = new List<GameObject>();             // クローンしたプレハブの保存先


    private GameObject originalParentObj;           // 上記プレハブの親Objの元となるオブジェクト
    private RectTransform backImg;                  // 上記変数の座標となるオブジェクト

    [SerializeField]
    private BattleSystem battleSystem;              

    [SerializeField]
    private bool nowSelect;                         // 選択中かどうか
    public void SetNowSelect(bool select) => nowSelect = select;

    public GetClickedGameObject getClicked;

    private void Start()
    {
        // バトルシステムを取得
        //battleSystem = GameObject.FindGameObjectWithTag("BattleManager").gameObject.GetComponent<BattleSystem>();
        battleSystem = ManagerAccessor.Instance.battleSystem;

        // ボタンを取得
        actionButton  = this.transform.Find("Canvas/Act_select/Action").gameObject.GetComponent<Button>();
        rapidButton   = this.transform.Find("Canvas/Act_select/Rapid").gameObject.GetComponent<Button>();
        standbyButton = this.transform.Find("Canvas/Act_select/Standby").gameObject.GetComponent<Button>();

        // ボタンにメソッドを加える
        actionButton.onClick.AddListener(OnClickAction);
        rapidButton.onClick.AddListener(OnClickRapid);
        standbyButton.onClick.AddListener(OnClickStandby);

        // コマンドを取得
        actionCommands = this.transform.Find("Canvas/Act_select/Action/ActionCommands").gameObject;
        rapidCommands  = this.transform.Find("Canvas/Act_select/Rapid/RapidCommands").gameObject;

        // バックイメージを取得
        backImg = this.transform.Find("Canvas/Act_select/Action/ActionCommands/BackImg").GetComponent<RectTransform>();

        // 独自のプレハブフォルダからクローンオブジェクトを取得
        prefabActButton = NonResources.Load<GameObject>("Assets/morii/Prefab/Commands/ActionButton.prefab");
        prefabRpdButton = NonResources.Load<GameObject>("Assets/morii/Prefab/Commands/RapidButton.prefab");

        // 独自のプレハブフォルダから上記プレハブの親Objの元となるオブジェクトを取得
        originalParentObj = NonResources.Load<GameObject>("Assets/morii/Prefab/UIparent/VerticalParent.prefab");

        // サイズ調整----ゴリ押しなの気に食わん--------------------------
        // クローンした親オブジェクトのサイズ調整用
        Vector2 parentSize = backImg.sizeDelta;
        parentSize.y = parentSize.y - COMMAND_SIZE / 2;

        Vector3 parentPos = backImg.position;
        parentPos.y = parentPos.y + COMMAND_SIZE / 4;
        // --------------------------------------------------------------

        // クローン生成用GameObject
        GameObject Instance;

        // あらかじめ1個目の親となるオブジェクトを生成
        Instance = Instantiate(originalParentObj);
        VerticalLayoutGroup parentClone = Instance.GetComponent<VerticalLayoutGroup>();
        parentClone.transform.parent = actionCommands.transform;
        // クローンしたオブジェクトの座標、サイズを調整する
        parentClone.GetComponent<RectTransform>().sizeDelta = parentSize;
        parentClone.GetComponent<RectTransform>().position = parentPos;
        parentsActObj.Add(parentClone.gameObject);

        AddManeuver(thisChara.GetHeadParts());
        AddManeuver(thisChara.GetArmParts());
        AddManeuver(thisChara.GetBodyParts());
        AddManeuver(thisChara.GetLegParts());

        // コマンドを生成
        for (int i = 0; i < ActionManeuvers.Count; i++)
        {
            Instance = Instantiate(prefabActButton, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0));
            ButtonTexts clone = Instance.GetComponent<ButtonTexts>();
            clone.SetName(ActionManeuvers[i].Name);
            clone.SetCost(ActionManeuvers[i].Cost.ToString());

            // ここでボタンにオンクリックを追加。内容はマニューバ発動

            // 射程が複数存在する場合と、一か所にしか存在しない場合、もしくは自身に効果が及ぶ場合で処理を分ける
            if (ActionManeuvers[i].MinRange == 10) 
            {
                clone.SetRange("自身");
            }
            else if (ActionManeuvers[i].MinRange != ActionManeuvers[i].MaxRange)
            {
                clone.SetRange(ActionManeuvers[i].MinRange.ToString() + "～" + ActionManeuvers[i].MaxRange.ToString());
            }
            else
            {
                clone.SetRange(ActionManeuvers[i].MinRange.ToString());
            }
            // 親を設定
            clone.transform.SetParent(parentsActObj[countParent].transform, false);
            ManerverAndArea buff;
            // バッファーに必要な情報を格納
            buff.maneuver = ActionManeuvers[i];
            buff.area = thisChara.potition;
            // コマンドを使えるようにする
            clone.GetComponent<Button>().onClick.AddListener(() => battleSystem.OnClickCommand(buff));
            
            // コマンド5個区切りでコマンドの親オブジェクトを複製する。
            if ((i + 1) % 5 == 0)
            {
                countParent++;
                Instance = Instantiate(originalParentObj, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0), actionCommands.transform);
                parentClone = Instance.GetComponent<VerticalLayoutGroup>();
                // クローンしたオブジェクトの座標、サイズを調整する
                parentClone.GetComponent<RectTransform>().position = backImg.position;
                parentClone.GetComponent<RectTransform>().sizeDelta = backImg.sizeDelta;
                parentClone.gameObject.SetActive(false);
                parentsActObj.Add(parentClone.gameObject);
            }

            //リストに保存
            prefabActObjList.Add(clone.gameObject);
        }
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
    /// キャラクターの各マニューバを取得
    /// </summary>
    /// <param name="maneuvers"></param>
    public void AddManeuver(List<CharaManeuver> maneuvers)
    {
        for (int i = 0; i < thisChara.GetLegParts().Count; i++)
        {
            if (maneuvers[i].Timing == CharaBase.ACTION)
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
    /// コマンド生成メソッド
    /// </summary>
    /// <param name="maneuvers">オブジェクトリストに格納するマニューバ</param>
    /// <param name="prefabList">マニューバボタンを格納したいオブジェクトリスト。戻り値になる</param>
    /// <returns>prefabList</returns>
    public List<GameObject> BuildCommands(List<CharaManeuver> maneuvers, List<GameObject> prefabList)
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
            clone.transform.SetParent(parentsActObj[countParent].transform, false);
            ManerverAndArea buff;
            // バッファーに必要な情報を格納
            buff.maneuver = maneuvers[i];
            buff.area = thisChara.potition;
            // コマンドを使えるようにする
            clone.GetComponent<Button>().onClick.AddListener(() => ManagerAccessor.Instance.battleSystem.OnClickCommand(buff));

            // コマンド5個区切りでコマンドの親オブジェクトを複製する。
            if ((i + 1) % 5 == 0)
            {
                countParent++;
                Instance = Instantiate(originalParentObj, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0), actionCommands.transform);
                VerticalLayoutGroup parentClone = Instance.GetComponent<VerticalLayoutGroup>();
                // クローンしたオブジェクトの座標、サイズを調整する
                parentClone.GetComponent<RectTransform>().position = backImg.position;
                parentClone.GetComponent<RectTransform>().sizeDelta = backImg.sizeDelta;
                parentClone.gameObject.SetActive(false);
                parentsActObj.Add(parentClone.gameObject);
            }

            //リストに保存
            prefabList.Add(clone.gameObject);
        }

        return prefabList;
    }

    // varticalParentオブジェクトを複製するメソッドを作る
}

public struct ManerverAndArea
{
    public CharaManeuver maneuver;
    public int area;
}