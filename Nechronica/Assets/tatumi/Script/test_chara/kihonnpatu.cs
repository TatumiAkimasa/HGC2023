using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kihonnpatu : CharaBase
{
    //基礎パーツ
    public CharaManeuver[] Base_Head_parts;
    public CharaManeuver[] Base_Arm_parts;
    public CharaManeuver[] Base_Body_parts;
    public CharaManeuver[] Base_Leg_parts;
    public CharaManeuver Treasure_parts;

    protected const int MAX_BASE_PARTS = 3;

    public int GET_MAX_BASE_PARTS() { return MAX_BASE_PARTS; }

    //インスペクター上から弄らんとNullObj判定。
    //void Awake()
    //{
    //    Base_Head_parts = new CharaManeuver[MAX_BASE_PARTS];
    //    Base_Arm_parts = new CharaManeuver[MAX_BASE_PARTS];
    //    Base_Body_parts = new CharaManeuver[MAX_BASE_PARTS];
    //    Base_Leg_parts = new CharaManeuver[MAX_BASE_PARTS];
    //}

    // Start is called before the first frame update
    void Start()
    {
        //NAME-------------------
        Base_Head_parts[0].Name = "あご";
        Base_Head_parts[1].Name = "のうみそ";
        Base_Head_parts[2].Name = "めだま";
        Base_Arm_parts[0].Name = "こぶし";
        Base_Arm_parts[1].Name = "かた";
        Base_Arm_parts[2].Name = "うで";
        Base_Body_parts[0].Name = "せぼね";
        Base_Body_parts[1].Name = "はらわた";
        Base_Body_parts[2].Name = "はらわた";
        Base_Leg_parts[0].Name = "ほね";
        Base_Leg_parts[1].Name = "ほね";
        Base_Leg_parts[2].Name = "あし";
        Treasure_parts.Name = "";

        //ダメージ値----------------------
        Base_Head_parts[0].EffectNum.Add("肉弾攻撃1",1);
        Base_Head_parts[1].EffectNum.Add("最大行動値1", 2);
        Base_Head_parts[2].EffectNum.Add("最大行動値1", 1);
        Base_Arm_parts[0].EffectNum.Add("肉弾攻撃1", 1);
        Base_Arm_parts[1].EffectNum.Add("移動1", 1);
        Base_Arm_parts[2].EffectNum.Add("支援1", 1);
        Base_Body_parts[0].EffectNum.Add("Cost-1", -1);
        Base_Body_parts[1].EffectNum.Add("", 0);
        Base_Body_parts[2].EffectNum.Add("", 0);
        Base_Leg_parts[0].EffectNum.Add("移動1", 1);
        Base_Leg_parts[1].EffectNum.Add("移動1", 1);
        Base_Leg_parts[2].EffectNum.Add("妨害1", 1);
        Treasure_parts.EffectNum.Add("宝", 0);

        //COST-------------
        Base_Head_parts[0].Cost = 2;
        Base_Arm_parts[0].Cost = 2;
        Base_Arm_parts[2].Cost = 1;
        Base_Arm_parts[1].Cost = 4;
        Base_Body_parts[0].Cost = 2;
        Base_Leg_parts[0].Cost = 3;
        Base_Leg_parts[1].Cost = 3;
        //AUTO
        Base_Head_parts[1].Cost = 0;
        Base_Head_parts[2].Cost = 0;
        Base_Body_parts[1].Cost = 0;
        Base_Body_parts[2].Cost = 0;
        Base_Leg_parts[2].Cost = 0;
        Treasure_parts.Cost = 0;

        //TIMING------------------^p^
        //0=オート,1=アクション,2=ラピッド,3=ジャッジ,4=ダメージ(処理順でわける)
        Base_Head_parts[0].Timing = 1;
        Base_Head_parts[1].Timing = 0;
        Base_Head_parts[2].Timing = 0;
        Base_Arm_parts[0].Timing = 1;
        Base_Arm_parts[1].Timing = 1;
        Base_Arm_parts[2].Timing = 3;
        Base_Body_parts[0].Timing = 1;
        Base_Body_parts[1].Timing = 0;
        Base_Body_parts[2].Timing = 0;
        Base_Leg_parts[0].Timing = 1;
        Base_Leg_parts[1].Timing = 1;
        Base_Leg_parts[2].Timing = 3;
        Treasure_parts.Timing = 0;

        //攻撃範囲-------------------------------
        //最小(10=自身)
        Base_Head_parts[0].MinRange = 0;
        Base_Head_parts[1].MinRange = 10;
        Base_Head_parts[2].MinRange = 10;
        Base_Arm_parts[0].MinRange = 0;
        Base_Arm_parts[1].MinRange = 10;
        Base_Arm_parts[2].MinRange = 0;
        Base_Body_parts[0].MinRange = 0;
        Base_Body_parts[1].MinRange = 10;
        Base_Body_parts[2].MinRange = 10;
        Base_Leg_parts[0].MinRange = 10;
        Base_Leg_parts[1].MinRange = 10;
        Base_Leg_parts[2].MinRange = 0;
        Treasure_parts.MinRange = 10;
        //最最大(10=自身)
        Base_Head_parts[0].MaxRange = 0;
        Base_Head_parts[1].MaxRange = 10;
        Base_Head_parts[2].MaxRange = 10;
        Base_Arm_parts[0].MaxRange = 0;
        Base_Arm_parts[1].MaxRange = 10;
        Base_Arm_parts[2].MaxRange = 0;
        Base_Body_parts[0].MaxRange = 0;
        Base_Body_parts[1].MaxRange = 10;
        Base_Body_parts[2].MaxRange = 10;
        Base_Leg_parts[0].MaxRange = 10;
        Base_Leg_parts[1].MaxRange = 10;
        Base_Leg_parts[2].MaxRange = 0;
        Treasure_parts.MaxRange = 10;

        //重さ------------------------------------
        //(基礎は今のところ1で固定)
        Base_Head_parts[0].Weight = 1;
        Base_Head_parts[1].Weight = 1;
        Base_Head_parts[2].Weight = 1;
        Base_Arm_parts[0].Weight = 1;
        Base_Arm_parts[1].Weight = 1;
        Base_Arm_parts[2].Weight = 1;
        Base_Body_parts[0].Weight = 1;
        Base_Body_parts[1].Weight = 1;
        Base_Body_parts[2].Weight = 1;
        Base_Leg_parts[0].Weight = 1;
        Base_Leg_parts[1].Weight = 1;
        Base_Leg_parts[2].Weight = 1;
        Treasure_parts.Weight = 1;
    }


}
