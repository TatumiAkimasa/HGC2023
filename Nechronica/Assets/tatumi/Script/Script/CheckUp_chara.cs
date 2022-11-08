using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckUp_chara : Scene_Change
{
    [SerializeField]
    protected Talk_Chara Talk_cs;

    protected GameObject PL;

    protected bool talk_now = false;

    [SerializeField,Header("シーン移動用変数(無でも可能)")]
    protected string Change_Scene;

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


