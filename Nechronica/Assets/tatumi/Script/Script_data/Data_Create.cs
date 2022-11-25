using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data_Create : Scene_Change
{
   public void StartGameandDataCreate()
   {
        Maneger_Accessor.Instance.chara_Data_Input_cs.ResetErrorText();

        //ƒf[ƒ^ˆ—‚ªˆÙí‚È‚ç
        if (!Maneger_Accessor.Instance.chara_Data_Input_cs.input())
            return;

        Save_Load_data SaveData = GameObject.FindGameObjectWithTag("AllyChara").GetComponent<Save_Load_data>();
        SaveData.ClickButtonSave();
        SaveData.ClickButtonLoad();

        Scene_change("tatumi_world");
   }
}
