using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public int InitArea;                 //初期位置
    
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
    //タイミングの定数
    public const int COUNT = -1;
    public const int AUTO = 0;
    public const int ACTION = 1;
    public const int MOVE = 2;
    public const int RAPID = 3;
    public const int JUDGE = 4;
    public const int DAMAGE = 5;

    //ゲッター
    public int GetMaxCount() => maxCount;
    public int GetNowCount() => nowCount;
    public int GetWeight() => allWeight;

    public List<CharaManeuver> GetHeadParts() => HeadParts; // 頭パーツ参照
    public List<CharaManeuver> GetArmParts() => ArmParts;   // 腕パーツ参照
    public List<CharaManeuver> GetBodyParts() => BodyParts; // 胴体パーツ参照
    public List<CharaManeuver> GetLegParts() => LegParts;   // 脚パーツ参照

    public int GetALLParts() => HeadParts.Count + ArmParts.Count + BodyParts.Count + LegParts.Count;

    public List<CharaManeuver> HeadParts;      // 頭のパーツ
    public List<CharaManeuver> ArmParts;       // 腕のパーツ
    public List<CharaManeuver> BodyParts;      // 胴のパーツ
    public List<CharaManeuver> LegParts;       // 脚のパーツ
    public List<CharaManeuver> Skill;       // 脚のパーツ

    //最大行動値計算
    public void MaxCountCal()
    {
        for (int i = 0; i < HeadParts.Count; i++)
        {
            //最大行動値加算パーツが破損していなければ最大行動値加算
            if (!HeadParts[i].isDmage && HeadParts[i].Timing == COUNT)
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




