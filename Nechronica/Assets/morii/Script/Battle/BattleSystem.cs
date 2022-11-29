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

    public List<Doll_blu_Nor> GetCharaObj()
    {
        return charaObject;
    }

    // キャラスポーンスクリプト参照用
    [SerializeField]
    private BattleSpone battleSpone;

    // クリックされたプレイアブルキャラを受け取るようの変数
    private GameObject clickedChara;

    // カウント中に動くキャラをこのリストに入れる
    private List<Doll_blu_Nor> CountMoveChara = new List<Doll_blu_Nor>();

    // カウント処理パートに移行するかの成否
    private bool battleExe = false;

    // キャラ変数のやり取りのマネージャー
    [SerializeField]
    private GetClickedGameObject controllManager;

    // クリックガード用のオブジェクト
    [SerializeField]
    private GameObject clickGuard;

    // レイガード用オブジェクト
    [SerializeField]
    private GameObject rayGuard;


    // -----------------------------------------------------------------
    // カウント関連-----------------------------------------------------

    private int NowCount = 20;

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
        CountText.text = NowCount.ToString();
        // AllyCharaというタグがついたキャラをすべて取得
        charaObjectsBuffer = GameObject.FindGameObjectsWithTag("AllyChara");
        for (int i = 0; i < charaObjectsBuffer.Length; i++)
        {
            charaObject.Add(charaObjectsBuffer[i].GetComponent<Doll_blu_Nor>());
        }
        // EnemyCharaというタグがついたキャラをすべて取得
        charaObjectsBuffer = GameObject.FindGameObjectsWithTag("EnemyChara");
        for (int i = 0; i < charaObjectsBuffer.Length; i++)
        {
            charaObject.Add(charaObjectsBuffer[i].GetComponent<Doll_blu_Nor>());
        }

        // キャラをスポーン
        charaObject =battleSpone.CharaSpone(charaObject);
    }

    private void Start()
    {
        TurnStart();
    }

    /// <summary>
    /// ターン開始時に入るメソッド
    /// </summary>
    public void TurnStart()
    {
        // 最大行動値ぶん、行動値を回復。
        for (int i = 0; i < charaObject.Count; i++)
        {
            charaObject[i].IncreaseNowCount();
            Debug.Log(charaObject[i].NowCount);
        }

        CountStart();
    }

    /// <summary>
    /// カウント開始時に入るメソッド
    /// </summary>
    public void CountStart()
    {
        // 現在のカウントを表示
        CountText.text = NowCount.ToString();

        // 現在のカウントと同じ行動値のキャラを取得し、スクロールビューに表示
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
    /// ターン終了時に入るメソッド
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

        // カウント終了時に左の行動表のリストをクリア
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
    /// カウント中にプレイアブルキャラがいるかいないか判断し、いたら操作を受け付けるようにする。
    /// </summary>
    public void BattleStart()
    {
        //for文でweightが小さい順に処理していく
        //プレイアブルキャラになったらクリックガードを外す
        for(int i=0;i<CountMoveChara.Count;i++)
        {
            if (CountMoveChara[i].gameObject.CompareTag("AllyChara"))
            {
                ProcessAccessor.Instance.actTiming.StandbyCharaSelect = true;
            }
            // else if(敵キャラなら…)
            // else(味方NPCなら…)
        }
    }

    // コマンド選択された時の処理
    public void BattleProcess(CharaManeuver maneuver)
    {
        //技の処理的な
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
        clone.SetName(indichara.GetName());
        clone.transform.parent = parent.transform;

        //リストに保存
        CloneCntPrefab.Add(Instance);
    }


    /// <summary>
    /// 選択されたコマンドを発動させるための準備段階に移行させる関数
    /// </summary>
    /// <param name="eff">発動するマニューバの内容</param>
    public void OnClickCommand(ManerverAndArea eff)
    {
        // 必要な情報を送信
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

