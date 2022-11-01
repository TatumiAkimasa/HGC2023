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
    public GameObject LoadData_ParentView_obj;

    [SerializeField]
    private Save_Load_data SaveObj;

    private void Start()
    {
        SaveObj = GameObject.FindGameObjectWithTag("AllyChara").GetComponent<Save_Load_data>();
        Set_SaveCell();
    }

    public void Set_SaveCell()
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

    public void Set_LoadCell()
    {
        // すべての子オブジェクトを取得
        foreach (Transform n in LoadData_ParentView_obj.transform)
        {
            GameObject.Destroy(n.gameObject);
        }


        for (int i = 0; i != 15; i++)
        {
            SaveData_CellSet cell = Instantiate(SaveData_Prefub_obj, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0), LoadData_ParentView_obj.transform).GetComponent<SaveData_CellSet>();

            SaveObj.ClickButtonLoad("SaveData(Clone)" + i.ToString());

           
            cell.Set_cell(SaveObj.bb.CharaField_data.Time[0], SaveObj.bb.CharaField_data.Time[1], SaveObj.bb.Name, i);
         
            cell.Set_SaveObj(SaveObj);
        }
    }
}
