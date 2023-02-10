using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharaStatusLabels : MonoBehaviour
{
    [SerializeField] Text nameText;
    [SerializeField] Text partsText;
    [SerializeField] Text countText;

    
    public void SetNameText(string name) { nameText.text = name; }
    public string GetNameText() { return nameText.text; }
    public void SetPartsText(int num) { partsText.text = num.ToString(); }
    public void SetCountText(int num) { countText.text = num.ToString(); }
}
