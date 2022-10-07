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

    private List<Doll_blu_Nor> MoveChara = new List<Doll_blu_Nor>();

    private int NowCount = 20;

    [SerializeField]
    private Text CountText;

    //�J�E���g�̏��ɃL������\�����邽�߂̃v���n�u
    [SerializeField]
    private GameObject OriginCntPrefab;    //�����̌��ƂȂ�v���n�u

    private List<GameObject> CloneCntPrefab = new List<GameObject>();  

    

    //�N���[�������v���n�u�̐e�ƂȂ鑶��
    [SerializeField]
    private GameObject parent;

    // Start is called before the first frame update
    void Start()
    {
        CountText.text = NowCount.ToString();
        // Chara�Ƃ����^�O�������L���������ׂĎ擾
        CharaObjectsBuffer = GameObject.FindGameObjectsWithTag("PlayableChara");
        for (int i = 0; i < CharaObjectsBuffer.Length; i++)
        {
            // �L����
            CharaObject.Add(CharaObjectsBuffer[i].GetComponent<Doll_blu_Nor>());
        }

        // �f�o�b�O�p�ɃX�^�[�g�ɏ����B�{����TurnStart�ɔz�u�B
        for (int i = 0; i < CharaObject.Count; i++)
        {
            CharaObject[i].IncreaseNowCount();
            Debug.Log(CharaObject[i].GetNowCount());
        }
    }

    /// <summary>
    /// �^�[���J�n���ɓ��郁�\�b�h
    /// </summary>
    public void TurnStart()
    {
        
        CountText.text = NowCount.ToString();
        IndicateMoveChara();
    }

    /// <summary>
    /// �^�[���I�����ɓ��郁�\�b�h
    /// </summary>
    public void TurnLast()
    {
        NowCount--;
        if(NowCount==0)
        {
            NowCount += 20;
        }
        TurnStart();
    }

    /// <summary>
    /// �\���J�E���g�ōs���ł���L������\��
    /// </summary>
    public void IndicateMoveChara()
    {
        for (int i = 0; i < CharaObject.Count; i++)
        {
            if(NowCount==CharaObject[i].GetNowCount())
            {
                // �N���[�������pGameObject
                GameObject Instance;

                Instance = Instantiate(OriginCntPrefab);
                Instance.GetComponent<IndiCountChara>();
                IndiCountChara clone = Instance.GetComponent<IndiCountChara>();
                clone.SetName(CharaObject[i].GetName());
                clone.transform.parent = parent.transform;

                //���X�g�ɕۑ�
                CloneCntPrefab.Add(Instance);
            }
        }
    }

    /// <summary>
    /// �o�g�����n�܂����Ƃ��ɌĂяo����郁�\�b�h�iStart�ł����������Ă�j
    /// </summary>
    public void ButtleStart()
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
}