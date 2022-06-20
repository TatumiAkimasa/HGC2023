using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeponManeger : ClassData_
{
    [SerializeField]
    private GameObject Cskils,CS_Maneger;

    private GameObject[] ChildObject;

    [SerializeField]
    private Text wepon_text, bio_text,mac_text;

    public int[] parts=new int[6];

    private string keyword,keyword2 = "X";
    private int    Button_num = 0;

    Button[] buttons = new Button[3];

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

    public void SetPSKill(Button a)
    {
        //MainSkill
        if(a.GetComponentInChildren<Text>().text.Contains(keyword))
        {
            if (Button_num == 1)
                Button_num = 0;


            a.interactable = true;
            ColorBlock cb = a.colors;
            cb.disabledColor = Color.yellow;
            a.colors = cb;

            buttons[Button_num] = a;

            Button_num++;
        }
        //SubSkill
        else if(a.GetComponentInChildren<Text>().text.Contains(keyword2))
        {

        }
        
    }

    public void Setparts(ClassData a,bool MorS)
    {
        if(MorS)
        {
            parts[0] = a.parts[0];
            parts[1] = a.parts[1];
            parts[2] = a.parts[2];

            //mainskill
            keyword = Classname_JtoE(a.name);
        }
        else
        {
            parts[3] = a.parts[0];
            parts[4] = a.parts[1];
            parts[5] = a.parts[2];

            //subskill
            keyword2 = Classname_JtoE(a.name);
        }
       
        wepon_text.text = (parts[0]+parts[3]).ToString();

       
        bio_text.text = (parts[1]+parts[4]).ToString();

       
        mac_text.text = (parts[2]+parts[5]).ToString();

        for (int i = 0; i < Cskils.transform.childCount; i++)
        {
            //éÊìæ
            GameObject test = Cskils.transform.GetChild(i).gameObject;

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
