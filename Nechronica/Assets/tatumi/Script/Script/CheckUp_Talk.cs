using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckUp_Talk : CheckUp_chara
{
    [SerializeField, Header("�K�v�A�C�e����")]
    private string Item_Name;

    [SerializeField, Header("�C�x���g�ɒǉ����邩")]
    private bool EventStart = false;

    [SerializeField, Header("�Ή��C�x���g�ԍ�")]
    private int Event_num;

    [SerializeField, Header("�������̑䎌")]
    private string[] Event_OK;
    [SerializeField, Header("���s���̑䎌")]
    private string[] Event_OUT;

    [SerializeField]
    private bool Item;

    [SerializeField]
    private GameObject ParentObj, UI;

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
                    if (Event_OK.Length != 0)
                        Talk_cs.Set_Talkstr(Event_OK);

                }
                else
                {
                    Debug.Log("�����؂���");
                    if (Event_OUT.Length != 0)
                    {
                        Talk_cs.Set_Talkstr(Event_OUT);
                    }
                }

                StartCoroutine(Talk_cs.Talk_Set((Talk_End =>
                {
                    talk_now = false;

                    if (Event != null)
                    {
                        if (EventStart)
                        {
                            Data_Scan.Instance.my_data[0].CharaField_data.Event[Event_num].flag = true;
                            ParentObj.SetActive(false);

                            UI.GetComponent<UI_Chara_status>().Eventstr_Update();
                        }
                    }

                    if(Item==true)
                    {
                        ParentObj.SetActive(false);
                    }

                    for (int i = 0; i != Talk_End.Length; i++)
                    {
                        Data_Scan.Instance.my_data[0].Item.Add(Talk_End[i]);
                    }

                    if (Change_Scene != "")
                        Scene_change(Change_Scene);

                })));

            }
        }
    }
}

    

