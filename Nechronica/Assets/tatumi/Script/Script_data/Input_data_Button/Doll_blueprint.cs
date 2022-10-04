using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharaManeuver))]
[System.Serializable]
public class Doll_blueprint : CharaBase
{
    public string Name;                    //�h�[���� 
    public string hide_hint;              //�Î�
    public string Death_year;              //���N
    public string temper;                  //�|�W�V����
    public short[] Memory;                 //�L���̂�����
    public string MainClass, SubClass;     //�E��
    public short Armament, Variant, Alter; //����,�ψ�,����
    public CharaBase parts;                       //�p�[�c��
    public string potition;                 //�����z�u
    //---------------------------------------------------������������
  
    public List<CharaManeuver> Skll;              //�X�L��
  

    private int Treasure_num;

    private CharaManeuver Treasure=new CharaManeuver { };
    private CharaManeuver backTreasure = new CharaManeuver { };

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
                parts.HeadParts.Remove(backTreasure);
                break;
            case 2:
                parts.ArmParts.Remove(backTreasure);
                break;
            case 3:
                parts.BodyParts.Remove(backTreasure);
                break;
            case 4:
                parts.LegParts.Remove(backTreasure);
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

        backTreasure = Treasure;
        Treasure_num = i;
    }

    
}


