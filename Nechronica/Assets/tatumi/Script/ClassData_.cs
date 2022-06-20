using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassData_ : MonoBehaviour
{
    public class ClassData
    {
        public string name;
        public int[] parts = new int[3];
    }

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
