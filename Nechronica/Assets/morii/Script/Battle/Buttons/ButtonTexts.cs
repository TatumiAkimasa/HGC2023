using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonTexts : MonoBehaviour
{
    [SerializeField]
    private Text name;
    [SerializeField]
    private Text cost;
    [SerializeField]
    private Text range;

    void SetName(string text) { name.text = text; }
    void SetCost(string text) { cost.text = text; }
    void SetRange(string text) { range.text = text; }
}
