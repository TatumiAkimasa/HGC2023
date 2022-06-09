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
}
