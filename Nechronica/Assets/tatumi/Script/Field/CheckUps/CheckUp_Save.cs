using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckUp_Save : CheckUp_Base
{
    [SerializeField]
    private GameObject SaveRoad;

    public UI_Chara_SaveLoad UI;

    void Update()
    {
        if (PL != null)
        {
            if (Input.GetKeyDown(KeyCode.E) && talk_now == false)
            {
                talk_now = true;

                SaveRoad.SetActive(true);
                UI.GetComponent<UI_Chara_SaveLoad>().SetSavePos(this.transform.position);
                UI.GetComponent<UI_Chara_SaveLoad>().Set_SaveCell();
               

                if (Change_Scene != "")
                    Scene_change(Change_Scene);

            }
        }
    }

    public void Set_talknow(bool set)
    {
        talk_now = set;
    }
}
