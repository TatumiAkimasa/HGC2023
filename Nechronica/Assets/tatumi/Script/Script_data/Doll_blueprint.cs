using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Doll_blueprint
{
    public string Name;                    //ドール名 
    public string hide_hint;              //暗示
    public string Death_year;              //享年
    public string temper;                  //ポジション
    public short[] Memory;                 //記憶のかけら
    public string MainClass, SubClass;     //職業
    public short Armament, Variant, Alter; //武装,変異,改造
    public short potition;                 //初期配置
    public List<Item> Item;              //所持アイテム
    public CharaBase_SaveData CharaBase_data;
    public Chara_Field_SaveData CharaField_data;
}

[System.Serializable]
public class Chara_Field_SaveData
{
    public string Scene_Name;
    public float[] Pos = new float[3];
    public string PosStr;
    public EventFlag[] Event = new EventFlag[2];
    public string[] Time = new string[2];
}

[System.Serializable]
public class EventFlag
{
    public string str;
    public bool flag;
}

[System.Serializable]
public class Item
{
    public string Tiltle;
    public string str;
}

[System.Serializable]
public class CharaBase_SaveData
{
    //ゲッター
    public int GetMaxCount() => MaxCount;
    public int GetNowCount() => NowCount;
    public int GetWeight() => AllWeight;
    public int GetALLParts()=> HeadParts.Count + ArmParts.Count + BodyParts.Count + LegParts.Count;
    

    public List<CharaManeuver> GetHeadParts() => HeadParts; //頭パーツ参照
    public List<CharaManeuver> GetArmParts() => ArmParts;   //腕パーツ参照
    public List<CharaManeuver> GetBodygParts() => BodyParts;//胴体パーツ参照
    public List<CharaManeuver> GetLegParts() => LegParts;   //脚パーツ参照
    public List<CharaManeuver> GetSkillParts() => Skill;      //SKILLのパーツ
   

    public List<CharaManeuver> HeadParts;      //頭のパーツ
    public List<CharaManeuver> ArmParts;       //腕のパーツ
    public List<CharaManeuver> BodyParts;      //胴のパーツ
    public List<CharaManeuver> LegParts;       //脚のパーツ
    public List<CharaManeuver> Skill;          //SKILLのパーツ

    private int MaxCount;                       //カウント最大値
    private int NowCount;                       //現在のカウント
    private int AllWeight;                      //重さ
}

[System.Serializable]
public class CharaManeuver_SaveData
{
    public string Name;            //パーツ名
    public int EffectNum;          //効果値
    public int Cost;               //コスト
    public int Timing;             //発動タイミング
    public int MinRange;           //射程の最小値
    public int MaxRange;           //射程の最大値
    public int Weight;             //重さ
    public int Moving;             //移動量(0で移動しない)
    public bool isUse;             //使用したかどうか
    public bool isDmage;           //破損したかどうか
    public ManeuverEffectsAtk Atk; //攻撃系
}

[System.Serializable]
public class ManeuverEffectsAtk_SaveData
{
    public int AtkType;       //攻撃属性
    public bool isExplosion;   //爆発攻撃かどうか
    public bool isCotting;     //切断攻撃かどうか
    public bool isAllAttack;   //全体攻撃かどうか
    public bool isSuccession;  //連撃かどうか
    public int Num_per_Action;//連撃回数
}







