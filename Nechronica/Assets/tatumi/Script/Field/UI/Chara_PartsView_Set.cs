using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Chara_PartsView_Set : CharaBase
{
    public string Timing_Set(int timing)
    {
        if(timing==AUTO)
        {
            return "オート";
        }
        else if(timing == ACTION)
        {
            return "アクション";
        }
        else if (timing == RAPID)
        {
            return "ラピッド";
        }
        else if (timing == JUDGE)
        {
            return "ジャッジ";
        }
        else if (timing == DAMAGE)
        {
            return "ダメージ";
        }

        return "ERROR:"+timing.ToString();
    }
    
}
