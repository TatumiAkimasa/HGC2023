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
    protected Doll_blu_Nor thisChara;                 // ���g���Q�Ƃ��邽�߂̕ϐ�

    [SerializeField] protected List<CharaManeuver> maneuvers;

    private List<CharaManeuver> ActionManeuvers;   // ���g�������Ă���A�N�V�����}�j���[�o��ۑ� 
    private List<CharaManeuver> RapidManeuvers;    // ���g�������Ă��郉�s�b�h�}�j���[�o��ۑ�
    private List<CharaManeuver> JudgeManeuvers;    // ���g�������Ă���W���b�W�}�j���[�o��ۑ�
    private List<CharaManeuver> DamageManeuvers;   // ���g�������Ă���_���[�W�}�j���[�o��ۑ�

    private GameObject actSelect;                                   // �A�N�V�����^�C�~���O�ł̓��������߂�{�^��
    public GameObject GetActSelect() => actSelect;
    
    [SerializeField] protected GameObject commands;
    [SerializeField] protected List<GameObject> parentCommand = new List<GameObject>();
    private List<GameObject> prefabObjList = new List<GameObject>();      // �^�C�~���O���Ƃɂ킯��ꂽ�}�j���[�o�̓��e���i�[����Ă��郊�X�g
    [SerializeField] protected GameObject originalParentObj;              // ��L�v���n�u�̐eObj�̌��ƂȂ�I�u�W�F�N�g
    [SerializeField] protected RectTransform backImg;
    [SerializeField] protected GameObject prefabButton;

    private List<GameObject> partsObjList = new List<GameObject>();     // ���ʂ��Ƃɕ�����ꂽ�}�j���[�o�̓��e���i�[����Ă��郊�X�g

    private GameObject actionCommands;             // �A�N�V�����^�C�~���O�̃R�}���h�I�u�W�F�N�g
    private GameObject rapidCommands;              // ���s�b�h�^�C�~���O�̃R�}���h�I�u�W�F�N�g
    private GameObject judgCommands;               // �W���b�W�^�C�~���O�̃R�}���h�I�u�W�F�N�g
    private GameObject damageCommands;             // �_���[�W�^�C�~���O�̃R�}���h�I�u�W�F�N�g

    public GameObject GetCommands() => commands;

    public GameObject GetActCommands() => actionCommands;
    public GameObject GetRpdCommands() => rapidCommands;
    public GameObject GetJdgCommands() => judgCommands;
    public GameObject GetDmgCommands() => damageCommands;

    private Button actionButton;                    // �A�N�V�����̃{�^��
    private Button rapidButton;                     // ���s�b�h�̃{�^��
    private Button standbyButton;                   // �ҋ@�̃{�^��



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

        // �A�N�V�����A���s�b�h�A�ҋ@��I��gameObject���擾
        actSelect = this.transform.Find("Canvas/Act_select").gameObject;

        
    }

    protected virtual void InitCommand(int timing)
    {
        parentCommand.Add(BuildingParent(true, backImg));

        // �p�[�c�̃}�j���[�o�Ƃ��Ă̊��蓖�Ă��Ă���^�C�~���O�ŕ�����
        AddManeuver(thisChara.HeadParts, timing);
        AddManeuver(thisChara.ArmParts, timing);
        AddManeuver(thisChara.BodyParts, timing);
        AddManeuver(thisChara.LegParts, timing);

        // �^�C�~���O�ŕ�����ꂽ�}�j���[�o
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
        // �J�E���g��1���炵�đҋ@
        thisChara.NowCount = thisChara.NowCount - 1;
    }

    public void OnClickAction()
    {
        // �A�N�V�����̃R�}���h��\��
        CommandAccessor.Instance.actCommands.GetCommands().SetActive(true);
    }

    public void OnClickRapid()
    {
        // ���s�b�h�̃R�}���h��\��
        CommandAccessor.Instance.rpdCommands.GetCommands().SetActive(true);
    }

    /// <summary>
    /// �L�����N�^�[�̊e�}�j���[�o�����ꂼ�ꕪ�ނ킯����
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
            Instance = Instantiate(prefabButton, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0));
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
    /// �R�}���h�𐶐����郁�\�b�h
    /// </summary>
    /// <param name="maneuvers">�}�j���[�o�̓��e���N���[���I�u�W�F�N�g�ɂ���</param>
    /// <param name="prefabList">��L�I�u�W�F�N�g���i�[���A�Ǘ����郊�X�g</param>
    /// <param name="parentObj">��L�I�u�W�F�N�g���X�g���i�[���A�R�}���h�I���̃y�[�W�Ƃ��Ă̈���������B</param>
    /// <returns><param name="prefabList"></returns>
    public List<GameObject> BuildParts(List<CharaManeuver> maneuvers, ref List<GameObject> parentObj, List<GameObject> prefabList, RectTransform backImg, int parts)
    {
        GameObject Instance;
        // �e�̐�
        int countParent = 0;
        for (int i = 0; i < maneuvers.Count; i++)
        {
            Instance = Instantiate(prefabButton, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0));
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
            ManeuverAndParts buff;
            buff.maneuver = maneuvers[i];
            buff.parts = parts;
            clone.GetComponent<Button>().onClick.AddListener(() => OnClickReflectDamage(buff));

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
    protected GameObject BuildingParent(bool isActive, RectTransform backImg)
    {
        // �T�C�Y����----�S�������Ȃ̋C�ɐH���--------------------------
        // �N���[�������e�I�u�W�F�N�g�̃T�C�Y�����p
        Vector2 parentSize = backImg.sizeDelta;
        parentSize.y = parentSize.y - COMMAND_SIZE / 2;

        Vector3 parentPos = backImg.position;
        parentPos.y = parentPos.y + COMMAND_SIZE / 4;
        // --------------------------------------------------------------

        GameObject Instance;
        Instance = Instantiate(originalParentObj, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0), commands.transform);
        VerticalLayoutGroup parentClone = Instance.GetComponent<VerticalLayoutGroup>();
        // �N���[�������I�u�W�F�N�g�̍��W�A�T�C�Y�𒲐�����
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

    void OnClickRpdCommand(CharaManeuver maneuver)
    {
        ProcessAccessor.Instance.rpdTiming.SkillSelected = true;
        ProcessAccessor.Instance.rpdTiming.SetManeuver(maneuver);
        ProcessAccessor.Instance.rpdTiming.SetArea(thisChara.area);
        if(maneuver.MinRange==10)
        {
            // �ړ��}�j���[�o�̏ꍇ�A�ړ���������̃{�^����\��
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
    /// �R�}���h�̃l�N�X�g�{�^���������ꂽ�Ƃ��ɔ������郁�\�b�h
    /// </summary>
    public void OnClickNext(List<GameObject> parentObj)
    {
        if(parentObj.Count >= 2)
        {

        }
    }

    /// <summary>
    /// �R�}���h�̃o�b�N�{�^���������ꂽ�Ƃ��ɔ������郁�\�b�h
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