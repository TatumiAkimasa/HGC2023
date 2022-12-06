using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class ActTimingProcess : GetClickedGameObject
{
    // �����F�ǂȂ������Ȃ�������BattleCommand���Q�Ƃ������Ȃ�

    private GameObject atkTargetEnemy;                                // �U������G�I�u�W�F�N�g���i�[�ꏊ
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
        if (selectedChara)
        {
            SkillSelectStandby();
        }
        else if (standbyEnemySelect)
        {
            EnemySelectStandby();
        }
        else if (standbyCharaSelect)
        {
            CharaSelectStandby();
        }
        
    }

    /// <summary>
    /// �L�����I��ҋ@��Ԏ��ɓ����֐�
    /// �N���b�N����܂œ��ɉ����������s��Ȃ�
    /// �N���b�N�����炻�̏ꏊ�Ƀ��C���΂��A�I�u�W�F�N�g���擾
    /// </summary>
    protected override void CharaSelectStandby()
    {
        //���N���b�N��
        if (Input.GetMouseButtonDown(0) && CharaCamera == null && !selectedChara)
        {
            GameObject clickedObj = ShotRay();

            //�N���b�N�����Q�[���I�u�W�F�N�g�������L�����Ȃ�
            if (clickedObj.CompareTag("AllyChara"))
            {
                // �R�}���h��\�����A�I�񂾃L�����ɋ߂Â�
                clickedObj.GetComponent<BattleCommand>().SetNowSelect(true);
                ZoomUpObj(clickedObj);
                // ���L�ϐ���true�ɂ��A�X�L���I��ҋ@��Ԃֈڍs
                selectedChara = true;
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
            // �}�j���[�o��I�ԃR�}���h�܂ŕ\������Ă����炻�̃R�}���h��������
            if()
            ZoomOutObj();
            // �L�����I��ҋ@��Ԃɂ���
            selectedChara = false;
        }
    }

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
                    // �I�������L�����̃R�}���h�̃I�u�W�F�N�g���擾
                    childCommand = move.transform.GetChild(CANVAS).transform.GetChild(ACTION);
                    // �Z�R�}���h��������\��
                    childCommand.gameObject.SetActive(true);
                }
                else if (move.CompareTag("EnemyChara"))
                {
                    // �X�e�[�^�X���擾���A�\���B���OnClickAtk�Ŏg���̂Ń����o�ϐ��Ɋi�[
                    childCommand = move.transform.GetChild(CANVAS);
                    childCommand.gameObject.SetActive(true);

                    // �G�L�����̃G���A�ƑI�����ꂽ�}�j���[�o�̎˒����Βl�Ŕ�ׂāA�˒����ł���΍U�����邩�I������R�}���h��\������
                    // �G�L�����̃G���A�̐�Βl���U���̍ő�˒��ȉ����A
                    // �G�L�����̃G���A�̐�Βl���U���̍ŏ��˒��ȏ�Ȃ�U������
                    if (dollManeuver.MinRange != 10 &&
                        (Mathf.Abs(move.GetComponent<Doll_blu_Nor>().area) <= Mathf.Abs(dollManeuver.MaxRange + actingChara.area) &&
                         Mathf.Abs(move.GetComponent<Doll_blu_Nor>().area) >= Mathf.Abs(dollManeuver.MinRange + actingChara.area)))
                    {
                        atkTargetEnemy = move;
                        atkTargetEnemy.transform.GetChild(CANVAS).gameObject.SetActive(true);

                        exeButton.onClick.AddListener(() => OnClickAtk(move.GetComponent<Doll_blu_Nor>()));

                        this.transform.GetChild(CANVAS).transform.GetChild(ATKBUTTONS).gameObject.SetActive(true);
                    }
                }
            }
        }
    }

    public void ZoomOutRequest()
    {
        ZoomOutObj();
    }

    public void OnClickAtk(Doll_blu_Nor enemy)
    {
        // �J���������̈ʒu�ɖ߂��AUI������
        ZoomOutObj();
        enemy.transform.GetChild(CANVAS).gameObject.SetActive(false);
        this.transform.GetChild(CANVAS).transform.GetChild(ATKBUTTONS).gameObject.SetActive(false);

        selectedChara = false;
        standbyEnemySelect = false;
        standbyCharaSelect = false;

        // ��������W���b�W�ɓ���
        ProcessAccessor.Instance.jdgTiming.enabled = true;
        ProcessAccessor.Instance.jdgTiming.SetActChara(actingChara);
        ProcessAccessor.Instance.jdgTiming.ActMneuver = dollManeuver;
        ProcessAccessor.Instance.jdgTiming.IsStandbyDiceRoll = true;
        ProcessAccessor.Instance.jdgTiming.AtkTargetEnemy = atkTargetEnemy;
        ProcessAccessor.Instance.jdgTiming.GetDiceRollButton().gameObject.SetActive(true);
        ProcessAccessor.Instance.jdgTiming.GetJudgeButton().SetActive(true);
        ProcessAccessor.Instance.jdgTiming.ActMneuver = dollManeuver;


        // �vif�������B����ȃR�X�g�łȂ���΃R�X�g������������
        // �s���l������������
        actingChara.NowCount -= dollManeuver.Cost;
    }
}
