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
                //��������ׂ肨��������ɂ��������Ƃ���΍��͖���(����PL�̑����t���ۉ��������Ƃ�����)
            })));
        }
    }
}
