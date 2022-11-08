using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade_FIeld : MonoBehaviour
{
    [SerializeField]
    private GameObject MainCamera_obj;
    [SerializeField]
    private GameObject RawCamera_obj;

    [SerializeField]
    private Animator RawImage_anim;

    private void OnTriggerEnter(Collider Item)
    {
        if (Item.tag == "Player")
        {
            //MainCamera_obj.SetActive(false);
            RawCamera_obj.SetActive(true);

            RawImage_anim.SetTrigger("FadeOn");
        }
    }

    private void OnTriggerExit(Collider Item)
    {
        if (Item.tag == "Player")
        {
            //MainCamera_obj.SetActive(true);
            RawCamera_obj.SetActive(false);

            RawImage_anim.SetTrigger("FadeOff");
        }
    }
}
