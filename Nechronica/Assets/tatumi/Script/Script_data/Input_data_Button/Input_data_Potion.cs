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

        Chara_intput_data_cs.temper_name = keyword;

        ChildObject = new GameObject[Pskils.transform.childCount];

        for (int i = 0; i < Pskils.transform.childCount; i++)
        {
            //取得
            GameObject test = Pskils.transform.GetChild(i).gameObject;

            Debug.Log(keyword);

            //選択許可命令
            if(!test.name.Contains(keyword))
            {
                test.SetActive(false);
            }
            else
            {
                test.SetActive(true);
            }
          
        }
    }

    
}
