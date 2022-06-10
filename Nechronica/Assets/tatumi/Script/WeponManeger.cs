using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeponManeger : MonoBehaviour
{


    [SerializeField]
    private Text wepon_text, bio_text,mac_text;

    public int[] parts=new int[6];

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

    public void Setparts(int[] Parts,bool MorS)
    {
        if(MorS)
        {
            parts[0] = Parts[0];
            parts[1] = Parts[1];
            parts[2] = Parts[2];
        }
        else
        {
            parts[3] = Parts[0];
            parts[4] = Parts[1];
            parts[5] = Parts[2];
        }
       
        wepon_text.text = (parts[0]+parts[3]).ToString();

       
        bio_text.text = (parts[1]+parts[4]).ToString();

       
        mac_text.text = (parts[2]+parts[5]).ToString();
    }
}
