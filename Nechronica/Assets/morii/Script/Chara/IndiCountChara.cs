using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IndiCountChara : MonoBehaviour
{
    [SerializeField]
    Text Name;
    [SerializeField]
    Image Img;

    public void SetName(string name) { Name.text = name; }
    public void SetImg(Image img) { Img = img; }
}
