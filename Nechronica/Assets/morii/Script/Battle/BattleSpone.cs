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

    private int[] charaCount = new int[5];         // 味方キャラ表示エリアのどこに表示するか判断するよう
    private int[] enemyCount = new int[5];         // 敵キャラ表示エリアのどこに表示するか判断するよう

    private void Awake()
    {
        ManagerAccessor.Instance.battleSpone = this;
    }

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
                switch(charaObject[i].area)
                {
                    case RAKUEN:
                        charaObject[i].transform.position = allySponeField[RAKUEN].transform.GetChild(charaCount[RAKUEN]).position;
                        charaCount[RAKUEN]++;
                        break;
                    case HANAZONO:
                        charaObject[i].transform.position = allySponeField[HANAZONO].transform.GetChild(charaCount[HANAZONO]).position;
                        charaCount[HANAZONO]++;
                        break;
                    case RENGOKU:
                        charaObject[i].transform.position = allySponeField[RENGOKU].transform.GetChild(charaCount[RENGOKU]).position;
                        charaCount[RENGOKU]++;
                        break;
                }
            }
            if(charaObject[i].gameObject.CompareTag("EnemyChara"))
            {
                switch (charaObject[i].area)
                {
                    case RAKUEN:
                        charaObject[i].transform.position = enemySponeField[RAKUEN].transform.GetChild(enemyCount[RAKUEN]).position;
                        enemyCount[RAKUEN]++;
                        break;
                    case HANAZONO:
                        charaObject[i].transform.position = enemySponeField[HANAZONO].transform.GetChild(enemyCount[HANAZONO]).position;
                        enemyCount[HANAZONO]++;
                        break;
                    case RENGOKU:
                        charaObject[i].transform.position = enemySponeField[RENGOKU].transform.GetChild(enemyCount[RENGOKU]).position;
                        enemyCount[RENGOKU]++;
                        break;
                    case JIGOKU:
                        charaObject[i].transform.position = enemySponeField[JIGOKU].transform.GetChild(enemyCount[JIGOKU]).position;
                        enemyCount[JIGOKU]++;
                        break;
                    case NARAKU:
                        charaObject[i].transform.position = enemySponeField[NARAKU].transform.GetChild(enemyCount[NARAKU]).position;
                        enemyCount[NARAKU]++;
                        break;
                }
            }

        }

        return charaObject;
    }

    /// <summary>
    /// キャラを移動させるメソッド
    /// </summary>
    /// <param name="chara">移動するキャラ</param>
    /// <param name="moveAmount">移動量</param>
    public void CharaMove(Doll_blu_Nor chara, int moveAmount)
    {
        if(chara.gameObject.CompareTag("AllyChara"))
        {
            chara.transform.position = allySponeField[chara.area + moveAmount].transform.GetChild(charaCount[chara.area + moveAmount]).position;
            // 現エリアから移動するので、現エリアのキャラ存在カウントを1つ減らす
            charaCount[chara.area]--;
            // 移動した後のエリアのキャラ存在カウントを1つ増やす
            charaCount[chara.area + moveAmount]++;
            chara.area = chara.area + moveAmount;
        }
        else if (chara.gameObject.CompareTag("EnemyChara"))
        {
            chara.transform.position = enemySponeField[chara.area + moveAmount].transform.GetChild(charaCount[chara.area + moveAmount]).position;
            // 現エリアから移動するので、現エリアのキャラ存在カウントを1つ減らす
            charaCount[chara.area]--;
            // 移動した後のエリアのキャラ存在カウントを1つ増やす
            chara.area = chara.area + moveAmount;
        }
    }
}

[System.Serializable]
class NullCharaObj
{
    public GameObject[] chara = new GameObject[4];
}