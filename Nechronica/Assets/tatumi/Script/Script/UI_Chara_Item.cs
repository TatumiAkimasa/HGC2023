using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Chara_Item : MonoBehaviour
{
    //
    [SerializeField]
    private GameObject ItemData_Prefub_obj;
    [SerializeField]
    private GameObject ItemData_ParentView_obj;

    [SerializeField]
    private Text Setumei_obj;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Chara_Item_View()
    {
        // すべての子オブジェクトを取得
        foreach (Transform n in ItemData_ParentView_obj.transform)
        {
            GameObject.Destroy(n.gameObject);
        }

        for (int i = 0; i != Data_Scan.Instance.my_data[0].Item.Count; i++)
        {
            Item_CellSet cell = Instantiate(ItemData_Prefub_obj, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0), ItemData_ParentView_obj.transform).GetComponent<Item_CellSet>();

            cell.Set_setumei(Data_Scan.Instance.my_data[0].Item[i].str);
            cell.Set_Title(Data_Scan.Instance.my_data[0].Item[i].Tiltle);
            cell.Set_setumei_obj(Setumei_obj);
        }
    }
}
