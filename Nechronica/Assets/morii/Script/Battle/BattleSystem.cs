using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSystem : MonoBehaviour
{
    // 定数
    const int ACTION = 0;

    // バトルフロー関連-------------------------------------------------
    // キャラのオブジェクトを取得する変数
    [SerializeField]
    private GameObject[] charaObjectsBuffer;

    // キャラのオブジェクトを実際に使用するためのクラス
    [SerializeField]
    private List<Doll_blu_Nor> charaObject = new List<Doll_blu_Nor>();

    private List<Doll_blu_Nor> enemyCharaObjs = new List<Doll_blu_Nor>();
    private List<Doll_blu_Nor> allyCharaObjs = new List<Doll_blu_Nor>();

    private List<GameObject> charaStatusList = new List<GameObject>();

    public List<Doll_blu_Nor> GetCharaObj() { return charaObject; }
    public List<Doll_blu_Nor> GetEnemyCharaObj() { return enemyCharaObjs; }
    public List<Doll_blu_Nor> GetAllyCharaObj() { return allyCharaObjs; }

    

    // キャラスポーンスクリプト参照用
    [SerializeField]
    private BattleSpone battleSpone;

    // カウント中に動くキャラをこのリストに入れる
    private List<Doll_blu_Nor> countMoveChara = new List<Doll_blu_Nor>();

    public List<Doll_blu_Nor> GetMoveChara() { return countMoveChara; }


    // カウント処理パートに移行するかの成否
    private bool battleExe = false;

    public bool BattleExe
    {
        get { return battleExe; }
        set { battleExe = value; }
    }


    // キャラ変数のやり取りのマネージャー
    [SerializeField]
    private GetClickedGameObject controllManager;

    // クリックガード用のオブジェクト
    [SerializeField]
    private GameObject clickGuard;

    [SerializeField] private GameObject charaStatus;
    [SerializeField] private GameObject statusParent;


    // -----------------------------------------------------------------
    // カウント関連-----------------------------------------------------

    private int nowCount = 20;

    public int NowCount
    {
        get { return nowCount; }
        set { nowCount = value; }
    }

    [SerializeField]
    private Text CountText;

    //カウントの所にキャラを表示するためのプレハブ
    [SerializeField]
    private GameObject OriginCntPrefab;    //生成の元となるプレハブ

    // 生成したクローンプレハブのリスト
    private List<GameObject> CloneCntPrefab = new List<GameObject>();

    //クローンしたカウント表示のプレハブの親となる存在
    [SerializeField]
    private GameObject parent;



    //-----------------------------------------------------------------

    // アクセサー------------------------------------------------------
    
    //-----------------------------------------------------------------

    void Awake()
    {
        ManagerAccessor.Instance.battleSystem = this;
        Debug.Log(ManagerAccessor.Instance.battleSystem);

        // カウントを表示
        CountText.text = nowCount.ToString();
        // AllyCharaというタグがついたキャラをすべて取得
        charaObjectsBuffer = GameObject.FindGameObjectsWithTag("AllyChara");
        for (int i = 0; i < charaObjectsBuffer.Length; i++)
        {
            charaObject.Add(charaObjectsBuffer[i].GetComponent<Doll_blu_Nor>());
            allyCharaObjs.Add(charaObjectsBuffer[i].GetComponent<Doll_blu_Nor>());
        }
        // EnemyCharaというタグがついたキャラをすべて取得
        charaObjectsBuffer = GameObject.FindGameObjectsWithTag("EnemyChara");
        for (int i = 0; i < charaObjectsBuffer.Length; i++)
        {
            charaObject.Add(charaObjectsBuffer[i].GetComponent<Doll_blu_Nor>());
            enemyCharaObjs.Add(charaObjectsBuffer[i].GetComponent<Doll_blu_Nor>());
        }

        // キャラをスポーン
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
    /// ターン開始時に入るメソッド
    /// </summary>
    public void TurnStart()
    {
        // 最大行動値分、行動値を回復。
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
    /// カウント開始時に入るメソッド
    /// </summary>
    public void CountStart()
    {
        // 現在のカウントを表示
        CountText.text = nowCount.ToString();

        // 現在のカウントと同じ行動値のキャラを取得し、スクロールビューに表示
        for (int i = 0; i < charaObject.Count; i++)
        {
            if (nowCount == charaObject[i].NowCount)
            {
                IndicateMoveChara(charaObject[i]);
                // 表示カウントで行動できるキャラをこのリストに格納
                countMoveChara.Add(charaObject[i]);
                battleExe = true;
            }
        }
        
    }

    /// <summary>
    /// ターン終了時に入るメソッド
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
    /// カウント中にプレイアブルキャラがいるかいないか判断し、いたら操作を受け付けるようにする。
    /// </summary>
    public void BattleStart()
    {
        //for文でweightが小さい順に処理していく
        //プレイアブルキャラになったらクリックガードを外す
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
            // else(味方NPCなら…)
        }
        battleExe = false;
    }

    /// <summary>
    /// 表示カウントで行動できるキャラを表示
    /// </summary>
    public void IndicateMoveChara(Doll_blu_Nor indichara)
    {
        // クローン生成用GameObject
        GameObject Instance;

        Instance = Instantiate(OriginCntPrefab);
        IndiCountChara clone = Instance.GetComponent<IndiCountChara>();
        clone.SetName(indichara.Name);
        clone.transform.parent = parent.transform;

        //リストに保存
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

    // 表示されている一番上のキャラを削除
    public void DeleteMoveChara()
    {
        Destroy(CloneCntPrefab[0]);
        CloneCntPrefab.RemoveAt(0);
    }

    // 表示されている一番上のキャラを削除
    // 今のカウントで動くキャラがジャッジ、ダメージタイミングのキャラがコストを支払った時用
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
    /// キャラの残りパーツを計算するだけのメソッド
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

