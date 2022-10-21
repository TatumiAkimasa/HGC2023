using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BattleCommand : MonoBehaviour
{
    [SerializeField]
    private Doll_blu_Nor thisChara;                 // ���g���Q�Ƃ��邽�߂̕ϐ�

    [SerializeField]
    private BattleCommand thisCharaCommand;         // ���ȎQ��

    [SerializeField]
    private List<CharaManeuver> ActionManeuvers;    // ���g�������Ă���A�N�V�����}�j���[�o��ۑ� 
    [SerializeField]
    private List<CharaManeuver> RapidManeuvers;     // ���g�������Ă��郉�s�b�h�}�j���[�o��ۑ�
                                      
    [SerializeField]
    private GameObject actionCommands;              // �A�N�V�����^�C�~���O�̃R�}���h�I�u�W�F�N�g

    [SerializeField]
    private GameObject rapidCommand;                // ���s�b�h�^�C�~���O�̃R�}���h�I�u�W�F�N�g

    [SerializeField]
    private Button actionButton;                    // �A�N�V�����̃{�^��
    [SerializeField]
    private Button rapidButton;                     // ���s�b�h�̃{�^��
    [SerializeField]
    private Button standbyButton;                     // ���s�b�h�̃{�^��



    private void Start()
    {
        // �{�^�������擾
        actionButton = thisChara.transform.Find("Canvas/Act_select/Action").gameObject.GetComponent<Button>();
        rapidButton = thisChara.transform.Find("Canvas/Act_select/Rapid").gameObject.GetComponent<Button>();
        standbyButton = thisChara.transform.Find("Canvas/Act_select/Standby").gameObject.GetComponent<Button>();

        // �{�^���Ƀ��\�b�h��������
        actionButton.onClick.AddListener(OnClickAction);
        rapidButton.onClick.AddListener(OnClickRapid);
        standbyButton.onClick.AddListener(OnClickStandby);

        // �R�}���h���擾
        actionCommands = thisChara.transform.Find("Canvas/Act_select/Action/ActionCommands").gameObject;
        rapidCommand = thisChara.transform.Find("Canvas/Act_select/Rapid/RapidCommands").gameObject;

        // �e���ʃp�[�c�̃A�N�V�����A���s�b�h�^�C�~���O�̃p�[�c���擾
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
        // �J�E���g��1���炵�đҋ@
        thisChara.NowCount = thisChara.NowCount - 1;
    }

    public void OnClickAction()
    {
        // �A�N�V�����̃R�}���h��\��
        actionCommands.SetActive(true);
    }

    public void OnClickRapid()
    {
        // ���s�b�h�̃R�}���h��\��
        rapidCommand.SetActive(true);
    }
}
