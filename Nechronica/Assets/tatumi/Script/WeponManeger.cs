using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeponManeger : MonoBehaviour
{
    [SerializeField]
    private GameObject Cskils;

    private GameObject[] ChildObject;

    [SerializeField]
    private Text wepon_text, bio_text,mac_text;

    public int[] parts=new int[6];

    private string keyword,keyword2 = "X";
    private int    Mword,Sword = -1;

    // Start is called before the first frame update
    void Start()
    {
        //èâä˙âª
        for(int i=0;i!=6;i++)
        {
            parts[i] = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSkill(string skill)
    {

    }

    public void Setparts(int[] Parts,bool MorS)
    {
        if(MorS)
        {
            parts[0] = Parts[0];
            parts[1] = Parts[1];
            parts[2] = Parts[2];

            //mainskill
            Mword = Parts[3];
        }
        else
        {
            parts[3] = Parts[0];
            parts[4] = Parts[1];
            parts[5] = Parts[2];

            //subskill
            Sword = Parts[3];
        }
       
        wepon_text.text = (parts[0]+parts[3]).ToString();

       
        bio_text.text = (parts[1]+parts[4]).ToString();

       
        mac_text.text = (parts[2]+parts[5]).ToString();

        for (int i = 0; i < Cskils.transform.childCount; i++)
        {
            //éÊìæ
            GameObject test = Cskils.transform.GetChild(i).gameObject;

           if(Mword==i)
           {
                keyword = test.name;
           }
           if(Sword==i)
           {
                keyword2 = test.name;
           }

            Debug.Log(keyword);
            Debug.Log(keyword2);

            //ëIëãñâ¬ñΩóﬂ
            if (test.name.Contains(keyword) && test.name.Contains(keyword2))
            {
                test.GetComponent<Button>().interactable = true;
            }
            else if (test.name.Contains(keyword)||test.name.Contains(keyword2))
            {
                test.GetComponent<Button>().interactable = true;

                //êØÉXÉLÉãïïàÛ
                if(i>Cskils.transform.childCount-8)
                    test.GetComponent<Button>().interactable = false;
            }
            else
            {
                test.GetComponent<Button>().interactable = false;
            }

           

        }
    }
}
