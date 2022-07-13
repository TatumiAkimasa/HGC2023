using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Wepon_Maneger : ClassData_
{
    [SerializeField]
    //•Šíí—Ş/ƒŒƒxƒ‹/‚Ä‚éŒÀ“xŒÂ”
    Toggle[,,] Wepon = new Toggle[3,3,3];

    [SerializeField]
    GameObject Content_Wepons;

    public int[] Wepon_limit = new int[3];

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Add_Wepon(Toggle add)
    {
        int parts_num = Wepon_limit[ARMAMENT] / 3;
        int parts_num_add = Wepon_limit[ARMAMENT] % 3;
    }
}
