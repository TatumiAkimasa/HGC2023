using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Chara_data_input : CharaBase
{
    //�Œ艻
    const int HEAD = 0;
    const int ARM = 1;
    const int BODY = 2;
    const int LEG = 3;

    private enum ErrorStr
    {
        NameError = 0,
        ClassError,
        PartsError,
        SkillError,
        Potition_SkillError,
        PotitionError,
        MAX,
    }


    //��b�f�[�^�󂯎���
    [SerializeField]
    private kihonnpatu ALL_Base_Parts;

    //��Ǘ��p�ϐ�
    private int Treasure_num;
    [SerializeField]
    private CharaManeuver backTreasure, Treasure;

    //�L�����f�[�^
    private string temper_name;            //���O
    private short position_;               //�����ʒu
    private string hide_hint_;             //�Î�
    private string name_;                  //�h�[���� 
    private string death_year_;            //���N
    private CharaManeuver Potition_Skill;  //�|�W�X�L��
    //�Q�b�^�[�̂ݎg�p
    public string SetTemper_name(string value) => temper_name = value;
    public short SetPosition_(short value) => position_ = value;
    public string SetHide_hint_(string value) => hide_hint_ = value;
    public string SetName_(string value) => name_ = value;
    public string SetDeath_year_(string value) => death_year_ = value;
    public CharaManeuver SetPotionSkill_(CharaManeuver value) => Potition_Skill = value;
    //�����ꂼ���SC���璼�ڂ��炤
    //�����̎q�݂̂��������ŏ��󂯎��
    public short[] Memory_ = new short[6];   //�L���̂�����


    //Chara�f�[�^�܂Ƃߐ�
    public Doll_blueprint Doll_data;

    //�f�[�^�G���[
    [SerializeField]
    private Text[] ErrorText;
    public void ResetErrorText()
    {
        for (int i = 0; i != ErrorText.Length; i++)
            ErrorText[i].text = "";
    }
    private bool[] ErrorData;
    public void SetErrorData(int Errornumber,bool value) { ErrorData[Errornumber] = value; }

    // Start is called before the first frame update
    void Awake()
    {
        //�Q�Ɠn���ł�����������iCopy�����܂������Ȃ�...�j
        Doll_data.Memory = Memory_;
        Treasure = ALL_Base_Parts.Treasure_parts;

        Maneger_Accessor.Instance.chara_Data_Input_cs = this;
        ErrorData = new bool[(int)ErrorStr.MAX];
        for (int i = 0; i != ErrorData.Length; i++)
        {
            ErrorData[i] = true;
        }
    }

    private void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Skill_Reset()
    {
        Doll_data.CharaBase_data.Skill.Remove(Potition_Skill);
        Potition_Skill = null;
    }

    public bool input()
    {
        //�}�l�[�W���[Set
        SkillManeger SK_Maneger = Maneger_Accessor.Instance.skillManeger_cs;

        SK_Maneger.ErrorCheck_CLASS();
        SK_Maneger.ErrorCheck_Skill();
        //�G���[�F������------------------------------------------------------------------------
        int Textnum = 0;

        for(int i=0;i!= ErrorData.Length; i++)
        {
            if (Textnum == ErrorText.Length - 1)
            {
                ErrorText[Textnum].text = "...etc";
                break;
            }
            else if (ErrorData[i])
            {
                switch (i)
                {
                    case (int)ErrorStr.NameError:
                        ErrorText[Textnum].text = "���O���ݒ肳��Ă��܂���";
                        Textnum++;
                        break;
                    case (int)ErrorStr.ClassError:
                        ErrorText[Textnum].text = "�E�Ƃ��ݒ肳��Ă��܂���";
                        Textnum++;
                        break;
                    case (int)ErrorStr.SkillError:
                        ErrorText[Textnum].text = "�E�ƃX�L�����ݒ肳��Ă��܂���";
                        Textnum++;
                        break;
                    case (int)ErrorStr.PartsError:
                        ErrorText[Textnum].text = "�p�[�c������Ă��܂���";
                        Textnum++;
                        break;
                    case (int)ErrorStr.Potition_SkillError:
                        ErrorText[Textnum].text = "�|�W�V�����X�L�����ݒ肳��Ă��܂���";
                        Textnum++;
                        break;
                    case (int)ErrorStr.PotitionError:
                        ErrorText[Textnum].text = "�����ʒu���ݒ肳��Ă��܂���";
                        Textnum++;
                        break;
                }
            }
        }

        if (Textnum != 0)
            return false;
        else
            ResetErrorText();
         //----------------------------------------------------------------------------------------

         //�}�l�[�W���[Set
         Wepon_Maneger WE_Maneger = Maneger_Accessor.Instance.weponManeger_cs;

        //�f�[�^�Z�b�g
        //������
        Doll_data.CharaBase_data.HeadParts.Clear();
        Doll_data.CharaBase_data.ArmParts.Clear();
        Doll_data.CharaBase_data.BodyParts.Clear();
        Doll_data.CharaBase_data.LegParts.Clear();

        //���������ǋL
        for (int i = 0; i != ALL_Base_Parts.GET_MAX_BASE_PARTS(); i++)
        {
            Doll_data.CharaBase_data.HeadParts.Add(ALL_Base_Parts.Base_Head_parts[i]);

            Doll_data.CharaBase_data.ArmParts.Add(ALL_Base_Parts.Base_Arm_parts[i]);

            Doll_data.CharaBase_data.BodyParts.Add(ALL_Base_Parts.Base_Body_parts[i]);

            Doll_data.CharaBase_data.LegParts.Add(ALL_Base_Parts.Base_Leg_parts[i]);
        }

        //�ǉ������ǋL
        for (int SITE = 0; SITE != 4; SITE++)
        {
            for (int i = 0; i != 2; i++)
            {
                for (int k = 0; k != 3; k++)
                {
                    //None���𔲂��ɂ��Đ���
                    if (WE_Maneger.Site_[SITE].Step[i].Text[k].GetComponent<Wepon_Data_SaveSet>().GetName()!="None")
                    {
                        if (SITE == HEAD)
                        {
                            Doll_data.CharaBase_data.HeadParts.Add(WE_Maneger.Site_[SITE].Step[i].Text[k].GetComponent<Wepon_Data_SaveSet>().GetParts());
                        }
                        if (SITE == ARM)
                        {
                            Doll_data.CharaBase_data.ArmParts.Add(WE_Maneger.Site_[SITE].Step[i].Text[k].GetComponent<Wepon_Data_SaveSet>().GetParts());
                        }
                        if (SITE == BODY)
                        {
                            Doll_data.CharaBase_data.BodyParts.Add(WE_Maneger.Site_[SITE].Step[i].Text[k].GetComponent<Wepon_Data_SaveSet>().GetParts());
                        }
                        if (SITE == LEG)
                        {
                            Doll_data.CharaBase_data.LegParts.Add(WE_Maneger.Site_[SITE].Step[i].Text[k].GetComponent<Wepon_Data_SaveSet>().GetParts());
                        }
                    }

                }
            }
        }

        //�����Ή��ꏊ�ɕt�^(�ꏊ�̓��X�g�Œ�)
        switch (Treasure_num)
        {
            case HEAD:
                Doll_data.CharaBase_data.HeadParts.Add(Treasure);
                break;
            case ARM:
                Doll_data.CharaBase_data.ArmParts.Add(Treasure);
                break;
            case BODY:
                Doll_data.CharaBase_data.BodyParts.Add(Treasure);
                break;
            case LEG:
                Doll_data.CharaBase_data.LegParts.Add(Treasure);
                break;
        }


        //�N���X�i���C���A�T�u�j
        Doll_data.MainClass = SK_Maneger.GetKeyWord_main();
        Doll_data.SubClass = SK_Maneger.GetKeyWord_sub();

        //���ꂼ��̕��탌�x���ݒ�
        Doll_data.Armament = (short)SK_Maneger.GetArmament();
        Doll_data.Variant = (short)SK_Maneger.GetVariantt();
        Doll_data.Alter= (short)SK_Maneger.GetAlter();

        //�����ʒu�A���O���̂��̑��ݒ�
        Doll_data.temper = temper_name;
        Doll_data.Death_year = death_year_;
        Doll_data.Name = name_;
        Doll_data.potition = position_;
        Doll_data.hide_hint = hide_hint_;
        

        //position�X�L���݂̂�����Őݒ�
        Doll_data.CharaBase_data.Skill.Add(Potition_Skill);

        Doll_data.CharaField_data.Event[0].str = "�X�̑q�ɂ̃J�M���g����q�ɂ֌���";
        Doll_data.CharaField_data.Event[1].str = "�L�����Ǎ��̃h�[���֓n��";

        return true;
    }

    //�󕨓��͊֐�
    public void SetTreasure(string name, int i)
    {
        switch (Treasure_num)
        {
            case HEAD:
                Doll_data.CharaBase_data.HeadParts.Remove(backTreasure);
                break;
            case ARM:
                Doll_data.CharaBase_data.ArmParts.Remove(backTreasure);
                break;
            case BODY:
                Doll_data.CharaBase_data.BodyParts.Remove(backTreasure);
                break;
            case LEG:
                Doll_data.CharaBase_data.LegParts.Remove(backTreasure);
                break;
            //�����ݒ�
            case -1:
                break;
        }


        Treasure.Name = name;

        backTreasure = Treasure;
        Treasure_num = i;
    }

}
