using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RpdTimingProcess : GetClickedGameObject
{
    List<MoveCharaList> standbyManeuver = new List<MoveCharaList>();

    private GameObject atkTargetEnemy;      // �U������G�I�u�W�F�N�g���i�[�ꏊ
    private GameObject charaCommand;     // �I�񂾃L�����̃R�}���h�i�[�p

    Doll_blu_Nor selectedAllyChara;         // �I�����������L����
    Doll_blu_Nor selectedTargetChara;       // �I�������G�L����

    bool moveDirection;
    public void SetDirection(bool isDirection) => moveDirection = isDirection;


    bool exeManeuverProcess = false;
    public bool ExemaneuverProcess
    {
        get { return exeManeuverProcess; }
        set { exeManeuverProcess = value; }
    }

    public GameObject AtkTargetEnemy
    {
        set { atkTargetEnemy = value; }
        get { return atkTargetEnemy; }
    }

    private CharaManeuver actManeuver;     // �A�N�V�����^�C�~���O�Ŕ������ꂽ�R�}���h�̊i�[�ꏊ

    public CharaManeuver ActMneuver
    {
        get { return actManeuver; }
        set { actManeuver = value; }
    }

    [SerializeField] private Button nextButton;     // ���s�b�h�^�C�~���O�̏������J�n����{�^��
    [SerializeField] private GameObject confirmatButton;    // �Ō�ɔ������邩�m�F����{�^��
    [SerializeField] private GameObject rapidButtons;       // �W���b�W�^�C�~���O�ň����{�^���̐e�I�u�W�F�N�g
    public GameObject GetConfirmatButton() => confirmatButton;
    public void SetRapidButton(bool isActive) => rapidButtons.SetActive(isActive);

    void Awake()
    {
        ProcessAccessor.Instance.rpdTiming = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (isSelectedChara)
        {
            SkillSelectStandby();
        }
        else if (isStandbyEnemySelect)
        {
            EnemySelectStandby();
        }
        else if (exeManeuverProcess)
        {
            startExeManeuverListProcess();
        }
        else if (isStandbyCharaSelect)
        {
            CharaSelectStandby();
        }
        
    }

    /// <summary>
    /// enemy�Ƃ͖��΂����
    /// �G�I��ҋ@��Ԏ��ɓ����֐�
    /// �N���b�N����܂œ��ɉ����������s��Ȃ�
    /// �N���b�N�����炻�̏ꏊ�Ƀ��C���΂��A�I�u�W�F�N�g���擾
    /// </summary>
    protected override void EnemySelectStandby()
    {
        //���N���b�N��
        if (Input.GetMouseButtonDown(0))
        {
            // �v���C�A�u���L�����Ɍ����Ă����J��������ۑ�
            saveCharaCamera = CharaCamera;

            GameObject clickedObj = ShotRay();

            //�N���b�N�����Q�[���I�u�W�F�N�g���G�L�����Ȃ�
            if (clickedObj.CompareTag("EnemyChara") || clickedObj.CompareTag("AllyChara"))
            {
                ZoomUpObj(clickedObj);
                // �����ŃR�}���h�\��
                StartCoroutine(MoveStandby(clickedObj));
            }
        }
    }

    override protected void CharaSelectStandby()
    {
        //���N���b�N��
        if (Input.GetMouseButtonDown(0) && CharaCamera == null && !isSelectedChara)
        {
            GameObject clickedObj = ShotRay();

            //�N���b�N�����Q�[���I�u�W�F�N�g�������L�����Ȃ�
            if (clickedObj.CompareTag("AllyChara"))
            {
                ZoomUpObj(clickedObj);
                isSelectedChara = true;
                childCommand = clickedObj.transform.GetChild(CANVAS).transform.GetChild(RAPID);
                
                // �����ŃR�}���h�\��
                StartCoroutine(MoveStandby(clickedObj));
            }
        }
    }

    /// <summary>
    /// �J�������߂Â��Ă���R�}���h��\�����郁�\�b�h
    /// </summary>
    /// <returns></returns>
    override protected IEnumerator MoveStandby(GameObject move)
    {
        for (int i = 0; i < 2; i++)
        {
            //�J�������N���b�N�����L�����ɋ߂Â��܂ő҂�
            if (i == 0)
            {
                yield return new WaitForSeconds(0.75f);
            }
            else
            {
                if (isStandbyCharaSelect)
                {
                    if (move.CompareTag("AllyChara"))
                    {
                        // �R�}���h��\������
                        isStandbyCharaSelect = false;
                        selectedAllyChara = move.GetComponent<Doll_blu_Nor>();
                        childCommand.gameObject.SetActive(true);
                    }
                }
                else if(isStandbyEnemySelect)
                {
                    Doll_blu_Nor selectedChara= move.GetComponent<Doll_blu_Nor>();
                    // �I�����ꂽ�L�����̃G���A�ƑI�����ꂽ�}�j���[�o�̎˒����Βl�Ŕ�ׂāA�˒����ł���Δ������邩�I������R�}���h��\������
                    // �I�����ꂽ�L�����̃G���A�̐�Βl���U���̍ő�˒��ȉ����A
                    // �I�����ꂽ�L�����̃G���A�̐�Βl���U���̍ŏ��˒��ȏ�Ȃ甭������
                    if ((Mathf.Abs(selectedChara.area) <= Mathf.Abs(dollManeuver.MaxRange + selectedAllyChara.area) &&
                         Mathf.Abs(selectedChara.area) >= Mathf.Abs(dollManeuver.MinRange + selectedAllyChara.area)) &&
                        (!dollManeuver.isUse && !dollManeuver.isDmage))
                    {
                        selectedTargetChara = selectedChara;
                        if(dollManeuver.EffectNum.ContainsKey(EffNum.Move))
                        {
                            ProcessAccessor.Instance.actTimingMove.GetMoveButtons().SetActive(true);
                            ProcessAccessor.Instance.actTimingMove.IsRapid = true;
                        }
                        else
                        {
                            confirmatButton.SetActive(true);
                        }
                    }
                    else
                    {
                        // �˒�������܂���݂����ȕ\�L������
                    }
                }
            }
        }
    }

    protected override void SkillSelectStandby()
    {
        // �E�N���b�N��
        if ((Input.GetMouseButtonDown(1) || skillSelected) && CharaCamera != null)
        {
            // �}�j���[�o��I�ԃR�}���h�܂ŕ\������Ă����炻�̃R�}���h��������
            ZoomOutObj();
            // �L�����I��ҋ@��Ԃɂ���
            isSelectedChara = false;
        }
    }

    public void OnClickBack()
    {
        ZoomOutObj();
        confirmatButton.SetActive(false);
        isStandbyCharaSelect = true;
    }

    /// <summary>
    /// �I�������}�j���[�o�[�����X�g�ɒǉ����鏈��
    /// </summary>
    public void OnClickManeuver()
    {
        // ���������X�g�Ɋi�[
        // �˒������g�݂̂̏ꍇ�A�^�[�Q�b�g�L�������̂̈����ɂ����g���i�[����
        if (dollManeuver.MinRange == 10)
        {
            AddRapidManeuver(selectedAllyChara, selectedAllyChara, dollManeuver);
        }
        // �}�j���[�o�[�����s����L�����A�^�[�Q�b�g�ƂȂ�L�����A�}�j���[�o�[���̂������ɂ��A�i�[
        else
        {
            AddRapidManeuver(selectedAllyChara, selectedTargetChara, dollManeuver);
        }
            
        // UI�Ƃ�������
        ZoomOutObj();
        confirmatButton.SetActive(false);
        // �������郊�X�g�Ɋi�[�����̂ŁA��X�g���ϐ���������
        selectedTargetChara = null;
        selectedAllyChara = null;
    }

    /// <summary>
    /// ���s�b�h�}�j���[�o�[���������X�g�ɒǉ����郁�\�b�h
    /// </summary>
    /// <param name="moveChara">�}�j���[�o�[�����s����L����</param>
    /// <param name="targetChara">�}�j���[�o�[�̌��ʂ��󂯂�L����</param>
    /// <param name="maneuver">�}�j���[�o�[����</param>
    public void AddRapidManeuver(Doll_blu_Nor moveChara, Doll_blu_Nor targetChara, CharaManeuver maneuver)
    {
        // ���������X�g�Ɋi�[
        MoveCharaList buff = new MoveCharaList();
        buff.moveChara = moveChara;
        buff.targetChara = targetChara;
        buff.moveManeuver = maneuver;
        if (maneuver.EffectNum.ContainsKey(EffNum.Move)) buff.direction = moveDirection;
        standbyManeuver.Add(buff);
        isStandbyCharaSelect = true;
    }

    protected override void ZoomOutObj()
    {
        base.ZoomOutObj();
        nextButton.gameObject.SetActive(true);
    }

    protected override void ZoomUpObj(GameObject clicked)
    {
        base.ZoomUpObj(clicked);
        nextButton.gameObject.SetActive(true);
    }

    /// <summary>
    /// ���̃��\�b�h����������_���[�W�^�C�~���O�Ɉڍs����
    /// </summary>
    public void OnClickNext()
    {
        exeManeuverProcess = true;
    }

    /// <summary>
    /// ���X�g�̒��ɂ���}�j���[�o�[�̏������J�n
    /// </summary>
    public void startExeManeuverListProcess()
    {
        if(standbyManeuver.Count!=0)
        {
            ProcessDivide(standbyManeuver.Last());
            // ��C�ɏ������Ȃ��悤�ɂ���
            //exeManeuverProcess = false;
            // �����̗v�f���폜
            standbyManeuver.RemoveAt(standbyManeuver.Count - 1);
        }
        else
        {
            rapidButtons.SetActive(false);
            exeManeuverProcess = false;
            isStandbyEnemySelect = false;
            isStandbyCharaSelect = false;

            Doll_blu_Nor cashedChara = atkTargetEnemy.GetComponent<Doll_blu_Nor>();

            // �U�������L�������˒��O�ɏo���肵�Ă��Ȃ���΃W���b�W�^�C�~���O�ֈڍs
            if ((Mathf.Abs(cashedChara.area) <= Mathf.Abs(actManeuver.MaxRange + actingChara.area) &&
                 Mathf.Abs(cashedChara.area) >= Mathf.Abs(actManeuver.MinRange + actingChara.area)) &&
                        !actManeuver.isDmage)
            {
                //�����ŃW���b�W�^�C�~���O�ֈڍs
                ProcessAccessor.Instance.jdgTiming.SetActChara(actingChara);
                ProcessAccessor.Instance.jdgTiming.ActMneuver = actManeuver;
                ProcessAccessor.Instance.jdgTiming.IsStandbyDiceRoll = true;
                ProcessAccessor.Instance.jdgTiming.AtkTargetEnemy = atkTargetEnemy.gameObject;
                ProcessAccessor.Instance.jdgTiming.GetDiceRollButton().gameObject.SetActive(true);
                if (actingChara.gameObject.CompareTag("EnemyChara")/*||�����_�C�X���[���I�ȁH�ݒ�Q�Ɨp*/)
                {
                    ProcessAccessor.Instance.jdgTiming.OnClickDiceRoll();
                }
                else if(actingChara.gameObject.CompareTag("AllyChara"))
                {
                    ProcessAccessor.Instance.jdgTiming.GetJudgeButton().SetActive(true);
                }
            }
            else
            {
                // �����A�A�j���[�V�����I����Ă���̏����ɂ������Ȃ�
                // �s�������L������\���������
                ManagerAccessor.Instance.battleSystem.DeleteMoveChara();
                ManagerAccessor.Instance.battleSystem.BattleExe = true;
            }
            

        }
    }

    public void ProcessDivide(MoveCharaList charaList)
    {
        if(charaList.moveManeuver.EffectNum.ContainsKey(EffNum.Move))
        {
            ProcessAccessor.Instance.actTimingMove.MoveChara(charaList.direction, charaList.targetChara, true);
            charaList.moveChara.NowCount -= charaList.moveManeuver.Cost;
        }
        else if (charaList.moveManeuver.EffectNum.ContainsKey(EffNum.YobunnnaUde))
        {

        }
    }


}

public class MoveCharaList
{
    public Doll_blu_Nor  moveChara;
    public Doll_blu_Nor  targetChara;
    public CharaManeuver moveManeuver;

    // �ړ��}�j���[�o�p
    public bool direction;
}