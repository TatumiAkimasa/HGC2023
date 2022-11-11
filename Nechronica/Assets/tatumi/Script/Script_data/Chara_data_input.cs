using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Chara_data_input : CharaBase 
{
    const int HEAD = 0;
    const int ARM = 1;
    const int BODY = 2;
    const int LEG = 3;

    private int Treasure_num;
    public string name_;                    //ドール名 
    public string death_year_;              //享年

    private Wepon_Maneger WE_Maneger;
    private SkillManeger SK_Maneger;
    [SerializeField]
    private kihonnpatu ALL_Base_Parts;

    public Doll_blueprint Doll_data;

    public CharaManeuver Potition_Skill;
    [SerializeField]
    private CharaManeuver backTreasure, Treasure;

    [System.NonSerialized]
    public string temper_name;
    [System.NonSerialized]
    public short position_;
    [System.NonSerialized]
    public string hide_hint_;              //暗示

    public short[] Memory_=new short[6];                 //記憶のかけら

    // Start is called before the first frame update
    void Awake()
    {
        //参照渡しでいったん解決（Copyがうまくいかない...）
        Doll_data.Memory = Memory_;
        Treasure = ALL_Base_Parts.Treasure_parts;

        Maneger_Accessor.Instance.chara_Data_Input_cs = this;
    }

    private void Start()
    {
        WE_Maneger = Maneger_Accessor.Instance.weponManeger_cs;
        SK_Maneger = Maneger_Accessor.Instance.skillManeger_cs;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Skill_Reset()
    {
        Doll_data.CharaBase_data.Skill.Remove(Potition_Skill);
        Potition_Skill = null;
    }

    public void input()
    {
        //初期化
        Doll_data.CharaBase_data.HeadParts.Clear();
        Doll_data.CharaBase_data.ArmParts.Clear();
        Doll_data.CharaBase_data.BodyParts.Clear();
        Doll_data.CharaBase_data.LegParts.Clear();

        //初期武装追記
        for (int i = 0; i != ALL_Base_Parts.GET_MAX_BASE_PARTS(); i++)
        {
            Doll_data.CharaBase_data.HeadParts.Add(ALL_Base_Parts.Base_Head_parts[i]);

            Doll_data.CharaBase_data.ArmParts.Add(ALL_Base_Parts.Base_Arm_parts[i]);

            Doll_data.CharaBase_data.BodyParts.Add(ALL_Base_Parts.Base_Body_parts[i]);

            Doll_data.CharaBase_data.LegParts.Add(ALL_Base_Parts.Base_Leg_parts[i]);
        }

        //追加武装追記
        for (int SITE = 0; SITE != 4; SITE++)
        {
            for (int i = 0; i != 2; i++)
            {
                for (int k = 0; k != 3; k++)
                {
                    //None情報を抜きにして整理
                    if (WE_Maneger.Site_[SITE].Step[i].Text[k].GetComponent<Wepon_Data_SaveSet>().GetName()!="None")
                    {
                        if (SITE == HEAD)
                        {
                            Doll_data.CharaBase_data.HeadParts.Add(WE_Maneger.Site_[SITE].Step[i].Text[k].GetComponent<Wepon_Data_SaveSet>().GetParts());
                        }
                        if (SITE == ARM)
                        {
                            Doll_data.CharaBase_data.ArmParts.Add(WE_Maneger.Site_[SITE].Step[i].Text[k].GetComponent<Wepon_Data_SaveSet>().GetParts());
                        }
                        if (SITE == BODY)
                        {
                            Doll_data.CharaBase_data.BodyParts.Add(WE_Maneger.Site_[SITE].Step[i].Text[k].GetComponent<Wepon_Data_SaveSet>().GetParts());
                        }
                        if (SITE == LEG)
                        {
                            Doll_data.CharaBase_data.LegParts.Add(WE_Maneger.Site_[SITE].Step[i].Text[k].GetComponent<Wepon_Data_SaveSet>().GetParts());
                        }
                    }

                }
            }
        }

        //お宝を対応場所に付与(場所はラスト固定)
        switch (Treasure_num)
        {
            case HEAD:
                Doll_data.CharaBase_data.HeadParts.Add(Treasure);
                break;
            case ARM:
                Doll_data.CharaBase_data.ArmParts.Add(Treasure);
                break;
            case BODY:
                Doll_data.CharaBase_data.BodyParts.Add(Treasure);
                break;
            case LEG:
                Doll_data.CharaBase_data.LegParts.Add(Treasure);
                break;
        }


        //クラス（メイン、サブ）
        Doll_data.MainClass = SK_Maneger.GetKeyWord_main();
        Doll_data.SubClass = SK_Maneger.GetKeyWord_sub();

        //それぞれの武器レベル設定
        Doll_data.Armament = (short)SK_Maneger.GetArmament();
        Doll_data.Variant = (short)SK_Maneger.GetVariantt();
        Doll_data.Alter= (short)SK_Maneger.GetAlter();

        //初期位置、名前等のその他設定
        Doll_data.temper = temper_name;
        Doll_data.Death_year = death_year_;
        Doll_data.Name = name_;
        Doll_data.potition = position_;
        Doll_data.hide_hint = hide_hint_;
        

        //positionスキルのみこちらで設定
        Doll_data.CharaBase_data.Skill.Add(Potition_Skill);

        Doll_data.CharaField_data.Event[0].str = "店の倉庫のカギを使い大倉庫へ向え";
        Doll_data.CharaField_data.Event[1].str = "記憶を孤高のドールへ渡せ";
    }

    //宝物入力関数
    public void SetTreasure(string name, int i)
    {
        switch (Treasure_num)
        {
            case HEAD:
                Doll_data.CharaBase_data.HeadParts.Remove(backTreasure);
                break;
            case ARM:
                Doll_data.CharaBase_data.ArmParts.Remove(backTreasure);
                break;
            case BODY:
                Doll_data.CharaBase_data.BodyParts.Remove(backTreasure);
                break;
            case LEG:
                Doll_data.CharaBase_data.LegParts.Remove(backTreasure);
                break;
            //初期設定
            case -1:
                break;
        }


        Treasure.Name = name;

        backTreasure = Treasure;
        Treasure_num = i;
    }

}
