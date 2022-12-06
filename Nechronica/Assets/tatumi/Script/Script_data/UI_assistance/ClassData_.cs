using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassData_ : MonoBehaviour
{
    public struct ClassData 
    {
        
        public string name;
        public int[] parts;
    }

    //定数
    protected const int ARMAMENT = 0;     //武装
    protected  const int VARIANT = 1;      //変異
    protected  const int ALTER = 2;        //改造

    //部位
    protected const int HEAD = 0;
    protected const int ARM = 1;
    protected const int BODY = 2;
    protected const int LEG = 3;

    //エラー
    protected const int NAME = 0;     
    protected const int CLASS = 1;      
    protected const int PARTS = 2;        
    protected const int SKILL = 3;        
    protected const int POTITIONSKILL = 4;
    protected const int POTITION = 5;

    protected const int MAXPARTS = 15;

    public string Classname_JtoE(string a)
    {
        if (a == "ステーシー")
        {
            return "Stacy";

        }
        else if (a == "タナトス")
        {
            return "Thanatos";

        }
        else if (a == "ゴシック")
        {
            return "Gothic";

        }
        else if (a == "レクイエム")
        {
            return "Requiem";

        }
        else if (a == "バロック")
        {
            return "Baroque";

        }
        else if (a == "ロマネスク")
        {
            return "Romanesque";

        }
        else if (a == "サイケデリック")
        {
            return "Psychedelic";
        }
        else
            return "error";
    }
}
