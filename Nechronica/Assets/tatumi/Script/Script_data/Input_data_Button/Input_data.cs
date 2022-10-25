using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Input_data : ClassData_
{
    [SerializeField]
    public InputField inputField;
    public Text text,Tre_pos_text;

    private Chara_data_input Chara_data_input_cs;

    public int Treasure_pos_num=0;
   
    void Start()
    {
        Chara_data_input_cs = Maneger_Accessor.Instance.chara_Data_Input_cs;
    }

    public void InputText(int ID)
    {
        //テキストにinputFieldの内容を反映
        text.text = inputField.text;

        switch (ID)
        {
            case 1:
                Chara_data_input_cs.name_ = inputField.text;
                break;
            case 2:
                Chara_data_input_cs.death_year_ = inputField.text;
                break;
            case 3:
                //入力手段用意（対応パーツ選択）
                Chara_data_input_cs.SetTreasure(inputField.text, Treasure_pos_num);
                break;
        }
        
    }

   public void Get_Treasure_pos(Dropdown pos)
    {
        Treasure_pos_num = pos.value;
        if(pos.value==HEAD)
        {
            Tre_pos_text.text = "頭";
        }
        else if(pos.value == ARM)
        {
            Tre_pos_text.text = "腕";
        }
        else if (pos.value == BODY)
        {
            Tre_pos_text.text = "胴";
        }
        else if (pos.value == LEG)
        {
            Tre_pos_text.text = "脚";
        }

    }

}