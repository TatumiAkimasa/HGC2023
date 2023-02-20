using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UI_Chara_SaveLoad : MonoBehaviour
{
    [SerializeField]
    private GameObject SaveData_Prefub_obj;
    [SerializeField]
    public GameObject SaveData_ParentView_obj;

    [SerializeField]
    public GameObject LoadData_ParentView_obj;

    [SerializeField]
    private GameObject LoadData_Prefub_obj;

    [SerializeField]
    private Save_Load_data SaveObj;

    private Vector3 SavePos;

    public Vector3 SetSavePos(Vector3 value) =>  SavePos = value; 

    private void Start()
    {
        
        //Set_SaveCell();
    }

    public void Set_SaveCell()
    {
        SaveObj = GameObject.FindGameObjectWithTag("SaveData").GetComponent<Save_Load_data>();

        // すべての子オブジェクトを取得
        foreach (Transform n in SaveData_ParentView_obj.transform)
        {
            GameObject.Destroy(n.gameObject);
        }


        for (int i = 0; i != 15; i++)
        {
            SaveData_CellSet cell = Instantiate(SaveData_Prefub_obj, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0), SaveData_ParentView_obj.transform).GetComponent<SaveData_CellSet>();

            cell.SetSaveCellPos(SavePos);

            if (i==0)
            cell.Set_cell(Data_Scan.Instance.my_data[i].CharaField_data.Time[0], Data_Scan.Instance.my_data[i].CharaField_data.Time[1], Data_Scan.Instance.my_data[i].Name,i);
            else
                cell.Set_cell("NULL", "NULL", "Noname", i);

            cell.Set_SaveObj(SaveObj);
        }
    }

    public void Set_LoadCell()
    {
        SaveObj = GameObject.FindGameObjectWithTag("SaveData").GetComponent<Save_Load_data>();

        // すべての子オブジェクトを取得
        foreach (Transform n in LoadData_ParentView_obj.transform)
        {
            GameObject.Destroy(n.gameObject);
        }


        for (int i = 0; i != 15; i++)
        {
            SaveData_CellSet cell = Instantiate(LoadData_Prefub_obj, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0), LoadData_ParentView_obj.transform).GetComponent<SaveData_CellSet>();

            //trueならデータなし
            if (SaveObj.ClickButtonLoad("SaveData(Clone)" + i.ToString(),false))
                break;

            cell.Set_cell(SaveObj.bb.CharaField_data.Time[0], SaveObj.bb.CharaField_data.Time[1], SaveObj.bb.Name, i);
         
            cell.Set_SaveObj(SaveObj);
        }
    }

    public void Load_Scene()
    {
        SceneManager.LoadScene("forest");
    }
}
