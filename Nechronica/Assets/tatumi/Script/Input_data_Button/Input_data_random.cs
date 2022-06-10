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
        memory_datas[0] = "îjã«";
        memory_datas[1] = "ê‚ñ]";
        memory_datas[2] = "ä◊‚v";
        memory_datas[3] = "êlå`";
        memory_datas[4] = "çﬂêl";
        memory_datas[5] = "ëré∏";
        memory_datas[6] = "äâñ]";
        memory_datas[7] = "îΩì]";
        memory_datas[8] = "äÛñ]";
        memory_datas[9] = "çKïü";
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
