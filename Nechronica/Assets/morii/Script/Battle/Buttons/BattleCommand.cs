using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.IO;


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
    private GameObject rapidCommands;                // ���s�b�h�^�C�~���O�̃R�}���h�I�u�W�F�N�g

    [SerializeField]
    private Button actionButton;                    // �A�N�V�����̃{�^��
    [SerializeField]
    private Button rapidButton;                     // ���s�b�h�̃{�^��
    [SerializeField]
    private Button standbyButton;                   // �ҋ@�̃{�^��

    [SerializeField]
    private GameObject prefabActButton;             // action�R�}���h�̃v���n�u
    [SerializeField]
    private List<GameObject> parentsActObj = new List<GameObject>();                // �A�N�V�����R�}���h�̐e�I�u�W�F�N�g���X�g
    private List<GameObject> prefabActObjList = new List<GameObject>();

    [SerializeField]
    private GameObject prefabRpdButton;             // rapid�R�}���h�̃v���n�u
    [SerializeField]
    private List<GameObject> parentsRpdObj = new List<GameObject>();                // ���s�b�h�R�}���h�̐e�I�u�W�F�N�g
    private List<GameObject> prefabRpdObjList = new List<GameObject>();

    private GameObject originalParentObj;           //��L�v���n�u�̐eObj�̌��ƂȂ�I�u�W�F�N�g

    [SerializeField]
    private bool nowSelect;                         // �I�𒆂��ǂ���
    public void SetNowSelect(bool select) => nowSelect = select;

    private void Start()
    {
        // �{�^�����擾
        actionButton = thisChara.transform.Find("Canvas/Act_select/Action").gameObject.GetComponent<Button>();
        rapidButton = thisChara.transform.Find("Canvas/Act_select/Rapid").gameObject.GetComponent<Button>();
        standbyButton = thisChara.transform.Find("Canvas/Act_select/Standby").gameObject.GetComponent<Button>();

        // �{�^���Ƀ��\�b�h��������
        actionButton.onClick.AddListener(OnClickAction);
        rapidButton.onClick.AddListener(OnClickRapid);
        standbyButton.onClick.AddListener(OnClickStandby);

        // �R�}���h���擾
        actionCommands = thisChara.transform.Find("Canvas/Act_select/Action/ActionCommands").gameObject;
        rapidCommands = thisChara.transform.Find("Canvas/Act_select/Rapid/RapidCommands").gameObject;

        // �Ǝ��̃v���n�u�t�H���_����N���[���I�u�W�F�N�g���擾
        prefabActButton = NonResources.Load<GameObject>("Assets/morii/Prefab/Commands/ActionButton.prefab");
        prefabRpdButton = NonResources.Load<GameObject>("Assets/morii/Prefab/Commands/RapidButton.prefab");

        // �Ǝ��̃v���n�u�t�H���_�����L�v���n�u�̐eObj�̌��ƂȂ�I�u�W�F�N�g���擾
        originalParentObj = NonResources.Load<GameObject>("Assets/morii/Prefab/UIparent/VerticalParent.prefab");

        // �e�̐�
        int countParent = 0;
        // �N���[�������pGameObject
        GameObject Instance;

        // ���炩����1�ڂ̐e�ƂȂ�I�u�W�F�N�g�𐶐�
        Instance = Instantiate(originalParentObj);
        VerticalLayoutGroup parentClone = Instance.GetComponent<VerticalLayoutGroup>();
        parentClone.transform.parent = actionCommands.transform;
        parentsActObj.Add(parentClone.gameObject);

        // �e���ʃp�[�c�̃A�N�V�����A���s�b�h�^�C�~���O�̃p�[�c���擾
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
            

            Instance = Instantiate(prefabActButton);
            ButtonTexts clone = Instance.GetComponent<ButtonTexts>();
            clone.SetName(ActionManeuvers[i].Name);
            clone.SetCost(ActionManeuvers[i].Cost.ToString());

            // �˒����������݂���ꍇ�ƁA�ꂩ���ɂ������݂��Ȃ��ꍇ�ŏ����𕪂���
            if (ActionManeuvers[i].MinRange != ActionManeuvers[i].MaxRange)
            {
                clone.SetRange(ActionManeuvers[i].MinRange.ToString() + "�`" + ActionManeuvers[i].MaxRange.ToString());
            }
            else
            {
                clone.SetRange(ActionManeuvers[i].MinRange.ToString());
            }
            clone.transform.SetParent(parentsActObj[countParent].transform, false);


            // �R�}���h5��؂�ŃR�}���h�̐e�I�u�W�F�N�g�𕡐�����B
            if ((i + 1) % 5 == 0)
            {
                countParent++;
                Instance = Instantiate(originalParentObj);
                parentClone = Instance.GetComponent<VerticalLayoutGroup>();
                parentClone.transform.parent = actionCommands.transform;
                parentClone.gameObject.SetActive(false);
                parentsActObj.Add(parentClone.gameObject);
            }

            //���X�g�ɕۑ�
            prefabActObjList.Add(clone.gameObject);
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
        rapidCommands.SetActive(true);
    }
}
