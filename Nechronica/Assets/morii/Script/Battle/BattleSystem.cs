using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSystem : MonoBehaviour
{
    // �萔
    const int ACTION = 0;

    // �o�g���t���[�֘A-------------------------------------------------
    // �L�����̃I�u�W�F�N�g���擾����ϐ�
    [SerializeField]
    private GameObject[] charaObjectsBuffer;

    // ���ۂɎg�p����N���X
    [SerializeField]
    private List<Doll_blu_Nor> charaObject = new List<Doll_blu_Nor>();

    // �N���b�N���ꂽ�v���C�A�u���L�������󂯎��悤�̕ϐ�
    private GameObject clickedChara;

    // �J�E���g���ɓ����L���������̃��X�g�ɓ����
    private List<Doll_blu_Nor> CountMoveChara = new List<Doll_blu_Nor>();

    // �J�E���g�����p�[�g�Ɉڍs���邩�̐���
    private bool battleExe = false;

    // �L�����ϐ��̂����̃}�l�[�W���[
    [SerializeField]
    private GetClickedGameObject controllManager;

    // -----------------------------------------------------------------
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
        charaObjectsBuffer = GameObject.FindGameObjectsWithTag("PlayableChara");
        for (int i = 0; i < charaObjectsBuffer.Length; i++)
        {
            // �L����
            charaObject.Add(charaObjectsBuffer[i].GetComponent<Doll_blu_Nor>());
        }

        // �f�o�b�O�p�ɃX�^�[�g�ɏ����B�{����TurnStart�ɔz�u�B
        for (int i = 0; i < charaObject.Count; i++)
        {
            charaObject[i].IncreaseNowCount();
            Debug.Log(charaObject[i].GetNowCount());
        }
    }

    /// <summary>
    /// �^�[���J�n���ɓ��郁�\�b�h
    /// </summary>
    public void TurnStart()
    {
        CountText.text = NowCount.ToString();
        for (int i = 0; i < charaObject.Count; i++)
        {
            if (NowCount == charaObject[i].GetNowCount())
            {
                IndicateMoveChara(charaObject[i]);
                CountMoveChara.Add(charaObject[i]);
                battleExe = true;
            }
        }
        if(battleExe)
        {
            BattleStart();
        }
        
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

        // �J�E���g�I�����ɍ��̍s���\�̃��X�g���N���A
        if(CloneCntPrefab.Count>=0)
        {
            CloneCntPrefab.Clear();
        }
        TurnStart();
    }

    /// <summary>
    /// �o�g�����n�܂����Ƃ��ɌĂяo����郁�\�b�h�iStart�ł����������Ă�j
    /// </summary>
    public void BattleStart()
    {
        //for����weight�����������ɏ������Ă���
        //�v���C�A�u���L�����ɂȂ�����ҋ@��ԂɈڍs
        for(int i=0;i<CountMoveChara.Count;i++)
        {
            if (CountMoveChara[i].gameObject.CompareTag("PlayableChara"))
            {
                controllManager.CharaSelectStandby();
            }
            // else if(�G�L�����Ȃ�c)
            // else(����NPC�Ȃ�c)
        }
    }

    // �R�}���h�I�����ꂽ���̏���
    public void BattleProcess(CharaManeuver maneuver)
    {
        //�Z�̏����I��
    }

    /// <summary>
    /// �\���J�E���g�ōs���ł���L������\��
    /// </summary>
    public void IndicateMoveChara(Doll_blu_Nor indichara)
    {
        // �N���[�������pGameObject
        GameObject Instance;

        Instance = Instantiate(OriginCntPrefab);
        IndiCountChara clone = Instance.GetComponent<IndiCountChara>();
        clone.SetName(indichara.GetName());
        clone.transform.parent = parent.transform;

        //���X�g�ɕۑ�
        CloneCntPrefab.Add(Instance);
    }

}

