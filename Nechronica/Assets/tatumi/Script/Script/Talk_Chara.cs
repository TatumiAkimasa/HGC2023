using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;

public class Talk_Chara : MonoBehaviour
{
    [SerializeField]
    private GameObject ParentObj;

    [SerializeField]
    private TextMeshPro ProText;

    private float FeedTime = 1.0f; // �������莞��
    private int Nowvisual_Len = 0;//���̕\��������
    private int text_Len = 0;//�K�v������

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            StopCoroutine("Talk_Active");
        }
    }

    public bool talk_Set()
    {
        ParentObj.SetActive(true);

        ProText.text = "Test1";

        text_Len = ProText.text.Length;
        Nowvisual_Len = 0;
        
        ProText.maxVisibleCharacters = 0; // �\�����������O��

        StartCoroutine(Talk_Active());

        ParentObj.SetActive(false);
        return true;
    }

    private IEnumerator Talk_Active()
    {
         while (true)
        {
            if (Nowvisual_Len < text_Len)
            {
                Nowvisual_Len++;
                ProText.maxVisibleCharacters = Nowvisual_Len; // �\����1���������₷
                yield return new WaitForSeconds(FeedTime);
            }
           
        }

    }

    //private IEnumerator Talk_End()
    //{
    //    if (Input.GetKeyDown(KeyCode.E))
    //        yield break;
    //    else
    //        yield return null;
    //}
}
