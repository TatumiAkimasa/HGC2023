using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doll_blu_Nor_tatumi : kihonnpatu
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
        //初期武装追記
        for (int i = 0; i != MAX_BASE_PARTS; i++)
        {
            //頭
            parts.HeadParts.Add(Base_Head_parts[i]);
            //腕
            parts.ArmParts.Add(Base_Arm_parts[i]);
            //胴
            parts.BodyParts.Add(Base_Body_parts[i]);
            //脚
            parts.LegParts.Add(Base_Leg_parts[i]);
        }
    }
}
