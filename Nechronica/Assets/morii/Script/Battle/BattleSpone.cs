using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BattleSpone : MonoBehaviour
{
    // 定数---------------------------
    public const int RAKUEN   = 4;
    public const int HANAZONO = 3;
    public const int RENGOKU  = 2;
    public const int JIGOKU   = 1;
    public const int NARAKU   = 0;

    //--------------------------------

    [SerializeField]
    private GameObject[] sponeField = new GameObject[5];             // 味方キャラを配置するためのnullObjを事前に用意

    [SerializeField]
    private BattleSystem battleSystem;

    [SerializeField]
    private List<Doll_blu_Nor> charaObject = new List<Doll_blu_Nor>();

    public List<Doll_blu_Nor> CharaSpone(List<Doll_blu_Nor> array)
    {
        // arrayの情報をcharaObjectに格納
        for(int i=0;i<array.Count;i++)
        {
            charaObject.Add(array[i]);
        }

        for (int i=0;i<charaObject.Count;i++)
        {
            if(charaObject[i].gameObject.CompareTag("AllyChara"))
            {
                switch(charaObject[i].potition)
                {
                    case RAKUEN:
                        charaObject[i].transform.position = sponeField[RAKUEN].transform.GetChild(0).position;
                        break;
                    case HANAZONO:
                        charaObject[i].transform.position = sponeField[HANAZONO].transform.GetChild(0).position;
                        break;
                    case RENGOKU:
                        charaObject[i].transform.position = sponeField[RENGOKU].transform.GetChild(0).position;
                        break;
                }
            }
        }

        return charaObject;
    }

}

[System.Serializable]
class NullCharaObj
{
    public GameObject[] chara = new GameObject[4];
}