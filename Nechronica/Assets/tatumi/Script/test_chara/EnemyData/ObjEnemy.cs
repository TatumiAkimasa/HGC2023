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
                        if (UseManever == null)
                        {
                            UseManever = Maneuvers[(int)EnemyPartsType.EAction][ActManeuvers];
                            //�D��x�X�V
                            //UseManever.EnemyAI[(int)EnemyPartsType.EAction] = 0;
                        }
                        else if (UseManever.EnemyAI[(int)EnemyPartsType.EAction] < Maneuvers[(int)EnemyPartsType.EAction][ActManeuvers].EnemyAI[(int)EnemyPartsType.EAction])
                            UseManever = Maneuvers[(int)EnemyPartsType.EAction][ActManeuvers];
                        target = PlayerDolls[i];

                    }
                }
            }
        }

        //�SACTION�ᖡ���A��ԗL���l�����������g�p����
        ProcessAccessor.Instance.actTiming.ExeAtkManeuver(target, UseManever, me);
        return;
    }
    //�ړ�����
    public void EnemyAI_Rapid(CharaManeuver OpponentManeuver, Doll_blu_Nor Opponent)
    {
        //�S�������
        List<Doll_blu_Nor> PlayerDolls = ManagerAccessor.Instance.battleSystem.GetCharaObj();

        int MaxOpponentRange = OpponentManeuver.MaxRange;
        int MinopponentRange = OpponentManeuver.MinRange;

        int differenceRange = 0;

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
                    //����̕����˒��ɂǂ����������ǂꂾ���]�T�����邩
                    //��:�_���Ă��镐��˒���2~0�ō��̏󋵂��˒��̐����ł����Ƃ����2,1,0�̂ǂꂩ���Z�o
                    differenceRange = Mathf.Abs(Opponent.area - me.area) - MaxOpponentRange;
                    if (Mathf.Abs(differenceRange) < Mathf.Abs(Mathf.Abs(Opponent.area - me.area) - MinopponentRange))
                        differenceRange = Mathf.Abs(Opponent.area - me.area) - MinopponentRange;

                    //���̎��_�Ő���=�˒���,+=�V����,-�n������������,0�̏ꍇ�͕ʓr
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
                            if (UseManever == null)
                            {
                                UseManever = Maneuvers[(int)EnemyPartsType.ERapid][ActManeuvers];
                                //�D��x�X�V
                                //UseManever.EnemyAI[(int)EnemyPartsType.EAction] = 0;
                            }
                            else if (UseManever.EnemyAI[(int)EnemyPartsType.ERapid] < Maneuvers[(int)EnemyPartsType.ERapid][ActManeuvers].EnemyAI[(int)EnemyPartsType.ERapid])
                                UseManever = Maneuvers[(int)EnemyPartsType.ERapid][ActManeuvers];

                            //����̎˒���0�̏ꍇ
                            if (Mathf.Abs(differenceRange) == 0)
                            {
                                if (4 >= Opponent.area + UseManever.EffectNum[EffNum.Move])
                                {
                                    //�Ƃ�ܒn�����ֈړ�
                                    Debug.Log("Enemy:�n���ɂƂ΂�");
                                    ProcessAccessor.Instance.rpdTiming.AddRapidManeuver(me, Opponent, UseManever);
                                    return;
                                }
                                else if(0 <= Opponent.area - UseManever.EffectNum[EffNum.Move])
                                {
                                    //�����Ȃ炵�Ԃ��Ԕ��΂�
                                    Debug.Log("Enemy:�V���ɂƂ΂�");
                                    ProcessAccessor.Instance.rpdTiming.AddRapidManeuver(me, Opponent, UseManever);
                                    return;
                                }

                            }
                            else
                            {
                                //�V�����ֈړ�(�ړ����ď�O�ɍs����������)
                                if (differenceRange > 0)
                                {
                                    if (0 <= Opponent.area - UseManever.EffectNum[EffNum.Move])
                                    {
                                        Debug.Log("Enemy:�V���ɂƂ΂�");
                                        ProcessAccessor.Instance.rpdTiming.AddRapidManeuver(me, Opponent, UseManever);
                                        return;
                                    }
                                }
                                //�n�����ֈړ�
                                else if (differenceRange < 0)
                                {
                                    if (4 >= Opponent.area + UseManever.EffectNum[EffNum.Move])
                                    {
                                        Debug.Log("Enemy:�n���ɂƂ΂�");
                                        ProcessAccessor.Instance.rpdTiming.AddRapidManeuver(me, Opponent, UseManever);
                                        return;
                                    }
                                }
                            }

                        }
                        //���g�ɋy�ڂ��ꍇ
                        else
                        {
                            //�g�p����X�V
                            if (UseManever == null)
                            {
                                UseManever = Maneuvers[(int)EnemyPartsType.ERapid][ActManeuvers];
                                //�D��x�X�V
                                //UseManever.EnemyAI[(int)EnemyPartsType.EAction] = 0;
                            }
                            else if (UseManever.EnemyAI[(int)EnemyPartsType.ERapid] < Maneuvers[(int)EnemyPartsType.ERapid][ActManeuvers].EnemyAI[(int)EnemyPartsType.ERapid])
                                UseManever = Maneuvers[(int)EnemyPartsType.ERapid][ActManeuvers];

                            //����̎˒���0�̏ꍇ
                            if (Mathf.Abs(differenceRange) == 0)
                            {
                                if (4 >= me.area + UseManever.EffectNum[EffNum.Move])
                                {
                                    //�Ƃ�ܒn�����ֈړ�
                                    Debug.Log("Enemy:�n���ɂƂ΂�");
                                    return;
                                }
                                else if (0 <= me.area - UseManever.EffectNum[EffNum.Move])
                                {
                                    //�����Ȃ炵�Ԃ��Ԕ��΂�
                                    Debug.Log("Enemy:�V���ɂƂ΂�");
                                    return;
                                }
                            }
                            else
                            {
                                //�V�����ֈړ�(�ړ����ď�O�ɍs����������)
                                if (differenceRange > 0)
                                {
                                    if (0 <= me.area - UseManever.EffectNum[EffNum.Move])
                                    {
                                        Debug.Log("Enemy:�V���ɂƂ΂�");
                                        return;
                                    }
                                }
                                //�n�����ֈړ�
                                else if (differenceRange < 0)
                                {
                                    if (4 >= me.area + UseManever.EffectNum[EffNum.Move])
                                    {
                                        Debug.Log("Enemy:�n���ɂƂ΂�");
                                        return;
                                    }
                                }
                            }
                        }
                    }
                    //���g�ł͑Ή��s��
                    else
                    {
                        Debug.Log("Enemy:Help!");
                        return;
                        //�S������
                        for (int i = 0; i != PlayerDolls.Count; i++)
                        {
                            //���̖����ɋ~���𑗂�
                            PlayerDolls[i].GetComponent<ObjEnemy>().HelpMoveRapid(me, Opponent,differenceRange);
                        }
                    }
                }
            }
            //���g�ł͑Ή��s��
            else
            {
                Debug.Log("Enemy:Help!");
                return;
                //�S������
                for (int i = 0; i != PlayerDolls.Count; i++)
                {
                    //���̖����ɋ~���𑗂�
                    PlayerDolls[i].GetComponent<ObjEnemy>().HelpMoveRapid(me, Opponent,differenceRange);
                }
            }

        }

        //�c�����񂪏����������Ȃ̂œc������ɕ����Ă�������
        Debug.Log("�Ȃ�ł����܂ł��ꂽ���...");
        return;
    }
    //�W�Q����
    public void EnemyAI_Judge(CharaManeuver OpponentManeuver, Doll_blu_Nor Opponent,int DiceRoll,int TargetParts,int Power)
    {
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
                //�]���Ȍv�ŕ֏�s�����񕜐�(Action�Ƃ��ŊǗ����邩���H)
                //if()
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

    //public List<CharaManeuver> GetDamageUPList(int TargetParts, int sonsyoukazu)
    //{
    //    List<CharaManeuver> DamageParts = new List<CharaManeuver>();
    //    int[] Discarded_num = new int[sonsyoukazu];

    //    for (int i = 0; i != sonsyoukazu; i++)
    //        Discarded_num = 0;

    //    int nowsonsyou = 0;
    //    if (TargetParts == HEAD)
    //    {
    //        //�K�{�p��
    //        for (int i = 0; i != me.HeadParts.Count; i++)
    //        {
    //            //�I�l0,�������ė~������
    //            if (i == 1200000000)
    //            {
    //                if (74)
    //                    DamageParts.Add(me.HeadParts[i]);
    //                Discarded_num[nowsonsyou] = 1500;
    //                nowsonsyou++;
    //            }
    //            //�I�l1,�͂�킽�n��
    //            else if (me.HeadParts[i].EnemyAI == null)
    //            {
    //                DamageParts.Add(me.HeadParts[i]);
    //                Discarded_num[nowsonsyou] = 1000;
    //                nowsonsyou++;
    //            }

    //        }

    //        //���������p��
    //        for (int i = 0; i != me.HeadParts.Count; i++)
    //        {
    //            //�I�l2-1,�g�p�ς݂�
    //            //�I�l2-2,�g�p�ς݂Ȃ�p�����Ғl����
    //            //�I�l3,���g�p�Ō��ʊ��Ғl���Ⴂ��
    //        }
    //    }

    //    else if (TargetParts == ARM)
    //        Maneuvers[(int)EnemyPartsType.ERapid].Add(me.LegParts[i]);
    //    else if (TargetParts == BODY)
    //        Maneuvers[(int)EnemyPartsType.EJudge].Add(me.LegParts[i]);
    //    else if (TargetParts == LEG)
    //        Maneuvers[(int)EnemyPartsType.Edamage].Add(me.LegParts[i]);
    //}

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

    private bool DiscardedManuber_comparison(int a,int[] aa)
    {
        //�K�{�p��
        for (int i = 0; i != aa.Length; i++)
        {
            //�ォ��}�j���[�o�[���Ȃ����Ƃ肠�����o�^
            if(aa[i]==0)
                return true;
            //��₪�����̂��̂��D��x��������Γ���ւ�
            else if (aa[i] < a)
                return true;
        }
        return false;
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
