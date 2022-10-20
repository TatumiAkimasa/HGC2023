using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSpone : MonoBehaviour
{
    // íËêî---------------------------
    public const int RAKUEN   = 0;
    public const int HANAZONO = 1;
    public const int RENGOKU  = 2;
    public const int JIGOKU   = 3;
    public const int NARAKU   = 4;

    //--------------------------------

    [SerializeField]
    private GameObject[,] sponeAlly = new GameObject[5,4];

    [SerializeField]
    private BattleSystem battleSystem;

    [SerializeField]
    private List<Doll_blu_Nor> charaObject = new List<Doll_blu_Nor>();
    
    public void SetCharaObject(Doll_blu_Nor[] array)
    {
        charaObject.CopyTo(array);
    }
    
}
