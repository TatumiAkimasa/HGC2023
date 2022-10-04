using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject[] CharaObjectsBuffer;

    [SerializeField]
    private List<CharaBase> CharaObject = new List<CharaBase>();

    private List<CharaBase> MoveChara = new List<CharaBase>();

    private int NowCount = 20;

    // Start is called before the first frame update
    void Start()
    {
        CharaObjectsBuffer = GameObject.FindGameObjectsWithTag("Chara");
        for (int i = 0; i < CharaObjectsBuffer.Length; i++) 
        {
            CharaObject.Add(CharaObjectsBuffer[i].GetComponent<CharaBase>());
        }
    }

    void ButtleStart()
    {
        for (int i = 0; i < CharaObject.Count; i++)
        {
            if(NowCount==CharaObject[i].GetNowCount())
            {
                //if(ここでプレイアブルキャラかどうか判断)
                //else if(ここで敵NPCかどうか判断)
                //else //ここまで来たら味方NPC
            }
            else
            {
                NowCount--;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
