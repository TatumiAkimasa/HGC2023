using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventStart : CheckUp_Base
{
   
    [SerializeField]
    MoveEvent_Chara test;

    void Update()
    {
        if (PL != null && !talk_now)
        {
            talk_now = true;
            StartCoroutine(test.Event((EndTiming =>
            {
                //Event—Ìˆæíœ
                Destroy(this.gameObject);
                //ålŒös“®‰Â”\ˆ—
                test.gameObject.GetComponent<Assist_MoveEvent>().Charas[0].MovePlayer = true;
                test.gameObject.GetComponent<Assist_MoveEvent>().Charas[0].myanim.SetEvent = false;
                
            })));
        }
    }
}
