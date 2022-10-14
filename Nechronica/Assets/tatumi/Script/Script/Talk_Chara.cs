using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;

public class Talk_Chara : MonoBehaviour
{
    [SerializeField]
    private GameObject ParentObj, EndProText;

   

    [SerializeField]
    private TextMeshPro ProText;

    private float FeedTime = 0.5f; // 文字送り時間
    private int Nowvisual_Len = 0;//今の表示文字数
    private int text_Len = 0;//必要文字数

   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public IEnumerator Talk_Set(System.Action<bool> End)
    {
        ParentObj.SetActive(true);

        ProText.text = "Test1";

        text_Len = ProText.text.Length;
        Nowvisual_Len = 0;
        
        ProText.maxVisibleCharacters = 0; // 表示文字数を０に

        StartCoroutine(Talk_Active((action=>
        {
            ParentObj.SetActive(false);
            EndProText.SetActive(false);
            End(false);
        })));

        yield return null;
    }

   

    private IEnumerator Talk_Active(System.Action<bool> action)
    {
         while (!Input.GetKeyDown(KeyCode.T))
         {
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

        action(true);
        yield break;

    }

   
}
