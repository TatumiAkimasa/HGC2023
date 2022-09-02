using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chara_data_inpit : MonoBehaviour
{
    const int HEAD = 0;
    const int ARM = 1;
    const int BODY = 2;
    const int LEG = 3;

    new CharaBase chara;

    public Wepon_Maneger WE_Maneger;
    public SkillManeger SK_Maneger;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void input()
    {
        for (int SITE = 0; SITE != 4; SITE++)
        {
            for (int i = 0; i != 2; i++)
            {
                for (int k = 0; k != 3; k++)
                {
                    if (WE_Maneger.Site_[SITE].Step[i].Text[k] != null) 
                    {
                        if(SITE==HEAD)
                        {
                            chara.HeadParts.Add(WE_Maneger.Site_[SITE].Step[i].Text[k].GetComponent<WeponData_Set>().Set_Parts);
                        }
                        if (SITE == ARM)
                        {
                            chara.ArmParts.Add(WE_Maneger.Site_[SITE].Step[i].Text[k].GetComponent<WeponData_Set>().Set_Parts);
                        }
                        if (SITE == BODY)
                        {
                            chara.BodyParts.Add(WE_Maneger.Site_[SITE].Step[i].Text[k].GetComponent<WeponData_Set>().Set_Parts);
                        }
                        if (SITE == LEG)
                        {
                            chara.LegParts.Add(WE_Maneger.Site_[SITE].Step[i].Text[k].GetComponent<WeponData_Set>().Set_Parts);
                        }
                    }

                }
            }
        }
    }
}
