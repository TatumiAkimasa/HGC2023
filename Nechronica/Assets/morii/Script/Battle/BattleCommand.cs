using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BattleCommand : MonoBehaviour
{
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
    private GameObject rapidCommand;                // ラピッドタイミングのコマンドオブジェクト

    [SerializeField]
    private Button actionButton;                    // アクションのボタン
    [SerializeField]
    private Button rapidButton;                     // ラピッドのボタン
    [SerializeField]
    private Button standbyButton;                     // ラピッドのボタン



    private void Start()
    {
        // ボタンおを取得
        actionButton = thisChara.transform.Find("Canvas/Act_select/Action").gameObject.GetComponent<Button>();
        rapidButton = thisChara.transform.Find("Canvas/Act_select/Rapid").gameObject.GetComponent<Button>();
        standbyButton = thisChara.transform.Find("Canvas/Act_select/Standby").gameObject.GetComponent<Button>();

        // ボタンにメソッドを加える
        actionButton.onClick.AddListener(OnClickAction);
        rapidButton.onClick.AddListener(OnClickRapid);
        standbyButton.onClick.AddListener(OnClickStandby);

        // コマンドを取得
        actionCommands = thisChara.transform.Find("Canvas/Act_select/Action/ActionCommands").gameObject;
        rapidCommand = thisChara.transform.Find("Canvas/Act_select/Rapid/RapidCommands").gameObject;

        // 各部位パーツのアクション、ラピッドタイミングのパーツを取得
        for(int i=0;i<thisChara.GetHeadParts().Count;i++)
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
        rapidCommand.SetActive(true);
    }
}
