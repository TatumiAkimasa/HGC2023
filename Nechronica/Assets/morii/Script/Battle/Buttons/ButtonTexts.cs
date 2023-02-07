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
    [SerializeField]
    private Image image;

    [SerializeField]
    private Toggle dmgCheckBox;
    [SerializeField]
    private Toggle useCheckBox;

    private CharaManeuver myManeuver;

    public void SetName(string text) { name.text = text; }
    public void SetCost(string text) { cost.text = text; }
    public void SetRange(string text) { range.text = text; }

    private void Update()
    {
        if (myManeuver.isUse)
        {
            useCheckBox.isOn = true;
        }
        else
        {
            useCheckBox.isOn = false;
        }

        if (myManeuver.isDmage)
        {
            dmgCheckBox.isOn = true;
        }
        else
        {
            dmgCheckBox.isOn = false;
        }
    }

    public void ColorChange(CharaManeuver maneuver)
    {
        Color col;
        col.a = 1.0f;
        myManeuver = maneuver;
        if (maneuver.EffectNum.ContainsKey(EffNum.Damage))
        {
            col.r = 1.0f;
            col.g = 0.8f;
            col.b = 0.8f;
            image.color = col;
        }
        else if (maneuver.EffectNum.ContainsKey(EffNum.Move))
        {
            col.r = 1.0f;
            col.g = 1.0f;
            col.b = 0.65f;
            image.color = col;
        }
        else if (maneuver.EffectNum.ContainsKey(EffNum.Judge))
        {
            col.r = 0.8f;
            col.g = 0.8f;
            col.b = 1.0f;
            image.color = col;
        }
        else if (maneuver.EffectNum.ContainsKey(EffNum.Guard))
        {
            col.r = 0.8f;
            col.g = 1.0f;
            col.b = 0.8f;
            image.color = col;
        }
    }
}
