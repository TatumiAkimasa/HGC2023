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

    // �L�����̃I�u�W�F�N�g�����ۂɎg�p���邽�߂̃N���X
    [SerializeField]
    private List<Doll_blu_Nor> charaObject = new List<Doll_blu_Nor>();

    public List<Doll_blu_Nor> GetCharaObj()
    {
        return charaObject;
    }

    // �L�����X�|�[���X�N���v�g�Q�Ɨp
    [SerializeField]
    private BattleSpone battleSpone;

    // �N���b�N���ꂽ�v���C�A�u���L�������󂯎��悤�̕ϐ�
    private GameObject clickedChara;

    // �J�E���g���ɓ����L���������̃��X�g�ɓ����
    private List<Doll_blu_Nor> CountMoveChara = new List<Doll_blu_Nor>();

    // �J�E���g�����p�[�g�Ɉڍs���邩�̐���
    private bool battleExe = false;

    public bool BattleExe
    {
        get { return battleExe; }
        set { battleExe = value; }
    }


    // �L�����ϐ��̂����̃}�l�[�W���[
    [SerializeField]
    private GetClickedGameObject controllManager;

    // �N���b�N�K�[�h�p�̃I�u�W�F�N�g
    [SerializeField]
    private GameObject clickGuard;

    // ���C�K�[�h�p�I�u�W�F�N�g
    [SerializeField]
    private GameObject rayGuard;


    // -----------------------------------------------------------------
    // �J�E���g�֘A-----------------------------------------------------

    private int nowCount = 20;

    public int NowCount
    {
        get { return nowCount; }
        set { nowCount = value; }
    }

    [SerializeField]
    private Text CountText;

    //�J�E���g�̏��ɃL������\�����邽�߂̃v���n�u
    [SerializeField]
    private GameObject OriginCntPrefab;    //�����̌��ƂȂ�v���n�u

    // ���������N���[���v���n�u�̃��X�g
    private List<GameObject> CloneCntPrefab = new List<GameObject>();

    //�N���[�������J�E���g�\���̃v���n�u�̐e�ƂȂ鑶��
    [SerializeField]
    private GameObject parent;



    //-----------------------------------------------------------------

    // �A�N�Z�T�[------------------------------------------------------
    
    //-----------------------------------------------------------------

    void Awake()
    {
        ManagerAccessor.Instance.battleSystem = this;
        Debug.Log(ManagerAccessor.Instance.battleSystem);

        // �J�E���g��\��
        CountText.text = nowCount.ToString();
        // AllyChara�Ƃ����^�O�������L���������ׂĎ擾
        charaObjectsBuffer = GameObject.FindGameObjectsWithTag("AllyChara");
        for (int i = 0; i < charaObjectsBuffer.Length; i++)
        {
            charaObject.Add(charaObjectsBuffer[i].GetComponent<Doll_blu_Nor>());
        }
        // EnemyChara�Ƃ����^�O�������L���������ׂĎ擾
        charaObjectsBuffer = GameObject.FindGameObjectsWithTag("EnemyChara");
        for (int i = 0; i < charaObjectsBuffer.Length; i++)
        {
            charaObject.Add(charaObjectsBuffer[i].GetComponent<Doll_blu_Nor>());
        }

        // �L�������X�|�[��
        charaObject =battleSpone.CharaSpone(charaObject);
    }

    private void Start()
    {
        TurnStart();
    }

    private void FixedUpdate()
    {
        if(battleExe)
        {
            BattleStart();
        }
        else if (CloneCntPrefab.Count <= 0)
        {
            CountLast();
        }
    }

    /// <summary>
    /// �^�[���J�n���ɓ��郁�\�b�h
    /// </summary>
    public void TurnStart()
    {
        // �ő�s���l���A�s���l���񕜁B
        for (int i = 0; i < charaObject.Count; i++)
        {
            charaObject[i].IncreaseNowCount();
            Debug.Log(charaObject[i].NowCount);
        }

        CountStart();
    }

    /// <summary>
    /// �J�E���g�J�n���ɓ��郁�\�b�h
    /// </summary>
    public void CountStart()
    {
        // ���݂̃J�E���g��\��
        CountText.text = nowCount.ToString();

        // ���݂̃J�E���g�Ɠ����s���l�̃L�������擾���A�X�N���[���r���[�ɕ\��
        for (int i = 0; i < charaObject.Count; i++)
        {
            if (nowCount == charaObject[i].NowCount)
            {
                IndicateMoveChara(charaObject[i]);
                // �\���J�E���g�ōs���ł���L���������̃��X�g�Ɋi�[
                CountMoveChara.Add(charaObject[i]);
                battleExe = true;
            }
        }
        
    }

    /// <summary>
    /// �^�[���I�����ɓ��郁�\�b�h
    /// </summary>
    public void CountLast()
    {
        nowCount--;
        CountMoveChara.Clear();
        if (nowCount<=0)
        {
            nowCount += 20;
            TurnStart();
        }

        CountStart();
    }

    /// <summary>
    /// �J�E���g���Ƀv���C�A�u���L���������邩���Ȃ������f���A�����瑀����󂯕t����悤�ɂ���B
    /// </summary>
    public void BattleStart()
    {
        //for����weight�����������ɏ������Ă���
        //�v���C�A�u���L�����ɂȂ�����N���b�N�K�[�h���O��
        for (int i = 0; i < CountMoveChara.Count; i++)
        {
            if (CountMoveChara[i].gameObject.CompareTag("AllyChara"))
            {
                ProcessAccessor.Instance.actTiming.StandbyCharaSelect = true;
                battleExe = false;
            }
            // else if(�G�L�����Ȃ�c)
            // else(����NPC�Ȃ�c)
        }
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
        clone.SetName(indichara.Name);
        clone.transform.parent = parent.transform;

        //���X�g�ɕۑ�
        CloneCntPrefab.Add(Instance);
    }


    void CharaCountSort()
    {
        for (int i = 0; i < CountMoveChara.Count; i++) 
        {
            for (int j = i + 1; j < CountMoveChara.Count; j++) 
            {
                if(CountMoveChara[i].GetWeight()>CountMoveChara[j].GetWeight())
                {
                    Doll_blu_Nor buff = CountMoveChara[i];
                    CountMoveChara[i] = CountMoveChara[j];
                    CountMoveChara[j] = buff;
                }
            }
        }
    }

    // �\������Ă����ԏ�̃L�������폜
    public void DeleteMoveChara()
    {
        Destroy(CloneCntPrefab[0]);
        CloneCntPrefab.RemoveAt(0);
    }

    // �\������Ă����ԏ�̃L�������폜
    // ���̃J�E���g�œ����L�������W���b�W�A�_���[�W�^�C�~���O�̃L�������R�X�g���x���������p
    public void DeleteMoveChara(string name)
    {
        for (int i=0;i<CloneCntPrefab.Count;i++)
        {
            if(name==CloneCntPrefab[i].GetComponent<IndiCountChara>().GetName())
            {
                Destroy(CloneCntPrefab[i]);
            }
        }
    }











    // ClickedGameObject


}

