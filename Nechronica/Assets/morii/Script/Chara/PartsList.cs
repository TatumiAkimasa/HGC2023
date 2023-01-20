using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PartsList : CharaBase
{
    [SerializeField]
    //��b�p�[�c
    protected CharaManeuver noumiso_H, medama_H, ago_H,hunnu_H,
                            kobusi_A, ude_A, kata_A,wirelille_A,
                            sebone_B, harawata_B, harawata2_B,
                            hone_L, hone2_L, asi_L;
    //TIMING------------------^p^
   
    //0=�I�[�g,1=�A�N�V����,2=���s�b�h,3=�W���b�W,4=�_���[�W(�������ł킯��)
    //�U���͈�-------------------------------
    //�ŏ�(10=���g)
    //�ōő�(10=���g)
    //�d��------------------------------------
    //(��b�͍��̂Ƃ���1�ŌŒ�)
    // Start is called before the first frame update
    public void InitParts()
    {
        //���p�[�c-------------------
        ago_H.Name = "����";
        ago_H.AnimName = AnimationName.Ago;
        ago_H.AnimEffect = NonResources.Load<GameObject>("Assets/morii/Prefab/Anim/"+ ago_H.AnimName + ".prefab");
        ago_H.EffectNum.Add(EffNum.Damage, 1);
        ago_H.Cost = 2;
        ago_H.Timing = ACTION;
        ago_H.MinRange = 0;
        ago_H.MaxRange = 0;
        ago_H.Weight = 1;

        noumiso_H.Name = "�̂��݂�";
        noumiso_H.AnimName = AnimationName.Null;
        noumiso_H.EffectNum.Add(EffNum.Count, 2);
        noumiso_H.Cost = 0;
        noumiso_H.Timing = COUNT;
        noumiso_H.MinRange = 10;
        noumiso_H.MaxRange = 10;
        noumiso_H.Weight = 1;

        medama_H.Name = "�߂���";
        medama_H.AnimName = AnimationName.Null;
        medama_H.EffectNum.Add(EffNum.Count, 1);
        medama_H.Cost = 0;
        medama_H.Timing = COUNT;
        medama_H.MinRange = 10;
        medama_H.MaxRange = 10;
        medama_H.Weight = 1;

        hunnu_H.Name = "���{";
        hunnu_H.AnimName = AnimationName.Null;
        hunnu_H.EffectNum.Add(EffNum.Damage, 5);
        hunnu_H.Cost = 0;
        hunnu_H.Timing = DAMAGE;
        hunnu_H.MinRange = 10;
        hunnu_H.MaxRange = 10;
        hunnu_H.Weight = 0;



        //�r---------------------------------
        kobusi_A.Name = "���Ԃ�";
        kobusi_A.AnimName = AnimationName.Kobushi;
        kobusi_A.AnimEffect = NonResources.Load<GameObject>("Assets/morii/Prefab/Anim/" + kobusi_A.AnimName + ".prefab");
        kobusi_A.EffectNum.Add(EffNum.Damage, 10);
        kobusi_A.Cost = 2;
        kobusi_A.Timing = ACTION;
        kobusi_A.MinRange = 0;
        kobusi_A.MaxRange = 0;
        kobusi_A.Weight = 1;
        kobusi_A.Atk.Num_per_Action = 2;
        kobusi_A.Atk.isFallDown = true;

        kata_A.Name = "����";
        kata_A.AnimName = AnimationName.Null;
        kata_A.EffectNum.Add(EffNum.Move, 1);
        kata_A.Cost = 4;
        kata_A.Timing = MOVE;
        kata_A.MinRange = 10;
        kata_A.MaxRange = 10;
        kata_A.Weight = 1;

        ude_A.Name = "����";
        ude_A.AnimName = AnimationName.Null;
        ude_A.EffectNum.Add(EffNum.Judge, 1);
        ude_A.Cost = 1;
        ude_A.Timing = JUDGE;
        ude_A.MinRange = 0;
        ude_A.MaxRange = 0;
        ude_A.Weight = 1;

        wirelille_A.Name = "���C���[���[��";
        wirelille_A.AnimName = AnimationName.Null;
        wirelille_A.EffectNum.Add(EffNum.Move, 1);
        wirelille_A.Cost = 3;
        wirelille_A.Timing = RAPID;
        wirelille_A.MinRange = 0;
        wirelille_A.MaxRange = 2;
        wirelille_A.Weight = 1;
        //��----------------------------

        sebone_B.Name = "���ڂ�";
        sebone_B.AnimName = AnimationName.Null;
        sebone_B.EffectNum.Add(EffNum.Extra, -1);
        sebone_B.Cost = 1;
        sebone_B.Timing = ACTION;
        sebone_B.MinRange = 0;
        sebone_B.MaxRange = 0;
        sebone_B.Weight = 1;

        harawata_B.Name = "�͂�킽";
        harawata_B.AnimName = AnimationName.Null;
        harawata_B.EffectNum.Add(EffNum.Extra, 0);
        harawata_B.Cost = 0;
        harawata_B.Timing = AUTO;
        harawata_B.MinRange = 10;
        harawata_B.MaxRange = 10;
        harawata_B.Weight = 1;

        harawata2_B.Name = "�͂�킽";
        harawata2_B.AnimName = AnimationName.Null;
        harawata2_B.EffectNum.Add(EffNum.Extra, 0);
        harawata2_B.Cost = 0;
        harawata2_B.Timing = AUTO;
        harawata2_B.MinRange = 10;
        harawata2_B.MaxRange = 10;
        harawata2_B.Weight = 1;

        //�r------------------------------

        hone_L.Name = "�ق�";
        hone_L.AnimName = AnimationName.Null;
        hone_L.EffectNum.Add(EffNum.Move, 1);
        hone_L.Cost = 3;
        hone_L.Timing = MOVE;
        hone_L.MinRange = 10;
        hone_L.MaxRange = 10;
        hone_L.Weight = 1;

        hone2_L.Name = "�ق�";
        hone2_L.AnimName = AnimationName.Null;
        hone2_L.EffectNum.Add(EffNum.Move, 1);
        hone2_L.Cost = 3;
        hone2_L.Timing = MOVE;
        hone2_L.MinRange = 10;
        hone2_L.MaxRange = 10;
        hone2_L.Weight = 1;

        asi_L.Name = "����";
        asi_L.AnimName = AnimationName.Null;
        asi_L.EffectNum.Add(EffNum.Judge, -1);
        asi_L.Cost = 1;
        asi_L.MinRange = 0;
        asi_L.MaxRange = 0;
        asi_L.Timing = JUDGE;
        asi_L.Weight = 1;

    }


}
