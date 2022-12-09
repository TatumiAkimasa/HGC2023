using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjEnemy : ClassData_
{
    [SerializeField]
    private Table_Enemy Enemy;

    [SerializeField]
    private kihonnpatu kihon;

    [SerializeField]
    private int EnemyAI;                  

    public Doll_blu_Nor me;

    private List<CharaManeuver>[] Maneuvers;
    private CharaManeuver UseManever;

    private void Start()
    {
        //���ʁ~4+Skill1
        Maneuvers = new List<CharaManeuver>[(int)EnemyPartsType.EPartsMax];

        //���������ǋL
        for (int i = 0; i != kihon.GET_MAX_BASE_PARTS(); i++)
        {
            me.HeadParts.Add(kihon.Base_Head_parts[i]);

            if (me.HeadParts[i].Timing == ACTION)
                Maneuvers[(int)EnemyPartsType.EAction].Add(me.HeadParts[i]);
            else if (me.HeadParts[i].Timing == RAPID)
                Maneuvers[(int)EnemyPartsType.ERapid].Add(me.HeadParts[i]);
            else if (me.HeadParts[i].Timing == JUDGE)
                Maneuvers[(int)EnemyPartsType.EJudge].Add(me.HeadParts[i]);
            else if (me.HeadParts[i].Timing == DAMAGE)
                Maneuvers[(int)EnemyPartsType.Edamage].Add(me.HeadParts[i]);

            me.ArmParts.Add(kihon.Base_Arm_parts[i]);

            if (me.ArmParts[i].Timing == ACTION)
                Maneuvers[(int)EnemyPartsType.EAction].Add(me.ArmParts[i]);
            else if (me.ArmParts[i].Timing == RAPID)
                Maneuvers[(int)EnemyPartsType.ERapid].Add(me.ArmParts[i]);
            else if (me.ArmParts[i].Timing == JUDGE)
                Maneuvers[(int)EnemyPartsType.EJudge].Add(me.ArmParts[i]);
            else if (me.ArmParts[i].Timing == DAMAGE)
                Maneuvers[(int)EnemyPartsType.Edamage].Add(me.ArmParts[i]);

            me.BodyParts.Add(kihon.Base_Body_parts[i]);

            if (me.ArmParts[i].Timing == ACTION)
                Maneuvers[(int)EnemyPartsType.EAction].Add(me.BodyParts[i]);
            else if (me.ArmParts[i].Timing == RAPID)
                Maneuvers[(int)EnemyPartsType.ERapid].Add(me.BodyParts[i]);
            else if (me.ArmParts[i].Timing == JUDGE)
                Maneuvers[(int)EnemyPartsType.EJudge].Add(me.BodyParts[i]);
            else if (me.ArmParts[i].Timing == DAMAGE)
                Maneuvers[(int)EnemyPartsType.Edamage].Add(me.BodyParts[i]);

            me.LegParts.Add(kihon.Base_Leg_parts[i]);

            if (me.ArmParts[i].Timing == ACTION)
                Maneuvers[(int)EnemyPartsType.EAction].Add(me.LegParts[i]);
            else if (me.ArmParts[i].Timing == RAPID)
                Maneuvers[(int)EnemyPartsType.ERapid].Add(me.LegParts[i]);
            else if (me.ArmParts[i].Timing == JUDGE)
                Maneuvers[(int)EnemyPartsType.EJudge].Add(me.LegParts[i]);
            else if (me.ArmParts[i].Timing == DAMAGE)
                Maneuvers[(int)EnemyPartsType.Edamage].Add(me.LegParts[i]);
        }

        //�f�[�^�����͂��A�}�j���[�o�[��ǉ�����
        //�ǉ������ǋL
        for (int SITE = 0; SITE != 4; SITE++)
        {
            for (int i = 0; i != Enemy.Wepons[SITE].Parts.Count; i++)
            {
                //None���𔲂��ɂ��Đ���
                if (SITE == HEAD)
                {
                    me.HeadParts.Add(Enemy.Wepons[HEAD].Parts[i].Maneuver);

                    if (me.HeadParts[i].Timing == ACTION)
                        Maneuvers[(int)EnemyPartsType.EAction].Add(me.HeadParts[i]);
                    else if (me.HeadParts[i].Timing == RAPID)
                        Maneuvers[(int)EnemyPartsType.ERapid].Add(me.HeadParts[i]);
                    else if (me.HeadParts[i].Timing == JUDGE)
                        Maneuvers[(int)EnemyPartsType.EJudge].Add(me.HeadParts[i]);
                    else if (me.HeadParts[i].Timing == DAMAGE)
                        Maneuvers[(int)EnemyPartsType.Edamage].Add(me.HeadParts[i]);

                }
                if (SITE == ARM)
                {
                    me.ArmParts.Add(Enemy.Wepons[ARM].Parts[i].Maneuver);

                    if (me.ArmParts[i].Timing == ACTION)
                        Maneuvers[(int)EnemyPartsType.EAction].Add(me.ArmParts[i]);
                    else if (me.ArmParts[i].Timing == RAPID)
                        Maneuvers[(int)EnemyPartsType.ERapid].Add(me.ArmParts[i]);
                    else if (me.ArmParts[i].Timing == JUDGE)
                        Maneuvers[(int)EnemyPartsType.EJudge].Add(me.ArmParts[i]);
                    else if (me.ArmParts[i].Timing == DAMAGE)
                        Maneuvers[(int)EnemyPartsType.Edamage].Add(me.ArmParts[i]);

                }
                if (SITE == BODY)
                {
                    me.BodyParts.Add(Enemy.Wepons[BODY].Parts[i].Maneuver);

                    if (me.ArmParts[i].Timing == ACTION)
                        Maneuvers[(int)EnemyPartsType.EAction].Add(me.BodyParts[i]);
                    else if (me.ArmParts[i].Timing == RAPID)
                        Maneuvers[(int)EnemyPartsType.ERapid].Add(me.BodyParts[i]);
                    else if (me.ArmParts[i].Timing == JUDGE)
                        Maneuvers[(int)EnemyPartsType.EJudge].Add(me.BodyParts[i]);
                    else if (me.ArmParts[i].Timing == DAMAGE)
                        Maneuvers[(int)EnemyPartsType.Edamage].Add(me.BodyParts[i]);

                }
                if (SITE == LEG)
                {
                    me.LegParts.Add(Enemy.Wepons[LEG].Parts[i].Maneuver);

                    if (me.ArmParts[i].Timing == ACTION)
                        Maneuvers[(int)EnemyPartsType.EAction].Add(me.LegParts[i]);
                    else if (me.ArmParts[i].Timing == RAPID)
                        Maneuvers[(int)EnemyPartsType.ERapid].Add(me.LegParts[i]);
                    else if (me.ArmParts[i].Timing == JUDGE)
                        Maneuvers[(int)EnemyPartsType.EJudge].Add(me.LegParts[i]);
                    else if (me.ArmParts[i].Timing == DAMAGE)
                        Maneuvers[(int)EnemyPartsType.Edamage].Add(me.LegParts[i]);
                }

            }
        }

    }

    public CharaManeuver EnemyAI_Action()
    {
        //�̂���PL�̂ݎ擾����悤�˗�
        List<Doll_blu_Nor> PlayerDolls = ManagerAccessor.Instance.battleSystem.GetCharaObj();

        // �G�L�����̃G���A�ƑI�����ꂽ�}�j���[�o�̎˒����Βl�Ŕ�ׂāA�˒����ł���΍U�����邩�I������R�}���h��\������
        // �G�L�����̃G���A�̐�Βl���U���̍ő�˒��ȉ����A
        // �G�L�����̃G���A�̐�Βl���U���̍ŏ��˒��ȏ�Ȃ�U������

        //�g�p����for��
        for(int ActManeuvers=0;ActManeuvers!=Maneuvers[(int)EnemyPartsType.EAction].Count;ActManeuvers++)
        { 
            for (int i = 0; i != PlayerDolls.Count; i++)
            {
                //�j������
                if (!Maneuvers[(int)EnemyPartsType.EAction][ActManeuvers].isDmage)
                {
                    //�G�̈ʒu���� & �˒���r
                    if (Maneuvers[(int)EnemyPartsType.EAction][ActManeuvers].MinRange != 10 &&                                   //����area�l����
                    (Mathf.Abs(PlayerDolls[i].area) <= Mathf.Abs(Maneuvers[(int)EnemyPartsType.EAction][ActManeuvers].MaxRange + me.area) &&
                     Mathf.Abs(PlayerDolls[i].area) >= Mathf.Abs(Maneuvers[(int)EnemyPartsType.EAction][ActManeuvers].MinRange + me.area)))
                    {
                        //�g�p����X�V
                        if (UseManever == null)
                        {
                            UseManever = Maneuvers[(int)EnemyPartsType.EAction][ActManeuvers];
                            //�D��x�X�V
                            //UseManever.EnemyAI[(int)EnemyPartsType.EAction] = 0;
                        }
                        else if (UseManever.EnemyAI[(int)EnemyPartsType.EAction] < Maneuvers[(int)EnemyPartsType.EAction][ActManeuvers].EnemyAI[(int)EnemyPartsType.EAction])
                            UseManever = Maneuvers[(int)EnemyPartsType.EAction][ActManeuvers];
                    }
                }
            }
        }

        //�c�����񂪏����������Ȃ̂œc������ɕ����Ă�������
        return UseManever;
    }

    public CharaManeuver EnemyAI_NonAction(EnemyPartsType PartsType)
    {
        //�S���Ƃ�
        List<Doll_blu_Nor> PlayerDolls = ManagerAccessor.Instance.battleSystem.GetCharaObj();

        // �G�L�����̃G���A�ƑI�����ꂽ�}�j���[�o�̎˒����Βl�Ŕ�ׂāA�˒����ł���΍U�����邩�I������R�}���h��\������
        // �G�L�����̃G���A�̐�Βl���U���̍ő�˒��ȉ����A
        // �G�L�����̃G���A�̐�Βl���U���̍ŏ��˒��ȏ�Ȃ�U������

        //�g�p����for��
        for (int ActManeuvers = 0; ActManeuvers != Maneuvers[(int)PartsType].Count; ActManeuvers++)
        {
            for (int i = 0; i != PlayerDolls.Count; i++)
            {
                //�j������
                if (!Maneuvers[(int)PartsType][ActManeuvers].isDmage)
                {
                    //�G�̈ʒu���� & �˒���r
                    if (Maneuvers[(int)PartsType][ActManeuvers].MinRange != 10 &&                                   
                    (Mathf.Abs(PlayerDolls[i].area) <= Mathf.Abs(Maneuvers[(int)PartsType][ActManeuvers].MaxRange + me.area) &&
                     Mathf.Abs(PlayerDolls[i].area) >= Mathf.Abs(Maneuvers[(int)PartsType][ActManeuvers].MinRange + me.area)))
                    {
                        //�g�p����X�V
                        if (UseManever == null)
                        {
                            UseManever = Maneuvers[(int)PartsType][ActManeuvers];
                            //�D��x�X�V
                            //UseManever.EnemyAI[(int)EnemyPartsType.EAction] = 0;
                        }
                        else if (UseManever.EnemyAI[(int)PartsType] < Maneuvers[(int)PartsType][ActManeuvers].EnemyAI[(int)PartsType])
                            UseManever = Maneuvers[(int)PartsType][ActManeuvers];
                    }
                }
            }
        }

        //�c�����񂪏����������Ȃ̂œc������ɕ����Ă�������
        return UseManever;
    }

    public void ActionTiming()
    {
        ;
    }

    public void RapidTiming()
    {
        ;
    }

    public void JudgeTiming()
    {
        ;
    }

    public void DamageTiming()
    {
        ;
    }
}

[System.Serializable]
public enum EnemyPartsType
{
    EAuto = -1,
    EAction = 0,
    ERapid,
    EJudge,
    Edamage,
    ESkill,
    EPartsMax = 5,
}
