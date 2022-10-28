using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Save_Load_data : MonoBehaviour
{
    const string filename = "Nechronica_savedata";
    const string filename_sub = "Nechronica_savedata_sub";

    [SerializeField]
    public Doll_blueprint aa;

    void Awake()
    {
       
        //‰ŠúÝ’è
        binarySaveLoad.IsZipArchive = true;
        binarySaveLoad.IsSimpleEncryption = true;
        binarySaveLoad.UserEncrypt = (data) => { for (int i = 0; i < data.Length; i++) data[i] += 1; };
        binarySaveLoad.UserDecrypt = (data) => { for (int i = 0; i < data.Length; i++) data[i] -= 1; };
        Data_Scan.Instance.save_Load_Data_cs = this;
    }



    public void ClickButtonSave()
    {
        Doll_blueprint test = Maneger_Accessor.Instance.chara_Data_Input_cs.Doll_data;
        binarySaveLoad.Save(filename_sub, test);
    }

    public void ClickButtonSave(string filename)
    {
        Doll_blueprint test = Data_Scan.Instance.my_data[0];
        binarySaveLoad.Save(filename, test);
    }

    public void ClickButtonLoad()
    {
        Doll_blueprint loaddata;

        binarySaveLoad.Load(filename_sub, out loaddata);

        // display load data
        if (loaddata == null)
        {
            Debug.LogError("ERROR-NUll_Savedata");
        }
        else
        {
            Debug.Log("LOAD-OK_savedata");
        }

        
        aa=loaddata;
    }

    public void ClickButtonLoad(string filename)
    {
        Doll_blueprint loaddata;

        binarySaveLoad.Load(filename, out loaddata);

        // display load data
        if (loaddata == null)
        {
            Debug.LogError("ERROR-NUll_Savedata");
        }
        else
        {
            Debug.Log("LOAD-OK_savedata");
        }

        //aa.CharaField_data.Event[0].str = "Œ®‚ðŽè‚É“ü‚êANPC2‚É˜b‚©‚¯‚æB";
        aa = loaddata;
    }


    public void ClickButtonDelete()
    {
        Debug.Log("Delet_savedata");

        binarySaveLoad.Delete(filename);
    }
}

