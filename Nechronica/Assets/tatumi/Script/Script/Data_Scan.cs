using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data_Scan : MonoBehaviour
{
    [SerializeField]
    public Doll_blueprint[] my_data;

    [SerializeField]
    private Save_Load_data save_Load_Data_cs;

    //�����I�ɑS�f�[�^����U�����ɒu���B
    void Start()
    {
        save_Load_Data_cs.ClickButtonLoad();

        my_data[0] = save_Load_Data_cs.aa;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
