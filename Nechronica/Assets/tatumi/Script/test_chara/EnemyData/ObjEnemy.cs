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

    private void Start()
    {
        //���ʁ~4+Skill1
        Maneuvers = new List<CharaManeuver>[(int)EnemyPartsType.EPartsMax];
        for (int i = 0; i != Maneuvers.Length; i++)
            Maneuvers[i] = new List<CharaManeuver>();

        //���������ǋL
        for (int i = 0; i != 3; i++)
        {
           
            //me.HeadParts.Add(kihon.Base_Head_parts[i]);

            Maneuvers[(int)EnemyPartsType.EAction].Add(me.HeadParts[i]);

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

        EnemyAI_Rapid(opponentManeuver, opponent);
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
                    }
                }
            }
        }

        //�c�����񂪏����������Ȃ̂œc������ɕ����Ă�������
        return UseManever;
    }

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
                if (Maneuvers[(int)EnemyPartsType.ERapid][ActManeuvers].Moving == 0)
                {

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
                    if (Maneuvers[(int)EnemyPartsType.ERapid][ActManeuvers].Moving > Mathf.Abs(differenceRange))
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
                                if (4 >= Opponent.area + UseManever.Moving)
                                {
                                    //�Ƃ�ܒn�����ֈړ�
                                    Debug.Log("Enemy:�n���ɂƂ΂�");
                                    return;
                                }
                                else if(0 <= Opponent.area - UseManever.Moving)
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
                                    if (0 <= Opponent.area - UseManever.Moving)
                                    {
                                        Debug.Log("Enemy:�V���ɂƂ΂�");
                                        return;
                                    }
                                }
                                //�n�����ֈړ�
                                else if (differenceRange < 0)
                                {
                                    if (4 >= Opponent.area + UseManever.Moving)
                                    {
                                        Debug.Log("Enemy:�n���ɂƂ΂�");
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
                                if (4 >= me.area + UseManever.Moving)
                                {
                                    //�Ƃ�ܒn�����ֈړ�
                                    Debug.Log("Enemy:�n���ɂƂ΂�");
                                    return;
                                }
                                else if (0 <= me.area - UseManever.Moving)
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
                                    if (0 <= me.area - UseManever.Moving)
                                    {
                                        Debug.Log("Enemy:�V���ɂƂ΂�");
                                        return;
                                    }
                                }
                                //�n�����ֈړ�
                                else if (differenceRange < 0)
                                {
                                    if (4 >= me.area + UseManever.Moving)
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
                            PlayerDolls[i].GetComponent<ObjEnemy>().HelpMoveRapid(me, differenceRange);
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
                    PlayerDolls[i].GetComponent<ObjEnemy>().HelpMoveRapid(me, differenceRange);
                }
            }

        }

        //�c�����񂪏����������Ȃ̂œc������ɕ����Ă�������
        Debug.Log("�Ȃ�ł����܂ł��ꂽ���...");
        return;
    }

    public void EnemyAI_Judge(CharaManeuver OpponentManeuver, Doll_blu_Nor Opponent)
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
                if (Maneuvers[(int)EnemyPartsType.ERapid][ActManeuvers].Moving == 0)
                {

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
                    if (Maneuvers[(int)EnemyPartsType.ERapid][ActManeuvers].Moving > Mathf.Abs(differenceRange))
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
                                if (4 >= Opponent.area + UseManever.Moving)
                                {
                                    //�Ƃ�ܒn�����ֈړ�
                                    Debug.Log("Enemy:�n���ɂƂ΂�");
                                    return;
                                }
                                else if (0 <= Opponent.area - UseManever.Moving)
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
                                    if (0 <= Opponent.area - UseManever.Moving)
                                    {
                                        Debug.Log("Enemy:�V���ɂƂ΂�");
                                        return;
                                    }
                                }
                                //�n�����ֈړ�
                                else if (differenceRange < 0)
                                {
                                    if (4 >= Opponent.area + UseManever.Moving)
                                    {
                                        Debug.Log("Enemy:�n���ɂƂ΂�");
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
                                if (4 >= me.area + UseManever.Moving)
                                {
                                    //�Ƃ�ܒn�����ֈړ�
                                    Debug.Log("Enemy:�n���ɂƂ΂�");
                                    return;
                                }
                                else if (0 <= me.area - UseManever.Moving)
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
                                    if (0 <= me.area - UseManever.Moving)
                                    {
                                        Debug.Log("Enemy:�V���ɂƂ΂�");
                                        return;
                                    }
                                }
                                //�n�����ֈړ�
                                else if (differenceRange < 0)
                                {
                                    if (4 >= me.area + UseManever.Moving)
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
                            PlayerDolls[i].GetComponent<ObjEnemy>().HelpMoveRapid(me, differenceRange);
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
                    PlayerDolls[i].GetComponent<ObjEnemy>().HelpMoveRapid(me, differenceRange);
                }
            }

        }

        //�c�����񂪏����������Ȃ̂œc������ɕ����Ă�������
        Debug.Log("�Ȃ�ł����܂ł��ꂽ���...");
        return;
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

    public void HelpMoveRapid(Doll_blu_Nor Follow, int NeedRange)
    {
        for (int ActManeuvers = 0; ActManeuvers != Maneuvers[(int)EnemyPartsType.ERapid].Count; ActManeuvers++)
        {
            //�j������&�g�p����
            if (!Maneuvers[(int)EnemyPartsType.ERapid][ActManeuvers].isDmage && !Maneuvers[(int)EnemyPartsType.ERapid][ActManeuvers].isDmage)
            {
                //�ړ��܂ɂ�΁[�I�o�i�o��������)
                if (Maneuvers[(int)EnemyPartsType.ERapid][ActManeuvers].Moving > NeedRange)
                {
                    //�錾�i�ΏۂɁj
                    //return ����Ƀ��s�b�g�����ĉ������I�Ȋ֐��H�ɑ���i����,�����܂ɂ�,�Ώۖ����j
                }
                //�]���Ȍv�ŕ֏�s�����񕜐�(Action�Ƃ��ŊǗ����邩���H)
                //if()
            }
        }

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
