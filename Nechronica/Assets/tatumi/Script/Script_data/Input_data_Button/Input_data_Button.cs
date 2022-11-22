using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Input_data_Button : ClassData_
{
    [SerializeField]
    private Text input_text, output_text;

    protected Chara_data_input Chara_intput_data_cs;

    private ClassData data;

    // Start is called before the first frame update
    void Start()
    {
        Chara_intput_data_cs = Maneger_Accessor.Instance.chara_Data_Input_cs;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Push_button()
    {
        output_text.text = input_text.text;

        //�|�W�V����SKILL�p�^�[��
        if (this.GetComponent<Wepon_Data_SaveSet>() != null)
        {
            Chara_intput_data_cs.SetPotionSkill_(this.GetComponent<Wepon_Data_SaveSet>().GetParts());
            Chara_intput_data_cs.SetErrorData(POTITIONSKILL, false);
        }
    }

    public void Push_button(int pos)
    {
        output_text.text = input_text.text;

        //position�p�^�[��
        Chara_intput_data_cs.SetPosition_((short)pos);
        Chara_intput_data_cs.SetErrorData(POTITION, false);
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

    public ClassData Push_button_class()
    {
        data.parts = new int[3];
        output_text.text = input_text.text;

        //ClassData data;

        if (input_text.text == "�X�e�[�V�[")
        {
            data.parts[ARMAMENT] = 1;
            data.parts[VARIANT] = 1;
            data.parts[ALTER] = 0;

        }
        else if (input_text.text == "�^�i�g�X")
        {
            data.parts[ARMAMENT] = 1;
            data.parts[VARIANT] = 0;
            data.parts[ALTER] = 1;

        }
        else if (input_text.text == "�S�V�b�N")
        {
            data.parts[ARMAMENT] = 0;
            data.parts[VARIANT] = 1;
            data.parts[ALTER] = 1;

        }
        else if (input_text.text == "���N�C�G��")
        {
            data.parts[ARMAMENT] = 2;
            data.parts[VARIANT] = 0;
            data.parts[ALTER] = 0;

        }
        else if (input_text.text == "�o���b�N")
        {
            data.parts[ARMAMENT] = 0;
            data.parts[VARIANT] = 2;
            data.parts[ALTER] = 0;

        }
        else if (input_text.text == "���}�l�X�N")
        {
            data.parts[ARMAMENT] = 0;
            data.parts[VARIANT] = 0;
            data.parts[ALTER] = 2;

        }
        else if (input_text.text == "�T�C�P�f���b�N")
        {
            data.parts[ARMAMENT] = 0;
            data.parts[VARIANT] = 0;
            data.parts[ALTER] = 1;

        }
        else
        {
            data.parts[ARMAMENT] = 0;
            data.parts[VARIANT] = 0;
            data.parts[ALTER] = 0;
        }

        data.name = input_text.text;
        return data;
    }

}



