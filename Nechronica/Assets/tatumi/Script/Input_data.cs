using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Input_data : MonoBehaviour
{
    [SerializeField]
    public InputField inputField;
    public Text text;

    void Start()
    {
        //Component��������悤�ɂ���
        inputField = inputField.GetComponent<InputField>();
        text = text.GetComponent<Text>();

    }

    public void InputText()
    {
        //�e�L�X�g��inputField�̓��e�𔽉f
        text.text = inputField.text;

    }

   
}