using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Input_data_Button : MonoBehaviour
{
    //�萔
    const int ARMAMENT = 0;     //����
    const int VARIANT = 1;      //�ψ�
    const int ALTER = 2;        //����

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

        if (input_text.text == "�A���X")
            return "Alis";
        else if (input_text.text == "�z���b�N")
            return "Holic";
        else if (input_text.text == "�I�[�g�}�g��")
            return "automaton";
        else if (input_text.text == "�R�[�g")
            return "coat";
        else if (input_text.text == "�W�����N")
            return "junk";
        else if (input_text.text == "�\�����e�B")
            return "Sorority";
        else
            return "error";
;
    }

    public int[] Push_button_class()
    {
        output_text.text = input_text.text;

        int[] Class_num = new int[4];

        if (input_text.text == "�X�e�[�V�[")
        {
            Class_num[0] = 1;
            Class_num[1] = 1;
            Class_num[2] = 0;
            Class_num[3] = 0;
            return Class_num;
        }
        else if (input_text.text == "�^�i�g�X")
        {
            Class_num[0] = 1;
            Class_num[1] = 0;
            Class_num[2] = 1;
            Class_num[3] = 1;
            return Class_num;
        }
        else if (input_text.text == "�S�V�b�N")
        {
            Class_num[0] = 0;
            Class_num[1] = 1;
            Class_num[2] = 1;
            Class_num[3] = 2;
            return Class_num;
        }
        else if (input_text.text == "���N�C�G��")
        {
            Class_num[0] = 2;
            Class_num[1] = 0;
            Class_num[2] = 0;
            Class_num[3] = 3;
            return Class_num;
        }
        else if (input_text.text == "�o���b�N")
        {
            Class_num[0] = 0;
            Class_num[1] = 2;
            Class_num[2] = 0;
            Class_num[3] = 4;
            return Class_num;
        }
        else if (input_text.text == "���}�l�X�N")
        {
            Class_num[0] = 0;
            Class_num[1] = 0;
            Class_num[2] = 2;
            Class_num[3] = 5;
            return Class_num;
        }
        else if (input_text.text == "�T�C�P�f���b�N")
        {
            Class_num[0] = 0;
            Class_num[1] = 0;
            Class_num[2] = 1;
            Class_num[3] = 6;
            return Class_num;
        }
        else
        {
            Class_num[0] = 0;
            Class_num[1] = 0;
            Class_num[2] = 0;
            Class_num[3] = 0;
            return Class_num;
        }
        
    }
}

