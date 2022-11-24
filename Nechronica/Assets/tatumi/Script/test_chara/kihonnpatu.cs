using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kihonnpatu : CharaBase
{
    //��b�p�[�c
    public CharaManeuver[] Base_Head_parts;
    public CharaManeuver[] Base_Arm_parts;
    public CharaManeuver[] Base_Body_parts;
    public CharaManeuver[] Base_Leg_parts;
    public CharaManeuver Treasure_parts;

    protected const int MAX_BASE_PARTS = 3;

    public int GET_MAX_BASE_PARTS() { return MAX_BASE_PARTS; }

    //�C���X�y�N�^�[�ォ��M����NullObj����B
    //void Awake()
    //{
    //    Base_Head_parts = new CharaManeuver[MAX_BASE_PARTS];
    //    Base_Arm_parts = new CharaManeuver[MAX_BASE_PARTS];
    //    Base_Body_parts = new CharaManeuver[MAX_BASE_PARTS];
    //    Base_Leg_parts = new CharaManeuver[MAX_BASE_PARTS];
    //}

    // Start is called before the first frame update
    void Start()
    {
        //NAME-------------------
        Base_Head_parts[0].Name = "����";
        Base_Head_parts[1].Name = "�̂��݂�";
        Base_Head_parts[2].Name = "�߂���";
        Base_Arm_parts[0].Name = "���Ԃ�";
        Base_Arm_parts[1].Name = "����";
        Base_Arm_parts[2].Name = "����";
        Base_Body_parts[0].Name = "���ڂ�";
        Base_Body_parts[1].Name = "�͂�킽";
        Base_Body_parts[2].Name = "�͂�킽";
        Base_Leg_parts[0].Name = "�ق�";
        Base_Leg_parts[1].Name = "�ق�";
        Base_Leg_parts[2].Name = "����";
        Treasure_parts.Name = "";

        //�_���[�W�l----------------------
        Base_Head_parts[0].EffectNum.Add("���e�U��1",1);
        Base_Head_parts[1].EffectNum.Add("�ő�s���l1", 2);
        Base_Head_parts[2].EffectNum.Add("�ő�s���l1", 1);
        Base_Arm_parts[0].EffectNum.Add("���e�U��1", 1);
        Base_Arm_parts[1].EffectNum.Add("�ړ�1", 1);
        Base_Arm_parts[2].EffectNum.Add("�x��1", 1);
        Base_Body_parts[0].EffectNum.Add("Cost-1", -1);
        Base_Body_parts[1].EffectNum.Add("", 0);
        Base_Body_parts[2].EffectNum.Add("", 0);
        Base_Leg_parts[0].EffectNum.Add("�ړ�1", 1);
        Base_Leg_parts[1].EffectNum.Add("�ړ�1", 1);
        Base_Leg_parts[2].EffectNum.Add("�W�Q1", 1);
        Treasure_parts.EffectNum.Add("��", 0);

        //COST-------------
        Base_Head_parts[0].Cost = 2;
        Base_Arm_parts[0].Cost = 2;
        Base_Arm_parts[2].Cost = 1;
        Base_Arm_parts[1].Cost = 4;
        Base_Body_parts[0].Cost = 2;
        Base_Leg_parts[0].Cost = 3;
        Base_Leg_parts[1].Cost = 3;
        //AUTO
        Base_Head_parts[1].Cost = 0;
        Base_Head_parts[2].Cost = 0;
        Base_Body_parts[1].Cost = 0;
        Base_Body_parts[2].Cost = 0;
        Base_Leg_parts[2].Cost = 0;
        Treasure_parts.Cost = 0;

        //TIMING------------------^p^
        //0=�I�[�g,1=�A�N�V����,2=���s�b�h,3=�W���b�W,4=�_���[�W(�������ł킯��)
        Base_Head_parts[0].Timing = 1;
        Base_Head_parts[1].Timing = 0;
        Base_Head_parts[2].Timing = 0;
        Base_Arm_parts[0].Timing = 1;
        Base_Arm_parts[1].Timing = 1;
        Base_Arm_parts[2].Timing = 3;
        Base_Body_parts[0].Timing = 1;
        Base_Body_parts[1].Timing = 0;
        Base_Body_parts[2].Timing = 0;
        Base_Leg_parts[0].Timing = 1;
        Base_Leg_parts[1].Timing = 1;
        Base_Leg_parts[2].Timing = 3;
        Treasure_parts.Timing = 0;

        //�U���͈�-------------------------------
        //�ŏ�(10=���g)
        Base_Head_parts[0].MinRange = 0;
        Base_Head_parts[1].MinRange = 10;
        Base_Head_parts[2].MinRange = 10;
        Base_Arm_parts[0].MinRange = 0;
        Base_Arm_parts[1].MinRange = 10;
        Base_Arm_parts[2].MinRange = 0;
        Base_Body_parts[0].MinRange = 0;
        Base_Body_parts[1].MinRange = 10;
        Base_Body_parts[2].MinRange = 10;
        Base_Leg_parts[0].MinRange = 10;
        Base_Leg_parts[1].MinRange = 10;
        Base_Leg_parts[2].MinRange = 0;
        Treasure_parts.MinRange = 10;
        //�ōő�(10=���g)
        Base_Head_parts[0].MaxRange = 0;
        Base_Head_parts[1].MaxRange = 10;
        Base_Head_parts[2].MaxRange = 10;
        Base_Arm_parts[0].MaxRange = 0;
        Base_Arm_parts[1].MaxRange = 10;
        Base_Arm_parts[2].MaxRange = 0;
        Base_Body_parts[0].MaxRange = 0;
        Base_Body_parts[1].MaxRange = 10;
        Base_Body_parts[2].MaxRange = 10;
        Base_Leg_parts[0].MaxRange = 10;
        Base_Leg_parts[1].MaxRange = 10;
        Base_Leg_parts[2].MaxRange = 0;
        Treasure_parts.MaxRange = 10;

        //�d��------------------------------------
        //(��b�͍��̂Ƃ���1�ŌŒ�)
        Base_Head_parts[0].Weight = 1;
        Base_Head_parts[1].Weight = 1;
        Base_Head_parts[2].Weight = 1;
        Base_Arm_parts[0].Weight = 1;
        Base_Arm_parts[1].Weight = 1;
        Base_Arm_parts[2].Weight = 1;
        Base_Body_parts[0].Weight = 1;
        Base_Body_parts[1].Weight = 1;
        Base_Body_parts[2].Weight = 1;
        Base_Leg_parts[0].Weight = 1;
        Base_Leg_parts[1].Weight = 1;
        Base_Leg_parts[2].Weight = 1;
        Treasure_parts.Weight = 1;
    }


}
