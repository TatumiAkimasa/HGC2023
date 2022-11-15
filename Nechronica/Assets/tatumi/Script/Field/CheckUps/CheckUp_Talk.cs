using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckUp_Talk : CheckUp_chara
{
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
                    if (Event_OK.Length != 0)
                        Talk_cs.Set_Talkstr(Event_OK);

                }
                else
                {
                    if (Event_OUT.Length != 0)
                    {
                        Talk_cs.Set_Talkstr(Event_OUT);
                    }
                }

                StartCoroutine(Talk_cs.Talk_Set((Talk_End =>
                {
                    talk_now = false;

                    if (item_data != null)
                    {
                        if (EventStart)
                        {
                            Data_Scan.Instance.my_data[0].CharaField_data.Event[Event_num].flag = true;
                            ParentObj.SetActive(false);

                            UI.GetComponent<UI_Chara_status>().Eventstr_Update();
                        }
                    }

                    if(OverStrObj == true)
                    {
                        ParentObj.SetActive(false);
                    }

                    for (int i = 0; i != Talk_End.Length; i++)
                    {
                        Data_Scan.Instance.my_data[0].Item.Add(Talk_End[i]);
                    }

                    if (Change_Scene != "")
                        Scene_change(Change_Scene);

                    item_data = null;

                })));

            }
        }
    }
}

    

