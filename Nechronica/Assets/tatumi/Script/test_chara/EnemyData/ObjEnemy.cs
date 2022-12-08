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

    public Doll_blueprint me;

    private List<CharaManeuver>[] Maneuvers;
    private CharaManeuver UseManever;

    //�^�C�~���O�̒萔
    private const int COUNT = -1;
    private const int AUTO = 0;
    private const int ACTION = 1;
    private const int MOVE = 2;
    private const int RAPID = 3;
    private const int JUDGE = 4;
    private const int DAMAGE = 5;

    private enum EnemyPartsType
    {
        EAuto=0,
        EAction,
        ERapid,
        EJudge,
        Edamage,
        ESkill,
        EPartsMax=5,
    }


    private void Start()
    {
        //���ʁ~4+Skill1
        Maneuvers = new List<CharaManeuver>[(int)EnemyPartsType.EPartsMax];

        //���������ǋL
        for (int i = 0; i != kihon.GET_MAX_BASE_PARTS(); i++)
        {
            me.CharaBase_data.HeadParts.Add(kihon.Base_Head_parts[i]);

            if (me.CharaBase_data.HeadParts[i].Timing == ACTION)
                Maneuvers[(int)EnemyPartsType.EAction].Add(me.CharaBase_data.HeadParts[i]);
            else if (me.CharaBase_data.HeadParts[i].Timing == RAPID)
                Maneuvers[(int)EnemyPartsType.ERapid].Add(me.CharaBase_data.HeadParts[i]);
            else if (me.CharaBase_data.HeadParts[i].Timing == JUDGE)
                Maneuvers[(int)EnemyPartsType.EJudge].Add(me.CharaBase_data.HeadParts[i]);
            else if (me.CharaBase_data.HeadParts[i].Timing == DAMAGE)
                Maneuvers[(int)EnemyPartsType.Edamage].Add(me.CharaBase_data.HeadParts[i]);

            me.CharaBase_data.ArmParts.Add(kihon.Base_Arm_parts[i]);

            if (me.CharaBase_data.ArmParts[i].Timing == ACTION)
                Maneuvers[(int)EnemyPartsType.EAction].Add(me.CharaBase_data.ArmParts[i]);
            else if (me.CharaBase_data.ArmParts[i].Timing == RAPID)
                Maneuvers[(int)EnemyPartsType.ERapid].Add(me.CharaBase_data.ArmParts[i]);
            else if (me.CharaBase_data.ArmParts[i].Timing == JUDGE)
                Maneuvers[(int)EnemyPartsType.EJudge].Add(me.CharaBase_data.ArmParts[i]);
            else if (me.CharaBase_data.ArmParts[i].Timing == DAMAGE)
                Maneuvers[(int)EnemyPartsType.Edamage].Add(me.CharaBase_data.ArmParts[i]);

            me.CharaBase_data.BodyParts.Add(kihon.Base_Body_parts[i]);

            if (me.CharaBase_data.ArmParts[i].Timing == ACTION)
                Maneuvers[(int)EnemyPartsType.EAction].Add(me.CharaBase_data.BodyParts[i]);
            else if (me.CharaBase_data.ArmParts[i].Timing == RAPID)
                Maneuvers[(int)EnemyPartsType.ERapid].Add(me.CharaBase_data.BodyParts[i]);
            else if (me.CharaBase_data.ArmParts[i].Timing == JUDGE)
                Maneuvers[(int)EnemyPartsType.EJudge].Add(me.CharaBase_data.BodyParts[i]);
            else if (me.CharaBase_data.ArmParts[i].Timing == DAMAGE)
                Maneuvers[(int)EnemyPartsType.Edamage].Add(me.CharaBase_data.BodyParts[i]);

            me.CharaBase_data.LegParts.Add(kihon.Base_Leg_parts[i]);

            if (me.CharaBase_data.ArmParts[i].Timing == ACTION)
                Maneuvers[(int)EnemyPartsType.EAction].Add(me.CharaBase_data.LegParts[i]);
            else if (me.CharaBase_data.ArmParts[i].Timing == RAPID)
                Maneuvers[(int)EnemyPartsType.ERapid].Add(me.CharaBase_data.LegParts[i]);
            else if (me.CharaBase_data.ArmParts[i].Timing == JUDGE)
                Maneuvers[(int)EnemyPartsType.EJudge].Add(me.CharaBase_data.LegParts[i]);
            else if (me.CharaBase_data.ArmParts[i].Timing == DAMAGE)
                Maneuvers[(int)EnemyPartsType.Edamage].Add(me.CharaBase_data.LegParts[i]);
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
                    me.CharaBase_data.HeadParts.Add(Enemy.Wepons[HEAD].Parts[i].Maneuver);

                    if (me.CharaBase_data.HeadParts[i].Timing == ACTION)
                        Maneuvers[(int)EnemyPartsType.EAction].Add(me.CharaBase_data.HeadParts[i]);
                    else if (me.CharaBase_data.HeadParts[i].Timing == RAPID)
                        Maneuvers[(int)EnemyPartsType.ERapid].Add(me.CharaBase_data.HeadParts[i]);
                    else if (me.CharaBase_data.HeadParts[i].Timing == JUDGE)
                        Maneuvers[(int)EnemyPartsType.EJudge].Add(me.CharaBase_data.HeadParts[i]);
                    else if (me.CharaBase_data.HeadParts[i].Timing == DAMAGE)
                        Maneuvers[(int)EnemyPartsType.Edamage].Add(me.CharaBase_data.HeadParts[i]);

                }
                if (SITE == ARM)
                {
                    me.CharaBase_data.ArmParts.Add(Enemy.Wepons[ARM].Parts[i].Maneuver);

                    if (me.CharaBase_data.ArmParts[i].Timing == ACTION)
                        Maneuvers[(int)EnemyPartsType.EAction].Add(me.CharaBase_data.ArmParts[i]);
                    else if (me.CharaBase_data.ArmParts[i].Timing == RAPID)
                        Maneuvers[(int)EnemyPartsType.ERapid].Add(me.CharaBase_data.ArmParts[i]);
                    else if (me.CharaBase_data.ArmParts[i].Timing == JUDGE)
                        Maneuvers[(int)EnemyPartsType.EJudge].Add(me.CharaBase_data.ArmParts[i]);
                    else if (me.CharaBase_data.ArmParts[i].Timing == DAMAGE)
                        Maneuvers[(int)EnemyPartsType.Edamage].Add(me.CharaBase_data.ArmParts[i]);

                }
                if (SITE == BODY)
                {
                    me.CharaBase_data.BodyParts.Add(Enemy.Wepons[BODY].Parts[i].Maneuver);

                    if (me.CharaBase_data.ArmParts[i].Timing == ACTION)
                        Maneuvers[(int)EnemyPartsType.EAction].Add(me.CharaBase_data.BodyParts[i]);
                    else if (me.CharaBase_data.ArmParts[i].Timing == RAPID)
                        Maneuvers[(int)EnemyPartsType.ERapid].Add(me.CharaBase_data.BodyParts[i]);
                    else if (me.CharaBase_data.ArmParts[i].Timing == JUDGE)
                        Maneuvers[(int)EnemyPartsType.EJudge].Add(me.CharaBase_data.BodyParts[i]);
                    else if (me.CharaBase_data.ArmParts[i].Timing == DAMAGE)
                        Maneuvers[(int)EnemyPartsType.Edamage].Add(me.CharaBase_data.BodyParts[i]);

                }
                if (SITE == LEG)
                {
                    me.CharaBase_data.LegParts.Add(Enemy.Wepons[LEG].Parts[i].Maneuver);

                    if (me.CharaBase_data.ArmParts[i].Timing == ACTION)
                        Maneuvers[(int)EnemyPartsType.EAction].Add(me.CharaBase_data.LegParts[i]);
                    else if (me.CharaBase_data.ArmParts[i].Timing == RAPID)
                        Maneuvers[(int)EnemyPartsType.ERapid].Add(me.CharaBase_data.LegParts[i]);
                    else if (me.CharaBase_data.ArmParts[i].Timing == JUDGE)
                        Maneuvers[(int)EnemyPartsType.EJudge].Add(me.CharaBase_data.LegParts[i]);
                    else if (me.CharaBase_data.ArmParts[i].Timing == DAMAGE)
                        Maneuvers[(int)EnemyPartsType.Edamage].Add(me.CharaBase_data.LegParts[i]);
                }

            }
        }

    }

    public void EnemyAI_Action()
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
                    (Mathf.Abs(PlayerDolls[i].area) <= Mathf.Abs(Maneuvers[(int)EnemyPartsType.EAction][ActManeuvers].MaxRange + me.potition) &&
                     Mathf.Abs(PlayerDolls[i].area) >= Mathf.Abs(Maneuvers[(int)EnemyPartsType.EAction][ActManeuvers].MinRange + me.potition)))
                    {
                        //�g�p����X�V
                        if (UseManever == null)
                        {
                            UseManever = Maneuvers[(int)EnemyPartsType.EAction][ActManeuvers];
                            //�D��x�X�V
                            UseManever.EnemyAI[(int)EnemyPartsType.EAction] = 0;
                        }
                        else if (UseManever.EnemyAI[(int)EnemyPartsType.EAction] < Maneuvers[(int)EnemyPartsType.EAction][ActManeuvers].EnemyAI[(int)EnemyPartsType.EAction])
                            UseManever = Maneuvers[(int)EnemyPartsType.EAction][ActManeuvers];
                    }
                }
            }
        }
    }
}
