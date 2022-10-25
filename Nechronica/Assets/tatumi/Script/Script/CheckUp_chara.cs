using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckUp_chara : MonoBehaviour
{
    [SerializeField]
    protected Talk_Chara Talk_cs;

    protected GameObject PL;

    protected bool talk_now=false;
   
    protected void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);

        if (other.gameObject.tag == "Player")
        {
            PL = other.gameObject;
        }
    }

    protected void OnTriggerExit(Collider Item)
    {
        Debug.Log(Item.name);

        if (Item.gameObject.tag == "Player")
        {
            PL = null;
        }
    }

    
}


