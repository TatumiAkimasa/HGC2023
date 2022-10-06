using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharaManeuver))]
[System.Serializable]
public class Doll_blueprint : CharaBase
{
    public string Name;                    //ドール名 
    public string hide_hint;              //暗示
    public string Death_year;              //享年
    public string temper;                  //ポジション
    public short[] Memory;                 //記憶のかけら
    public string MainClass, SubClass;     //職業
    public short Armament, Variant, Alter; //武装,変異,改造
    public short potition;                 //初期配置
    //---------------------------------------------------↑完了↓未完
  
}


