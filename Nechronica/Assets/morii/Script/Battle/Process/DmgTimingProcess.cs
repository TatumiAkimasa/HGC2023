using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DmgTimingProcess : GetClickedGameObject
{
    private int addDamage;
    private int giveDamage;

    [SerializeField] private Button nextButton;

    private int rollResult;             // �_�C�X���[���̌��ʂ̒l

    private Doll_blu_Nor damagedChara;  // �_���[�W���󂯂�\��̃L����
    public Doll_blu_Nor DamagedChara
    {
        set { damagedChara = value; }
    }


    private CharaManeuver actManeuver;     // �A�N�V�����^�C�~���O�Ŕ������ꂽ�R�}���h�̊i�[�ꏊ

    public CharaManeuver ActMneuver
    {
        get { return actManeuver; }
        set { actManeuver = value; }
    }

    public int GiveDamage
    {
        get { return giveDamage; }
        set { giveDamage = value; }
    }

    // Update is called once per frame
    void Update()
    {
        if(standbyCharaSelect)
        {
            CharaSelectStandby();
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
                    standbyCharaSelect = false;
                    // �I�������L�����̃R�}���h�̃I�u�W�F�N�g���擾
                    childCommand = move.transform.GetChild(CANVAS).transform.GetChild(DAMAGE);
                    // �Z�R�}���h��������\��
                    childCommand.gameObject.SetActive(true);
                }
            }
        }
    }

    private void ExeManeuver()
    {
        if(dollManeuver.EffectNum.ContainsKey(EffNum.Damage))
        {

        }
        else if(dollManeuver.EffectNum.ContainsKey(EffNum.Guard))   
        {

        }
        else if(dollManeuver.EffectNum.ContainsKey(EffNum.Extra))   // �ŗL�̌��ʃ}�W�łǂ����悤
        {

        }
    }

    private void DamageUPProcess()
    {
        // �^����_���[�W���オ��n�̏���
    }

    private void GuardProcess()
    {
        // �h��Ƃ��̏���
    }

    private void EXProcess()
    {
        // �w���̉x�тƂ�
    }

    public void OnClickNextButton()
    {
        // �ŏI�I�ȃ_���[�W�̌��ʂ������A�U�����ꂽ�L�����N�^�[���_���[�W���󂯂鏈���ɂ�����



        if (rollResult >= 10)
        {
            for (int i = 0; i < giveDamage; i++)
            {
                if (!damagedChara.GetHeadParts()[i].isDmage)
                {
                    damagedChara.GetHeadParts()[i].isDmage = true;
                }
            }
        }
        else if (rollResult == 9)
        {

        }
        else if (rollResult == 8)
        {

        }
        else if (rollResult == 7)
        {

        }
        else if (rollResult == 6)
        {

        }
    }
}
