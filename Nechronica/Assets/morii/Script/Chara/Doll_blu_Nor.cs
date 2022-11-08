using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doll_blu_Nor : PartsList
{
    public string Name="test";                    //ドール名 
    public string hide_hint="test";              //暗示
    public string Death_year="10";              //享年s
    public string temper="アリス";                  //ポジション
    public short[] Memory= {1,2 };                 //記憶のかけら

    public string GetName() => Name;

    

                                           //---------------------------------------------------↑完了↓未完
    public short potition=2;                 //初期配置(煉獄)
    public string MainClass="Stacy", SubClass="Stacy";     //職業(skill)
    public short Armament=0, Variant=0, Alter=0; //武装,変異,改造(Skill)
    

    private void Awake()
    {
        InitParts();

        //頭----------------------
        GetHeadParts().Add(noumiso_H);
        GetHeadParts().Add(medama_H);
        GetHeadParts().Add(ago_H);

        //腕----------------------
        GetArmParts().Add(ude_A);
        GetArmParts().Add(kata_A);
        GetArmParts().Add(kobusi_A);

        //胴----------------------
        GetBodyParts().Add(harawata2_B);
        GetBodyParts().Add(harawata_B);
        GetBodyParts().Add(sebone_B);

        //脚----------------------
        GetLegParts().Add(hone2_L);
        GetLegParts().Add(hone_L);
        GetLegParts().Add(asi_L);

        MaxCountCal();
        Debug.Log(maxCount);
    }
}
