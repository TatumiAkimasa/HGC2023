using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;

[System.Serializable]
public class Players_Item
{
    [Header("0=�^�C�g��,1=����")]
    public String[] Item_strs;

}

public class Talk_Chara : MonoBehaviour
{
    [SerializeField]
    private GameObject Parent3DObj,Parent2DObj, EndProText;

    [SerializeField]
    private string[] Talk;

    [SerializeField, Header("UI��ł̕\��")]
    private string UI_str;

    [SerializeField]
    private TextMeshPro ProText;

    [SerializeField]
    private TextMeshProUGUI ItemGetText;

    [SerializeField, Header("��b�ɂ��A�C�e������i�K�{�ł͂Ȃ�")]
    private Players_Item[] Item;

    private float FeedTime = 0.1f; // �������莞��
    private int Nowvisual_Len = 0;//���̕\��������
    private int text_Len = 0;//�K�v������

    private int Max_count = 0;//�ő啶���؂�ւ���

    public string Set_Itemstr(string set) => UI_str = set;
    public void Set_Talkstr(string[] set) 
    {
        Talk = new string[set.Length];
        Max_count = set.Length;
        set.CopyTo(Talk, 0); 
    }

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
        if (Parent2DObj == null)
            Parent3DObj.SetActive(true);
        else if (Parent3DObj == null)
            ;
        //Parent2DObj.SetActive(true);
        else
            Parent3DObj.SetActive(true);

        StartCoroutine(Talk_Active((action=>
        {
            EndProText.SetActive(false);
          
            if (Parent2DObj == null)
                Parent3DObj.SetActive(false);
            else if (Parent3DObj == null)
                Parent2DObj.SetActive(false);
            else
            {
                Parent2DObj.SetActive(false);
                Parent3DObj.SetActive(false);
            }
                

            var Items = new Item[Item.Length];

            for (int i = 0; i != Item.Length; i++)
            {
                //���g��������A���
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

            ProText.maxVisibleCharacters = 0; // �\�����������O��

            while (true)
            {
                if (Input.GetKeyDown(KeyCode.T) && Nowvisual_Len == text_Len)
                    break;

                if (Nowvisual_Len < text_Len)
                {
                    Nowvisual_Len++;
                    ProText.maxVisibleCharacters = Nowvisual_Len; // �\����1���������₷
                    yield return new WaitForSeconds(FeedTime);

                }
                //���I��
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

        //�A�C�e���\��Break!!
        if(Parent2DObj==null)
        {
            action_end(true);
            yield break;
        }


        ItemGetText.text = UI_str;

        text_Len = ItemGetText.text.Length;
        Nowvisual_Len = 0;

        ItemGetText.maxVisibleCharacters = 0; // �\�����������O��

        Parent2DObj.SetActive(true);

        while (true)
        {
            if (Input.GetKeyDown(KeyCode.T) && Nowvisual_Len == text_Len)
                break;

            if (Nowvisual_Len < text_Len)
            {
                Nowvisual_Len += text_Len;
                ItemGetText.maxVisibleCharacters = Nowvisual_Len; // �\����1���������₷
                yield return new WaitForSeconds(FeedTime);

            }
            //���I��
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


