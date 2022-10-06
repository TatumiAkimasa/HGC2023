using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    // �L�����̃I�u�W�F�N�g���ǂꂾ�����邩������ϐ�
    [SerializeField]
    private GameObject[] CharaObjectsBuffer;

    [SerializeField]
    private List<Doll_blu_Nor> CharaObject = new List<Doll_blu_Nor>();

    private List<CharaBase> MoveChara = new List<CharaBase>();

    private int NowCount = 20;

    // Start is called before the first frame update
    void Start()
    {
        // Chara�Ƃ����^�O�������L���������ׂĎ擾
        CharaObjectsBuffer = GameObject.FindGameObjectsWithTag("PlayableChara");
        for (int i = 0; i < CharaObjectsBuffer.Length; i++) 
        {
            // �L����
            CharaObject.Add(CharaObjectsBuffer[i].GetComponent<Doll_blu_Nor>());
        }

        for(int i=0;i<CharaObject.Count;i++)
        {
            Debug.Log(CharaObject[i].GetHeadParts()[0].Name);
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
