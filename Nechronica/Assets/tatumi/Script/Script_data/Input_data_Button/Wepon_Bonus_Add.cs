using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Wepon_Bonus_Add : MonoBehaviour
{
   

    [SerializeField]
    private int myType;

    public void Bounus_Wepon_add(int num)
    {
        Maneger_Accessor.Instance.weponManeger_cs.Bonus_Parts_int[myType] = num;
        Debug.Log("");
    }


}
