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

    private List<CharaBase> MoveChara = new List<CharaBase>();

    private int NowCount = 20;

    //カウントの所にキャラを表示するためのプレハブ
    [SerializeField]
    private IndiCountChara PrefubcountChara;
    [SerializeField]
    private IndiCountChara CloneCountChara;

    //クローンしたプレハブの親となる存在
    [SerializeField]
    private GameObject parent;

    // Start is called before the first frame update
    void Start()
    {
        // Charaというタグがついたキャラをすべて取得
        CharaObjectsBuffer = GameObject.FindGameObjectsWithTag("PlayableChara");
        for (int i = 0; i < CharaObjectsBuffer.Length; i++) 
        {
            // キャラ
            CharaObject.Add(CharaObjectsBuffer[i].GetComponent<Doll_blu_Nor>());
        }

        for (int i = 0; i < CharaObject.Count; i++)
        {
            Vector3 pos = parent.transform.position;
            PrefubcountChara.Name.text = CharaObject[i].Name;
            //CloneCountChara = Instantiate<IndiCountChara>(PrefubcountChara, parent.transform);
        }
    }

    void ButtleStart()
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

    // Update is called once per frame
    void Update()
    {
    }
}

//仮クラス。キャラ画像と名前のクラスが必要なのでいったんここに作る。
class IndiCountChara
{
    public Text Name;
    public Image Img;
}
