using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kihonnpatu : CharaBase
{
    //��b�p�[�c
    protected CharaManeuver noumiso_H, medama_H, ago_H,
                            kobusi_A, ude_A, kata_A,
                            sebone_B, harawata_B, harawata2_B,
                            hone_L, hone2_L, asi_L;

    // Start is called before the first frame update
    void Start()
    {

        //NAME-------------------
        ago_H.Name = "����";
        noumiso_H.Name = "�̂��݂�";
        medama_H.Name = "�߂���";
        kobusi_A.Name = "���Ԃ�";
        kata_A.Name = "����";
        ude_A.Name = "����";
        sebone_B.Name = "���ڂ�";
        harawata_B.Name = "�͂�킽";
        harawata2_B.Name = "�͂�킽";
        hone_L.Name = "�ق�";
        hone2_L.Name = "�ق�";
        asi_L.Name = "����";

        //�_���[�W�l----------------------
        ago_H.EffectNum = 1;
        noumiso_H.EffectNum = 2;
        medama_H.EffectNum = 1;
        kobusi_A.EffectNum = 1;
        kata_A.EffectNum = 1;
        ude_A.EffectNum = 1;
        sebone_B.EffectNum = -1;
        harawata_B.EffectNum = 0;
        harawata2_B.EffectNum = 0;
        hone_L.EffectNum = 1;
        hone2_L.EffectNum = 1;
        asi_L.EffectNum = 1;

        //COST-------------
        ago_H.Cost = 2;
        kobusi_A.Cost = 2;
        ude_A.Cost = 1;
        kata_A.Cost = 4;
        sebone_B.Cost = 2;
        hone_L.Cost = 3;
        hone2_L.Cost = 3;
        //AUTO
        noumiso_H.Cost = 0;
        medama_H.Cost = 0;
        harawata_B.Cost = 0;
        harawata2_B.Cost = 0;

        //TIMING------------------^p^
        //0=�I�[�g,1=�A�N�V����,2=���s�b�h,3=�W���b�W,4=�_���[�W(�������ł킯��)
        ago_H.Timing = 1;
        noumiso_H.Timing = 0;
        medama_H.Timing = 0;
        kobusi_A.Timing = 1;
        kata_A.Timing = 1;
        ude_A.Timing = 3;
        sebone_B.Timing = 1;
        harawata_B.Timing = 0;
        harawata2_B.Timing = 0;
        hone_L.Timing = 1;
        hone2_L.Timing = 1;
        asi_L.Timing = 3;

        //�U���͈�-------------------------------
        //�ŏ�(10=���g)
        ago_H.MinRange = 0;
        noumiso_H.MinRange = 10;
        medama_H.MinRange = 10;
        kobusi_A.MinRange = 0;
        kata_A.MinRange = 10;
        ude_A.MinRange = 0;
        sebone_B.MinRange = 0;
        harawata_B.MinRange = 10;
        harawata2_B.MinRange = 10;
        hone_L.MinRange = 10;
        hone2_L.MinRange = 10;
        asi_L.MinRange = 0;
        //�ōő�(10=���g)
        ago_H.MaxRange = 0;
        noumiso_H.MaxRange = 10;
        medama_H.MaxRange = 10;
        kobusi_A.MaxRange = 0;
        kata_A.MaxRange = 10;
        ude_A.MaxRange = 0;
        sebone_B.MaxRange = 0;
        harawata_B.MaxRange = 10;
        harawata2_B.MaxRange = 10;
        hone_L.MaxRange = 10;
        hone2_L.MaxRange = 10;
        asi_L.MaxRange = 0;

        //�d��------------------------------------
        //(��b�͍��̂Ƃ���1�ŌŒ�)
        ago_H.Weight = 1;
        noumiso_H.Weight = 1;
        medama_H.Weight = 1;
        kobusi_A.Weight = 1;
        kata_A.Weight = 1;
        ude_A.Weight = 1;
        sebone_B.Weight = 1;
        harawata_B.Weight = 1;
        harawata2_B.Weight = 1;
        hone_L.Weight = 1;
        hone2_L.Weight = 1;
        asi_L.Weight = 1;

    }


}
