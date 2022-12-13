using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveActProcess : GetClickedGameObject
{
    private bool standbyTargetSelect = false;
    public bool SetStandbyTarget(bool isStandby) => standbyTargetSelect = isStandby;

    Doll_blu_Nor moveChara;         // ���ۂɈړ�����L����

    [SerializeField] private GameObject moveButtons;            // �ړ�������{�^����\�����邽�߂̂���


    private void Awake()
    {
        ProcessAccessor.Instance.actTimingMove = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(skillSelected)
        {
            RangeJudgment();
        }
        else if(standbyTargetSelect)
        {
            TargetSelectStandby();
        }
    }

    /// <summary>
    /// �˒����f�p���\�b�h�B���g���ړ����邩�L������I��ňړ������邩�I��
    /// </summary>
    public void RangeJudgment()
    {
        // �˒������g�݂̂Ȃ炻�̂܂܈ړ����邩�̑I���{�^�����o���B
        if(dollManeuver.MinRange==10)
        {
            moveChara = actingChara;
            moveButtons.SetActive(true);
        }
        // ���g�łȂ���Γ������Ώۂ�I�Ԃ܂ł̑ҋ@��Ԃֈڍs
        else
        {
            standbyTargetSelect = true;
        }
        // �������̃��\�b�h�ł���d���͂Ȃ��̂ł��̃��\�b�h�ɓ���Ȃ��悤�ɂ���
        skillSelected = false;
    }

    /// <summary>
    /// �˒������g�łȂ��ꍇ�ɃL�����N�^�[��I�ԏ���
    /// </summary>
    public void TargetSelectStandby()
    {
        //���N���b�N��
        if (Input.GetMouseButtonDown(0))
        {
            // �v���C�A�u���L�����Ɍ����Ă����J��������ۑ�
            saveCharaCamera = CharaCamera;

            GameObject clickedObj = ShotRay();

            //�N���b�N�����Q�[���I�u�W�F�N�g���I�ׂ�L�����Ȃ�
            if (clickedObj.CompareTag("AllyChara") || clickedObj.CompareTag("EnemyChara"))
            {
                ZoomUpObj(clickedObj);
                // �����ŃR�}���h�\��
                StartCoroutine(MoveStandby(clickedObj));
            }
        }
    }

    /// <summary>
    /// �I�񂾃L�������˒����Ȃ�A�I�񂾃L�������ړ��\��̃L�����ϐ��Ɋi�[���A�ړ��{�^����\������
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
                // �G�L�����̃G���A�ƑI�����ꂽ�}�j���[�o�̎˒����Βl�Ŕ�ׂāA�˒����ł���΍U�����邩�I������R�}���h��\������
                // �G�L�����̃G���A�̐�Βl���U���̍ő�˒��ȉ����A
                // �G�L�����̃G���A�̐�Βl���U���̍ŏ��˒��ȏ�Ȃ�U������
                if (dollManeuver.MinRange != 10 &&
                    (Mathf.Abs(move.GetComponent<Doll_blu_Nor>().area) <= Mathf.Abs(dollManeuver.MaxRange + actingChara.area) &&
                     Mathf.Abs(move.GetComponent<Doll_blu_Nor>().area) >= Mathf.Abs(dollManeuver.MinRange + actingChara.area)))
                {
                    moveChara = move.GetComponent<Doll_blu_Nor>();
                    // �vif�������B�ړ��ʂ���������Ȃ炢������ǂꂾ���ړ����邩��I������{�^�����o��

                    moveButtons.SetActive(true);
                }

            }
        }
    }

    // �y�������Ɉړ�����{�^���p�̃��\�b�h
    public void OnClickParadiseMove()
    {
        MoveChara(true,moveChara,false);
        ProcessAccessor.Instance.actTiming.SkillSelected = true;
    }

    // �ޗ������Ɉړ�����{�^���p�̃��\�b�h
    public void OnClickAbyssMove()
    {
        MoveChara(false,moveChara,false);
        ProcessAccessor.Instance.actTiming.SkillSelected = true;
    }



    /// <summary>
    /// 
    /// </summary>
    /// <param name="direction">true�Ŋy�������Ɉړ��Afalse�œޗ������Ɉړ�</param>
    /// <param name="beMovedChara">�ړ���������L����</param>
    /// <param name="isRapid">���s�b�h�^�C�~���O���ǂ������f</param>
    public void MoveChara(bool direction, Doll_blu_Nor beMovedChara, bool isRapid)
    {
        if ((beMovedChara.area == BattleSpone.NARAKU && !direction)||
             beMovedChara.area == BattleSpone.RAKUEN &&  direction)
        {
            // ����ȏ�ړ��ł��܂���݂����ȕ\�L����
        }
        else
        {
            // ���̃N���X�ɓ���O��ActTimingProcess�N���X�ŃJ�����̏��������Ă���̂�ActTimingProcess�N���X�ŃY�[���A�E�g�̏������s��
            ProcessAccessor.Instance.actTiming.ZoomOutRequest();
            // ���ۂɃL�����𓮂���
            if(direction)
            {
                ManagerAccessor.Instance.battleSpone.CharaMove(moveChara, 1);
            }
            else
            {
                ManagerAccessor.Instance.battleSpone.CharaMove(moveChara, -1);
            }
            
            moveButtons.SetActive(false);
            actingChara.NowCount -= dollManeuver.Cost;
            // �����A�A�j���[�V�����I����Ă���̏����ɂ������Ȃ�
            // �s�������L������\���������
            if(isRapid)
            {
                ProcessAccessor.Instance.rpdTiming.ExemaneuverProcess = true;
            }
            else
            {

                ManagerAccessor.Instance.battleSystem.DeleteMoveChara();
                ManagerAccessor.Instance.battleSystem.BattleExe = true;
            }
        }
    }
}
