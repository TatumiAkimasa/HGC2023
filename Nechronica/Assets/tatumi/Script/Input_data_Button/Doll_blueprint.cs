using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doll_blueprint : CharaBase
{
    public string Name;                    //�h�[����
    public string hide_hint;              //���N
    public string Death_year;              //���N
    public short potition;                 //�����z�u
    public string temper;                  //�|�W�V����
    public string MainClass, SubClass;     //�E��
    public short Armament, Variant, Alter; //����,�ψ�,����
    public short[] Memory;                 //�L���̂�����
    public List<CharaManeuver> Skll;              //�X�L��
    public CharaBase parts;                       //�p�[�c��

    private int Treasure_num;

    private CharaManeuver Treasure,backTrasure;

    private void Start()
    {
        //�󏉊������Ă�
        Treasure.MaxRange = 0;
        Treasure.MinRange = 0;
        Treasure.Timing = -1;
        Treasure.Weight = 1;
        Treasure.EffectNum = -1;
        Treasure.Cost = -1;
        Treasure.Atk = null;
        Treasure.isDmage = false;
        Treasure.isUse = false;
    }

    //�󕨓��͊֐�
    public void SetTreasure(string name,int i)
    {
        switch (Treasure_num)
        {
            case 1:
                parts.HeadParts.Remove(Treasure);
                break;
            case 2:
                parts.ArmParts.Remove(Treasure);
                break;
            case 3:
                parts.BodyParts.Remove(Treasure);
                break;
            case 4:
                parts.LegParts.Remove(Treasure);
                break;
                //�����ݒ�
            case -1:
                break;
        }


        Treasure.Name = name;

        //�Ή��ꏊ�ɕt�^
        switch (i)
        {
            case 1:
                parts.HeadParts.Add(Treasure);
                break;
            case 2:
                parts.ArmParts.Add(Treasure);
                break;
            case 3:
                parts.BodyParts.Add(Treasure);
                break;
            case 4:
                parts.LegParts.Add(Treasure);
                break;
        }

        backTrasure = Treasure;
        Treasure_num = i;
    }
}


