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
    public CharaBase parts;                       //パーツ類
    public string potition;                 //初期配置
    //---------------------------------------------------↑完了↓未完
  
    public List<CharaManeuver> Skll;              //スキル
  

    private int Treasure_num;

    private CharaManeuver Treasure=new CharaManeuver { };
    private CharaManeuver backTreasure = new CharaManeuver { };

    private void Start()
    {
        //宝初期せってい
        Treasure.MaxRange = 0;
        Treasure.MinRange = 0;
        Treasure.Timing = -1;
        Treasure.Weight = 1;
        Treasure.EffectNum = -1;
        Treasure.Cost = -1;
        Treasure.Atk = null;
        Treasure.isDmage = false;
        Treasure.isUse = false;
    }

    //宝物入力関数
    public void SetTreasure(string name,int i)
    {
        switch (Treasure_num)
        {
            case 1:
                parts.HeadParts.Remove(backTreasure);
                break;
            case 2:
                parts.ArmParts.Remove(backTreasure);
                break;
            case 3:
                parts.BodyParts.Remove(backTreasure);
                break;
            case 4:
                parts.LegParts.Remove(backTreasure);
                break;
                //初期設定
            case -1:
                break;
        }


        Treasure.Name = name;

        //対応場所に付与
        switch (i)
        {
            case 1:
                parts.HeadParts.Add(Treasure);
                break;
            case 2:
                parts.ArmParts.Add(Treasure);
                break;
            case 3:
                parts.BodyParts.Add(Treasure);
                break;
            case 4:
                parts.LegParts.Add(Treasure);
                break;
        }

        backTreasure = Treasure;
        Treasure_num = i;
    }

    
}


