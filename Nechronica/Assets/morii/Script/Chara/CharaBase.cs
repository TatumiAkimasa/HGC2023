using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaBase : MonoBehaviour
{
    //ゲッター
    public int GetMaxCount() => MaxCount;
    public int GetNowCount() => NowCount;
    public int GetWeight() => AllWeight;

    public List<CharaManeuver> GetHeadParts() => HeadParts; //頭パーツ参照
    public List<CharaManeuver> GetArmParts() => ArmParts;   //腕パーツ参照
    public List<CharaManeuver> GetBodygParts() => BodyParts;//胴体パーツ参照
    public List<CharaManeuver> GetLegParts() => LegParts;   //脚パーツ参照

    public List<CharaManeuver> HeadParts;      //頭のパーツ
    public List<CharaManeuver> ArmParts;       //腕のパーツ
    public List<CharaManeuver> BodyParts;      //胴のパーツ
    public List<CharaManeuver> LegParts;       //脚のパーツ

    private int MaxCount;                       //カウント最大値
    private int NowCount;                       //現在のカウント
    private int AllWeight;                      //重さ
}

public class CharaManeuver
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

public class ManeuverEffectsAtk
{
    public int AtkType;       //攻撃属性
    public  bool isExplosion;   //爆発攻撃かどうか
    public bool isCotting;     //切断攻撃かどうか
    public bool isAllAttack;   //全体攻撃かどうか
    public bool isSuccession;  //連撃かどうか
    public int Num_per_Action;//連撃回数
}


