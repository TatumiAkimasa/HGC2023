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

    public Wepon_Maneger WE_Maneger;
    public SkillManeger SK_Maneger;
    public Doll_blueprint DOLL_Maneger;

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
        //‰Šú‰»
        DOLL_Maneger.parts.HeadParts.Clear();
        DOLL_Maneger.parts.ArmParts.Clear();
        DOLL_Maneger.parts.BodyParts.Clear();
        DOLL_Maneger.parts.LegParts.Clear();

        //‰Šú•‘•’Ç‹L

        //’Ç‰Á•‘•’Ç‹L
        for (int SITE = 0; SITE != 4; SITE++)
        {
            for (int i = 0; i != 2; i++)
            {
                for (int k = 0; k != 3; k++)
                {
                    if (WE_Maneger.Site_[SITE].Step[i].Text[k] != null)
                    {
                        if (SITE == HEAD)
                        {
                            DOLL_Maneger.parts.HeadParts.Add(WE_Maneger.Site_[SITE].Step[i].Text[k].GetComponent<Wepon_Data_SaveSet>().Set_Parts);
                        }
                        if (SITE == ARM)
                        {
                            DOLL_Maneger.parts.ArmParts.Add(WE_Maneger.Site_[SITE].Step[i].Text[k].GetComponent<Wepon_Data_SaveSet>().Set_Parts);
                        }
                        if (SITE == BODY)
                        {
                            DOLL_Maneger.parts.BodyParts.Add(WE_Maneger.Site_[SITE].Step[i].Text[k].GetComponent<Wepon_Data_SaveSet>().Set_Parts);
                        }
                        if (SITE == LEG)
                        {
                            DOLL_Maneger.parts.LegParts.Add(WE_Maneger.Site_[SITE].Step[i].Text[k].GetComponent<Wepon_Data_SaveSet>().Set_Parts);
                        }
                    }

                }
            }
        }

        DOLL_Maneger.MainClass = SK_Maneger.GetKeyWord_main();
        DOLL_Maneger.SubClass = SK_Maneger.GetKeyWord_sub();

        DOLL_Maneger.Armament = (short)SK_Maneger.GetArmament();
       // DOLL_Maneger.Variant = (short)SK_Maneger.GetArmament();
       // DOLL_Maneger.Alter=
    }
}
