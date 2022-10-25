using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Parts_CellSet : Chara_PartsView_Set
{
    [SerializeField]
    private Text Cost;
    [SerializeField]
    private Text Timing;
    [SerializeField]
    private Text Name;
    [SerializeField]
    private Text Preview;
    [SerializeField]
    private GameObject Range;
    [SerializeField]
    private GameObject Range_Red;

    public void SetParts(int cost, int timing, string name, int MaxRange, int MinRange)
    {
        Cost.text = cost.ToString();
        Timing.text = Timing_Set(timing);
        Name.text = name;

        if (MaxRange == MinRange)
        {
            if (MaxRange != 10)
                MaxRange++;
        }

        for (int i = 0; i != MaxRange - MinRange; i++)
        {
            Debug.Log(i);
            if (MinRange >= i)
            {
                //範囲の内分赤色着色
                Instantiate(Range_Red, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0), Range.transform);
            }
        }

        Preview.text = "戦闘システムに習う(まだ未実装)";
    }
}
