using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doll_blu_Nor : kihonnpatu
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
        //��----------------------
        parts.HeadParts.Add(noumiso_H);
        parts.HeadParts.Add(medama_H);
        parts.HeadParts.Add(ago_H);

        //�r----------------------
        parts.ArmParts.Add(ude_A);
        parts.ArmParts.Add(kata_A);
        parts.ArmParts.Add(kobusi_A);

        //��----------------------
        parts.BodyParts.Add(harawata2_B);
        parts.BodyParts.Add(harawata_B);
        parts.BodyParts.Add(sebone_B);

        //�r----------------------
        parts.LegParts.Add(hone2_L);
        parts.LegParts.Add(hone_L);
        parts.LegParts.Add(asi_L);
    }
}
