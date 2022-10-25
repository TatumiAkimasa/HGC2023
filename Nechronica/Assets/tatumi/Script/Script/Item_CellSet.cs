using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Item_CellSet : MonoBehaviour
{
    [SerializeField]
    private Text Title_obj;

    private string Title_str, Setumei_bunn;
    private Text Setumei;

    public string Set_Title(string title) => Title_str = title;
    public string Set_setumei(string setumei) => Setumei_bunn = setumei;
    public Text Set_setumei_obj(Text setumei_obj) => Setumei = setumei_obj;

    public void Item_text_click()
    {
        Setumei.text = Setumei_bunn;
    }

    private void Start()
    {
        Title_obj.text = Title_str;
    }
}
