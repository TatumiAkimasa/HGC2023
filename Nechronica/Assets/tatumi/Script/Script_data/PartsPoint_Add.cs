using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class PartsPoint_Add : ClassData_
{
    ToggleGroup mToggleGroup;
    private SkillManeger skillManeger_cs;

    // Start is called before the first frame update
    void Start()
    {
        skillManeger_cs = Maneger_Accessor.Instance.skillManeger_cs;
        mToggleGroup = this.GetComponent<ToggleGroup>();
        skillManeger_cs.parts[ARMAMENT + 6]++;
    }

    // Update is called once per frame
    public void Change_Toggle(Toggle me)
    {
        if (!me.isOn)
            return;

        Toggle tgl = mToggleGroup.ActiveToggles().FirstOrDefault();


        if (tgl.name.Contains("ARMAMENT"))
        {
            skillManeger_cs.parts[ARMAMENT + 6] = 1;
            skillManeger_cs.parts[VARIANT + 6] = 0;
            skillManeger_cs.parts[ALTER + 6] = 0;

            Debug.Log("ARMAMENT");
        }
        else if (tgl.name.Contains("VARIANT"))
        {
            skillManeger_cs.parts[ARMAMENT + 6]=0;
            skillManeger_cs.parts[VARIANT + 6] = 1;
            skillManeger_cs.parts[ALTER + 6] = 0;

            Debug.Log("VARIANT");
        }
        else if (tgl.name.Contains("ALTER"))
        {
            skillManeger_cs.parts[ARMAMENT + 6] = 0;
            skillManeger_cs.parts[VARIANT + 6] = 0;
            skillManeger_cs.parts[ALTER + 6] = 1;

            Debug.Log("ALTER");
        }


        skillManeger_cs.ClassPoint_Update();
    }
}
