using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doll_blu_Nor : kihonnpatu
{
    public string Name="test";                    //ドール名 
    public string hide_hint="test";              //暗示
    public string Death_year="10";              //享年
    public string temper="アリス";                  //ポジション
    public short[] Memory= {1,2 };                 //記憶のかけら
                                           //---------------------------------------------------↑完了↓未完
    public short potition=3;                 //初期配置(煉獄)
    public string MainClass="Stacy", SubClass="Stacy";     //職業(skill)
    public short Armament=0, Variant=0, Alter=0; //武装,変異,改造(Skill)
    public List<CharaManeuver> Skll;              //スキル
    public CharaBase parts;                       //パーツ類

    private void Start()
    {
        //頭----------------------
        parts.HeadParts.Add(noumiso_H);
        parts.HeadParts.Add(medama_H);
        parts.HeadParts.Add(ago_H);

        //腕----------------------
        parts.ArmParts.Add(ude_A);
        parts.ArmParts.Add(kata_A);
        parts.ArmParts.Add(kobusi_A);

        //胴----------------------
        parts.BodyParts.Add(harawata2_B);
        parts.BodyParts.Add(harawata_B);
        parts.BodyParts.Add(sebone_B);

        //脚----------------------
        parts.LegParts.Add(hone2_L);
        parts.LegParts.Add(hone_L);
        parts.LegParts.Add(asi_L);
    }
}
