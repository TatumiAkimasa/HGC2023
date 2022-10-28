using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class Players_Status
{
    [Header("名前,パーツ数,最大行動値")]
    public Text[] text = new Text[4];

}

public class UI_Chara_status : ClassData_
{
    [SerializeField, Header("多次元配列-キャラデータ")]
    public Players_Status[] Charas;

    private int Data_Length;
    private Data_Scan data_Scan_cs;

    [SerializeField]
    private GameObject ClassData_Prefub_obj;
    [SerializeField]
    private GameObject ClassData_ParentView_obj;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Data_Scan.Instance.Init((End=>
        {
            //これいらなく根？
            data_Scan_cs = Data_Scan.Instance;

            Data_Length = Data_Scan.Instance.my_data.Length;

            for (int i = 0; i != Data_Length; i++)
            {
                Charas[i].text[0].text = data_Scan_cs.my_data[i].Name;
                Charas[i].text[1].text = data_Scan_cs.my_data[i].CharaBase_data.GetALLParts().ToString();
                Charas[i].text[2].text = data_Scan_cs.my_data[i].CharaBase_data.GetMaxCount().ToString();
                Charas[i].text[3].text = data_Scan_cs.my_data[i].CharaField_data.Event[0].str;
            }
        })));

        
    }

    public void Chara_Parts_View(int SITE)
    {
        // すべての子オブジェクトを取得
        foreach (Transform n in ClassData_ParentView_obj.transform)
        {
            GameObject.Destroy(n.gameObject);
        }
        

        List<CharaManeuver> data = null;


        if (SITE == HEAD)
        {
            data  = data_Scan_cs.my_data[0].CharaBase_data.GetHeadParts();
        }
        else if (SITE == ARM)
        {
            data = data_Scan_cs.my_data[0].CharaBase_data.GetArmParts();

        }
        else if (SITE == BODY)
        {
            data = data_Scan_cs.my_data[0].CharaBase_data.GetBodygParts();
        }
        else if (SITE == LEG)
        {
            data = data_Scan_cs.my_data[0].CharaBase_data.GetLegParts();
        }
        else
        {
            Debug.Log(SITE);
            return;
        }

        Debug.Log(data);

        for (int i = 0; i != data.Count; i++)
        {
            Parts_CellSet cell = Instantiate(ClassData_Prefub_obj, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0), ClassData_ParentView_obj.transform).GetComponent<Parts_CellSet>();

            cell.SetParts(data[i].Cost, data[i].Timing, data[i].Name, data[i].MaxRange, data[i].MinRange);
        }
    }

    public void Eventstr_Update()
    {
        Charas[0].text[3].text = data_Scan_cs.my_data[0].CharaField_data.Event[1].str;
    }
}
