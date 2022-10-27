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

    public void SetName(string text) { name.text = text; }
    public void SetCost(string text) { cost.text = text; }
    public void SetRange(string text) { range.text = text; }
}
