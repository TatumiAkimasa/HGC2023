using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Save_Load_data : MonoBehaviour
{
   
    [SerializeField]
    public Chara_data_input Chara_data_input_cs;

    [SerializeField]
    public Text test;

    const string filename = "Nechronica_savedata";

    void Awake()
    {
        test.text=(Path.Combine(Application.persistentDataPath, filename));
       
        //‰ŠúÝ’è
        binarySaveLoad.IsZipArchive = true;
        binarySaveLoad.IsSimpleEncryption = true;
        binarySaveLoad.UserEncrypt = (data) => { for (int i = 0; i < data.Length; i++) data[i] += 1; };
        binarySaveLoad.UserDecrypt = (data) => { for (int i = 0; i < data.Length; i++) data[i] -= 1; };

    }

    public void ClickButtonSave()
    {
        Doll_blueprint test = Chara_data_input_cs.Doll_data;
        binarySaveLoad.Save(filename, test);
    }

    public void ClickButtonLoad()
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
    }

    public void ClickButtonDelete()
    {
        Debug.Log("Delet_savedata");

        binarySaveLoad.Delete(filename);
    }
}

