using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doll_blu_Nor : PartsList
{
    //�������[
    public string GetName() => Name;

    
    public string Name;                    //�h�[���� 
    public string hide_hint="test";              //�Î�
    public string Death_year="10";              //���N
    public string temper="�A���X";                  //�|�W�V����
    public short[] Memory= {1,2 };                  //�L���̂�����
    public int area;                 //���݈ʒu
    public int initArea = 2;                 //�����z�u(����)
    public string MainClass = "Stacy", SubClass = "Stacy";     //�E��(skill)
    public short Armament = 0, Variant = 0, Alter = 0; //����,�ψ�,����(Skill)
    public List<CharaManeuver> Skill;              //�X�L��

    private void Awake()
    {
        if (this.CompareTag("AllyChara"))
        {
            //                                                          SaveData�I�Ȃ��̂���H���̂Ƃ���AllyChara����{����
            Save_Load_data SaveData = GameObject.FindGameObjectWithTag("SaveData").GetComponent<Save_Load_data>();

            Name = SaveData.aa.Name;
            hide_hint = SaveData.aa.hide_hint;
            Death_year = SaveData.aa.Death_year;
            temper = SaveData.aa.temper;
            Memory= SaveData.aa.Memory;
            initArea = 2;
            MainClass = SaveData.aa.MainClass;
            SubClass = SaveData.aa.SubClass;
            Armament = SaveData.aa.Armament;
            Variant = SaveData.aa.Variant;
            Alter = SaveData.aa.Alter;

            for(int i=0;i!= SaveData.aa.CharaBase_data.HeadParts.Count;i++)
                headParts.Add(SaveData.aa.CharaBase_data.HeadParts[i]);

            for (int i = 0; i != SaveData.aa.CharaBase_data.ArmParts.Count; i++)
                armParts.Add(SaveData.aa.CharaBase_data.ArmParts[i]);

            for (int i = 0; i != SaveData.aa.CharaBase_data.BodyParts.Count; i++)
                bodyParts.Add(SaveData.aa.CharaBase_data.BodyParts[i]);

            for (int i = 0; i != SaveData.aa.CharaBase_data.LegParts.Count; i++)
                legParts.Add(SaveData.aa.CharaBase_data.LegParts[i]);
        }
        else if(this.CompareTag("EnemyChara"))
        {
            Name = "��܂ꂵ��";
        }
        InitParts();

        //�����ʒu�����݈ʒu�ɑ��
        area = initArea;

        //��----------------------
        HeadParts.Add(noumiso_H);
        HeadParts.Add(medama_H);
        HeadParts.Add(ago_H);
        HeadParts.Add(hunnu_H);

        //�r----------------------
        ArmParts.Add(ude_A);
        ArmParts.Add(kata_A);
        ArmParts.Add(kobusi_A);
        ArmParts.Add(wirelille_A);

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