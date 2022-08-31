using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Input_data : MonoBehaviour
{
    [SerializeField]
    public InputField inputField;
    public Text text;

    [SerializeField]
    Doll_blueprint Doll_Script;

   
    void Start()
    {
      
    }

    public void InputText(int ID)
    {
        //テキストにinputFieldの内容を反映
        text.text = inputField.text;

        switch (ID)
        {
            case 1:
                Doll_Script.Name = inputField.text;
                break;
            case 2:
                Doll_Script.Death_year = inputField.text;
                break;
            case 3:
                //入力手段用意（対応パーツ選択）
                int i = 1;
                Doll_Script.SetTreasure(inputField.text, i);
                break;
        }
        
    }

   
}