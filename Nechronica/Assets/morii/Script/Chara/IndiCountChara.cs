using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IndiCountChara : MonoBehaviour
{
    [SerializeField]
    private Text Name;

    [SerializeField]
    Image Img;

    public void SetName(string name) { Name.text = name; }
    public string GetName() { return Name.text; }
    public void SetImg(Image img) { Img = img; }
}
