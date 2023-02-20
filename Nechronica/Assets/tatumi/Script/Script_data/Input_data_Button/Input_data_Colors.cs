using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Input_data_Colors : MonoBehaviour
{
    private List<Button> Listbuttons = new List<Button>();
    private void Start()
    {
        for(int i=0; i!=this.gameObject.transform.childCount; i++)
        {
            Listbuttons.Add(this.gameObject.transform.GetChild(i).GetComponent<Button>());
        }
    }

    public void Setcolor(Button Value)
    {
        for (int i = 0; i != Listbuttons.Count; i++)
        {
            if(Listbuttons[i].name == Value.name)
            {
                //選択色
                ColorBlock cb = Listbuttons[i].colors;
                cb.normalColor = Color.yellow;
                cb.highlightedColor = Color.yellow;
                cb.selectedColor = Color.yellow;
                Listbuttons[i].colors = cb;
            }
            else
            {
                //選択色
                ColorBlock cb = Listbuttons[i].colors;
                cb.normalColor = Color.white;
                cb.highlightedColor = Color.white;
                cb.selectedColor = Color.white;
                Listbuttons[i].colors = cb;
            }
        }
    }

    public void ResetJob()
    {
        for (int i = 0; i != Listbuttons.Count; i++)
        {
            //選択色
            ColorBlock cb = Listbuttons[i].colors;
            cb.normalColor = Color.white;
            cb.highlightedColor = Color.white;
            cb.selectedColor = Color.white;
            Listbuttons[i].colors = cb;

        }
    }
}
