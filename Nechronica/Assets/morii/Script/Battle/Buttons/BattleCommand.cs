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

    [SerializeField] private List<CharaManeuver> ActionManeuvers;    // ���g�������Ă���A�N�V�����}�j���[�o��ۑ� 
    [SerializeField] private List<CharaManeuver> RapidManeuvers;     // ���g�������Ă��郉�s�b�h�}�j���[�o��ۑ�
    [SerializeField] private List<CharaManeuver> JudgeManeuvers;     // ���g�������Ă���W���b�W�}�j���[�o��ۑ�
    [SerializeField] private List<CharaManeuver> DamageManeuvers;    // ���g�������Ă���_���[�W�}�j���[�o��ۑ�

    [SerializeField] private GameObject actionCommands;              // �A�N�V�����^�C�~���O�̃R�}���h�I�u�W�F�N�g
    [SerializeField] private GameObject rapidCommands;               // ���s�b�h�^�C�~���O�̃R�}���h�I�u�W�F�N�g
    [SerializeField] private GameObject judgCommands;               // �W���b�W�^�C�~���O�̃R�}���h�I�u�W�F�N�g
    [SerializeField] private GameObject damageCommands;              // �_���[�W�^�C�~���O�̃R�}���h�I�u�W�F�N�g

    public GameObject GetActCommands() => actionCommands;
    public GameObject GetRpdCommands() => rapidCommands;
    public GameObject GetJdgCommands() => judgCommands;
    public GameObject GetDmgCommands() => damageCommands;

    [SerializeField] private Button actionButton;                    // �A�N�V�����̃{�^��
    [SerializeField] private Button rapidButton;                     // ���s�b�h�̃{�^��
    [SerializeField] private Button standbyButton;                   // �ҋ@�̃{�^��

    [SerializeField] private GameObject prefabActButton;                               // action�R�}���h�̃v���n�u
    [SerializeField] private GameObject prefabRpdButton;                               // rapid�R�}���h�̃v���n�u
    [SerializeField] private GameObject prefabJdgButton;                               // judge�R�}���h�̃v���n�u
    [SerializeField] private GameObject prefabDmgButton;                               // Damage�R�}���h�̃v���n�u

    [SerializeField] private List<GameObject> parentsActObj = new List<GameObject>();  // �A�N�V�����R�}���h�̐e�I�u�W�F�N�g���X�g
    [SerializeField] private List<GameObject> parentsRpdObj = new List<GameObject>();  // ���s�b�h�R�}���h�̐e�I�u�W�F�N�g
    [SerializeField] private List<GameObject> parentsJdgObj = new List<GameObject>();  // �W���b�W�R�}���h�̐e�I�u�W�F�N�g
    [SerializeField] private List<GameObject> parentsDmgObj = new List<GameObject>();  // �_���[�W�R�}���h�̐e�I�u�W�F�N�g

    private List<GameObject> prefabActObjList = new List<GameObject>();                // �N���[�������A�N�V�����R�}���h�v���n�u�̕ۑ���
    private List<GameObject> prefabRpdObjList = new List<GameObject>();                // �N���[���������s�b�h�R�}���h�v���n�u�̕ۑ���
    private List<GameObject> prefabJdgObjList = new List<GameObject>();                // �N���[�������W���b�W�R�}���h�v���n�u�̕ۑ���
    private List<GameObject> prefabDmgObjList = new List<GameObject>();                // �N���[�������_���[�W�R�}���h�v���n�u�̕ۑ���


    private GameObject originalParentObj;              // ��L�v���n�u�̐eObj�̌��ƂȂ�I�u�W�F�N�g
    private RectTransform backActImg;                  // ��L�ϐ��̍��W�ƂȂ�I�u�W�F�N�g
    private RectTransform backRpdImg;                  // ��L�ϐ��̍��W�ƂȂ�I�u�W�F�N�g
    private RectTransform backJdgImg;                  // ��L�ϐ��̍��W�ƂȂ�I�u�W�F�N�g
    private RectTransform backDmgImg;                  // ��L�ϐ��̍��W�ƂȂ�I�u�W�F�N�g

    [SerializeField]
    private BattleSystem battleSystem;

    [SerializeField]
    private bool nowSelect;                         // �I�𒆂��ǂ���
    public void SetNowSelect(bool select) => nowSelect = select;

    private void Start()
    {
        // �o�g���V�X�e�����擾
        thisChara = this.GetComponent<Doll_blu_Nor>();

        // �{�^�����擾
        actionButton = this.transform.Find("Canvas/Act_select/Action").gameObject.GetComponent<Button>();
        rapidButton = this.transform.Find("Canvas/Act_select/Rapid").gameObject.GetComponent<Button>();
        standbyButton = this.transform.Find("Canvas/Act_select/Standby").gameObject.GetComponent<Button>();

        // �{�^���Ƀ��\�b�h��������
        actionButton.onClick.AddListener(OnClickAction);
        rapidButton.onClick.AddListener(OnClickRapid);
        standbyButton.onClick.AddListener(OnClickStandby);

        // �R�}���h���擾
        actionCommands = this.transform.Find("Canvas/Act_select/Action/ActionCommands").gameObject;
        rapidCommands = this.transform.Find("Canvas/Act_select/Rapid/RapidCommands").gameObject;
        judgCommands = this.transform.Find("Canvas/Judge/JudgeCommands").gameObject;
        damageCommands = this.transform.Find("Canvas/Damage/DamageCommands").gameObject;

        // �o�b�N�C���[�W���擾
        backActImg = this.transform.Find("Canvas/Act_select/Action/ActionCommands/BackImg").GetComponent<RectTransform>();
        backRpdImg = this.transform.Find("Canvas/Act_select/Rapid/RapidCommands/BackImg").GetComponent<RectTransform>();
        backJdgImg = this.transform.Find("Canvas/Judge/JudgeCommands/BackImg").GetComponent<RectTransform>();
        backDmgImg = this.transform.Find("Canvas/Damage/DamageCommands/BackImg").GetComponent<RectTransform>();

        // �Ǝ��̃v���n�u�t�H���_����N���[���I�u�W�F�N�g���擾
        prefabActButton = NonResources.Load<GameObject>("Assets/morii/Prefab/Commands/ActionButton.prefab");
        prefabRpdButton = NonResources.Load<GameObject>("Assets/morii/Prefab/Commands/RapidButton.prefab");

        // �Ǝ��̃v���n�u�t�H���_�����L�v���n�u�̐eObj�̌��ƂȂ�I�u�W�F�N�g���擾
        originalParentObj = NonResources.Load<GameObject>("Assets/morii/Prefab/UIparent/VerticalParent.prefab");


        // �e�e�I�u�W�F�N�g��1�����炩���ߍ��
        parentsActObj.Add(BuildingParent(true, backActImg));
        parentsRpdObj.Add(BuildingParent(true, backRpdImg));
        parentsJdgObj.Add(BuildingParent(true, backJdgImg));
        parentsDmgObj.Add(BuildingParent(true, backDmgImg));

        // �p�[�c�̃}�j���[�o�Ƃ��Ă̊��蓖�Ă��Ă���^�C�~���O�ŕ�����
        AddManeuver(thisChara.GetHeadParts());
        AddManeuver(thisChara.GetArmParts());
        AddManeuver(thisChara.GetBodyParts());
        AddManeuver(thisChara.GetLegParts());

        // �^�C�~���O�ŕ�����ꂽ�}�j���[�o
        prefabActObjList = BuildCommands(ActionManeuvers, ref parentsActObj, prefabActObjList, backActImg);
        prefabRpdObjList = BuildCommands(RapidManeuvers, ref parentsRpdObj, prefabRpdObjList, backRpdImg);
        prefabJdgObjList = BuildCommands(JudgeManeuvers, ref parentsJdgObj, prefabJdgObjList, backJdgImg);
        prefabDmgObjList = BuildCommands(DamageManeuvers, ref parentsDmgObj, prefabDmgObjList, backDmgImg);
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
    /// �L�����N�^�[�̊e�}�j���[�o�����ꂼ�ꕪ�ނ킯����
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
    /// �R�}���h�𐶐����郁�\�b�h
    /// </summary>
    /// <param name="maneuvers">�}�j���[�o�̓��e���N���[���I�u�W�F�N�g�ɂ���</param>
    /// <param name="prefabList">��L�I�u�W�F�N�g���i�[���A�Ǘ����郊�X�g</param>
    /// <param name="parentObj">��L�I�u�W�F�N�g���X�g���i�[���A�R�}���h�I���̃y�[�W�Ƃ��Ă̈���������B</param>
    /// <returns><param name="prefabList"></returns>
    public List<GameObject> BuildCommands(List<CharaManeuver> maneuvers, ref List<GameObject> parentObj, List<GameObject> prefabList, RectTransform backImg)
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
            clone.transform.SetParent(parentObj[countParent].transform, false);
            AddFuncToButton(ref clone, maneuvers[i]);

            // �R�}���h5��؂�ŃR�}���h�̐e�I�u�W�F�N�g�𕡐�����B
            if ((i + 1) % 5 == 0)
            {
                countParent++;
                parentObj.Add(BuildingParent(false, backImg));
            }

            //���X�g�ɕۑ�
            prefabList.Add(clone.gameObject);
        }

        return prefabList;
    }

    /// <summary>
    /// �R�}���h�{�^�����i�[�����I�u�W�F�N�g���X�g�B
    /// </summary>
    /// <param name="isActive">�A�N�e�B�u��ԂŃN���[�����邩�ǂ���</param>
    /// <returns></returns>
    GameObject BuildingParent(bool isActive, RectTransform backImg)
    {
        // �T�C�Y����----�S�������Ȃ̋C�ɐH���--------------------------
        // �N���[�������e�I�u�W�F�N�g�̃T�C�Y�����p
        Vector2 parentSize = backImg.sizeDelta;
        parentSize.y = parentSize.y - COMMAND_SIZE / 2;

        Vector3 parentPos = backImg.position;
        parentPos.y = parentPos.y + COMMAND_SIZE / 4;
        // --------------------------------------------------------------

        GameObject Instance;
        Instance = Instantiate(originalParentObj, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0), actionCommands.transform);
        VerticalLayoutGroup parentClone = Instance.GetComponent<VerticalLayoutGroup>();
        // �N���[�������I�u�W�F�N�g�̍��W�A�T�C�Y�𒲐�����
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
        // �K�v�ȏ��𑗐M
        ProcessAccessor.Instance.actTiming.SkillSelected = true;
        ProcessAccessor.Instance.actTiming.SetManeuver(maneuver);
        // ��̃W���b�W�A�_���[�W�^�C�~���O�ōU�����s���L�������K�v�ɂȂ�̂ł��̎��_�ōs������L�������i�[���Ă���
        ProcessAccessor.Instance.actTiming.ActingChara = thisChara;
        ProcessAccessor.Instance.actTiming.StandbyEnemySelect = true;
        // �A���S���Y�������F�U�����Ȃ��A�N�V�����^�C�~���O�̈�if����������
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