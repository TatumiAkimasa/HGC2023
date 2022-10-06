using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSystem : MonoBehaviour
{
    // �L�����̃I�u�W�F�N�g���ǂꂾ�����邩������ϐ�
    [SerializeField]
    private GameObject[] CharaObjectsBuffer;

    [SerializeField]
    private List<Doll_blu_Nor> CharaObject = new List<Doll_blu_Nor>();

    private List<CharaBase> MoveChara = new List<CharaBase>();

    private int NowCount = 20;

    //�J�E���g�̏��ɃL������\�����邽�߂̃v���n�u
    [SerializeField]
    private IndiCountChara PrefubcountChara;
    [SerializeField]
    private IndiCountChara CloneCountChara;

    //�N���[�������v���n�u�̐e�ƂȂ鑶��
    [SerializeField]
    private GameObject parent;

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

        for (int i = 0; i < CharaObject.Count; i++)
        {
            Vector3 pos = parent.transform.position;
            PrefubcountChara.Name.text = CharaObject[i].Name;
            //CloneCountChara = Instantiate<IndiCountChara>(PrefubcountChara, parent.transform);
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

//���N���X�B�L�����摜�Ɩ��O�̃N���X���K�v�Ȃ̂ł������񂱂��ɍ��B
class IndiCountChara
{
    public Text Name;
    public Image Img;
}
