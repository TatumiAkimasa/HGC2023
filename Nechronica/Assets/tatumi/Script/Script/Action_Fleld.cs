using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_Fleld : MonoBehaviour
{
   
    [SerializeField]
    private GameObject[] TargetObjs;

    public enum Layers
    {
        Defult = 0,
        InHouse = 3,
        SkeletonWall = 7
    }

    [SerializeField]
    private Layers Inlay,Outlay;

    private void OnTriggerEnter(Collider Item)
    {
        if (Item.tag == "Player")
        {
           
            for(int i=0;i!=TargetObjs.Length;i++)
                TargetObjs[i].layer = (int)Inlay;

        }
    }

    private void OnTriggerExit(Collider Item)
    {
        if (Item.tag == "Player")
        {
            for (int i = 0; i != TargetObjs.Length; i++)
                TargetObjs[i].layer = (int)Outlay;

        }
    }
}
