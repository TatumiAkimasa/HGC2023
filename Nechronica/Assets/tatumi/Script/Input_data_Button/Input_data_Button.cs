using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Input_data_Button : MonoBehaviour
{
    [SerializeField]
    private Text input_text,output_text;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Push_button()
    {
        output_text.text = input_text.text;
    }

    public string Push_button_pos()
    {
        output_text.text = input_text.text;

        if (input_text.text == "アリス")
            return "Alis";
        else if (input_text.text == "ホリック")
            return "Holic";
        else if (input_text.text == "オートマトン")
            return "automaton";
        else if (input_text.text == "コート")
            return "coat";
        else if (input_text.text == "ジャンク")
            return "junk";
        else if (input_text.text == "ソロリティ")
            return "Sorority";
        else
            return "error";
;
    }

    public int[] Push_button_class()
    {
        output_text.text = input_text.text;

        int[] Class_num = new int[3];

        if (input_text.text == "ステーシー")
        {
            Class_num[0] = 1;
            Class_num[1] = 1;
            Class_num[2] = 0;
            return Class_num;
        }
        else if (input_text.text == "タナトス")
        {
            Class_num[0] = 1;
            Class_num[1] = 0;
            Class_num[2] = 1;
            return Class_num;
        }
        else if (input_text.text == "ゴシック")
        {
            Class_num[0] = 0;
            Class_num[1] = 1;
            Class_num[2] = 1;
            return Class_num;
        }
        else if (input_text.text == "レクイエム")
        {
            Class_num[0] = 2;
            Class_num[1] = 0;
            Class_num[2] = 0;
            return Class_num;
        }
        else if (input_text.text == "バロック")
        {
            Class_num[0] = 0;
            Class_num[1] = 0;
            Class_num[2] = 2;
            return Class_num;
        }
        else if (input_text.text == "ロマネスク")
        {
            Class_num[0] = 0;
            Class_num[1] = 0;
            Class_num[2] = 2;
            return Class_num;
        }
        else if (input_text.text == "サイケデリック")
        {
            Class_num[0] = 0;
            Class_num[1] = 0;
            Class_num[2] = 1;
            return Class_num;
        }
        else
        {
            Class_num[0] = 0;
            Class_num[1] = 0;
            Class_num[2] = 0;
            return Class_num;
        }
        
    }
}

