using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DmgTimingProcess : GetClickedGameObject
{
    //-------------------------------
    // �ق�����񃁃�
    // �U������enemy
    // �^�[�Q�b�g�ɂȂ��Ă閡���L����
    //-------------------------------

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
        if(isStandbyCharaSelect)
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
                    isStandbyCharaSelect = false;
                    selectedAllyChara = move.GetComponent<Doll_blu_Nor>();
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
        nextButton.gameObject.SetActive(false);
    }

    public void OnClickExe()
    {
        ExeManeuver(dollManeuver, selectedAllyChara);
    }


    /// <summary>
    /// ���������}�j���[�o�[����������̂��̊m�F������
    /// </summary>
    public void ExeManeuver(CharaManeuver maneuver, Doll_blu_Nor dmgExeChara)
    {
        // �_���[�W�𑝉�����}�j���[�o�[�̏���
        if(maneuver.EffectNum.ContainsKey(EffNum.Damage))
        {
            DamageUPProcess(maneuver,dmgExeChara);
        }
        // �h��l�𑝉�����}�j���[�o�[�̏���
        else if(dollManeuver.EffectNum.ContainsKey(EffNum.Guard))   
        {
            GuardProcess(maneuver, dmgExeChara);
        }
        else   // ��̓�ɊY�����Ȃ��ꍇ�A�ŗL�̌��ʂƎg�p����
        {

        }

        // ���J�E���g�œ����L�������_���[�W�^�C�~���O�̃}�j���[�o�[�𔭓������ꍇ�A���J�E���g�ɍs�����ł��Ȃ��Ȃ�̂ō��̕\������ѓ�����\��̃L�����̃��X�g����폜����
        if(selectedAllyChara.NowCount==ManagerAccessor.Instance.battleSystem.NowCount)
        {
            ManagerAccessor.Instance.battleSystem.DeleteMoveChara(selectedAllyChara.Name);
        }

        confirmatButton.SetActive(false);
        ZoomOutObj();
    }

    private void DamageUPProcess(CharaManeuver maneuver, Doll_blu_Nor dmgExeChara)
    {
        // �^����_���[�W���オ��n�̏���
        // �˒������g�݂̂̏ꍇ�A�_���[�W��^����L�����ƃ_���[�W�^�C�~���O�œ����L�������������ǂ������ׂ�
        if (dollManeuver.MinRange == 10)
        {
            if(actingChara == dmgExeChara)
            {
                addDamage += maneuver.EffectNum[EffNum.Damage];
                // �vif�������B����ȃR�X�g�ǂ������f����
                dmgExeChara.NowCount -= maneuver.Cost;
            }
        }
        // �G�L�����̃G���A�ƑI�����ꂽ�}�j���[�o�̎˒����Βl�Ŕ�ׂāA�˒����ł���΍U�����邩�I������R�}���h��\������
        // �G�L�����̃G���A�̐�Βl���U���̍ő�˒��ȉ����A
        // �G�L�����̃G���A�̐�Βl���U���̍ŏ��˒��ȏ�Ȃ甭������
        else if ((Mathf.Abs(actingChara.area) <= Mathf.Abs(maneuver.MaxRange + dmgExeChara.area)  &&
                  Mathf.Abs(actingChara.area) >= Mathf.Abs(maneuver.MinRange + dmgExeChara.area)) &&
                (!maneuver.isUse && !maneuver.isDmage))
        {
            addDamage += maneuver.EffectNum[EffNum.Damage];
            // �vif�������B����ȃR�X�g�ǂ������f����
            dmgExeChara.NowCount -= maneuver.Cost;
        }
        else
        {
            // �˒�������܂��񌩂����ȕ\�L����
        }
    }

    private void GuardProcess(CharaManeuver maneuver, Doll_blu_Nor dmgExeChara)
    {
        // �h��Ƃ��̏���
        // �˒������g�݂̂̏ꍇ�A�_���[�W���󂯂�L�����ƃ_���[�W�^�C�~���O�œ����L�������������ǂ������ׂ�
        if (maneuver.MinRange == 10)
        {
            if (damageChara == dmgExeChara)
            {
                dmgGuard += maneuver.EffectNum[EffNum.Guard];
                // �vif�������B����ȃR�X�g�ǂ������f����
                dmgExeChara.NowCount -= maneuver.Cost;
            }
        }
        else if ((Mathf.Abs(damageChara.area) <= Mathf.Abs(maneuver.MaxRange + dmgExeChara.area) &&
                  Mathf.Abs(damageChara.area) >= Mathf.Abs(maneuver.MinRange + dmgExeChara.area)) &&
                (!maneuver.isUse && !maneuver.isDmage))
        {
            if(maneuver.EffectNum.ContainsKey(EffNum.Guard))
            {
                dmgGuard += maneuver.EffectNum[EffNum.Guard];
            }
            // �vif�������B����ȃR�X�g�ǂ������f����
            selectedAllyChara.NowCount -= maneuver.Cost;
        }
        else
        {
            // �˒�������܂��񌩂����ȕ\�L����
        }
    }

    private void EXProcess()
    {
        // ���΂��̏���
        if (dollManeuver.EffectNum.ContainsKey(EffNum.Protect))
        {
            // �������΂��̏������邩��
            // �vif�������B����ȃR�X�g�ǂ������f����
            selectedAllyChara.NowCount -= dollManeuver.Cost;
        }
        
    }

    public void OnClickNextButton()
    {
        // �ŏI�I�ȃ_���[�W�̌��ʂ������A�U�����ꂽ�L�����N�^�[���_���[�W���󂯂�
        // �I�[�g�^�C�~���O�̂��̂����킹�ĉ��Z����\��
        bool isAnim = false;
        

        giveDamage = actManeuver.EffectNum[EffNum.Damage] + addDamage - dmgGuard;

        // rollResult��10��葽���ꍇ�͍U������L�������ǂ��̕��ʂɓ��Ă邩���߂��邪���͉��ɓ��Ƃ���
        // �vif�������B�T���@���g���z���[�����M�I����
        if (rollResult > 10)
        {
            // �_�C�X���[���̌��ʂ�10����̏ꍇ�̒ǉ��_���[�W����
            int addDmg = rollResult - 10;
            giveDamage = giveDamage + addDmg;

            // �^����_���[�W���p�[�c�̐���葽���ꍇ�A�v�f����葽�������Q�Ƃ��Ȃ��悤�ɂ���B
            if (giveDamage > damageChara.GetHeadParts().Count)
            {
                giveDamage = damageChara.GetHeadParts().Count;
            }

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
            // �^����_���[�W���p�[�c�̐���葽���ꍇ�A�v�f����葽�������Q�Ƃ��Ȃ��悤�ɂ���B
            if (giveDamage>damageChara.GetHeadParts().Count)
            {
                giveDamage = damageChara.GetHeadParts().Count;
            }

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
            // �^����_���[�W���p�[�c�̐���葽���ꍇ�A�v�f����葽�������Q�Ƃ��Ȃ��悤�ɂ���B
            if (giveDamage > damageChara.GetArmParts().Count)
            {                               
                giveDamage = damageChara.GetArmParts().Count;
            }

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
            // �^����_���[�W���p�[�c�̐���葽���ꍇ�A�v�f����葽�������Q�Ƃ��Ȃ��悤�ɂ���B
            if (giveDamage > damageChara.GetBodyParts().Count)
            {                               
                giveDamage = damageChara.GetBodyParts().Count;
            }

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
            // �^����_���[�W���p�[�c�̐���葽���ꍇ�A�v�f����葽�������Q�Ƃ��Ȃ��悤�ɂ���B
            if (giveDamage > damageChara.GetLegParts().Count)
            {                               
                giveDamage = damageChara.GetLegParts().Count;
            }

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
            // �^����_���[�W���p�[�c�̐���葽���ꍇ�A�v�f����葽�������Q�Ƃ��Ȃ��悤�ɂ���B
            if (giveDamage > damageChara.GetHeadParts().Count)
            {
                giveDamage = damageChara.GetHeadParts().Count;
            }

            // ���肪�I�ԁB���͉��ɓ��Ƀ_���[�W������悤�ɂ���
            for (int i = 0; i < giveDamage; i++)
            {
                if (!damageChara.GetHeadParts()[i].isDmage)
                {
                    damageChara.GetHeadParts()[i].isDmage = true;
                }
            }
        }

        // �����A�A�j���[�V�����I����Ă���̏����ɂ������Ȃ�

        StartCoroutine(ManeuverAnimation(actManeuver, callBack =>
        {
            // �s�������L������\���������
            ManagerAccessor.Instance.battleSystem.DeleteMoveChara();
            ManagerAccessor.Instance.battleSystem.BattleExe = true;
            nextButton.gameObject.SetActive(false);
        }));
    }

    public IEnumerator ManeuverAnimation(CharaManeuver maneuver, System.Action<bool> callBack)
    {
        if(maneuver.AnimEffect!=null)
        {
            GameObject instance = Instantiate(maneuver.AnimEffect, damageChara.transform.position, Quaternion.identity, damageChara.transform);
            EffctEnd effctEnd = instance.GetComponent<EffctEnd>();

            while (!effctEnd.AnimEnd)
            {
                yield return null;  
            }
        }
        else
        {
            yield break;
        }

        callBack(true);
    }

}
