using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class CharaBase : MonoBehaviour
{
    //タイミングの定数
    protected const int COUNT  = -1;
    protected const int AUTO   = 0;
    protected const int ACTION = 1;
    protected const int RAPID  = 2;
    protected const int JUDGE  = 3;
    protected const int DAMAGE = 4;

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

    //最大行動値計算
    public void MaxCountCal()
    {
        for(int i=0;i<HeadParts.Count;i++)
        {
            //最大行動値加算パーツが破損していなければ最大行動値加算
            if(!HeadParts[i].isDmage&& HeadParts[i].Timing==COUNT)
            {
                MaxCount += HeadParts[i].EffectNum;
            }
        }
        for (int i = 0; i < ArmParts.Count; i++)
        {
            //最大行動値加算パーツが破損していなければ最大行動値加算
            if (!ArmParts[i].isDmage && ArmParts[i].Timing == COUNT)
            {
                MaxCount += ArmParts[i].EffectNum;
            }
        }
        for (int i = 0; i < BodyParts.Count; i++)
        {
            //最大行動値加算パーツが破損していなければ最大行動値加算
            if (!BodyParts[i].isDmage && BodyParts[i].Timing == COUNT)
            {
                MaxCount += BodyParts[i].EffectNum;
            }
        }
        for (int i = 0; i < LegParts.Count; i++)
        {
            //最大行動値加算パーツが破損していなければ最大行動値加算
            if (!LegParts[i].isDmage && LegParts[i].Timing == COUNT)
            {
                MaxCount += LegParts[i].EffectNum;
            }
        }
    }

    public void IncreaseNowCount()
    {
        NowCount += MaxCount;
    }

    protected int MaxCount = 6;                   //カウント最大値 ルール上もともと最大行動値は6あるので6で初期化
    protected int NowCount;                       //現在のカウント
    protected int AllWeight;                      //重さ

    [SerializeField]
    protected Image CharaImg;
    [SerializeField]
    protected Text CharaName;
}   
    
[System.Serializable]
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

[System.Serializable]
public class ManeuverEffectsAtk
{
    public int AtkType;       //攻撃属性
    public  bool isExplosion;   //爆発攻撃かどうか
    public bool isCotting;     //切断攻撃かどうか
    public bool isAllAttack;   //全体攻撃かどうか
    public bool isSuccession;  //連撃かどうか
    public int Num_per_Action;//連撃回数
}


