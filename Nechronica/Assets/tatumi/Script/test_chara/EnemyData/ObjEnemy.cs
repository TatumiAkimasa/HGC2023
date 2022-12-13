using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjEnemy : ClassData_
{
    [SerializeField]
    private Table_Enemy Enemy;

    [SerializeField]
    private kihonnpatu kihon;

    [SerializeField]
    private int EnemyAI;                  

    public Doll_blueprint me;

   

    private void Start()
    {
        //初期武装追記
        for (int i = 0; i != kihon.GET_MAX_BASE_PARTS(); i++)
        {
            me.CharaBase_data.HeadParts.Add(kihon.Base_Head_parts[i]);

            me.CharaBase_data.ArmParts.Add(kihon.Base_Arm_parts[i]);

            me.CharaBase_data.BodyParts.Add(kihon.Base_Body_parts[i]);

            me.CharaBase_data.LegParts.Add(kihon.Base_Leg_parts[i]);
        }

        //データから解析し、マニューバーを追加する
        //追加武装追記
        for (int SITE = 0; SITE != 4; SITE++)
        {
            for (int i = 0; i != Enemy.Wepons[SITE].Parts.Count; i++)
            {
                //None情報を抜きにして整理
                if (SITE == HEAD)
                {
                    me.CharaBase_data.HeadParts.Add(Enemy.Wepons[HEAD].Parts[i].Maneuver);

                }
                if (SITE == ARM)
                {
                    me.CharaBase_data.ArmParts.Add(Enemy.Wepons[ARM].Parts[i].Maneuver);

                }
                if (SITE == BODY)
                {
                    me.CharaBase_data.BodyParts.Add(Enemy.Wepons[BODY].Parts[i].Maneuver);

                }
                if (SITE == LEG)
                {
                    me.CharaBase_data.LegParts.Add(Enemy.Wepons[LEG].Parts[i].Maneuver);

                }

            }
        }
    }


}
