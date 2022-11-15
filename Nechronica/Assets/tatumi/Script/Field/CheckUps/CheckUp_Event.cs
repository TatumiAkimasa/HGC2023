using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CheckUp_Event : CheckUp_chara
{
   
    [SerializeField, Header("イベントを進ませるか")]
    private bool Event_Go;

 
    void Update()
    {
        if (PL != null)
        {
            if (Input.GetKeyDown(KeyCode.E) && talk_now == false)
            {
                talk_now = true;
                item_data = Data_Scan.Instance.my_data[0].Item.Find(item => item.Tiltle == Item_Name);

                if (item_data != null)
                {
                    //今の所一行のみ
                    Talk_cs.Set_Itemstr(Event_OK[0]);
                  
                }
                else
                {
                    Talk_cs.Set_Itemstr(Event_OUT[0]);
                }

                StartCoroutine(Talk_cs.Talk_Set((Talk_End =>
                {
                    talk_now = false;

                    if (item_data != null)
                    {
                        if(Event_Go==true)
                        {
                            Data_Scan.Instance.my_data[0].CharaField_data.Event[Event_num].flag = true;
                            UI.GetComponent<UI_Chara_status>().Eventstr_Update();
                        }
                       
                        ParentObj.SetActive(false);
                        
                    }

                    for (int i = 0; i != Talk_End.Length; i++)
                    {
                        Data_Scan.Instance.my_data[0].Item.Add(Talk_End[i]);
                    }

                    if (OverStrObj == true)
                    {
                        ParentObj.SetActive(false);
                    }

                    if (Change_Scene!="")
                    Scene_change(Change_Scene);

                    item_data = null;
                })));

            }
        }
    }
}
