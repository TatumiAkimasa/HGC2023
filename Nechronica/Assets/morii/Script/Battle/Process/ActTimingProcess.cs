using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class ActTimingProcess : GetClickedGameObject
{
    // �萔--------------------------
    private const bool ALLY  = true;
    private const bool ENEMY = false;
    //-------------------------------

    // �L�����N�^�[�̃A�N�V�����R�}���h���擾
    private BattleCommand actCharaCommand;
    private ActCommands actCommand;
    private RpdCommand rpdCommand;
    private bool isAllyOrEnemy;                       // �I�񂾃L�������G���������̔��f

    private GameObject atkTargetEnemy;                // �U������G�I�u�W�F�N�g���i�[�ꏊ
    public GameObject AtkTargetEnemy
    {
        set { atkTargetEnemy = value; }
        get { return atkTargetEnemy; }
    }

    [SerializeField] protected Button exeButton;
    public Button ExeButton
    {
        get { return exeButton; }
    }


    // Start is called before the first frame update
    void Awake()
    {
        ProcessAccessor.Instance.actTiming = this;
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
        else if (isStandbyCharaSelect)
        {
            CharaSelectStandby();
        }
        
    }

    // �J�����̓����A�L�����I���֘A�̃��\�b�h--------------------------------------------------

    /// <summary>
    /// �L�����I��ҋ@��Ԏ��ɓ����֐�
    /// �N���b�N����܂œ��ɉ����������s��Ȃ�
    /// �N���b�N�����炻�̏ꏊ�Ƀ��C���΂��A�I�u�W�F�N�g���擾
    /// </summary>
    protected override void CharaSelectStandby()
    {
        //���N���b�N��
        if (Input.GetMouseButtonDown(0) && CharaCamera == null && !isSelectedChara)
        {
            GameObject clickedObj = ShotRay();


            if(clickedObj==null)
            {
                return;
            }
            //�N���b�N�����Q�[���I�u�W�F�N�g�������L�����Ȃ�
            else if (clickedObj.CompareTag("AllyChara")�@|| clickedObj.CompareTag("EnemyChara"))
            {
                // �R�}���h��\�����A�I�񂾃L�����ɋ߂Â�
                actCharaCommand = clickedObj.GetComponent<BattleCommand>();
                actCommand = clickedObj.GetComponent<ActCommands>();
                rpdCommand = clickedObj.GetComponent<RpdCommand>();
                ZoomUpObj(clickedObj);
                // �����ŃR�}���h�\��
                StartCoroutine(MoveStandby(clickedObj));
            }
        }
    }

    /// <summary>
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
            if (clickedObj.CompareTag("EnemyChara"))
            {
                ZoomUpObj(clickedObj);
                // �����ŃR�}���h�\��
                StartCoroutine(MoveStandby(clickedObj));
            }
        }
    }

    /// <summary>
    /// �X�L���I��ҋ@���
    /// �����ł̓J���������ɖ߂鏈�������s��
    /// ���ɖ߂�^�C�~���O�̓X�L�����I�����ꂽ�^�C�~���O�A�߂�{�^���������ꂽ�^�C�~���O
    /// </summary>
    protected override void SkillSelectStandby()
    {
        // �E�N���b�N��
        if ((Input.GetMouseButtonDown(1) || skillSelected) && CharaCamera != null)
        {
            if(isAllyOrEnemy==ALLY)
            {
                // �}�j���[�o��I�ԃR�}���h�܂ŕ\������Ă����炻�̃R�}���h��������
                if (ProcessAccessor.Instance.actTimingMove.GetMoveButtons().activeSelf)
                {
                    ProcessAccessor.Instance.actTimingMove.GetMoveButtons().SetActive(false);
                }
                else if (actCommand.GetCommands().activeSelf)
                {
                    actCommand.GetCommands().SetActive(false);
                }
                else if(rpdCommand.GetCommands().activeSelf)
                { 
                    rpdCommand.GetCommands().SetActive(false);
                }
                else
                {
                    MyZoomOutObj(actCharaCommand.GetActSelect());
                    // �L�����I��ҋ@��Ԃɂ���
                    isSelectedChara = false;
                    if(skillSelected)
                    {
                        skillSelected = false;
                    }
                }
            }
            else
            {
                ZoomOutObj();
            }
        }
    }

    public void ZoomOutRequest()
    {
        ZoomOutObj();
    }

    private void MyZoomOutObj(GameObject hideCommand)
    {
        // �S�̂�\��������J������D��ɂ���B
        CharaCamera.Priority = 0;
        // �R�}���h������
        hideCommand.SetActive(false);
        // ���������v���n�u�J�����������B
        StartCoroutine(DstroyCamera());
    }

    /// <summary>
    /// �G�I���̎��ɖ߂������������s����郁�\�b�h
    /// </summary>
    protected void OnClickBack()
    {
        // �J���������̈ʒu�ɖ߂��AUI������
        ZoomOutObj();
        this.transform.GetChild(CANVAS).transform.GetChild(0).gameObject.SetActive(false);
        // childCommand�̒��g���Ȃ���
        if (childCommand != null)
        {
            childCommand = null;
        }
    }

    //-----------------------------------------------------------------------------------------

    /// <summary>
    /// �J�������߂Â�����̏����B�G�Ɩ����ŏ������قȂ�
    /// ����-------------------
    /// �R�}���h��\�����邾��
    /// �G---------------------
    /// �G�I�u�W�F�N�g���擾���A�X�e�[�^�X��\��
    /// ���̌�A�U���{�^����\������
    /// </summary>
    /// <param name="move"></param>
    /// <returns></returns>
    protected override IEnumerator MoveStandby(GameObject move)
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
                if (move.CompareTag("AllyChara"))
                {
                    isAllyOrEnemy = ALLY;
                    for (int j=0;j< ManagerAccessor.Instance.battleSystem.GetMoveChara().Count;j++)
                    {
                        if (move.name == ManagerAccessor.Instance.battleSystem.GetMoveChara()[j].name)
                        {
                            // �Z�R�}���h��������\��
                            actCharaCommand.GetActSelect().SetActive(true);
                            break;
                        }
                    }
                    
                }
                else if (move.CompareTag("EnemyChara"))
                {
                    // �X�e�[�^�X���擾���A�\���B���OnClickAtk�Ŏg���̂Ń����o�ϐ��Ɋi�[
                    childCommand = move.transform.GetChild(CANVAS);
                    childCommand.gameObject.SetActive(true);
                    isAllyOrEnemy = ENEMY;

                    // �G�L�����̃G���A�ƑI�����ꂽ�}�j���[�o�̎˒����Βl�Ŕ�ׂāA�˒����ł���΍U�����邩�I������R�}���h��\������
                    // �G�L�����̃G���A�̐�Βl���U���̍ő�˒��ȉ����A
                    // �G�L�����̃G���A�̐�Βl���U���̍ŏ��˒��ȏ�Ȃ�U������
                    if (isStandbyEnemySelect && dollManeuver.MinRange != 10 && RangeCheck(move.GetComponent<Doll_blu_Nor>(), dollManeuver, actingChara))
                    {
                        atkTargetEnemy = move;
                        atkTargetEnemy.transform.GetChild(CANVAS).gameObject.SetActive(true);

                        exeButton.onClick.AddListener(() => OnClickAtkRequest());

                        this.transform.GetChild(CANVAS).transform.GetChild(ATKBUTTONS).gameObject.SetActive(true);
                    }
                }
                isSelectedChara = true;
            }
        }
    }

    /// <summary>
    /// ���̃W���b�W�^�C�~���O�Ɉڍs�����N�G�X�g����{�^��
    /// </summary>
    private void OnClickAtkRequest()
    {
        ExeAtkManeuver(atkTargetEnemy.GetComponent<Doll_blu_Nor>(), dollManeuver, actingChara);
    }

    /// <summary>
    /// �ҋ@������{�^��
    /// </summary>
    public void OnClickStandby()
    {
        ExeStandby(actingChara);
    }


    /// <summary>
    /// �W���b�W�^�C�~���O�ֈڍs���郁�\�b�h
    /// </summary>
    /// <param name="enemy">�U���Ώ�</param>
    /// <param name="maneuver">�U�����e</param>
    /// <param name="actChara">�U�����s���L����</param>
    public void ExeAtkManeuver(Doll_blu_Nor enemy, CharaManeuver maneuver,Doll_blu_Nor actChara)
    {
        // actChara�̃^�O��AllyChara�̏ꍇ�J������UI���o�Ă���̂ŁA�J���������̈ʒu�ɖ߂�UI������
        if(actChara.gameObject.CompareTag("AllyChara"))
        {
            ZoomOutObj();
            this.transform.GetChild(CANVAS).transform.GetChild(ATKBUTTONS).gameObject.SetActive(false);
        }


        isSelectedChara = false;
        isStandbyEnemySelect = false;
        isStandbyCharaSelect = false;

        // ��������W���b�W�ɓ���
        ProcessAccessor.Instance.rpdTiming.SetActChara(actChara);
        ProcessAccessor.Instance.rpdTiming.ActMneuver = maneuver;
        ProcessAccessor.Instance.rpdTiming.AtkTargetEnemy = enemy.gameObject;
        ProcessAccessor.Instance.rpdTiming.StandbyCharaSelect = true;
        ProcessAccessor.Instance.rpdTiming.SetRapidButton(true); 
        ProcessAccessor.Instance.rpdTiming.SetTimingText("���s�b�h");
        if (rpdCommand!=null)
        {
            rpdCommand.GetCommands().SetActive(true);
        }
        
        //actChara.GetComponent<RpdCommand>

        // �����Ń��s�b�h�^�C�~���O�̃}�j���[�o�[��G���������邩�ǂ������f
        //enemy.GetComponent<ObjEnemy>().EnemyAI_Rapid(maneuver, actChara);

        // �vif�������B����ȃR�X�g�łȂ���΃R�X�g������������
        // �s���l������������
        actChara.NowCount -= maneuver.Cost;
    }

    /// <summary>
    /// �ҋ@�������Ȃ�����
    /// </summary>
    public void ExeStandby(Doll_blu_Nor chara)
    {
        ManagerAccessor.Instance.battleSystem.DeleteMoveChara(chara.Name);
        chara.NowCount -= 1;
        MyZoomOutObj(actCharaCommand.GetActSelect());
        ManagerAccessor.Instance.battleSystem.BattleExe = true;
    }

}
