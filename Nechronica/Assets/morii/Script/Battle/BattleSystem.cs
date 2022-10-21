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

    // キャラスポーンスクリプト参照用
    [SerializeField]
    private BattleSpone battleSpone;

    // クリックされたプレイアブルキャラを受け取るようの変数
    private GameObject clickedChara;

    // カウント宙に動くキャラをこのリストに入れる
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

    private List<GameObject> CloneCntPrefab = new List<GameObject>();  

    //クローンしたカウント表示のプレハブの親となる存在
    [SerializeField]
    private GameObject parent;

    //-----------------------------------------------------------------

    // アクセサー------------------------------------------------------
    
    //-----------------------------------------------------------------

    void Awake()
    {
        CountText.text = NowCount.ToString();
        // Charaというタグがついたキャラをすべて取得
        charaObjectsBuffer = GameObject.FindGameObjectsWithTag("AllyChara");
        for (int i = 0; i < charaObjectsBuffer.Length; i++)
        {
            // キャラ
            charaObject.Add(charaObjectsBuffer[i].GetComponent<Doll_blu_Nor>());
        }

        // キャラをスポーン
        charaObject=battleSpone.CharaSpone(charaObject);
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
        // デバッグ用にスタートに書く。本当はTurnStartに配置。
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
    /// バトルが始まったときに呼び出されるメソッド（Startでいい感じしてる）
    /// </summary>
    public void BattleStart()
    {
        //for文でweightが小さい順に処理していく
        //プレイアブルキャラになったらクリックガードを外す
        for(int i=0;i<CountMoveChara.Count;i++)
        {
            if (CountMoveChara[i].gameObject.CompareTag("AllyChara"))
            {
                rayGuard.SetActive(false);
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

}

