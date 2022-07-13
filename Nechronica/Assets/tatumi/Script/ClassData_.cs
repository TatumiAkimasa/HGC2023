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
    public const int ARMAMENT = 0;     //武装
    public const int VARIANT = 1;      //変異
    public const int ALTER = 2;        //改造

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
