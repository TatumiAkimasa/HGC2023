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

    // 実際に使用するクラス
    [SerializeField]
    private List<Doll_blu_Nor> charaObject = new List<Doll_blu_Nor>();

    // クリックされたプレイアブルキャラを受け取るようの変数
    private GameObject clickedChara;

    // カウント宙に動くキャラをこのリストに入れる
    private List<Doll_blu_Nor> CountMoveChara = new List<Doll_blu_Nor>();

    // カウント処理パートに移行するかの成否
    private bool battleExe = false;

    // キャラ変数のやり取りのマネージャー
    [SerializeField]
    private GetClickedGameObject controllManager;

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

    // Start is called before the first frame update
    void Start()
    {
        CountText.text = NowCount.ToString();
        // Charaというタグがついたキャラをすべて取得
        charaObjectsBuffer = GameObject.FindGameObjectsWithTag("PlayableChara");
        for (int i = 0; i < charaObjectsBuffer.Length; i++)
        {
            // キャラ
            charaObject.Add(charaObjectsBuffer[i].GetComponent<Doll_blu_Nor>());
        }

        // デバッグ用にスタートに書く。本当はTurnStartに配置。
        for (int i = 0; i < charaObject.Count; i++)
        {
            charaObject[i].IncreaseNowCount();
            Debug.Log(charaObject[i].GetNowCount());
        }
    }

    /// <summary>
    /// ターン開始時に入るメソッド
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
    /// ターン終了時に入るメソッド
    /// </summary>
    public void TurnLast()
    {
        NowCount--;
        if(NowCount==0)
        {
            NowCount += 20;
        }

        // カウント終了時に左の行動表のリストをクリア
        if(CloneCntPrefab.Count>=0)
        {
            CloneCntPrefab.Clear();
        }
        TurnStart();
    }

    /// <summary>
    /// バトルが始まったときに呼び出されるメソッド（Startでいい感じしてる）
    /// </summary>
    public void BattleStart()
    {
        //for文でweightが小さい順に処理していく
        //プレイアブルキャラになったら待機状態に移行
        for(int i=0;i<CountMoveChara.Count;i++)
        {
            if (CountMoveChara[i].gameObject.CompareTag("PlayableChara"))
            {
                controllManager.CharaSelectStandby();
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

