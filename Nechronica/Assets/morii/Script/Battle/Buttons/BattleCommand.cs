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
    private List<GameObject> prefabActObjList = new List<GameObject>();

    [SerializeField]
    private GameObject prefabRpdButton;             // rapidコマンドのプレハブ
    [SerializeField]
    private List<GameObject> parentsRpdObj = new List<GameObject>();                // ラピッドコマンドの親オブジェクト
    private List<GameObject> prefabRpdObjList = new List<GameObject>();

    private GameObject originalParentObj;           // 上記プレハブの親Objの元となるオブジェクト
    private RectTransform backImg;                  // 上記変数の座標となるオブジェクト

    [SerializeField]
    private bool nowSelect;                         // 選択中かどうか
    public void SetNowSelect(bool select) => nowSelect = select;

    private void Start()
    {
        // ボタンを取得
        actionButton  = thisChara.transform.Find("Canvas/Act_select/Action").gameObject.GetComponent<Button>();
        rapidButton   = thisChara.transform.Find("Canvas/Act_select/Rapid").gameObject.GetComponent<Button>();
        standbyButton = thisChara.transform.Find("Canvas/Act_select/Standby").gameObject.GetComponent<Button>();

        // ボタンにメソッドを加える
        actionButton.onClick.AddListener(OnClickAction);
        rapidButton.onClick.AddListener(OnClickRapid);
        standbyButton.onClick.AddListener(OnClickStandby);

        // コマンドを取得
        actionCommands = thisChara.transform.Find("Canvas/Act_select/Action/ActionCommands").gameObject;
        rapidCommands  = thisChara.transform.Find("Canvas/Act_select/Rapid/RapidCommands").gameObject;

        // バックイメージを取得
        backImg = thisChara.transform.Find("Canvas/Act_select/Action/ActionCommands/BackImg").GetComponent<RectTransform>();

        // 独自のプレハブフォルダからクローンオブジェクトを取得
        prefabActButton = NonResources.Load<GameObject>("Assets/morii/Prefab/Commands/ActionButton.prefab");
        prefabRpdButton = NonResources.Load<GameObject>("Assets/morii/Prefab/Commands/RapidButton.prefab");

        // 独自のプレハブフォルダから上記プレハブの親Objの元となるオブジェクトを取得
        originalParentObj = NonResources.Load<GameObject>("Assets/morii/Prefab/UIparent/VerticalParent.prefab");

        // 親の数
        int countParent = 0;
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

        // 各部位パーツのアクション、ラピッドタイミングのパーツを取得
        for (int i=0;i<thisChara.GetHeadParts().Count;i++)
        {
            if (thisChara.GetHeadParts()[i].Timing == CharaBase.ACTION)
            {
                ActionManeuvers.Add(thisChara.GetHeadParts()[i]);
            }
            else if (thisChara.GetHeadParts()[i].Timing == CharaBase.RAPID) 
            {
                RapidManeuvers.Add(thisChara.GetHeadParts()[i]);
            }
        }

        for (int i = 0; i < thisChara.GetArmParts().Count; i++)
        {
            if (thisChara.GetArmParts()[i].Timing == CharaBase.ACTION)
            {
                ActionManeuvers.Add(thisChara.GetArmParts()[i]);
            }
            else if (thisChara.GetHeadParts()[i].Timing == CharaBase.RAPID)
            {
                RapidManeuvers.Add(thisChara.GetArmParts()[i]);
            }
        }

        for (int i = 0; i < thisChara.GetBodyParts().Count; i++)
        {
            if (thisChara.GetBodyParts()[i].Timing == CharaBase.ACTION)
            {
                ActionManeuvers.Add(thisChara.GetBodyParts()[i]);
            }
            else if (thisChara.GetBodyParts()[i].Timing == CharaBase.RAPID)
            {
                RapidManeuvers.Add(thisChara.GetBodyParts()[i]);
            }
        }

        for (int i = 0; i < thisChara.GetLegParts().Count; i++)
        {
            if (thisChara.GetLegParts()[i].Timing == CharaBase.ACTION)
            {
                ActionManeuvers.Add(thisChara.GetLegParts()[i]);
            }
            else if (thisChara.GetLegParts()[i].Timing == CharaBase.RAPID)
            {
                RapidManeuvers.Add(thisChara.GetLegParts()[i]);
            }
        }

        for (int i = 0; i < ActionManeuvers.Count; i++)
        {

            Instance = Instantiate(prefabActButton, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0));
            ButtonTexts clone = Instance.GetComponent<ButtonTexts>();
            clone.SetName(ActionManeuvers[i].Name);
            clone.SetCost(ActionManeuvers[i].Cost.ToString());

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
            clone.transform.SetParent(parentsActObj[countParent].transform, false);


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
}
