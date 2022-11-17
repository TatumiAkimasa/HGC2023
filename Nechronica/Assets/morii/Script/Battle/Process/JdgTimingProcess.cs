using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class JdgTimingProcess : GetClickedGameObject
{
    private bool isDiceRoll;
    private bool isStandbyDiceRoll;
    public bool IsStandbyDiceRoll
    {
        get { return isStandbyDiceRoll; }
        set { isStandbyDiceRoll = value; }
    }

    private bool isJudgeTiming = false;


    [SerializeField]
    protected Text text;

    protected Unity.Mathematics.Random randoms;       // �Č��\�ȗ����̓�����Ԃ�ێ�����C���X�^���X

    protected int rollResult = 0;                     // �_�C�X���[���̌��ʂ��i�[����ϐ�


    void Awake()
    {
        ProcessAccessor.Instance.jdgTiming = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (isStandbyDiceRoll)
        {
            if (isDiceRoll)
            {
                isStandbyDiceRoll = false;
                isDiceRoll = false;

                // ��������W���b�W�^�C�~���O
                standbyCharaSelect = true;
                isJudgeTiming = true;
            }
        }
    }


    /// <summary>
    /// �J�������߂Â��Ă���R�}���h��\�����郁�\�b�h
    /// </summary>
    /// <param name="charaCommand">�N���b�N���ꂽ�L�����̎q�I�u�W�F�N�g�i�R�}���h�j</param>
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
                if (move.CompareTag("AllyChara"))
                {
                    if (isJudgeTiming)
                    {
                        // �I�������L�����̃R�}���h�̃I�u�W�F�N�g���擾
                        childCommand = move.transform.GetChild(CANVAS).transform.GetChild(JUDGE);
                        // �Z�R�}���h��������\��
                        childCommand.gameObject.SetActive(true);
                    }
                }
                else if (move.CompareTag("EnemyChara"))
                {
                    // �X�e�[�^�X���擾���A�\���B���ZoomOutObj�Ŏg���̂Ń����o�ϐ��Ɋi�[
                    childCommand = move.transform.GetChild(CANVAS);
                    childCommand.gameObject.SetActive(true);

                    // �G�L�����̃G���A�ƑI�����ꂽ�}�j���[�o�̎˒����Βl�Ŕ�ׂāA�˒����ł���΍U�����邩�I������R�}���h��\������
                    // �G�L�����̃G���A�̐�Βl���U���̍ő�˒��ȉ����A
                    // �G�L�����̃G���A�̐�Βl���U���̍ŏ��˒��ȏ�Ȃ�U������
                    if (Mathf.Abs(move.GetComponent<Doll_blu_Nor>().potition) <= Mathf.Abs(dollManeuver.MaxRange + dollArea) &&
                        Mathf.Abs(move.GetComponent<Doll_blu_Nor>().potition) >= Mathf.Abs(dollManeuver.MinRange + dollArea))
                    {

                    }

                    // �U������`��I�񂾂�AselectedChara�AstandbyCharaSelect��false�ɂ��A�_�C�X���[����
                    // �_�C�X���[����A�}�j���[�o�A�GObj(move)�A�_�C�X�̒l�������Ƃ��A�_���[�W�^�C�~���O��
                }
            }
        }
    }

    public void OnClickDiceRoll()
    {
        randoms = new Unity.Mathematics.Random((uint)Random.Range(0, 468446876));
        rollResult = randoms.NextInt(1, 10);
        text.text = rollResult.ToString();
        isDiceRoll = true;
    }

}

