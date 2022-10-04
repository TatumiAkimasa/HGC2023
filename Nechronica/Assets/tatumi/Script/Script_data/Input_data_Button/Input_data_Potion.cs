using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Input_data_Potion : Input_data_Button
{
    [SerializeField]
    private GameObject Pskils;

    private GameObject[] ChildObject;

    private string keyword="";

    public SkillManeger S_Maneger;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Push_Position_button()
    {
        keyword=Push_button_pos();

        S_Maneger.position_kewword = keyword;

        ChildObject = new GameObject[Pskils.transform.childCount];

        for (int i = 0; i < Pskils.transform.childCount; i++)
        {
            //Žæ“¾
            GameObject test = Pskils.transform.GetChild(i).gameObject;

            Debug.Log(keyword);

            //‘I‘ð‹–‰Â–½—ß
            if(!test.name.Contains(keyword))
            {
                test.GetComponent<Button>().interactable = false;
            }
            else
            {
                test.GetComponent<Button>().interactable = true;
            }
          
        }
    }

    
}
