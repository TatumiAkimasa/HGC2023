using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kihonnpatu : CharaBase
{
    //基礎パーツ
    protected CharaManeuver noumiso_H, medama_H, ago_H,
                            kobusi_A, ude_A, kata_A,
                            sebone_B, harawata_B, harawata2_B,
                            hone_L, hone2_L, asi_L;


    //TIMING------------------^p^
    //0=オート,1=アクション,2=ラピッド,3=ジャッジ,4=ダメージ(処理順でわける)
    //攻撃範囲-------------------------------
    //最小(10=自身)
    //最最大(10=自身)
    //重さ------------------------------------
    //(基礎は今のところ1で固定)
    // Start is called before the first frame update
    void Start()
    {

        //頭パーツ-------------------
        ago_H.Name = "あご";
        ago_H.EffectNum = 1;
        ago_H.Cost = 2;
        ago_H.Timing = 1;
        ago_H.MinRange = 0;
        ago_H.MaxRange = 0;
        ago_H.Weight = 1;

        noumiso_H.Name = "のうみそ";
        noumiso_H.EffectNum = 2;
        noumiso_H.Cost = 0;
        noumiso_H.Timing = 0;
        noumiso_H.MinRange = 10;
        noumiso_H.MaxRange = 10;
        noumiso_H.Weight = 1;

        medama_H.Name = "めだま";
        medama_H.EffectNum = 1;
        medama_H.Cost = 0;
        medama_H.Timing = 0;
        medama_H.MinRange = 10;
        medama_H.MaxRange = 10;
        medama_H.Weight = 1;

        HeadParts.Add(noumiso_H);
        HeadParts.Add(medama_H);
        HeadParts.Add(ago_H);


        //腕---------------------------------
        kobusi_A.Name = "こぶし";
        kobusi_A.EffectNum = 1;
        kobusi_A.Cost = 2;
        kobusi_A.Timing = 1;
        kobusi_A.MinRange = 0;
        kobusi_A.MaxRange = 0;
        kobusi_A.Weight = 1;

        kata_A.Name = "かた";
        kata_A.EffectNum = 1;
        kata_A.Cost = 4;
        kata_A.Timing = 1;
        kata_A.MinRange = 10;
        kata_A.MaxRange = 10;
        kata_A.Weight = 1;

        ude_A.Name = "うで";
        ude_A.EffectNum = 1;
        ude_A.Cost = 1;
        ude_A.Timing = 3;
        ude_A.MinRange = 0;
        ude_A.MaxRange = 0;
        ude_A.Weight = 1;

        ArmParts.Add(kobusi_A);
        ArmParts.Add(kata_A);
        ArmParts.Add(ude_A);

        //胴----------------------------

        sebone_B.Name = "せぼね";
        sebone_B.EffectNum = -1;
        sebone_B.Cost = 2;
        sebone_B.Timing = 1;
        sebone_B.MinRange = 0;
        sebone_B.MaxRange = 0;
        sebone_B.Weight = 1;

        harawata_B.Name = "はらわた";
        harawata_B.EffectNum = 0;
        harawata_B.Cost = 0;
        harawata_B.Timing = 0;
        harawata_B.MinRange = 10;
        harawata_B.MaxRange = 10;
        harawata_B.Weight = 1;

        harawata2_B.Name = "はらわた";
        harawata2_B.EffectNum = 0;
        harawata2_B.Cost = 0;
        harawata2_B.Timing = 0;
        harawata2_B.MinRange = 10;
        harawata2_B.MaxRange = 10;
        harawata2_B.Weight = 1;

        //脚------------------------------

        hone_L.Name = "ほね";
        hone_L.EffectNum = 1;
        hone_L.Cost = 3;
        hone_L.Timing = 1;
        hone_L.MinRange = 10;
        hone_L.MaxRange = 10;
        hone_L.Weight = 1;

        hone2_L.Name = "ほね";
        hone2_L.EffectNum = 1;
        hone2_L.Cost = 3;
        hone2_L.Timing = 1;
        hone2_L.MinRange = 10;
        hone2_L.MaxRange = 10;
        hone2_L.Weight = 1;

        asi_L.Name = "あし";
        asi_L.EffectNum = 1;
        asi_L.Cost = 1;
        asi_L.MinRange = 0;
        asi_L.MaxRange = 0;
        asi_L.Timing = 3;
        asi_L.Weight = 1;

    }


}
