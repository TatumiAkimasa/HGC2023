using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Field_change : MonoBehaviour
{
    [SerializeField]
    private GameObject[] Field;

    private int Wepon,level = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InputField(Dropdown DD)
    {
        level = DD.value;

        //�����ށ����x��
        for(int i=0;i!=3;i++)
        {
            //�w�蕐��ON
            if (i == level)
                Field[Wepon].transform.GetChild(i).gameObject.SetActive(true);
            else
                Field[Wepon].transform.GetChild(i).gameObject.SetActive(false);

            //���̑�OFF
            for(int k=0;k!=3;k++)
            {
                if (k != Wepon)
                    Field[k].transform.GetChild(i).gameObject.SetActive(false);
            }
            
        }
    }

    private void InputField(int DD)
    {
        //�����ށ����x��
        for (int i = 0; i != 3; i++)
        {
            //�w�蕐��ON
            if (i == level)
                Field[Wepon].transform.GetChild(i).gameObject.SetActive(true);
            else
                Field[Wepon].transform.GetChild(i).gameObject.SetActive(false);

            //���̑�OFF
            for (int k = 0; k != 3; k++)
            {
                if (k != Wepon)
                    Field[k].transform.GetChild(i).gameObject.SetActive(false);
            }

        }
    }

    public void WeponChange(Dropdown DD)
    {
        Wepon = DD.value;
        InputField(level);
    }
}
