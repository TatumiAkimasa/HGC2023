using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doll_blu_Nor : PartsList
{
    public string Name="test";                    //�h�[���� 
    public string hide_hint="test";              //�Î�
    public string Death_year="10";              //���N
    public string temper="�A���X";                  //�|�W�V����
    public short[] Memory= {1,2 };                  //�L���̂�����

    public string GetName() => Name;

    

                                           //---------------------------------------------------������������
    public short position=3;                 //�����z�u(����)
    public string MainClass="Stacy", SubClass="Stacy";     //�E��(skill)
    public short Armament=0, Variant=0, Alter=0; //����,�ψ�,����(Skill)
    public List<CharaManeuver> Skill;              //�X�L��

    private void Awake()
    {
        InitParts();
        //��----------------------
        HeadParts.Add(noumiso_H);
        HeadParts.Add(medama_H);
        HeadParts.Add(ago_H);

        //�r----------------------
        ArmParts.Add(ude_A);
        ArmParts.Add(kata_A);
        ArmParts.Add(kobusi_A);

        //��----------------------
        BodyParts.Add(harawata2_B);
        BodyParts.Add(harawata_B);
        BodyParts.Add(sebone_B);

        //�r----------------------
        LegParts.Add(hone2_L);
        LegParts.Add(hone_L);
        LegParts.Add(asi_L);

        MaxCountCal();
        Debug.Log(maxCount);
    }
}
