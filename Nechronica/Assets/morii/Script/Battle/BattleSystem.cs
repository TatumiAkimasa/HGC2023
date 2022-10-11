using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSystem : MonoBehaviour
{
    // �萔
    const int ACTION = 0;

    // �L�����̃I�u�W�F�N�g���擾����ϐ�
    [SerializeField]
    private GameObject[] CharaObjectsBuffer;

    // ���ۂɎg�p����N���X
    [SerializeField]
    private List<Doll_blu_Nor> CharaObject = new List<Doll_blu_Nor>();


    private GameObject MoveChara;

    // �J�E���g�֘A-----------------------------------------------------

    private int NowCount = 20;

    [SerializeField]
    private Text CountText;

    //�J�E���g�̏��ɃL������\�����邽�߂̃v���n�u
    [SerializeField]
    private GameObject OriginCntPrefab;    //�����̌��ƂȂ�v���n�u

    private List<GameObject> CloneCntPrefab = new List<GameObject>();  

    //�N���[�������J�E���g�\���̃v���n�u�̐e�ƂȂ鑶��
    [SerializeField]
    private GameObject parent;

    //-----------------------------------------------------------------

    // �A�N�Z�T�[------------------------------------------------------
    
    //-----------------------------------------------------------------

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
    public void IndicateMoveChara(Doll_blu_Nor indichara)
    {
        // �N���[�������pGameObject
        GameObject Instance;

        Instance = Instantiate(OriginCntPrefab);
        Instance.GetComponent<IndiCountChara>();
        IndiCountChara clone = Instance.GetComponent<IndiCountChara>();
        clone.SetName(indichara.GetName());
        clone.transform.parent = parent.transform;

        //���X�g�ɕۑ�
        CloneCntPrefab.Add(Instance);
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
                IndicateMoveChara(CharaObject[i]);
                if (CharaObject[i].gameObject.CompareTag("PlayableChara"))
                {
                    //�����ŃN���b�N�ҋ@���Ă܂�����ď�����������

                    //�N���b�N���ꂽ��A�����ŃR�}���h�ҋ@���Ă܂�����ď������������B
                }
                //else if(�����œGNPC���ǂ������f)
                //else (�����܂ŗ����疡��NPC)
                
            }
        }
    }
    

    /// <summary>
    /// ClickedGameObject���\�b�h�ŌĂяo�����B�N���b�N���ꂽ�L�����̃R�}���h��\�����邽�߂̃��\�b�h
    /// </summary>
    /// <param name="move">�N���b�N���ꂽ�L����</param>
    void StandbyChara(GameObject move)
    {
        Transform childCommand;
        childCommand = move.transform.GetChild(ACTION);
        StartCoroutine(MoveStandby(childCommand));
    }

    /// <summary>
    /// �J�������߂Â��Ă���R�}���h��\�����郁�\�b�h
    /// </summary>
    /// <param name="charaCommand">�N���b�N���ꂽ�L�����̎q�I�u�W�F�N�g�i�R�}���h�j</param>
    /// <returns></returns>
    public IEnumerator MoveStandby(Transform charaCommand)
    {
        while(true)
        {
            for (int i = 0; i < 2; i++)
            {
                //�J�������N���b�N�����L�����ɋ߂Â��܂ő҂�
                if (i == 0)
                {
                    yield return new WaitForSeconds(0.75f);
                }
                else
                {
                    //�Z�R�}���h��������\��
                    charaCommand.gameObject.SetActive(true);
                }
            }
        }
    }
}

