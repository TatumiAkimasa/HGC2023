using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Chara_SaveLoad : MonoBehaviour
{
    [SerializeField]
    private GameObject SaveData_Prefub_obj;
    [SerializeField]
    public GameObject SaveData_ParentView_obj;

    [SerializeField]
    private Save_Load_data SaveObj;

    private void Start()
    {
        SaveObj = GameObject.FindGameObjectWithTag("AllyChara").GetComponent<Save_Load_data>();
    }

    public void nanana()
    {
        // すべての子オブジェクトを取得
        foreach (Transform n in SaveData_ParentView_obj.transform)
        {
            GameObject.Destroy(n.gameObject);
        }


        for (int i = 0; i != 15; i++)
        {
            SaveData_CellSet cell = Instantiate(SaveData_Prefub_obj, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0), SaveData_ParentView_obj.transform).GetComponent<SaveData_CellSet>();

            if(i==0)
            cell.Set_cell(Data_Scan.Instance.my_data[i].CharaField_data.Time[0], Data_Scan.Instance.my_data[i].CharaField_data.Time[1], Data_Scan.Instance.my_data[i].Name,i);
            else
                cell.Set_cell("NULL", "NULL", "Noname", i);

            cell.Set_SaveObj(SaveObj);
        }
    }
}
