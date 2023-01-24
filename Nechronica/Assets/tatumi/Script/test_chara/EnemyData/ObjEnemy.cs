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

    public CharaManeuver opponentManeuver;
    public Doll_blu_Nor opponent;
    public int Expected_FatalDamage;//�v�����_���[�W��

    private void Start()
    {
        opponentManeuver.EffectNum.Add(EffNum.Damage, 1);

        //���ʁ~4+Skill1
        Maneuvers = new List<CharaManeuver>[(int)EnemyPartsType.EPartsMax];
        for (int i = 0; i != Maneuvers.Length; i++)
            Maneuvers[i] = new List<CharaManeuver>();

        //���������ǋL
        for (int i = 0; i != 3; i++)
        {
           
            //me.HeadParts.Add(kihon.Base_Head_parts[i]);

            if (me.HeadParts[i].Timing == ACTION)
                Maneuvers[(int)EnemyPartsType.EAction].Add(me.HeadParts[i]);
            else if (me.HeadParts[i].Timing == RAPID)
                Maneuvers[(int)EnemyPartsType.ERapid].Add(me.HeadParts[i]);
            else if (me.HeadParts[i].Timing == JUDGE)
                Maneuvers[(int)EnemyPartsType.EJudge].Add(me.HeadParts[i]);
            else if (me.HeadParts[i].Timing == DAMAGE)
                Maneuvers[(int)EnemyPartsType.Edamage].Add(me.HeadParts[i]);

            //me.ArmParts.Add(kihon.Base_Arm_parts[i]);

            if (me.ArmParts[i].Timing == ACTION)
                Maneuvers[(int)EnemyPartsType.EAction].Add(me.ArmParts[i]);
            else if (me.ArmParts[i].Timing == RAPID)
                Maneuvers[(int)EnemyPartsType.ERapid].Add(me.ArmParts[i]);
            else if (me.ArmParts[i].Timing == JUDGE)
                Maneuvers[(int)EnemyPartsType.EJudge].Add(me.ArmParts[i]);
            else if (me.ArmParts[i].Timing == DAMAGE)
                Maneuvers[(int)EnemyPartsType.Edamage].Add(me.ArmParts[i]);

            //me.BodyParts.Add(kihon.Base_Body_parts[i]);

            if (me.BodyParts[i].Timing == ACTION)
                Maneuvers[(int)EnemyPartsType.EAction].Add(me.BodyParts[i]);
            else if (me.BodyParts[i].Timing == RAPID)
                Maneuvers[(int)EnemyPartsType.ERapid].Add(me.BodyParts[i]);
            else if (me.BodyParts[i].Timing == JUDGE)
                Maneuvers[(int)EnemyPartsType.EJudge].Add(me.BodyParts[i]);
            else if (me.BodyParts[i].Timing == DAMAGE)
                Maneuvers[(int)EnemyPartsType.Edamage].Add(me.BodyParts[i]);

           // me.LegParts.Add(kihon.Base_Leg_parts[i]);

            if (me.LegParts[i].Timing == ACTION)
                Maneuvers[(int)EnemyPartsType.EAction].Add(me.LegParts[i]);
            else if (me.LegParts[i].Timing == RAPID)
                Maneuvers[(int)EnemyPartsType.ERapid].Add(me.LegParts[i]);
            else if (me.LegParts[i].Timing == JUDGE)
                Maneuvers[(int)EnemyPartsType.EJudge].Add(me.LegParts[i]);
            else if (me.LegParts[i].Timing == DAMAGE)
                Maneuvers[(int)EnemyPartsType.Edamage].Add(me.LegParts[i]);
        }

        TableParts_EffctUp(Enemy);

        //�f�[�^�����͂��A�}�j���[�o�[��ǉ�����
        //�ǉ������ǋL
        for (int SITE = 0; SITE != 4; SITE++)
        {
            for (int i = 0; i != Enemy.Wepons[SITE].Parts.Count; i++)
            {
                CharaManeuver AddManuver = null;
                //None���𔲂��ɂ��Đ���
                if (SITE == HEAD)
                {
                    //�ǋL����}�j���[�o�[
                    AddManuver = Enemy.Wepons[HEAD].Parts[i].Maneuver;
                    me.HeadParts.Add(AddManuver);
                    
                }
                if (SITE == ARM)
                {
                    //�ǋL����}�j���[�o�[
                    AddManuver = Enemy.Wepons[ARM].Parts[i].Maneuver;
                    me.ArmParts.Add(Enemy.Wepons[ARM].Parts[i].Maneuver);

                }
                if (SITE == BODY)
                {
                    //�ǋL����}�j���[�o�[
                    AddManuver = Enemy.Wepons[BODY].Parts[i].Maneuver;
                    me.BodyParts.Add(Enemy.Wepons[BODY].Parts[i].Maneuver);

                }
                if (SITE == LEG)
                {
                    //�ǋL����}�j���[�o�[
                    AddManuver = Enemy.Wepons[LEG].Parts[i].Maneuver;
                    me.LegParts.Add(Enemy.Wepons[LEG].Parts[i].Maneuver);
                }
                //�ǋL
                if (AddManuver.Timing == ACTION)
                    Maneuvers[(int)EnemyPartsType.EAction].Add(AddManuver);
                else if (AddManuver.Timing == RAPID)
                    Maneuvers[(int)EnemyPartsType.ERapid].Add(AddManuver);
                else if (AddManuver.Timing == JUDGE)
                    Maneuvers[(int)EnemyPartsType.EJudge].Add(AddManuver);
                else if (AddManuver.Timing == DAMAGE)
                    Maneuvers[(int)EnemyPartsType.Edamage].Add(AddManuver);
                else
                    ;//�o�^�̕K�v�Ȃ��^�C�~���O
            }
        }

    }

    public void EnemyAI_Action()
    {
        //������
        UseManever = null;

        List<Doll_blu_Nor> PlayerDolls = new List<Doll_blu_Nor>();
        Doll_blu_Nor target = null;
       //PL�̂ݎ擾
        for (int i=0;i< ManagerAccessor.Instance.battleSystem.GetCharaObj().Count;i++)
        {
            if(ManagerAccessor.Instance.battleSystem.GetCharaObj()[i].gameObject.CompareTag("AllyChara"))
            {
                PlayerDolls.Add(ManagerAccessor.Instance.battleSystem.GetCharaObj()[i]);
            }
        }
        

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
                    if (Maneuvers[(int)EnemyPartsType.EAction][ActManeuvers].MinRange != 10 &&                                  
                    (Mathf.Abs(PlayerDolls[i].area) <= Mathf.Abs(Maneuvers[(int)EnemyPartsType.EAction][ActManeuvers].MaxRange + me.area) &&
                     Mathf.Abs(PlayerDolls[i].area) >= Mathf.Abs(Maneuvers[(int)EnemyPartsType.EAction][ActManeuvers].MinRange + me.area)))
                    {
                        //�g�p����X�V
                        UseManever = UseManuber_Change((int)EnemyPartsType.EAction, ActManeuvers, UseManever);
                        target = PlayerDolls[i];

                    }
                }
            }
        }

        //�SACTION�ᖡ���A��ԗL���l�����������g�p����
        if (UseManever != null)
            ProcessAccessor.Instance.actTiming.ExeAtkManeuver(target, UseManever, me);

        return;
    }
    //�ړ�����
    public void EnemyAI_Rapid(CharaManeuver OpponentManeuver, Doll_blu_Nor Opponent)
    {
        //������
        UseManever = null;

        //�S�������
        List<Doll_blu_Nor> PlayerDolls = ManagerAccessor.Instance.battleSystem.GetCharaObj();

        int MaxOpponentRange = OpponentManeuver.MaxRange;
        int MinopponentRange = OpponentManeuver.MinRange;

        int differenceRange = 0;
        bool MeorOp = true;

        //����̕����˒��ɂǂ����������ǂꂾ���]�T�����邩
        //��:�_���Ă��镐��˒���2~0�ō��̏󋵂��˒��̐����ł����Ƃ����2,1,0�̂ǂꂩ���Z�o
        differenceRange = Mathf.Abs(Opponent.area - me.area) - MaxOpponentRange;
        if (Mathf.Abs(differenceRange) < Mathf.Abs(Mathf.Abs(Opponent.area - me.area) - MinopponentRange))
            differenceRange = Mathf.Abs(Opponent.area - me.area) - MinopponentRange;

        // �G�L�����̃G���A�ƑI�����ꂽ�}�j���[�o�̎˒����Βl�Ŕ�ׂāA�˒����ł���΍U�����邩�I������R�}���h��\������
        // �G�L�����̃G���A�̐�Βl���U���̍ő�˒��ȉ����A
        // �G�L�����̃G���A�̐�Βl���U���̍ŏ��˒��ȏ�Ȃ�U������
        //�g�p����for��
        for (int ActManeuvers = 0; ActManeuvers != Maneuvers[(int)EnemyPartsType.ERapid].Count; ActManeuvers++)
        {
            //�j������&�g�p����
            if (!Maneuvers[(int)EnemyPartsType.ERapid][ActManeuvers].isDmage && !Maneuvers[(int)EnemyPartsType.ERapid][ActManeuvers].isDmage)
            {
                //�ړ��ȊO�̃}�j���[�o�[
                if (Maneuvers[(int)EnemyPartsType.ERapid][ActManeuvers].EffectNum[EffNum.Move] == 0)
                {
                    ;//���̂Ƃ���Ȃ��H
                }
                //�ړ��܂ɂ�΁[
                else
                {
                    //���̎��_�Ő���=�˒���,+=�V����,-�n������������,0�̏ꍇ�͕ʓr���g�Ŕ���
                    //�����ؒ���Parts���˒��O�ɂȂ�܂Ō��ʂ��y�ڂ��邩����
                    //�˒���1�ȏ�]�T������ꍇ
                    if (Maneuvers[(int)EnemyPartsType.ERapid][ActManeuvers].EffectNum[EffNum.Move] > Mathf.Abs(differenceRange))
                    {
                        //���ʂ��y�ڂ���Ƃ�
                        //�G�̈ʒu���� & �˒���r(����𓮂���)
                        if (Maneuvers[(int)EnemyPartsType.ERapid][ActManeuvers].MinRange != 10 &&
                    (Mathf.Abs(Opponent.area) <= Mathf.Abs(Maneuvers[(int)EnemyPartsType.ERapid][ActManeuvers].MaxRange + me.area) &&
                     Mathf.Abs(Opponent.area) >= Mathf.Abs(Maneuvers[(int)EnemyPartsType.ERapid][ActManeuvers].MinRange + me.area)))
                        {
                            //�g�p����X�V
                            UseManever = UseManuber_Change((int)EnemyPartsType.ERapid, ActManeuvers, UseManever);
                            MeorOp = true;

                        }
                        //���g�ɋy�ڂ��ꍇ
                        else if(Maneuvers[(int)EnemyPartsType.ERapid][ActManeuvers].MinRange == 10 || (Maneuvers[(int)EnemyPartsType.ERapid][ActManeuvers].MaxRange == 0 && Maneuvers[(int)EnemyPartsType.ERapid][ActManeuvers].MinRange == 0))
                        {
                            //�g�p����X�V
                            UseManever = UseManuber_Change((int)EnemyPartsType.ERapid, ActManeuvers, UseManever);
                            MeorOp = false;
                        }
                    }
                }
            }

        }

        if (UseManever != null)
        {
            //�����Ɏg���Ƃ��̉e������
            if (MeorOp == false)
            {
                var item = Opponent;
                Opponent = me;
                me = item;
            }

            //����̎˒���0�̏ꍇ
            if (4 >= Opponent.area + UseManever.EffectNum[EffNum.Move])
            {
                //�Ƃ�ܒn�����ֈړ�
                Debug.Log("Enemy:�n���ɂƂ΂�");
                ProcessAccessor.Instance.rpdTiming.SetDirection(false);
                ProcessAccessor.Instance.rpdTiming.AddRapidManeuver(me, Opponent, UseManever);
                return;
            }
            else if (0 <= Opponent.area - UseManever.EffectNum[EffNum.Move])
            {
                //�����Ȃ炵�Ԃ��Ԕ��΂�
                Debug.Log("Enemy:�V���ɂƂ΂�");
                ProcessAccessor.Instance.rpdTiming.SetDirection(true);
                ProcessAccessor.Instance.rpdTiming.AddRapidManeuver(me, Opponent, UseManever);
                return;
            }

            //�����Ɏg���Ƃ��̉e������
            if (MeorOp == false)
            {
                var item = Opponent;
                Opponent = me;
                me = item;
            }

            //���g�ł͑Ή��s��
            Debug.Log("Enemy:Help!");
            return;
            //�S������
            for (int i = 0; i != PlayerDolls.Count; i++)
            {
                //���̖����ɋ~���𑗂�
                PlayerDolls[i].GetComponent<ObjEnemy>().HelpMoveRapid(me, Opponent, differenceRange);
            }
        }

        return;

    }
    //�W�Q����
    public void EnemyAI_Judge(CharaManeuver OpponentManeuver, Doll_blu_Nor Opponent,int DiceRoll,int TargetParts,int Power)
    {
        //������
        UseManever = null;

        //������x�v���̃p�^�[��
        if (Power != 0)
            ;
        //�_�C�X�l�����Ή��ł���͈�(6�ȉ��Œv�������f������)
        else if (DiceRoll >= 6)
        {
            //�v����1(���݂̃p�[�c���ŋ��e�ł���_���[�W�ʂ�)���f�B���Ȃ��Ȃ�X���[
            if (TargetParts == HEAD)
            {
                if (OpponentManeuver.Moving == 0 && OpponentManeuver.EffectNum[EffNum.Damage] < me.HeadParts.Count)
                    return;
            }
            else if (TargetParts == ARM)
            {
                if (OpponentManeuver.Moving == 0 && OpponentManeuver.EffectNum[EffNum.Damage] < me.ArmParts.Count)
                    return;
            }
            else if (TargetParts == LEG)
            {
                if (OpponentManeuver.Moving == 0 && OpponentManeuver.EffectNum[EffNum.Damage] < me.LegParts.Count)
                    return;
            }
            else if (TargetParts == BODY)
            {
                if (OpponentManeuver.Moving == 0 && OpponentManeuver.EffectNum[EffNum.Damage] < me.BodyParts.Count)
                    return;
            }

            //�v����2(���e�ł���_���[�W�ʂ�)���f�B���Ȃ��Ȃ�X���[
            if (opponentManeuver.EffectNum[EffNum.Damage] < Expected_FatalDamage)
                return;
        }


        //�v�����̏ꍇ�i�ǂ����Ă���������)
        //�S�������
        List<Doll_blu_Nor> PlayerDolls = ManagerAccessor.Instance.battleSystem.GetCharaObj();

        int MaxOpponentRange = OpponentManeuver.MaxRange;
        int MinopponentRange = OpponentManeuver.MinRange;

        // �G�L�����̃G���A�ƑI�����ꂽ�}�j���[�o�̎˒����Βl�Ŕ�ׂāA�˒����ł���΍U�����邩�I������R�}���h��\������
        // �G�L�����̃G���A�̐�Βl���U���̍ő�˒��ȉ����A
        // �G�L�����̃G���A�̐�Βl���U���̍ŏ��˒��ȏ�Ȃ�U������
        //�g�p����for��
        for (int ActManeuvers = 0; ActManeuvers != Maneuvers[(int)EnemyPartsType.EJudge].Count; ActManeuvers++)
        {
            //�j������&�g�p����
            if (!Maneuvers[(int)EnemyPartsType.EJudge][ActManeuvers].isDmage && !Maneuvers[(int)EnemyPartsType.EJudge][ActManeuvers].isDmage)
            {
                if (Maneuvers[(int)EnemyPartsType.EJudge][ActManeuvers].MinRange != 10 &&
            (Mathf.Abs(Opponent.area) <= Mathf.Abs(Maneuvers[(int)EnemyPartsType.EJudge][ActManeuvers].MaxRange + me.area) &&
             Mathf.Abs(Opponent.area) >= Mathf.Abs(Maneuvers[(int)EnemyPartsType.EJudge][ActManeuvers].MinRange + me.area)))
                {
                    //�g�p����X�V
                    UseManever = UseManuber_Change((int)EnemyPartsType.EJudge, ActManeuvers, UseManever);
                   
                }

            }
           
        }

        if (UseManever != null)
        {
            //�}�j���[�o�[�K�����ʂ��܂����������̏ꍇ
            if (5 < DiceRoll - UseManever.EffectNum["Judge"] - Power)
            {
                Debug.Log("������x�W�Q�ł��邩�`�������W�I");
                return;
                //������x�������J��Ԃ�
                EnemyAI_Judge(OpponentManeuver, Opponent, DiceRoll, TargetParts, UseManever.EffectNum["Judge"]);
                //�v���i�s��
                ProcessAccessor.Instance.jdgTiming.ExeJudgManeuver(UseManever, me);

            }
            else
            {
                //�v���i�s��
                Debug.Log("�W�Q�`�������W����");
                ProcessAccessor.Instance.jdgTiming.ExeJudgManeuver(UseManever, me);
                return;
            }
        }

        //���g�ł͑Ή��s��
        Debug.Log("Enemy:JudgeHelp!");
        return;
        //�S������
        for (int i = 0; i != PlayerDolls.Count; i++)
        {
            //���̖����ɋ~���𑗂�
            Power += PlayerDolls[i].GetComponent<ObjEnemy>().HelpJudge_OP(Opponent,OpponentManeuver.EffectNum["Action"] - Power);

            //���e�l�Ȃ�
            if (6 > DiceRoll - Power)
                break;
        }

        //��񂿂��J��Ԃ����̖����ɖW�Q�d�˂Ă��炤�����C���J��
        return;
    }
    //�x������
    public void EnemyAI_Judge(CharaManeuver OpponentManeuver, Doll_blu_Nor Opponent, int DiceRoll, int Power)
    {
        //������
        UseManever = null;

        int MaxOpponentRange = OpponentManeuver.MaxRange;
        int MinopponentRange = OpponentManeuver.MinRange;

        //�S�������
        List<Doll_blu_Nor> PlayerDolls = ManagerAccessor.Instance.battleSystem.GetCharaObj();

        //�g�p����for��
        for (int ActManeuvers = 0; ActManeuvers != Maneuvers[(int)EnemyPartsType.EJudge].Count; ActManeuvers++)
        {
            //�j������&�g�p����
            if (!Maneuvers[(int)EnemyPartsType.EJudge][ActManeuvers].isDmage && !Maneuvers[(int)EnemyPartsType.EJudge][ActManeuvers].isDmage)
            {
                //���g�̈ʒu�Ǝ˒���r
                if ( (Mathf.Abs(Opponent.area) <= Mathf.Abs(Maneuvers[(int)EnemyPartsType.EJudge][ActManeuvers].MaxRange + me.area) &&
                      Mathf.Abs(Opponent.area) >= Mathf.Abs(Maneuvers[(int)EnemyPartsType.EJudge][ActManeuvers].MinRange + me.area)))
                {
                    //�g�p����X�V
                    UseManever = UseManuber_Change((int)EnemyPartsType.EJudge, ActManeuvers, UseManever);
                   
                }
            }

        }

        if (UseManever != null)
        {
            //�}�j���[�o�[�K�����ʂ��܂����������̏ꍇ
            if (5 > DiceRoll - UseManever.EffectNum["Judge"] - Power)
            {
                //������x�������J��Ԃ�

                //�v���i�s��

                return;
            }
            else
            {
                EnemyAI_Judge(OpponentManeuver, Opponent, DiceRoll, UseManever.EffectNum["Judge"]);

                //�v���i�s��
                return;
            }
        }

        //���g�ł͑Ή��s��
        Debug.Log("Enemy:Help!");
        return;
        //�S������
        for (int i = 0; i != PlayerDolls.Count; i++)
        {
            //���̖����ɋ~���𑗂�
            Power += PlayerDolls[i].GetComponent<ObjEnemy>().HelpJudge_ME(me, OpponentManeuver.EffectNum["Action"] - Power);

            //���e�l�Ȃ�
            if (6 > DiceRoll - Power)
                break;
        }

        //��񂿂��J��Ԃ����̖����ɖW�Q�d�˂Ă��炤�����C���J��
        return;
    }
    //�_���[�W�^�C�~���O�̏ꍇ
    public void EnemyAI_Damage(CharaManeuver OpponentManeuver, Doll_blu_Nor Opponent,bool ATorDF)
    {
        //������
        UseManever = null;

        //�S�������
        List<Doll_blu_Nor> PlayerDolls = ManagerAccessor.Instance.battleSystem.GetCharaObj();

        //�g�p����for��
        for (int ActManeuvers = 0; ActManeuvers != Maneuvers[(int)EnemyPartsType.Edamage].Count; ActManeuvers++)
        {
            //�j������&�g�p����
            if (!Maneuvers[(int)EnemyPartsType.Edamage][ActManeuvers].isDmage && !Maneuvers[(int)EnemyPartsType.Edamage][ActManeuvers].isDmage)
            {
                //�I���������̂��K�[�h�n�̋Z�Ȃ�
                if (Maneuvers[(int)EnemyPartsType.Edamage][ActManeuvers].EffectNum.ContainsKey(EffNum.Guard) && ATorDF)
                {
                    //�g�p����X�V
                    UseManever = UseManuber_Change((int)EnemyPartsType.Edamage, ActManeuvers, UseManever);

                }
                //�_���[�W�ǉ��n�̏ꍇ
                else if(Maneuvers[(int)EnemyPartsType.Edamage][ActManeuvers].EffectNum.ContainsKey(EffNum.Damage) && !ATorDF)
                {
                    //�g�p����X�V
                    UseManever = UseManuber_Change((int)EnemyPartsType.Edamage, ActManeuvers, UseManever);

                }

                
            }

        }

        //��L�Ń}�j���[�o�[������Α���
        if(UseManever!=null)
        ProcessAccessor.Instance.dmgTiming.ExeManeuver(UseManever, me);

        //�h��̃^�C�~���O�������Ăق����ꍇHELP����
        if (UseManever==null&&ATorDF)
        {
            //���g�ł͑Ή��s��
            Debug.Log("Enemy:Help!");
            return;
            //�S�����ɂ��΂��v��
            for (int i = 0; i != PlayerDolls.Count; i++)
            {
                //���̖����ɋ~���𑗂�
                PlayerDolls[i].GetComponent<ObjEnemy>().HelpDamege(me);

            }
        }
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

    //�ړ��⏕�ړIHELP(RAPID
    public void HelpMoveRapid(Doll_blu_Nor Follow, Doll_blu_Nor Opponent, int NeedRange)
    {
        for (int ActManeuvers = 0; ActManeuvers != Maneuvers[(int)EnemyPartsType.ERapid].Count; ActManeuvers++)
        {
            //�j������&�g�p����
            if (!Maneuvers[(int)EnemyPartsType.ERapid][ActManeuvers].isDmage && !Maneuvers[(int)EnemyPartsType.ERapid][ActManeuvers].isDmage)
            {
                //�G�̈ʒu���� & �˒���r(�G�𓮂���)
                if (Maneuvers[(int)EnemyPartsType.ERapid][ActManeuvers].MinRange != 10 &&
            (Mathf.Abs(Opponent.area) <= Mathf.Abs(Maneuvers[(int)EnemyPartsType.ERapid][ActManeuvers].MaxRange + me.area) &&
             Mathf.Abs(Opponent.area) >= Mathf.Abs(Maneuvers[(int)EnemyPartsType.ERapid][ActManeuvers].MinRange + me.area)))
                {
                    //�錾�i�ΏۂɁj+=�V����,-�͒n���֔�΂�
                    //return ����Ƀ��s�b�g�����ĉ������I�Ȋ֐��H�ɑ���i����,�����܂ɂ�,�Ώۖ����j
                }
                //�G�̈ʒu���� & �˒���r(�����𓮂���)
                else if (Maneuvers[(int)EnemyPartsType.ERapid][ActManeuvers].MinRange != 10 &&
            (Mathf.Abs(Follow.area) <= Mathf.Abs(Maneuvers[(int)EnemyPartsType.ERapid][ActManeuvers].MaxRange + me.area) &&
             Mathf.Abs(Follow.area) >= Mathf.Abs(Maneuvers[(int)EnemyPartsType.ERapid][ActManeuvers].MinRange + me.area)))
                {
                    //�錾�i�ΏۂɁj+=�V����,-�͒n���֔�΂�
                    //return ����Ƀ��s�b�g�����ĉ������I�Ȋ֐��H�ɑ���i����,�����܂ɂ�,�Ώۖ����j
                }
               
            }
        }

    }

    //�W�Q�ړIHELP(JUDGE
    public int HelpJudge_OP(Doll_blu_Nor Opponent,int NeedPower)
    {
        for (int ActManeuvers = 0; ActManeuvers != Maneuvers[(int)EnemyPartsType.EJudge].Count; ActManeuvers++)
        {
            //�j������&�g�p����
            if (!Maneuvers[(int)EnemyPartsType.EJudge][ActManeuvers].isDmage && !Maneuvers[(int)EnemyPartsType.EJudge][ActManeuvers].isDmage)
            {
                //�G�̈ʒu���� & �˒���r(�G�𓮂���)
                if (Maneuvers[(int)EnemyPartsType.EJudge][ActManeuvers].MinRange != 10 &&
            (Mathf.Abs(Opponent.area) <= Mathf.Abs(Maneuvers[(int)EnemyPartsType.EJudge][ActManeuvers].MaxRange + me.area) &&
             Mathf.Abs(Opponent.area) >= Mathf.Abs(Maneuvers[(int)EnemyPartsType.EJudge][ActManeuvers].MinRange + me.area)))
                {
                    //�W�Q�܂ɂ�΁[
                    //�錾�i�ΏۂɁj

                    //�W�Q�l����������ԂŎ󂯂Ă��鑤�̓z�ɂ�����x���f������B
                    return Maneuvers[(int)EnemyPartsType.EJudge][ActManeuvers].EffectNum["Judge"];
                }
            }
        }

        return 0;

    }
    //�x���ړIHELP(JUDGE
    public int HelpJudge_ME(Doll_blu_Nor Follow,int NeedPower)
    {
        for (int ActManeuvers = 0; ActManeuvers != Maneuvers[(int)EnemyPartsType.EJudge].Count; ActManeuvers++)
        {
            //�j������&�g�p����
            if (!Maneuvers[(int)EnemyPartsType.EJudge][ActManeuvers].isDmage && !Maneuvers[(int)EnemyPartsType.EJudge][ActManeuvers].isDmage)
            {
                //�x���܂ɂ�΁[
                //�錾�i�ΏۂɁj

                //�W�Q�l����������ԂŎ󂯂Ă��鑤�̓z�ɂ�����x���f������B
                return Maneuvers[(int)EnemyPartsType.EJudge][ActManeuvers].EffectNum["Judge"];
            }
        }

        return 0;

    }
    //�����݂����炢�����Ȃ�
    public void HelpDamege(Doll_blu_Nor Follow)
    {
        for (int ActManeuvers = 0; ActManeuvers != Maneuvers[(int)EnemyPartsType.ERapid].Count; ActManeuvers++)
        {
            //�j������&�g�p����
            if (!Maneuvers[(int)EnemyPartsType.ERapid][ActManeuvers].isDmage && !Maneuvers[(int)EnemyPartsType.ERapid][ActManeuvers].isDmage)
            {
                //�G�̈ʒu���� & �˒���r(�����𓮂���)-�Ȃ����̂Ƃ���u�݂��v���炢
                if (Maneuvers[(int)EnemyPartsType.ERapid][ActManeuvers].MinRange != 10 &&
            (Mathf.Abs(Follow.area) <= Mathf.Abs(Maneuvers[(int)EnemyPartsType.ERapid][ActManeuvers].MaxRange + me.area) &&
             Mathf.Abs(Follow.area) >= Mathf.Abs(Maneuvers[(int)EnemyPartsType.ERapid][ActManeuvers].MinRange + me.area)))
                {
                    //ROLL����
                    return;
                }
            }
        }
    }

    //���ʑI���ł���ꍇ
    //�����]�T����Ԃ���Ƃ���i���̂Ƃ���j
    public List<CharaManeuver> GetDamageUPList_ALL(int sonsyoukazu)
    {
        int num = 0;
        int tmp = 0;
        if (num < me.HeadParts.Count)
        {
            num = HEAD;
            tmp = me.HeadParts.Count;
        } 
        if (num < me.ArmParts.Count)
        {
            num = ARM;
            tmp = me.ArmParts.Count;
        }
        if (num < me.BodyParts.Count)
        {
            num = BODY;
            tmp = me.BodyParts.Count;
        }
        if (num < me.LegParts.Count)
        {
            num = LEG;
            tmp = me.LegParts.Count;
        }

        return GetDamageUPList(num,sonsyoukazu);
    }

    //�_���[�W���ʑI��{�֐�
    public List<CharaManeuver> GetDamageUPList(int TargetParts, int sonsyoukazu)
    {
        List<CharaManeuver> DamageParts = new List<CharaManeuver>();
        int[] Discarded_num = new int[sonsyoukazu];

        for (int i = 0; i != sonsyoukazu; i++)
            Discarded_num[i] = 0;

        if (TargetParts == HEAD)
            DiscardedManuber_omoto(Discarded_num, DamageParts, me.HeadParts);
        else if (TargetParts == ARM)
            DiscardedManuber_omoto(Discarded_num, DamageParts, me.ArmParts);
        else if (TargetParts == BODY)
            DiscardedManuber_omoto(Discarded_num, DamageParts, me.BodyParts);
        else if (TargetParts == LEG)
            DiscardedManuber_omoto(Discarded_num, DamageParts, me.LegParts);

        return DamageParts;
    }

    private CharaManeuver UseManuber_Change(int ActionType,int ActManuber,CharaManeuver NowManuver)
    {
        //�g�p����X�V
        //�������܂��ĂȂ��ꍇ
        if (NowManuver == null)
        {
            return Maneuvers[ActionType][ActManuber];
           
        }
        //�����āA���D��x�������ꍇ
        else if (NowManuver.EnemyAI[ActionType] < Maneuvers[ActionType][ActManuber].EnemyAI[ActionType])
            return Maneuvers[ActionType][ActManuber];

        //���ɂȂɂ��Ȃ��ꍇ
        return NowManuver;
    }

    //�_���[�W���ʊ֐���{�B
    private void DiscardedManuber_omoto(int[] aa, List<CharaManeuver> DamageList, List<CharaManeuver> SiteList)
    {
        //�K�{�p�����i����Ȃ��Ă��\�j
        //�D��x�p���x�������z����p�������
        for (int i = 0; i != SiteList.Count; i++)
        {
            //�I�l0,�������ė~������
            if (false)
            {
                DiscardedManuber_comparison(100, aa, SiteList[i], DamageList);

            }
            //�I�l1,�͂�킽�n��
            else if (me.HeadParts[i].EnemyAI == null)
            {
                DiscardedManuber_comparison(80, aa, SiteList[i], DamageList);

            }
            else if (me.HeadParts[i].isUse)
            {
                //�����D��x���Ⴂ��Ԃ���X�^�[�g
                DiscardedManuber_comparison(10 - SiteList[i].EnemyAI[4], aa, me.HeadParts[i], DamageList);

            }
            else if (!me.HeadParts[i].isUse)
            {
                //�����D��x���Ⴂ��Ԃ���X�^�[�g
                DiscardedManuber_comparison(2 - SiteList[i].EnemyAI[4], aa, me.HeadParts[i], DamageList);

            }
        }
    }

    //�_���[�W���ʊ֐��T�u�B
    private void DiscardedManuber_comparison(int a,int[] aa,CharaManeuver NowManuver,List<CharaManeuver> DamageList)
    {
        int Lownum = 99999;
        int Lownum2 = 0;
        //�p���D��x�ɂĔ��f
        for (int i = 0; i != aa.Length; i++)
        {
            //�ォ��}�j���[�o�[���Ȃ����Ƃ肠�����o�^
            if(aa[i]==0)
            {
                DamageList.Add(NowManuver);
                aa[i] = a;
                break;
            }
            //��₪�����̂��̂��D��x��������Γ���ւ�(List���̈�ԒႢ����I��)
            else if (Lownum > aa[i])
            {
                Lownum = aa[i];
                Lownum2 = i;
            }

            
        }

        //�Ђ�����Ƃ�������(�Ⴍ�Ȃ���΃X���[)
        //�����܂ł��遁�������܂œ�����������
        if (a > Lownum)
        {
            aa[Lownum2] = a;
            DamageList[Lownum2] = NowManuver;
        }
            return;
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
