using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class ActTimingProcess : GetClickedGameObject
{
    private GameObject atkTargetEnemy;              // �U������G�I�u�W�F�N�g���i�[�ꏊ

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

    protected override void CharaSelectStandby()
    {
        //���N���b�N��
        if (Input.GetMouseButtonDown(0) && CharaCamera == null && !selectedChara)
        {
            GameObject clickedObj = ShotRay();

            //�N���b�N�����Q�[���I�u�W�F�N�g�������L�����Ȃ�
            if (clickedObj.CompareTag("AllyChara"))
            {
                clickedObj.GetComponent<BattleCommand>().SetNowSelect(true);
                ZoomUpObj(clickedObj);
                selectedChara = true;
                // �����ŃR�}���h�\��
                StartCoroutine(MoveStandby(clickedObj));
            }
        }
    }

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

    protected override void SkillSelectStandby()
    {
        // �E�N���b�N��
        if ((Input.GetMouseButtonDown(1) || skillSelected) && CharaCamera != null)
        {
            // �}�j���[�o��I�ԃR�}���h�܂ŕ\������Ă����炻�̃R�}���h��������
            ZoomOutObj();
            // �L�����I��ҋ@��Ԃɂ���
            selectedChara = false;
        }
    }

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
                    // �X�e�[�^�X���擾���A�\���B���ZoomOutObj�Ŏg���̂Ń����o�ϐ��Ɋi�[
                    childCommand = move.transform.GetChild(CANVAS);
                    childCommand.gameObject.SetActive(true);

                    // �G�L�����̃G���A�ƑI�����ꂽ�}�j���[�o�̎˒����Βl�Ŕ�ׂāA�˒����ł���΍U�����邩�I������R�}���h��\������
                    // �G�L�����̃G���A�̐�Βl���U���̍ő�˒��ȉ����A
                    // �G�L�����̃G���A�̐�Βl���U���̍ŏ��˒��ȏ�Ȃ�U������
                    if (dollManeuver.MinRange != 10 &&
                        (Mathf.Abs(move.GetComponent<Doll_blu_Nor>().potition) <= Mathf.Abs(dollManeuver.MaxRange + targetArea) &&
                         Mathf.Abs(move.GetComponent<Doll_blu_Nor>().potition) >= Mathf.Abs(dollManeuver.MinRange + targetArea)))
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

    public void OnClickAtk(Doll_blu_Nor enemy)
    {
        // �J���������̈ʒu�ɖ߂��AUI������
        ZoomOutObj();
        enemy.transform.GetChild(CANVAS).gameObject.SetActive(false);
        this.transform.GetChild(CANVAS).transform.GetChild(ATKBUTTONS).gameObject.SetActive(false);

        selectedChara = false;
        standbyEnemySelect = false;
        standbyCharaSelect = false;

        // �����W���b�W�������
        ProcessAccessor.Instance.jdgTiming.enabled = true;
        ProcessAccessor.Instance.jdgTiming.DiceRollButton.gameObject.SetActive(true);
        ProcessAccessor.Instance.jdgTiming.MovingCharaArea = targetArea;
        ProcessAccessor.Instance.jdgTiming.IsStandbyDiceRoll = true;

        // �W���b�W�ɓ����Ă���o�g���v���Z�X�������Ȃ��悤�ɔ�A�N�e�B�u�ɂ���
        this.enabled = false;

        // Debug�p
        //battleSystem.DamageTiming(dollManeuver, enemy);
    }
}
