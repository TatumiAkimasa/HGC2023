using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_Fleld : MonoBehaviour
{
   
    [SerializeField]
    private GameObject TargetObjs;

    public enum Layers
    {
        FX = 1,
        InHouse = 3,
        SkeletonWall = 7
    }

    [SerializeField]
    private Layers Inlay,Outlay;

    private void OnTriggerEnter(Collider Item)
    {
        if (Item.tag == "Player")
        {

            // すべての子オブジェクトを取得
            foreach (Transform n in TargetObjs.transform)
            {
               n.gameObject.layer = (int)Inlay;
            }

        }
    }

    private void OnTriggerExit(Collider Item)
    {
        if (Item.tag == "Player")
        {
            // すべての子オブジェクトを取得
            foreach (Transform n in TargetObjs.transform)
            {
                n.gameObject.layer = (int)Outlay;
            }

        }
    }
}
