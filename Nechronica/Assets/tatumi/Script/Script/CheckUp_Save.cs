using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckUp_Save : CheckUp_chara
{
    [SerializeField]
    private GameObject SaveRoad;

    void Update()
    {
        if (PL != null)
        {
            if (Input.GetKeyDown(KeyCode.E) && talk_now == false)
            {
                talk_now = true;

                SaveRoad.SetActive(true);
             
            }
        }
    }

    public void Set_talknow(bool set)
    {
        talk_now = set;
    }
}
