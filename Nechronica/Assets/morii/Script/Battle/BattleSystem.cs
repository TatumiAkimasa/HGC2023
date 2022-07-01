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
                //if(�����Ńv���C�A�u���L�������ǂ������f)
                //else if(�����œGNPC���ǂ������f)
                //else //�����܂ŗ����疡��NPC
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
