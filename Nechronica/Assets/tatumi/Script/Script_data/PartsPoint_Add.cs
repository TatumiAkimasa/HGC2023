using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class PartsPoint_Add : ClassData_
{
    ToggleGroup mToggleGroup;
    public SkillManeger skillManeger_cs;

    // Start is called before the first frame update
    void Start()
    {
        mToggleGroup = this.GetComponent<ToggleGroup>();
        skillManeger_cs.parts[ARMAMENT + 6]++;
    }

    // Update is called once per frame
    public void Change_Toggle()
    {
        Toggle tgl = mToggleGroup.ActiveToggles().FirstOrDefault();


        if (tgl.name.Contains("ARMAMENT"))
        {
            skillManeger_cs.parts[ARMAMENT + 6]++;
            skillManeger_cs.parts[VARIANT + 6] = 0;
            skillManeger_cs.parts[ALTER + 6] = 0;
        }
        else if (tgl.name.Contains("VARIANT"))
        {
            skillManeger_cs.parts[ARMAMENT + 6]=0;
            skillManeger_cs.parts[VARIANT + 6]++;
            skillManeger_cs.parts[ALTER + 6] = 0;
        }
        else if (tgl.name.Contains("ALTER"))
        {
            skillManeger_cs.parts[ARMAMENT + 6] = 0;
            skillManeger_cs.parts[VARIANT + 6] = 0;
            skillManeger_cs.parts[ALTER + 6]++;
        }
            
    }
}
