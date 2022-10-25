using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SaveData_CellSet : MonoBehaviour
{
    [SerializeField]
    private Text PlayTime, RealTime,Name,Num;

    
    private Save_Load_data SaveObj;

    public Save_Load_data Set_SaveObj(Save_Load_data value) => SaveObj = value;

    public void Set_cell(string playtime,string realTime,string name,int num)
    {
        PlayTime.text = playtime;
        RealTime.text = realTime;
        Name.text = name;
        Num.text = "Data" + num.ToString();
    }

    public void Click_Save()
    {
        Data_Scan.Instance.my_data[0].CharaField_data.Time[0] = "00:00:12";
        Data_Scan.Instance.my_data[0].CharaField_data.Time[1] = DateTime.Now.ToString();
        SaveObj.ClickButtonSave(this.gameObject.name);
    }

    public void Click_Load()
    {
        SaveObj.ClickButtonLoad(this.gameObject.name);
    }
}
