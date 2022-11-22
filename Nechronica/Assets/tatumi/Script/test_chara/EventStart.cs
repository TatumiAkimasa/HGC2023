using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventStart : CheckUp_Base
{
   
    [SerializeField]
    MoveEvent_Chara test;

    void Update()
    {
        if (PL != null)
        {
            StartCoroutine(test.Event());
        }
    }
}
