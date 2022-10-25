using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckUp_chara : MonoBehaviour
{
    [SerializeField]
    private Talk_Chara Talk_cs;

    private GameObject PL;

    private bool talk_now=false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PL != null)
        {
            if (Input.GetKeyDown(KeyCode.E) && talk_now == false)
            {
                talk_now = true;

                StartCoroutine(Talk_cs.Talk_Set((Talk_End =>
                {
                    talk_now = true;

                    for (int i = 0; i != Talk_End.Length; i++)
                    {
                        Data_Scan.Instance.my_data[0].Item.Add(Talk_End[i]);
                    }
                })));

            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);

        if (other.gameObject.tag == "Player")
        {
            PL = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider Item)
    {
        Debug.Log(Item.name);

        if (Item.gameObject.tag == "Player")
        {
            PL = null;
        }
    }

    
}


