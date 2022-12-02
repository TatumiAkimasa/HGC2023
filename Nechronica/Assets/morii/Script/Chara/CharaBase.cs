using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class CharaBase : MonoBehaviour
{
    //タイミングの定数
    public const int COUNT  = -1;
    public const int AUTO   = 0;
    public const int ACTION = 1;
    public const int MOVE   = 2;
    public const int RAPID  = 3;
    public const int JUDGE  = 4;
    public const int DAMAGE = 5;

    //ゲッター
    public int GetMaxCount() => maxCount;
    public int GetNowCount() => nowCount;
    public int GetWeight() => allWeight;

    public List<CharaManeuver> GetHeadParts() => HeadParts; // 頭パーツ参照
    public List<CharaManeuver> GetArmParts() => ArmParts;   // 腕パーツ参照
    public List<CharaManeuver> GetBodyParts() => BodyParts; // 胴体パーツ参照
    public List<CharaManeuver> GetLegParts() => LegParts;   // 脚パーツ参照

    public List<CharaManeuver> HeadParts;      // 頭のパーツ
    public List<CharaManeuver> ArmParts;       // 腕のパーツ
    public List<CharaManeuver> BodyParts;      // 胴のパーツ
    public List<CharaManeuver> LegParts;       // 脚のパーツ
                                                  
    //最大行動値計算
    public void MaxCountCal()
    {
        for(int i=0;i<HeadParts.Count;i++)
        {
            //最大行動値加算パーツが破損していなければ最大行動値加算
            if(!HeadParts[i].isDmage&& HeadParts[i].Timing==COUNT)
            {
                maxCount += HeadParts[i].EffectNum[EffNum.Count];
            }
        }
        for (int i = 0; i < ArmParts.Count; i++)
        {
            //最大行動値加算パーツが破損していなければ最大行動値加算
            if (!ArmParts[i].isDmage && ArmParts[i].Timing == COUNT)
            {
                maxCount += ArmParts[i].EffectNum[EffNum.Count];
            }
        }
        for (int i = 0; i < BodyParts.Count; i++)
        {
            //最大行動値加算パーツが破損していなければ最大行動値加算
            if (!BodyParts[i].isDmage && BodyParts[i].Timing == COUNT)
            {
                maxCount += BodyParts[i].EffectNum[EffNum.Count];
            }
        }
        for (int i = 0; i < LegParts.Count; i++)
        {
            //最大行動値加算パーツが破損していなければ最大行動値加算
            if (!LegParts[i].isDmage && LegParts[i].Timing == COUNT)
            {
                maxCount += LegParts[i].EffectNum[EffNum.Count];
            }
        }
    }

    /// <summary>
    /// 行動力回復メソッド
    /// </summary>
    public void IncreaseNowCount()
    {
        nowCount += maxCount;
    }

    protected int maxCount = 6;                   // カウント最大値 ルール上もともと最大行動値は6あるので6で初期化
    protected int nowCount;                       // 現在のカウント
    public int NowCount
    {
        get { return nowCount; }
        set { nowCount = value; }
    }
    protected int allWeight;                      // 重さ

    [SerializeField]
    protected Image CharaImg;
    [SerializeField]
    protected Text CharaName;
}   
    
[System.Serializable]
public class CharaManeuver
{
    public string Name;            // パーツ名
    //public int EffectNum;        // 効果値
    public Dictionary<string,int> EffectNum = new Dictionary<string, int>();          // 効果値
    public int Cost;               // コスト
    public int Timing;             // 発動タイミング
    public int MinRange;           // 射程の最小値
    public int MaxRange;           // 射程の最大値
    public int Weight;             // 重さ
    public int Moving;             // 移動量(0で移動しない)
    public bool isUse;             // 使用したかどうか
    public bool isDmage;           // 破損したかどうか
    public ManeuverEffectsAtk Atk; // 攻撃系
}

[System.Serializable]
public class ManeuverEffectsAtk
{
    public int atkType;        // 攻撃属性
    public bool isExplosion;   // 爆発攻撃かどうか
    public bool isCutting;     // 切断攻撃かどうか
    public bool isAllAttack;   // 全体攻撃かどうか
    public bool isSuccession;  // 連撃かどうか
    public int Num_per_Action; // 連撃回数
}

[System.Serializable]
public class EffNum
{
    public const string Damage   = "Damage";
    public const string Guard    = "Guard";
    public const string Judge    = "Judge";
    public const string Move     = "Move";
    public const string Count    = "Count";
    public const string Insanity = "Insanity";      // 狂気点が関与するものはこれを入れる
    public const string Extra    = "Extra";
    
    
    // オンリーワンの効果
    public const string Protect  = "Protect";       // かばうの効果はこれで認識
}


