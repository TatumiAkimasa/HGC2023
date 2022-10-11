using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSystem : MonoBehaviour
{
    // 定数
    const int ACTION = 0;

    // キャラのオブジェクトを取得する変数
    [SerializeField]
    private GameObject[] CharaObjectsBuffer;

    // 実際に使用するクラス
    [SerializeField]
    private List<Doll_blu_Nor> CharaObject = new List<Doll_blu_Nor>();


    private GameObject MoveChara;

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
        CharaObjectsBuffer = GameObject.FindGameObjectsWithTag("PlayableChara");
        for (int i = 0; i < CharaObjectsBuffer.Length; i++)
        {
            // キャラ
            CharaObject.Add(CharaObjectsBuffer[i].GetComponent<Doll_blu_Nor>());
        }

        // デバッグ用にスタートに書く。本当はTurnStartに配置。
        for (int i = 0; i < CharaObject.Count; i++)
        {
            CharaObject[i].IncreaseNowCount();
            Debug.Log(CharaObject[i].GetNowCount());
        }
    }

    /// <summary>
    /// ターン開始時に入るメソッド
    /// </summary>
    public void TurnStart()
    {
        CountText.text = NowCount.ToString();
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
        TurnStart();
    }

    /// <summary>
    /// 表示カウントで行動できるキャラを表示
    /// </summary>
    public void IndicateMoveChara(Doll_blu_Nor indichara)
    {
        // クローン生成用GameObject
        GameObject Instance;

        Instance = Instantiate(OriginCntPrefab);
        Instance.GetComponent<IndiCountChara>();
        IndiCountChara clone = Instance.GetComponent<IndiCountChara>();
        clone.SetName(indichara.GetName());
        clone.transform.parent = parent.transform;

        //リストに保存
        CloneCntPrefab.Add(Instance);
    }

    /// <summary>
    /// バトルが始まったときに呼び出されるメソッド（Startでいい感じしてる）
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
                    //ここでクリック待機してますよって処理をしたい

                    //クリックされたら、ここでコマンド待機してますよって処理をしたい。
                }
                //else if(ここで敵NPCかどうか判断)
                //else (ここまで来たら味方NPC)
                
            }
        }
    }
    

    /// <summary>
    /// ClickedGameObjectメソッドで呼び出される。クリックされたキャラのコマンドを表示するためのメソッド
    /// </summary>
    /// <param name="move">クリックされたキャラ</param>
    void StandbyChara(GameObject move)
    {
        Transform childCommand;
        childCommand = move.transform.GetChild(ACTION);
        StartCoroutine(MoveStandby(childCommand));
    }

    /// <summary>
    /// カメラが近づいてからコマンドを表示するメソッド
    /// </summary>
    /// <param name="charaCommand">クリックされたキャラの子オブジェクト（コマンド）</param>
    /// <returns></returns>
    public IEnumerator MoveStandby(Transform charaCommand)
    {
        while(true)
        {
            for (int i = 0; i < 2; i++)
            {
                //カメラがクリックしたキャラに近づくまで待つ
                if (i == 0)
                {
                    yield return new WaitForSeconds(0.75f);
                }
                else
                {
                    //技コマンドもろもろを表示
                    charaCommand.gameObject.SetActive(true);
                }
            }
        }
    }
}

