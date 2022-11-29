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

    private int NowCount = 20;

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
        CountText.text = NowCount.ToString();
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

    /// <summary>
    /// �^�[���J�n���ɓ��郁�\�b�h
    /// </summary>
    public void TurnStart()
    {
        // �ő�s���l�Ԃ�A�s���l���񕜁B
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
        CountText.text = NowCount.ToString();

        // ���݂̃J�E���g�Ɠ����s���l�̃L�������擾���A�X�N���[���r���[�ɕ\��
        for (int i = 0; i < charaObject.Count; i++)
        {
            if (NowCount == charaObject[i].NowCount)
            {
                IndicateMoveChara(charaObject[i]);
                CountMoveChara.Add(charaObject[i]);
                battleExe = true;
            }
        }
        if (battleExe)
        {
            
            BattleStart();
        }
        else
        {
            CountLast();
        }
    }

    /// <summary>
    /// �^�[���I�����ɓ��郁�\�b�h
    /// </summary>
    public void CountLast()
    {
        NowCount--;
        if(NowCount==0)
        {
            NowCount += 20;
            for (int i = 0; i < CloneCntPrefab.Count; i++)
            {
                Destroy(CloneCntPrefab[i]);
            }
            CloneCntPrefab.Clear();
            TurnStart();
        }

        // �J�E���g�I�����ɍ��̍s���\�̃��X�g���N���A
        else if(CloneCntPrefab.Count>=0)
        {
            for(int i=0;i<CloneCntPrefab.Count;i++)
            {
                Destroy(CloneCntPrefab[i]);
            }
            CloneCntPrefab.Clear();
            CountStart();
        }
    }

    /// <summary>
    /// �J�E���g���Ƀv���C�A�u���L���������邩���Ȃ������f���A�����瑀����󂯕t����悤�ɂ���B
    /// </summary>
    public void BattleStart()
    {
        //for����weight�����������ɏ������Ă���
        //�v���C�A�u���L�����ɂȂ�����N���b�N�K�[�h���O��
        for(int i=0;i<CountMoveChara.Count;i++)
        {
            if (CountMoveChara[i].gameObject.CompareTag("AllyChara"))
            {
                ProcessAccessor.Instance.actTiming.StandbyCharaSelect = true;
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


    /// <summary>
    /// �I�����ꂽ�R�}���h�𔭓������邽�߂̏����i�K�Ɉڍs������֐�
    /// </summary>
    /// <param name="eff">��������}�j���[�o�̓��e</param>
    public void OnClickCommand(ManerverAndArea eff)
    {
        // �K�v�ȏ��𑗐M
        controllManager.SkillSelected = true;
        controllManager.SetManeuver(eff.maneuver);
        controllManager.SetArea(eff.area);
        controllManager.StandbyEnemySelect = true;
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











    // ClickedGameObject


}

