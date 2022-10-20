using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class Players_Status
{
    [Header("名前,パーツ数,最大行動値")]
    public Text[] text = new Text[3];

}

public class UI_Chara_status : ClassData_
{
    [SerializeField, Header("多次元配列-キャラデータ")]
    public Players_Status[] Charas;

    private int Data_Length;
    private Data_Scan data_Scan_cs;

    [SerializeField]
    private GameObject ClassData_View_Preobj;
    [SerializeField]
    private GameObject ClassData_ParentView_obj;

    // Start is called before the first frame update
    void Start()
    {
        data_Scan_cs = GameObject.FindWithTag("AllyChara").GetComponent<Data_Scan>();
        Data_Length = data_Scan_cs.my_data.Length;

        for(int i=0;i!= Data_Length; i++)
        {
            Charas[i].text[0].text = data_Scan_cs.my_data[i].Name;
            Charas[i].text[1].text = data_Scan_cs.my_data[i].CharaBase_data.GetALLParts().ToString();
            Charas[i].text[2].text = data_Scan_cs.my_data[i].CharaBase_data.GetMaxCount().ToString();
        }
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
            CharaParts_View_CellSet cell = Instantiate(ClassData_View_Preobj, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0), ClassData_ParentView_obj.transform).GetComponent<CharaParts_View_CellSet>();

            cell.SetParts(data[i].Cost, data[i].Timing, data[i].Name, data[i].MaxRange, data[i].MinRange);
        }
    }
}
