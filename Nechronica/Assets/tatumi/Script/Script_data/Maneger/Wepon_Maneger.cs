using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class MultiSite
{
    [Header("個数")]
    public Text[] Text=new Text [2];

    public MultiSite(Text[] _multisite)
    {
        Text = _multisite;
    }
}

[System.Serializable]
public class MultiSite2
{
    [Header("段")]
    public MultiSite[] Step=new MultiSite[3];

    public MultiSite2(MultiSite[] _multisite)
    {
        Step = _multisite;
    }
}

public class Wepon_Maneger : ClassData_
{
    [SerializeField, Header("多次元配列-部位")]
    public MultiSite2[] Site_;

    [SerializeField]
    //武器種類/レベル/持てる限度個数
    Toggle[,,] Wepon = new Toggle[3,3,3];

    [Header("不足パーツ種類Text")]
    public Text[] ErrorText;

    public int[] Wepon_limit = new int[3];

    private int Reset_num = 0;

    private const int MAX_BONUS_WEPON = 3;
    private const int MAX_WEPON_NUM = 3;

    public int [] Bonus_Parts_int = new int [MAX_BONUS_WEPON + 1];
  

    // Start is called before the first frame update
    void Awake()
    {
        Maneger_Accessor.Instance.weponManeger_cs = this;
    }

    private int Bounus_Parts(int Level,int Type)
    {
        if (Level == 2)
            return Type;
        else
            return 3;
    }

    public void Add_Wepon(Toggle add,int Type,int Level)
    {
        //それぞれ選べないものを選択下ときに何もしないようにする計算用変数
        int Max_Wepon_num = (Wepon_limit[Type] - 1) / 3;
        int parts_num_add = (Wepon_limit[Type] - 1) % 3;
        //その分野・レベルで選べれる数保存用変数
        int Serect_parts = 1;

        //そもそも値が0なら何もしない
        if (Wepon_limit[Type] == 0)
        {
            add.isOn = false;
            return;
        }
        //値1の時2,3選んだら排除
        else if (Wepon_limit[Type] < Level + Max_Wepon_num + 1)
        {
            add.isOn = false;
            return;
        }

        if(parts_num_add>=Level)
        {
            Serect_parts += Max_Wepon_num;
        }
            
        //                  　選べる数    + Skillによる増加数
        for (int i = 0; i != Serect_parts + Bonus_Parts_int[Bounus_Parts(Level,Type)]; i++)
        {
            //データなしの場合
            if (Wepon[Type, Level, i] == null)
            {
                NameChange(add, true);
                Wepon[Type, Level, i] = add;

                WeponLimit_TextChange();
                return;
            }
            //2回目の時情報を抜く
            else if(Wepon[Type, Level, i]==add)
            {
                NameChange(add, false);
                Wepon[Type, Level, i].isOn = false;
                Wepon[Type, Level, i] = null;

                WeponLimit_TextChange();
                return;
            }
           
        }

       

        if (Serect_parts + Bonus_Parts_int[Bounus_Parts(Level, Type)] <= Reset_num)
            Reset_num = 0;

        //もし限度数かつ＋で追加したら
        //既存情報を抜いて
        NameChange(Wepon[Type, Level, Reset_num], false);
        Wepon[Type, Level, Reset_num].isOn = false;
        Wepon[Type, Level, Reset_num] = null;

        //新規情報を入力
        Wepon[Type, Level, Reset_num] = add;
        NameChange(Wepon[Type, Level, Reset_num], true);

        Reset_num++;
        WeponLimit_TextChange();
    }

    public void Reset_wepon(int Type)
    {
        //リセット
        for (int Level = 0; Level != 3; Level++)
        {
            
            for (int i = 0; i != MAX_WEPON_NUM; i++)
            {
                if (Wepon[Type, Level, i] != null)
                {
                    NameChange(Wepon[Type, Level, i], false);
                    Wepon[Type, Level, i].isOn = false;
                    Wepon[Type, Level, i] = null;
                }

            }

        }

        Reset_num = 0;
    }

    private void NameChange(Toggle wepon,bool mode)
    {
        int SITE = -1;

        //腕
        if(wepon.name.EndsWith("A"))
        {
            SITE = ARM;
        }
        //頭
        else if (wepon.name.EndsWith("H"))
        {
            SITE = HEAD;
        }
        //胴
        else if (wepon.name.EndsWith("B"))
        {
            SITE = BODY;
        }
        //脚
        else if (wepon.name.EndsWith("L"))
        {
            SITE = LEG;
        }
        //全部位
        else if (wepon.name.EndsWith("S"))
        {
            return;
        }

        for (int i = 0; i != 2; i++)
        {
            for (int k = 0; k != 3; k++)
            {
                //入力
                if (mode == true)
                {
                    //情報が無ければ
                    if (Site_[SITE].Step[i].Text[k].text == "None")
                    {
                        Site_[SITE].Step[i].Text[k].text = wepon.GetComponent<WeponData_Set>().Get_Wepon_Text();
                        Site_[SITE].Step[i].Text[k].GetComponent<Wepon_Data_SaveSet>().SetParts(wepon.GetComponent<Wepon_Data_SaveSet>().GetParts());
                        return;
                    }
                    //あればするー。
                }
                //キャンセル
                else
                {
                    //特定情報があれば初期化
                    if (Site_[SITE].Step[i].Text[k].text == wepon.GetComponent<WeponData_Set>().Get_Wepon_Text())
                    {
                        Site_[SITE].Step[i].Text[k].text = "None";
                        Site_[SITE].Step[i].Text[k].GetComponent<Wepon_Data_SaveSet>().Reset();
                        return;
                    }
                    //なければするー。
                }
            }
        }
    }

    private void WeponLimit_TextChange()
    {
        for(int Type = 0; Type!=3;Type++)
        {
            for(int Level=0;Level!=3;Level++)
            {
                //それぞれ選べないものを選択下ときに何もしないようにする計算用変数
                int Max_Wepon_num = (Wepon_limit[Type] - 1) / 3;
                int parts_num_add = (Wepon_limit[Type] - 1) % 3;
                //その分野・レベルで選べれる数保存用変数
                int Serect_parts = 1;

                //そもそも値が0なら何もしない
                if (Wepon_limit[Type] == 0)
                {
                    Serect_parts = 0;
                }
                //値1の時2,3選んだら排除
                else if (Wepon_limit[Type] < Level + Max_Wepon_num + 1)
                {
                    Serect_parts = 0;
                }

                if (parts_num_add >= Level)
                {
                    Serect_parts += Max_Wepon_num + Bounus_Parts(Level,Type);
                }

                for(int i=0;i!=3;i++)
                {
                    if (Wepon[Type, Level, i] != null)
                        Serect_parts--;
                }
                
                ErrorText[Type + Level].text = "LEVEL" + Level.ToString() + ":"+Serect_parts.ToString();
                
            }
        }
    }
}
