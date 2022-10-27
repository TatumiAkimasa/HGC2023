using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CheckUp_Event : CheckUp_chara
{
    [SerializeField]
    private GameObject ParentObj,UI;

    [SerializeField,Header("必要アイテム名")]
    private string Item_Name;

    [SerializeField, Header("対応イベント番号")]
    private int Event_num;

    [SerializeField, Header("成功時の台詞")]
    private string Event_OK;
    [SerializeField, Header("失敗時の台詞")]
    private string Event_OUT;

    void Update()
    {
        if (PL != null)
        {
            if (Input.GetKeyDown(KeyCode.E) && talk_now == false)
            {
                talk_now = true;

                var Event = Data_Scan.Instance.my_data[0].Item.Find(item => item.Tiltle == Item_Name);

                if (Event != null)
                {
                    
                    Talk_cs.Set_Itemstr(Event_OK);
                  
                }
                else
                {
                    Talk_cs.Set_Itemstr(Event_OUT);
                }

                StartCoroutine(Talk_cs.Talk_Set((Talk_End =>
                {
                    talk_now = false;

                    if (Event != null)
                    {
                        Data_Scan.Instance.my_data[0].CharaField_data.Event[Event_num].flag = true;
                        ParentObj.SetActive(false);
                        
                        UI.GetComponent<UI_Chara_status>().Eventstr_Update();
                    }

                    for (int i = 0; i != Talk_End.Length; i++)
                    {
                        Data_Scan.Instance.my_data[0].Item.Add(Talk_End[i]);
                    }

                    
                })));

            }
        }
    }
}
