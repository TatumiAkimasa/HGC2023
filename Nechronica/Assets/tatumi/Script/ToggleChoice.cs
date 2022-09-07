using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
public class ToggleChoice : MonoBehaviour
{
    private Toggle[] Toggles;

    private GameObject aaaa;

    string[] Add_names=new string[3];

    private int Max;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleGroup_add(Toggle add)
    {
        int ADD_NUM = (Max / 3);

        if(int.Parse(Regex.Replace(this.gameObject.name, @"[^0-9]", ""))==(Max%3))
        {
            ADD_NUM++;
        }

        int count = 0;
       
        for(int i=0;i!=Toggles.Length;i++)
        {
            if(Toggles[i]==add)
            {
                add.isOn=true;

                Add_names[count] = add.name;

                count++;
            }
            else if(Toggles[i].name!=Add_names[0] && Toggles[i].name != Add_names[1] && Toggles[i].name != Add_names[2])
            {
                Toggles[i].isOn = false;
            }
        }
    }

}
