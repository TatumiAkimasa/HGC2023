using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSystem : MonoBehaviour
{
    // キャラのオブジェクトがどれだけあるか数える変数
    [SerializeField]
    private GameObject[] CharaObjectsBuffer;

    [SerializeField]
    private List<Doll_blu_Nor> CharaObject = new List<Doll_blu_Nor>();

    private List<Doll_blu_Nor> MoveChara = new List<Doll_blu_Nor>();

    private int NowCount = 20;

    [SerializeField]
    private Text CountText;

    //カウントの所にキャラを表示するためのプレハブ
    [SerializeField]
    private GameObject OriginCntPrefab;    //生成の元となるプレハブ

    private List<GameObject> CloneCntPrefab = new List<GameObject>();  

    

    //クローンしたプレハブの親となる存在
    [SerializeField]
    private GameObject parent;

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
        IndicateMoveChara();
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
    public void IndicateMoveChara()
    {
        for (int i = 0; i < CharaObject.Count; i++)
        {
            if(NowCount==CharaObject[i].GetNowCount())
            {
                // クローン生成用GameObject
                GameObject Instance;

                Instance = Instantiate(OriginCntPrefab);
                Instance.GetComponent<IndiCountChara>();
                IndiCountChara clone = Instance.GetComponent<IndiCountChara>();
                clone.SetName(CharaObject[i].GetName());
                clone.transform.parent = parent.transform;

                //リストに保存
                CloneCntPrefab.Add(Instance);
            }
        }
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
                //if(ここでプレイアブルキャラかどうか判断)
                //else if(ここで敵NPCかどうか判断)
                //else //ここまで来たら味方NPC
            }
            else
            {
                NowCount--;
            }
        }
    }
}