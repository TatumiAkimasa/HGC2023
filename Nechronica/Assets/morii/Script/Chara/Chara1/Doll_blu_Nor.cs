using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doll_blu_Nor : PartsList
{
    //げったー
    public string GetName() => Name;

    
    public string Name;                    //ドール名 
    public string hide_hint="test";              //暗示
    public string Death_year="10";              //享年
    public string temper="アリス";                  //ポジション
    public short[] Memory= {1,2 };                  //記憶のかけら
    public int area;                 //現在位置
    public int initArea = 2;                 //初期配置(煉獄)
    public string MainClass = "Stacy", SubClass = "Stacy";     //職業(skill)
    public short Armament = 0, Variant = 0, Alter = 0; //武装,変異,改造(Skill)
    public List<CharaManeuver> Skill;              //スキル

    private void Awake()
    {
        if (this.CompareTag("AllyChara"))
        {
            if(this.name=="PLChara")
            {
                Name = "ミリカ";
            }
            else if (this.name == "PLChara2")
            {
                Name = "ネアン";
            }
        }
        else if(this.CompareTag("EnemyChara"))
        {
            if(this.GetComponent<ObjEnemy>().DOOLmode)
            {
                Name = "指揮官";
            }
            else if(!this.GetComponent<ObjEnemy>().DOOLmode && this.GetComponent<ObjEnemy>().armynum==0)
            {
                Name = "兵士";
            }
            else
            {

            }
            //Name = "包まれし者";
        }
        InitParts();

        //初期位置を現在位置に代入
        area = initArea;

        //頭----------------------
        HeadParts.Add(noumiso_H);
        HeadParts.Add(medama_H);
        HeadParts.Add(ago_H);
        HeadParts.Add(hunnu_H);

        //腕----------------------
        ArmParts.Add(ude_A);
        ArmParts.Add(kata_A);
        ArmParts.Add(kobusi_A);

        //胴----------------------
        BodyParts.Add(harawata2_B);
        BodyParts.Add(harawata_B);
        BodyParts.Add(harawata3_B);

        //脚----------------------
        LegParts.Add(hone2_L);
        LegParts.Add(hone_L);
        LegParts.Add(asi_L);

        if(Name=="ミリカ")
        {
            ArmParts.Add(bearGun_U);
            ArmParts.Add(shotGun_U); 
            ArmParts.Add(doubleGun_U);
            HeadParts.Add(scope_H);
            HeadParts.Add(kanhu_H);
        }
        else if(Name=="ネアン")
        {
            ArmParts.Add(meitou_U);
            ArmParts.Add(wirelille_A);
            ArmParts.Add(uroko_B);
            BodyParts.Add(tale_B);
            HeadParts.Add(kanhu2_H);
        }

        MaxCountCal();
    }
}