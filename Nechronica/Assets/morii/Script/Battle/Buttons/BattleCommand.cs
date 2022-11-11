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
    private Doll_blu_Nor thisChara;                 // ���g���Q�Ƃ��邽�߂̕ϐ�

    [SerializeField]
    private BattleCommand thisCharaCommand;         // ���ȎQ��

    [SerializeField]
    private List<CharaManeuver> ActionManeuvers;    // ���g�������Ă���A�N�V�����}�j���[�o��ۑ� 
    [SerializeField]
    private List<CharaManeuver> RapidManeuvers;     // ���g�������Ă��郉�s�b�h�}�j���[�o��ۑ�
    [SerializeField]
    private List<CharaManeuver> JudgeManeuvers;     // ���g�������Ă���W���b�W�}�j���[�o��ۑ�
    [SerializeField]
    private List<CharaManeuver> DamageManeuvers;    // ���g�������Ă���_���[�W�}�j���[�o��ۑ�
                                      
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
    private List<GameObject> prefabActObjList = new List<GameObject>();             // �N���[�������v���n�u�̕ۑ���

    [SerializeField]
    private GameObject prefabRpdButton;             // rapid�R�}���h�̃v���n�u
    [SerializeField]
    private List<GameObject> parentsRpdObj = new List<GameObject>();                // ���s�b�h�R�}���h�̐e�I�u�W�F�N�g
    private List<GameObject> prefabRpdObjList = new List<GameObject>();             // �N���[�������v���n�u�̕ۑ���

    [SerializeField]
    private GameObject prefabJdgButton;             // judge�R�}���h�̃v���n�u
    [SerializeField]
    private List<GameObject> parentsJdgObj = new List<GameObject>();                // ���s�b�h�R�}���h�̐e�I�u�W�F�N�g
    private List<GameObject> prefabJdgObjList = new List<GameObject>();             // �N���[�������v���n�u�̕ۑ���

    [SerializeField]
    private GameObject prefabDmgButton;             // Damage�R�}���h�̃v���n�u
    [SerializeField]
    private List<GameObject> parentsDmgObj = new List<GameObject>();                // ���s�b�h�R�}���h�̐e�I�u�W�F�N�g
    private List<GameObject> prefabDmgObjList = new List<GameObject>();             // �N���[�������v���n�u�̕ۑ���


    private GameObject originalParentObj;           // ��L�v���n�u�̐eObj�̌��ƂȂ�I�u�W�F�N�g
    private RectTransform backImg;                  // ��L�ϐ��̍��W�ƂȂ�I�u�W�F�N�g

    [SerializeField]
    private BattleSystem battleSystem;              

    [SerializeField]
    private bool nowSelect;                         // �I�𒆂��ǂ���
    public void SetNowSelect(bool select) => nowSelect = select;

    public GetClickedGameObject getClicked;

    private void Start()
    {
        // �o�g���V�X�e�����擾
        //battleSystem = GameObject.FindGameObjectWithTag("BattleManager").gameObject.GetComponent<BattleSystem>();
        battleSystem = ManagerAccessor.Instance.battleSystem;

        // �{�^�����擾
        actionButton  = this.transform.Find("Canvas/Act_select/Action").gameObject.GetComponent<Button>();
        rapidButton   = this.transform.Find("Canvas/Act_select/Rapid").gameObject.GetComponent<Button>();
        standbyButton = this.transform.Find("Canvas/Act_select/Standby").gameObject.GetComponent<Button>();

        // �{�^���Ƀ��\�b�h��������
        actionButton.onClick.AddListener(OnClickAction);
        rapidButton.onClick.AddListener(OnClickRapid);
        standbyButton.onClick.AddListener(OnClickStandby);

        // �R�}���h���擾
        actionCommands = this.transform.Find("Canvas/Act_select/Action/ActionCommands").gameObject;
        rapidCommands  = this.transform.Find("Canvas/Act_select/Rapid/RapidCommands").gameObject;

        // �o�b�N�C���[�W���擾
        backImg = this.transform.Find("Canvas/Act_select/Action/ActionCommands/BackImg").GetComponent<RectTransform>();

        // �Ǝ��̃v���n�u�t�H���_����N���[���I�u�W�F�N�g���擾
        prefabActButton = NonResources.Load<GameObject>("Assets/morii/Prefab/Commands/ActionButton.prefab");
        prefabRpdButton = NonResources.Load<GameObject>("Assets/morii/Prefab/Commands/RapidButton.prefab");

        // �Ǝ��̃v���n�u�t�H���_�����L�v���n�u�̐eObj�̌��ƂȂ�I�u�W�F�N�g���擾
        originalParentObj = NonResources.Load<GameObject>("Assets/morii/Prefab/UIparent/VerticalParent.prefab");

        // �T�C�Y����----�S�������Ȃ̋C�ɐH���--------------------------
        // �N���[�������e�I�u�W�F�N�g�̃T�C�Y�����p
        Vector2 parentSize = backImg.sizeDelta;
        parentSize.y = parentSize.y - COMMAND_SIZE / 2;

        Vector3 parentPos = backImg.position;
        parentPos.y = parentPos.y + COMMAND_SIZE / 4;
        // --------------------------------------------------------------

        // �N���[�������pGameObject
        GameObject Instance;

        // ���炩����1�ڂ̐e�ƂȂ�I�u�W�F�N�g�𐶐�
        Instance = Instantiate(originalParentObj);
        VerticalLayoutGroup parentClone = Instance.GetComponent<VerticalLayoutGroup>();
        parentClone.transform.parent = actionCommands.transform;
        // �N���[�������I�u�W�F�N�g�̍��W�A�T�C�Y�𒲐�����
        parentClone.GetComponent<RectTransform>().sizeDelta = parentSize;
        parentClone.GetComponent<RectTransform>().position = parentPos;
        parentsActObj.Add(parentClone.gameObject);

        AddManeuver(thisChara.GetHeadParts());
        AddManeuver(thisChara.GetArmParts());
        AddManeuver(thisChara.GetBodyParts());
        AddManeuver(thisChara.GetLegParts());

        // �R�}���h�𐶐�
        for (int i = 0; i < ActionManeuvers.Count; i++)
        {
            Instance = Instantiate(prefabActButton, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0));
            ButtonTexts clone = Instance.GetComponent<ButtonTexts>();
            clone.SetName(ActionManeuvers[i].Name);
            clone.SetCost(ActionManeuvers[i].Cost.ToString());

            // �����Ń{�^���ɃI���N���b�N��ǉ��B���e�̓}�j���[�o����

            // �˒����������݂���ꍇ�ƁA�ꂩ���ɂ������݂��Ȃ��ꍇ�A�������͎��g�Ɍ��ʂ��y�ԏꍇ�ŏ����𕪂���
            if (ActionManeuvers[i].MinRange == 10) 
            {
                clone.SetRange("���g");
            }
            else if (ActionManeuvers[i].MinRange != ActionManeuvers[i].MaxRange)
            {
                clone.SetRange(ActionManeuvers[i].MinRange.ToString() + "�`" + ActionManeuvers[i].MaxRange.ToString());
            }
            else
            {
                clone.SetRange(ActionManeuvers[i].MinRange.ToString());
            }
            // �e��ݒ�
            clone.transform.SetParent(parentsActObj[countParent].transform, false);
            ManerverAndArea buff;
            // �o�b�t�@�[�ɕK�v�ȏ����i�[
            buff.maneuver = ActionManeuvers[i];
            buff.area = thisChara.potition;
            // �R�}���h���g����悤�ɂ���
            clone.GetComponent<Button>().onClick.AddListener(() => battleSystem.OnClickCommand(buff));
            
            // �R�}���h5��؂�ŃR�}���h�̐e�I�u�W�F�N�g�𕡐�����B
            if ((i + 1) % 5 == 0)
            {
                countParent++;
                Instance = Instantiate(originalParentObj, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0), actionCommands.transform);
                parentClone = Instance.GetComponent<VerticalLayoutGroup>();
                // �N���[�������I�u�W�F�N�g�̍��W�A�T�C�Y�𒲐�����
                parentClone.GetComponent<RectTransform>().position = backImg.position;
                parentClone.GetComponent<RectTransform>().sizeDelta = backImg.sizeDelta;
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

    /// <summary>
    /// �L�����N�^�[�̊e�}�j���[�o���擾
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
    /// �R�}���h�������\�b�h
    /// </summary>
    /// <param name="maneuvers">�I�u�W�F�N�g���X�g�Ɋi�[����}�j���[�o</param>
    /// <param name="prefabList">�}�j���[�o�{�^�����i�[�������I�u�W�F�N�g���X�g�B�߂�l�ɂȂ�</param>
    /// <returns>prefabList</returns>
    public List<GameObject> BuildCommands(List<CharaManeuver> maneuvers, List<GameObject> prefabList)
    {
        GameObject Instance;
        // �e�̐�
        int countParent = 0;
        for (int i = 0; i < maneuvers.Count; i++)
        {
            Instance = Instantiate(prefabActButton, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0));
            ButtonTexts clone = Instance.GetComponent<ButtonTexts>();
            clone.SetName(maneuvers[i].Name);
            clone.SetCost(maneuvers[i].Cost.ToString());

            // �����Ń{�^���ɃI���N���b�N��ǉ��B���e�̓}�j���[�o����

            // �˒����������݂���ꍇ�ƁA�ꂩ���ɂ������݂��Ȃ��ꍇ�A�������͎��g�Ɍ��ʂ��y�ԏꍇ�ŏ����𕪂���
            if (maneuvers[i].MinRange == 10)
            {
                clone.SetRange("���g");
            }
            else if (maneuvers[i].MinRange != maneuvers[i].MaxRange)
            {
                clone.SetRange(maneuvers[i].MinRange.ToString() + "�`" + maneuvers[i].MaxRange.ToString());
            }
            else
            {
                clone.SetRange(maneuvers[i].MinRange.ToString());
            }
            // �e��ݒ�
            clone.transform.SetParent(parentsActObj[countParent].transform, false);
            ManerverAndArea buff;
            // �o�b�t�@�[�ɕK�v�ȏ����i�[
            buff.maneuver = maneuvers[i];
            buff.area = thisChara.potition;
            // �R�}���h���g����悤�ɂ���
            clone.GetComponent<Button>().onClick.AddListener(() => ManagerAccessor.Instance.battleSystem.OnClickCommand(buff));

            // �R�}���h5��؂�ŃR�}���h�̐e�I�u�W�F�N�g�𕡐�����B
            if ((i + 1) % 5 == 0)
            {
                countParent++;
                Instance = Instantiate(originalParentObj, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0), actionCommands.transform);
                VerticalLayoutGroup parentClone = Instance.GetComponent<VerticalLayoutGroup>();
                // �N���[�������I�u�W�F�N�g�̍��W�A�T�C�Y�𒲐�����
                parentClone.GetComponent<RectTransform>().position = backImg.position;
                parentClone.GetComponent<RectTransform>().sizeDelta = backImg.sizeDelta;
                parentClone.gameObject.SetActive(false);
                parentsActObj.Add(parentClone.gameObject);
            }

            //���X�g�ɕۑ�
            prefabList.Add(clone.gameObject);
        }

        return prefabList;
    }

    // varticalParent�I�u�W�F�N�g�𕡐����郁�\�b�h�����
}

public struct ManerverAndArea
{
    public CharaManeuver maneuver;
    public int area;
}