using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doll_blu_Nor : PartsList
{
    public string Name="test";                    //ドール名 
    public string hide_hint="test";              //暗示
    public string Death_year="10";              //享年
    public string temper="アリス";                  //ポジション
    public short[] Memory= {1,2 };                  //記憶のかけら

    public string GetName() => Name;

    

                                           //---------------------------------------------------↑完了↓未完
    public short position=3;                 //初期配置(煉獄)
    public string MainClass="Stacy", SubClass="Stacy";     //職業(skill)
    public short Armament=0, Variant=0, Alter=0; //武装,変異,改造(Skill)
    public List<CharaManeuver> Skill;              //スキル

    private void Awake()
    {
        InitParts();
        //頭----------------------
        HeadParts.Add(noumiso_H);
        HeadParts.Add(medama_H);
        HeadParts.Add(ago_H);

        //腕----------------------
        ArmParts.Add(ude_A);
        ArmParts.Add(kata_A);
        ArmParts.Add(kobusi_A);

        //胴----------------------
        BodyParts.Add(harawata2_B);
        BodyParts.Add(harawata_B);
        BodyParts.Add(sebone_B);

        //脚----------------------
        LegParts.Add(hone2_L);
        LegParts.Add(hone_L);
        LegParts.Add(asi_L);

        MaxCountCal();
        Debug.Log(maxCount);
    }
}
