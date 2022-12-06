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

    public void EnemyAI_Action()
    {
        //のちにPLのみ取得するよう依頼
        List<Doll_blu_Nor> PlayerDolls = ManagerAccessor.Instance.battleSystem.GetCharaObj();

        //PL
        //PlayerDolls

        // 敵キャラのエリアと選択されたマニューバの射程を絶対値で比べて、射程内であれば攻撃するか選択するコマンドを表示する
        // 敵キャラのエリアの絶対値が攻撃の最大射程以下且つ、
        // 敵キャラのエリアの絶対値が攻撃の最小射程以上なら攻撃する

        //使用武具for文

        //敵の位置検索&射程比較
        //for (int i = 0; i != PlayerDolls.Count; i++)
        //{
            
        //    if (this.GetComponent<Doll_blu_Nor>()/*使用武具番号*/. != 10 &&
        //        (Mathf.Abs(PlayerDolls[i].area) <= Mathf.Abs(dollManeuver.MaxRange + actingChara.area) &&
        //         Mathf.Abs(PlayerDolls[i].area) >= Mathf.Abs(dollManeuver.MinRange + actingChara.area)))
        //    {
        //        atkTargetEnemy = move;
        //        atkTargetEnemy.transform.GetChild(CANVAS).gameObject.SetActive(true);

        //        exeButton.onClick.AddListener(() => OnClickAtk(move.GetComponent<Doll_blu_Nor>()));

        //        this.transform.GetChild(CANVAS).transform.GetChild(ATKBUTTONS).gameObject.SetActive(true);
        //    }
        //}
    }
}
