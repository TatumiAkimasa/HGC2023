using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chara_data_input : CharaBase
{
    const int HEAD = 0;
    const int ARM = 1;
    const int BODY = 2;
    const int LEG = 3;

    private int Treasure_num;
    public string name_;                    //�h�[���� 
    public string death_year_;              //���N

    public Wepon_Maneger WE_Maneger;
    public SkillManeger SK_Maneger;
    public Doll_blueprint DOLL_Maneger;

    public CharaManeuver Potition_Skill;
    [SerializeField]
    private CharaManeuver backTreasure, Treasure;

    [System.NonSerialized]
    public string temper_name;
    [System.NonSerialized]
    public short position;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Skill_Reset()
    {
        DOLL_Maneger.Skill.Remove(Potition_Skill);
        Potition_Skill = null;
    }

    public void input()
    {
        //������
        DOLL_Maneger.HeadParts.Clear();
        DOLL_Maneger.ArmParts.Clear();
        DOLL_Maneger.BodyParts.Clear();
        DOLL_Maneger.LegParts.Clear();

        //���������ǋL

        //�ǉ������ǋL
        for (int SITE = 0; SITE != 4; SITE++)
        {
            for (int i = 0; i != 2; i++)
            {
                for (int k = 0; k != 3; k++)
                {
                    if (WE_Maneger.Site_[SITE].Step[i].Text[k] != null)
                    {
                        if (SITE == HEAD)
                        {
                            DOLL_Maneger.HeadParts.Add(WE_Maneger.Site_[SITE].Step[i].Text[k].GetComponent<Wepon_Data_SaveSet>().GetParts());
                        }
                        if (SITE == ARM)
                        {
                            DOLL_Maneger.ArmParts.Add(WE_Maneger.Site_[SITE].Step[i].Text[k].GetComponent<Wepon_Data_SaveSet>().GetParts());
                        }
                        if (SITE == BODY)
                        {
                            DOLL_Maneger.BodyParts.Add(WE_Maneger.Site_[SITE].Step[i].Text[k].GetComponent<Wepon_Data_SaveSet>().GetParts());
                        }
                        if (SITE == LEG)
                        {
                            DOLL_Maneger.LegParts.Add(WE_Maneger.Site_[SITE].Step[i].Text[k].GetComponent<Wepon_Data_SaveSet>().GetParts());
                        }
                    }

                }
            }
        }

        //�N���X�i���C���A�T�u�j
        DOLL_Maneger.MainClass = SK_Maneger.GetKeyWord_main();
        DOLL_Maneger.SubClass = SK_Maneger.GetKeyWord_sub();

        //���ꂼ��̕��탌�x���ݒ�
        DOLL_Maneger.Armament = (short)SK_Maneger.GetArmament();
        DOLL_Maneger.Variant = (short)SK_Maneger.GetVariantt();
        DOLL_Maneger.Alter= (short)SK_Maneger.GetAlter();

        //�����ʒu�A���O���̂��̑��ݒ�
        DOLL_Maneger.temper = temper_name;
        DOLL_Maneger.Death_year = death_year_;
        DOLL_Maneger.Name = name_;

        //position�X�L���݂̂�����Őݒ�
        DOLL_Maneger.Skill.Add(Potition_Skill);

    }

    //�󕨓��͊֐�
    public void SetTreasure(string name, int i)
    {
        switch (Treasure_num)
        {
            case HEAD:
                DOLL_Maneger.HeadParts.Remove(backTreasure);
                break;
            case ARM:
                DOLL_Maneger.ArmParts.Remove(backTreasure);
                break;
            case BODY:
                DOLL_Maneger.BodyParts.Remove(backTreasure);
                break;
            case LEG:
                DOLL_Maneger.LegParts.Remove(backTreasure);
                break;
            //�����ݒ�
            case -1:
                break;
        }


        Treasure.Name = name;

        //�Ή��ꏊ�ɕt�^
        switch (i)
        {
            case HEAD:
                DOLL_Maneger.HeadParts.Add(Treasure);
                break;
            case ARM:
                DOLL_Maneger.ArmParts.Add(Treasure);
                break;
            case BODY:
                DOLL_Maneger.BodyParts.Add(Treasure);
                break;
            case LEG:
                DOLL_Maneger.LegParts.Add(Treasure);
                break;
        }

        backTreasure = Treasure;
        Treasure_num = i;
    }
}
