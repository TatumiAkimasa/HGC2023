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

    public int[] Wepon_limit = new int[3];

    public int Reset_num = 0;

   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Add_Wepon(Toggle add,int Type,int Level)
    {
        if (Wepon_limit[Type] == 0)
        {
            add.isOn = false;
            return;
        }
           

        int Max_Wepon_num = Wepon_limit[Type] / 3;

        int parts_num_add = Wepon_limit[Type] % 3;

        if(Max_Wepon_num==0)
        {
            if (parts_num_add < Level+1)
            {
                add.isOn = false;
                return;
            }
                
        }
         
        for (int i = 0; i != Max_Wepon_num + (1-(Max_Wepon_num / 3)); i++)
        {
            //データなしの場合
            if (Wepon[Type, Level, i] == null)
            {
                NameChange(add, true);
                Wepon[Type, Level, i] = add;
               
                return;
            }
            //2回目の時情報を抜く
            else if(Wepon[Type, Level, i]==add)
            {
                NameChange(add, false);
                Wepon[Type, Level, i].isOn = false;
                Wepon[Type, Level, i] = null;
               
                return;
            }
           
        }

       

        if (Max_Wepon_num + (1 - (Max_Wepon_num / 3)) == Reset_num)
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
                        Site_[SITE].Step[i].Text[k].GetComponent<Wepon_Data_SaveSet>().SetParts(wepon.GetComponent<Wepon_Data_SaveSet>().GetPrats());
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
}
