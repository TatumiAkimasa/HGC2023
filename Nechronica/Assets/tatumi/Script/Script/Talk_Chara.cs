using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;

[System.Serializable]
public class Players_Item
{
    [Header("0=タイトル,1=説明")]
    public String[] Item_strs;

}

public class Talk_Chara : MonoBehaviour
{
    [SerializeField]
    private GameObject ParentObj, EndProText;

    [SerializeField]
    private string[] Talk;

    [SerializeField]
    private string Item_str;

    [SerializeField]
    private TextMeshPro ProText;

    [SerializeField]
    private TextMeshProUGUI ItemGetText;

    [SerializeField, Header("会話によるアイテム入手（必須ではない")]
    private Players_Item[] Item;

    private float FeedTime = 0.1f; // 文字送り時間
    private int Nowvisual_Len = 0;//今の表示文字数
    private int text_Len = 0;//必要文字数

    private int Max_count = 0;//最大文字切り替え数

    // Start is called before the first frame update
    void Start()
    {
        Max_count = Talk.Length;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public IEnumerator Talk_Set(System.Action<Item[]> End)
    {
        ParentObj.SetActive(true);

        StartCoroutine(Talk_Active((action=>
        {
            ParentObj.SetActive(false);
            EndProText.SetActive(false);

            var Items = new Item[Item.Length];

            for (int i = 0; i != Item.Length; i++)
            {
                //中身初期化後、代入
                Items[i] = new Item();
                Items[i].str = Item[i].Item_strs[1];
                Items[i].Tiltle = Item[i].Item_strs[0];
            }

            End(Items);
        })));

      
        yield return null;
    }

   

    private IEnumerator Talk_Active(System.Action<bool> action_end)
    {
        for(int Now_Count=0; Now_Count != Max_count;Now_Count++)
        {
            ProText.text = Talk[Now_Count];

            text_Len = ProText.text.Length;
            Nowvisual_Len = 0;

            ProText.maxVisibleCharacters = 0; // 表示文字数を０に

            while (true)
            {
                if (Input.GetKeyDown(KeyCode.T) && Nowvisual_Len == text_Len)
                    break;

                if (Nowvisual_Len < text_Len)
                {
                    Nowvisual_Len++;
                    ProText.maxVisibleCharacters = Nowvisual_Len; // 表示を1文字ずつ増やす
                    yield return new WaitForSeconds(FeedTime);

                }
                //文終了
                else if (Nowvisual_Len == text_Len)
                {
                    EndProText.SetActive(true);
                    yield return null;
                }
                else
                    yield return null;

            }

            EndProText.SetActive(false);
        }


        ItemGetText.text = Item_str;

        text_Len = ItemGetText.text.Length;
        Nowvisual_Len = 0;

        ItemGetText.maxVisibleCharacters = 0; // 表示文字数を０に

        while (true)
        {
            if (Input.GetKeyDown(KeyCode.T) && Nowvisual_Len == text_Len)
                break;

            if (Nowvisual_Len < text_Len)
            {
                Nowvisual_Len += text_Len;
                ItemGetText.maxVisibleCharacters = Nowvisual_Len; // 表示を1文字ずつ増やす
                yield return new WaitForSeconds(FeedTime);

            }
            //文終了
            else if (Nowvisual_Len == text_Len)
            {
                EndProText.SetActive(true);
                yield return null;
            }
            else
                yield return null;
        }

        action_end(true);
        
        yield break;

    }

   
}


