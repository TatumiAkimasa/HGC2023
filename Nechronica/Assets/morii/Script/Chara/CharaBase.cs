using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharaBase : MonoBehaviour
{
    //ゲッター
    public int GetNowCount()  => NowCount;
    public int GetWeight() => Weight;

    List<CharaManeuver> HeadParts;      //頭のパーツ
    List<CharaManeuver> ArmParts;       //腕のパーツ
    List<CharaManeuver> BodyParts;      //胴のパーツ
    List<CharaManeuver> LegParts;       //脚のパーツ

    int MaxCount;                       //カウント最大値


    [SerializeField]
    int NowCount;                       //現在のカウント

    [SerializeField]
    int Weight;                         //重さ
}

[System.Serializable]
public class CharaManeuver
{
    string Name;            //パーツ名
    int EffectNum;          //効果値
    int Cost;               //コスト
    int Timing;             //発動タイミング
    int MinRange;           //射程の最小値
    int MaxRange;           //射程の最大値
    bool isUse;             //使用したかどうか
    bool isDmage;           //破損したかどうか
    ManeuverEffectsAtk Atk; //攻撃系
}

[System.Serializable]
public class ManeuverEffectsAtk
{
    int  AtkType;       //攻撃属性
    bool isExplosion;   //爆発攻撃かどうか
    bool isCotting;     //切断攻撃かどうか
    bool isAllAttack;   //全体攻撃かどうか
    bool isSuccession;  //連撃かどうか
    int  Num_per_Action;//連撃回数
}
