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

    private List<Doll_blu_Nor> enemyCharaObjs = new List<Doll_blu_Nor>();
    private List<Doll_blu_Nor> allyCharaObjs = new List<Doll_blu_Nor>();

    private List<GameObject> charaStatusList = new List<GameObject>();

    public List<Doll_blu_Nor> GetCharaObj() { return charaObject; }
    public List<Doll_blu_Nor> GetEnemyCharaObj() { return enemyCharaObjs; }
    public List<Doll_blu_Nor> GetAllyCharaObj() { return allyCharaObjs; }

    

    // �L�����X�|�[���X�N���v�g�Q�Ɨp
    [SerializeField]
    private BattleSpone battleSpone;

    // �J�E���g���ɓ����L���������̃��X�g�ɓ����
    private List<Doll_blu_Nor> countMoveChara = new List<Doll_blu_Nor>();

    public List<Doll_blu_Nor> GetMoveChara() { return countMoveChara; }


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

    [SerializeField] private GameObject charaStatus;
    [SerializeField] private GameObject statusParent;


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
            allyCharaObjs.Add(charaObjectsBuffer[i].GetComponent<Doll_blu_Nor>());
        }
        // EnemyChara�Ƃ����^�O�������L���������ׂĎ擾
        charaObjectsBuffer = GameObject.FindGameObjectsWithTag("EnemyChara");
        for (int i = 0; i < charaObjectsBuffer.Length; i++)
        {
            charaObject.Add(charaObjectsBuffer[i].GetComponent<Doll_blu_Nor>());
            enemyCharaObjs.Add(charaObjectsBuffer[i].GetComponent<Doll_blu_Nor>());
        }

        // �L�������X�|�[��
        charaObject =battleSpone.CharaSpone(charaObject);

        GameObject instance = null;
        for(int i=0;i<allyCharaObjs.Count;i++)
        {
            instance = Instantiate(charaStatus);
            instance.GetComponent<CharaStatusLabels>().SetNameText(allyCharaObjs[i].Name);
            instance.GetComponent<CharaStatusLabels>().SetPartsText(CharaResidueParts(allyCharaObjs[i]));
            instance.GetComponent<CharaStatusLabels>().SetCountText(allyCharaObjs[i].NowCount);

            instance.transform.SetParent(statusParent.transform);

            charaStatusList.Add(instance);
        }
        
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
            if(charaObject[i].gameObject.CompareTag("AllyChara"))
            {
                StatusLabelUpdate(charaObject[i]);
            }
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
                countMoveChara.Add(charaObject[i]);
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
        countMoveChara.Clear();
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
        for (int i = 0; i < countMoveChara.Count; i++)
        {
            if (countMoveChara[i].gameObject.CompareTag("AllyChara"))
            {
                ProcessAccessor.Instance.actTiming.StandbyCharaSelect = true;
                break;
            }
            else if (countMoveChara[i].gameObject.CompareTag("EnemyChara"))
            {
                countMoveChara[i].gameObject.GetComponent<ObjEnemy>().EnemyAI_Action();
                break;
            }
            // else(����NPC�Ȃ�c)
        }
        battleExe = false;
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
        CloneCntPrefab.Add(clone.gameObject);
    }


    void CharaCountSort()
    {
        for (int i = 0; i < countMoveChara.Count; i++) 
        {
            for (int j = i + 1; j < countMoveChara.Count; j++) 
            {
                if(countMoveChara[i].GetWeight()>countMoveChara[j].GetWeight())
                {
                    Doll_blu_Nor buff = countMoveChara[i];
                    countMoveChara[i] = countMoveChara[j];
                    countMoveChara[j] = buff;
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
            IndiCountChara aaaa = CloneCntPrefab[i].GetComponent<IndiCountChara>();
            if (name==CloneCntPrefab[i].GetComponent<IndiCountChara>().GetName())
            {
                Destroy(CloneCntPrefab[i]);
                CloneCntPrefab.RemoveAt(i);
                break;
            }
        }
        for (int i = 0; i < countMoveChara.Count; i++)
        {
            if(name==countMoveChara[i].Name)
            {
                countMoveChara.RemoveAt(i);
                break;
            }
        }
    }

    /// <summary>
    /// �L�����̎c��p�[�c���v�Z���邾���̃��\�b�h
    /// </summary>
    /// <returns></returns>
    public int CharaResidueParts(Doll_blu_Nor chara)
    {
        int num = 0;
        num += chara.HeadParts.Count;
        num += chara.ArmParts.Count;
        num += chara.BodyParts.Count;
        num += chara.LegParts.Count;

        return num;
    }

    public void StatusLabelUpdate(Doll_blu_Nor chara)
    {
        for(int i=0;i<charaStatusList.Count;i++)
        {
            if(charaStatusList[i].GetComponent<CharaStatusLabels>().GetNameText() ==chara.Name)
            {
                charaStatusList[i].GetComponent<CharaStatusLabels>().SetPartsText(CharaResidueParts(chara));
                charaStatusList[i].GetComponent<CharaStatusLabels>().SetCountText(chara.NowCount);
            }
        }
    }










}

