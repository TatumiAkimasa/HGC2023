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
    void FixedUpdate()
    {
        if (PL != null)
        {
            if (Input.GetKeyDown(KeyCode.E) && talk_now == false)
            {
                talk_now = true;

                StartCoroutine(Talk_cs.Talk_Set((End =>
                {
                    talk_now = End;
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
