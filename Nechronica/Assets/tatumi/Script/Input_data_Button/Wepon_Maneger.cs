using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Wepon_Maneger : ClassData_
{
    [SerializeField]
    //武器種類/レベル/持てる限度個数
    Toggle[,,] Wepon = new Toggle[3,3,3];

    [SerializeField]
    GameObject Content_Wepons;

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
                Wepon[Type, Level, i] = add;
                return;
            }
            //2回目の時情報を抜く
            else if(Wepon[Type, Level, i]==add)
            {
                Wepon[Type, Level, i].isOn = false;
                Wepon[Type, Level, i] = null;
                return;
            }
           
        }

       

        if (Max_Wepon_num + (1 - (Max_Wepon_num / 3)) == Reset_num)
            Reset_num = 0;

        //もし限度数かつ＋で追加したら
        Wepon[Type, Level, Reset_num].isOn = false;
        Wepon[Type, Level, Reset_num] = null;
        Wepon[Type, Level, Reset_num] = add;

        Reset_num++;

    }
}
