using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maneger_Accessor 
{
    //シングルトンパターン
    private static Maneger_Accessor instance = null;

    public static Maneger_Accessor Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Maneger_Accessor();
            }
            return instance;
        }

    }

    //マネージャの参照
    public Wepon_Maneger weponManeger_cs;
    public SkillManeger skillManeger_cs;
    public Chara_data_input chara_Data_Input_cs;
}
