using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Input_data_random : MonoBehaviour
{
    [SerializeField]
    private Text memorytext1, memorytext2, anothertext;

    protected string[] memory_datas = new string[10];

    // Start is called before the first frame update
    void Start()
    {
        memory_datas[0] = "�j��";
        memory_datas[1] = "��]";
        memory_datas[2] = "���v";
        memory_datas[3] = "�l�`";
        memory_datas[4] = "�ߐl";
        memory_datas[5] = "�r��";
        memory_datas[6] = "���]";
        memory_datas[7] = "���]";
        memory_datas[8] = "��]";
        memory_datas[9] = "�K��";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RandData()
    {
        int value = Random.Range(0, 10);

        anothertext.text = memory_datas[value];

        memorytext1.text = "No."+Random.Range(0, 100).ToString();
        memorytext2.text = "No." + Random.Range(0, 100).ToString();
    }
}
