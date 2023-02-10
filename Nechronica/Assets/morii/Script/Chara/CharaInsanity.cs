using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaInsanity : MonoBehaviour
{
    Doll_blu_Nor target;
    int insanityPoint;
    string insanityElement;

    public void PlusInsanity()
    {
        insanityPoint++;
    }

    /// <summary>
    /// ‹¶‹C‚Ì“à—e‚ð“o˜^
    /// </summary>
    /// <param name="key">‹¶‹C–¼</param>
    public void SetInsanityElement(string key)
    {
        insanityElement = key;
    }

    /// <summary>
    /// ”­‹¶‚É‚æ‚éƒoƒgƒ‹’†‚Ì‰e‹¿
    /// </summary>
    public void InsanityInfluence()
    {
        if (insanityElement == InsanityElement.disgust)
        {

        }
        else if (insanityElement == InsanityElement.monopoly)
        {

        }
        else if (insanityElement == InsanityElement.reliance)
        {

        }
        else if (insanityElement == InsanityElement.obsession)
        {

        }
        else if (insanityElement == InsanityElement.love)
        {

        }
        else if (insanityElement == InsanityElement.opposition)
        {

        }
        else if (insanityElement == InsanityElement.friendship)
        {

        }
        else if (insanityElement == InsanityElement.admiration)
        {

        }
        else if (insanityElement == InsanityElement.protection)
        {

        }
        else if (insanityElement == InsanityElement.trust)
        {

        }
    }

}
