using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Input_data_ClassButton : Input_data_Button
{
    [SerializeField]
    private GameObject PA_Maneger;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Push_button_Class(bool MtoS)
    {
        PA_Maneger.GetComponent<WeponManeger>().Setparts(Push_button_class(),MtoS);
    }
}
