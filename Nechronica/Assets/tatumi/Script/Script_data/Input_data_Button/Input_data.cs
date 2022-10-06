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
    Chara_data_input Chara_data_input_cs;

    public int Treasure_pos_num=0;
   
    void Start()
    {
      
    }

    public void InputText(int ID)
    {
        //�e�L�X�g��inputField�̓��e�𔽉f
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
                //���͎�i�p�Ӂi�Ή��p�[�c�I���j
                Chara_data_input_cs.SetTreasure(inputField.text, Treasure_pos_num);
                break;
        }
        
    }

   public void Get_Treasure_pos(Dropdown pos)
    {
        Treasure_pos_num = pos.value;
    }

}