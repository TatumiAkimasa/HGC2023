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
                Destroy(this.gameObject);
                //‰½‚©‚µ‚á‚×‚è‚¨‚í‚Á‚½Žž‚É‚µ‚½‚¢‚±‚Æ‚ ‚ê‚Î¡‚Í–³‚µ(‘½•ªPL‚Ì‘€ìŽó•t‹‘”Û‰ðœˆ—‚Æ‚©“ü‚é)
            })));
        }
    }
}
