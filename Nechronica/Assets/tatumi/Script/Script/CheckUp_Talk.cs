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

                StartCoroutine(Talk_cs.Talk_Set((Talk_End =>
                {
                    talk_now = true;

                    for (int i = 0; i != Talk_End.Length; i++)
                    {
                        Data_Scan.Instance.my_data[0].Item.Add(Talk_End[i]);
                    }
                })));

            }
        }
    }
}
