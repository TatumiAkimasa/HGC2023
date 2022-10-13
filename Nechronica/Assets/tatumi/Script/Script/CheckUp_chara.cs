using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckUp_chara : MonoBehaviour
{
    
    private Talk_Chara talk_cs = null;

    private bool talk_now=false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionStay(Collision Item)
    {
        Debug.Log("Hit!");

        if (Item.gameObject.GetComponent<Talk_Chara>() == null)
            return;
        else
            talk_cs = Item.gameObject.GetComponent<Talk_Chara>();

        if (Input.GetKeyDown(KeyCode.E)&&talk_now==false)
        {
            talk_now = talk_cs.talk_Set();
        }
    }
}
