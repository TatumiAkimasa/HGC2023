using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Input_data_parts : MonoBehaviour
{
    ToggleGroup[] Wepon = new ToggleGroup[3];

    private int OK_Wepon;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OK_signal_Wepon(int a)
    {
        int result = a / 3;
        int resilt_child = a % 3;

        for(int i=0;i!=3;i++)
        {

        }
    }
}
