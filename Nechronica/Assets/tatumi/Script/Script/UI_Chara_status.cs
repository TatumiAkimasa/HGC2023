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

public class UI_Chara_status : MonoBehaviour
{
    [SerializeField, Header("多次元配列-キャラデータ")]
    public Players_Status[] Charas;

    private int Data_Length;
    private Data_Scan data_Scan_cs;

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

    // Update is called once per frame
    void Update()
    {
        
    }
}
