using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Wepon_Bonus_Add : MonoBehaviour
{
    [SerializeField]
    Wepon_Maneger wepon_cs;

    [SerializeField]
    private int myType;

    public void Bounus_Wepon_add(int num)
    {
        wepon_cs.Bonus_Parts_int[myType] = num;
        Debug.Log("");
    }


}
