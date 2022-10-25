using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Input_data_ClassButton : Input_data_Button
{
   
    public void Push_button_Class(bool MtoS)
    {
        Maneger_Accessor.Instance.skillManeger_cs.Setparts(Push_button_class(),MtoS);
    }

    public void Parent_Set(int MtoS)
    {
        Maneger_Accessor.Instance.skillManeger_cs.SetParent_SKill(this.GetComponent<Button>(), MtoS);
    }
}
