using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doll_blu_Nor : PartsList
{
    public string Name="test";                    //h[¼ 
    public string hide_hint="test";              //Ã¦
    public string Death_year="10";              //Ns
    public string temper="AX";                  //|WV
    public short[] Memory= {1,2 };                 //L¯Ì©¯ç

    public string GetName() => Name;

    

                                           //---------------------------------------------------ª®¹«¢®
    public short potition=2;                 //úzu(ù)
    public string MainClass="Stacy", SubClass="Stacy";     //EÆ(skill)
    public short Armament=0, Variant=0, Alter=0; //,ÏÙ,ü¢(Skill)
    

    private void Awake()
    {
        InitParts();

        //ª----------------------
        GetHeadParts().Add(noumiso_H);
        GetHeadParts().Add(medama_H);
        GetHeadParts().Add(ago_H);

        //r----------------------
        GetArmParts().Add(ude_A);
        GetArmParts().Add(kata_A);
        GetArmParts().Add(kobusi_A);

        //·----------------------
        GetBodyParts().Add(harawata2_B);
        GetBodyParts().Add(harawata_B);
        GetBodyParts().Add(sebone_B);

        //r----------------------
        GetLegParts().Add(hone2_L);
        GetLegParts().Add(hone_L);
        GetLegParts().Add(asi_L);

        MaxCountCal();
        Debug.Log(maxCount);
    }
}
