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
    private GameObject[] allySponeField = new GameObject[5];             // 味方キャラを配置するためのnullObjを事前に用意

    [SerializeField]
    private GameObject[] enemySponeField = new GameObject[5];             // 味方キャラを配置するためのnullObjを事前に用意



    [SerializeField]
    private BattleSystem battleSystem;

    [SerializeField]
    private List<Doll_blu_Nor> charaObject = new List<Doll_blu_Nor>();

    private int charaCount = 0;
    private int enemyCount = 0;

    public List<Doll_blu_Nor> CharaSpone(List<Doll_blu_Nor> array)
    {
        // arrayの情報をcharaObjectに格納
        for(int i=0;i<array.Count;i++)
        {
            charaObject.Add(array[i]);
        }

        for (int i=0;i<charaObject.Count;i++)
        {
            // 敵キャラか味方キャラか判断
            if(charaObject[i].gameObject.CompareTag("AllyChara"))
            {
                switch(charaObject[i].potition)
                {
                    case RAKUEN:
                        charaObject[i].transform.position = allySponeField[RAKUEN].transform.GetChild(charaCount).position;
                        charaCount++;
                        break;
                    case HANAZONO:
                        charaObject[i].transform.position = allySponeField[HANAZONO].transform.GetChild(charaCount).position;
                        charaCount++;
                        break;
                    case RENGOKU:
                        charaObject[i].transform.position = allySponeField[RENGOKU].transform.GetChild(charaCount).position;
                        charaCount++;
                        break;
                }
            }
            if(charaObject[i].gameObject.CompareTag("EnemyChara"))
            {
                switch (charaObject[i].potition)
                {
                    case RAKUEN:
                        charaObject[i].transform.position = enemySponeField[RAKUEN].transform.GetChild(enemyCount).position;
                        enemyCount++;
                        break;
                    case HANAZONO:
                        charaObject[i].transform.position = enemySponeField[HANAZONO].transform.GetChild(enemyCount).position;
                        enemyCount++;
                        break;
                    case RENGOKU:
                        charaObject[i].transform.position = enemySponeField[RENGOKU].transform.GetChild(enemyCount).position;
                        enemyCount++;
                        break;
                    case JIGOKU:
                        charaObject[i].transform.position = enemySponeField[JIGOKU].transform.GetChild(enemyCount).position;
                        enemyCount++;
                        break;
                    case NARAKU:
                        charaObject[i].transform.position = enemySponeField[NARAKU].transform.GetChild(enemyCount).position;
                        enemyCount++;
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