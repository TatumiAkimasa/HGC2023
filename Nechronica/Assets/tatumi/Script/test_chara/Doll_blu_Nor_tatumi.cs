using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doll_blu_Nor_tatumi : kihonnpatu
{
    public string Name="test";                    //�h�[���� 
    public string hide_hint="test";              //�Î�
    public string Death_year="10";              //���N
    public string temper="�A���X";                  //�|�W�V����
    public short[] Memory= {1,2 };                 //�L���̂�����
                                           //---------------------------------------------------������������
    public short potition=3;                 //�����z�u(����)
    public string MainClass="Stacy", SubClass="Stacy";     //�E��(skill)
    public short Armament=0, Variant=0, Alter=0; //����,�ψ�,����(Skill)
    public List<CharaManeuver> Skll;              //�X�L��
    public CharaBase parts;                       //�p�[�c��

    private void Start()
    {
        //���������ǋL
        for (int i = 0; i != MAX_BASE_PARTS; i++)
        {
            //��
            parts.HeadParts.Add(Base_Head_parts[i]);
            //�r
            parts.ArmParts.Add(Base_Arm_parts[i]);
            //��
            parts.BodyParts.Add(Base_Body_parts[i]);
            //�r
            parts.LegParts.Add(Base_Leg_parts[i]);
        }
    }
}
