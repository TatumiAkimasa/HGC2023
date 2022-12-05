using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DmgTimingProcess : GetClickedGameObject
{
    private int addDamage = 0;          // �_���[�W�^�C�~���O�̃}�j���[�o��ʖ�ǉ��_���[�W
    private int giveDamage = 0;         // �^����_���[�W
    private int dmgGuard = 0;           // �^����_���[�W�����̕ϐ��̒l�����炷

    Doll_blu_Nor selectedAllyChara;

    [SerializeField] private Button nextButton;
    [SerializeField] private GameObject confirmatButton;    // �������邩�ǂ����̊m�F����{�^���̃Q�[���I�u�W�F�N�g
    [SerializeField] private GameObject damageButtons;      // �_���[�W�^�C�~���O�̃{�^���̐e�I�u�W�F�N�g

    private int rollResult;             // �_�C�X���[���̌��ʂ̒l
    private Doll_blu_Nor damageChara;   // �_���[�W���󂯂�\��̃L����
    private CharaManeuver actManeuver;     // �A�N�V�����^�C�~���O�Ŕ������ꂽ�R�}���h�̊i�[�ꏊ

    public GameObject GetConfirmatButton() => confirmatButton;
    public GameObject GetDamageButtons() => damageButtons;
    public void SetDamageChara(Doll_blu_Nor value) => damageChara = value;
    public void SetRollResult(int value) => rollResult = value;
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

    private void Awake()
    {
        ProcessAccessor.Instance.dmgTiming = this;
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
                    selectedAllyChara = move.GetComponent<Doll_blu_Nor>();
                    standbyCharaSelect = false;
                    // �I�������L�����̃R�}���h�̃I�u�W�F�N�g���擾
                    childCommand = move.transform.GetChild(CANVAS).transform.GetChild(DAMAGE);
                    // �Z�R�}���h��������\��
                    childCommand.gameObject.SetActive(true);
                }
            }
        }
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

    private void ExeManeuver()
    {
        if(dollManeuver.EffectNum.ContainsKey(EffNum.Damage))
        {
            DamageUPProcess();
        }
        else if(dollManeuver.EffectNum.ContainsKey(EffNum.Guard))   
        {
            GuardProcess();
        }
        else   // ��̓�ɊY�����Ȃ��ꍇ�A�ŗL�̌��ʂƎg�p����
        {

        }
    }

    private void DamageUPProcess()
    {
        // �^����_���[�W���オ��n�̏���
        // �˒������g�݂̂̏ꍇ�A�_���[�W��^����L�����ƃ_���[�W�^�C�~���O�œ����L�������������ǂ������ׂ�
        if (dollManeuver.MinRange == 10)
        {
            if(actingChara==selectedChara)
            {
                addDamage += dollManeuver.EffectNum[EffNum.Damage];
            }
        }
        // �G�L�����̃G���A�ƑI�����ꂽ�}�j���[�o�̎˒����Βl�Ŕ�ׂāA�˒����ł���΍U�����邩�I������R�}���h��\������
        // �G�L�����̃G���A�̐�Βl���U���̍ő�˒��ȉ����A
        // �G�L�����̃G���A�̐�Βl���U���̍ŏ��˒��ȏ�Ȃ甭������
        else if ((Mathf.Abs(actingChara.position) <= Mathf.Abs(dollManeuver.MaxRange + selectedAllyChara.position)  &&
                  Mathf.Abs(actingChara.position) >= Mathf.Abs(dollManeuver.MinRange + selectedAllyChara.position)) &&
                (!dollManeuver.isUse && !dollManeuver.isDmage))
        {
            addDamage += dollManeuver.EffectNum[EffNum.Damage];
        }
    }

    private void GuardProcess()
    {
        // �h��Ƃ��̏���
        // �˒������g�݂̂̏ꍇ�A�_���[�W���󂯂�L�����ƃ_���[�W�^�C�~���O�œ����L�������������ǂ������ׂ�
        if (dollManeuver.MinRange == 10)
        {
            if (damageChara == selectedChara)
            {
                dmgGuard += dollManeuver.EffectNum[EffNum.Guard];
            }
        }
        else if ((Mathf.Abs(damageChara.position) <= Mathf.Abs(dollManeuver.MaxRange + selectedAllyChara.position) &&
                  Mathf.Abs(damageChara.position) >= Mathf.Abs(dollManeuver.MinRange + selectedAllyChara.position)) &&
                (!dollManeuver.isUse && !dollManeuver.isDmage))
        {
            if(dollManeuver.EffectNum.ContainsKey(EffNum.Guard))
            {
                dmgGuard += dollManeuver.EffectNum[EffNum.Guard];
            }
            
        }
    }

    private void EXProcess()
    {
        // ���΂��̏���
        if (dollManeuver.EffectNum.ContainsKey(EffNum.Protect))
        {
            // �������΂��̏������邩��
        }
        
    }

    public void OnClickNextButton()
    {
        // �ŏI�I�ȃ_���[�W�̌��ʂ������A�U�����ꂽ�L�����N�^�[���_���[�W���󂯂�
        // �I�[�g�^�C�~���O�̂��̂����킹�ĉ��Z����\��

        

        giveDamage = actManeuver.EffectNum[EffNum.Damage] + addDamage - dmgGuard;

        // rollResult��10��葽���ꍇ�͍U������L�������ǂ��̕��ʂɓ��Ă邩���߂��邪���͉��ɓ��Ƃ���
        if (rollResult > 10)
        {
            // �_�C�X���[���̌��ʂ�10����̏ꍇ�̒ǉ��_���[�W����
            int addDmg = rollResult - 10;
            giveDamage = giveDamage + addDmg;

            for (int i = 0; i < giveDamage; i++)
            {
                if (!damageChara.GetHeadParts()[i].isDmage)
                {
                    damageChara.GetHeadParts()[i].isDmage = true;
                }
            }
        }
        else if (rollResult == 10)
        {
            for (int i = 0; i < giveDamage; i++)
            {
                if (!damageChara.GetHeadParts()[i].isDmage)
                {
                    damageChara.GetHeadParts()[i].isDmage = true;
                }
            }
        }
        else if (rollResult == 9)
        {
            for (int i = 0; i < giveDamage; i++)
            {
                if (!damageChara.GetArmParts()[i].isDmage)
                {
                    damageChara.GetArmParts()[i].isDmage = true;
                }
            }
        }
        else if (rollResult == 8)
        {
            for (int i = 0; i < giveDamage; i++)
            {
                if (!damageChara.GetBodyParts()[i].isDmage)
                {
                    damageChara.GetBodyParts()[i].isDmage = true;
                }
            }
        }
        else if (rollResult == 7)
        {
            for (int i = 0; i < giveDamage; i++)
            {
                if (!damageChara.GetLegParts()[i].isDmage)
                {
                    damageChara.GetLegParts()[i].isDmage = true;
                }
            }
        }
        else if (rollResult == 6)
        {
            // ���肪�I�ԁB���͉��ɓ��Ƀ_���[�W������悤�ɂ���
            for (int i = 0; i < giveDamage; i++)
            {
                if (!damageChara.GetHeadParts()[i].isDmage)
                {
                    damageChara.GetHeadParts()[i].isDmage = true;
                }
            }
        }
    }
}
