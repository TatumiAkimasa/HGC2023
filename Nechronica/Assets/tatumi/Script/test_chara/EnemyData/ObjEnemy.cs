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

    private List<CharaManeuver>[] Maneuvers;
    private CharaManeuver UseManever;

    //タイミングの定数
    private const int COUNT = -1;
    private const int AUTO = 0;
    private const int ACTION = 1;
    private const int MOVE = 2;
    private const int RAPID = 3;
    private const int JUDGE = 4;
    private const int DAMAGE = 5;

    private enum EnemyPartsType
    {
        EAuto=0,
        EAction,
        ERapid,
        EJudge,
        Edamage,
        ESkill,
        EPartsMax=5,
    }


    private void Start()
    {
        //部位×4+Skill1
        Maneuvers = new List<CharaManeuver>[(int)EnemyPartsType.EPartsMax];

        //初期武装追記
        for (int i = 0; i != kihon.GET_MAX_BASE_PARTS(); i++)
        {
            me.CharaBase_data.HeadParts.Add(kihon.Base_Head_parts[i]);

            if (me.CharaBase_data.HeadParts[i].Timing == ACTION)
                Maneuvers[(int)EnemyPartsType.EAction].Add(me.CharaBase_data.HeadParts[i]);
            else if (me.CharaBase_data.HeadParts[i].Timing == RAPID)
                Maneuvers[(int)EnemyPartsType.ERapid].Add(me.CharaBase_data.HeadParts[i]);
            else if (me.CharaBase_data.HeadParts[i].Timing == JUDGE)
                Maneuvers[(int)EnemyPartsType.EJudge].Add(me.CharaBase_data.HeadParts[i]);
            else if (me.CharaBase_data.HeadParts[i].Timing == DAMAGE)
                Maneuvers[(int)EnemyPartsType.Edamage].Add(me.CharaBase_data.HeadParts[i]);

            me.CharaBase_data.ArmParts.Add(kihon.Base_Arm_parts[i]);

            if (me.CharaBase_data.ArmParts[i].Timing == ACTION)
                Maneuvers[(int)EnemyPartsType.EAction].Add(me.CharaBase_data.ArmParts[i]);
            else if (me.CharaBase_data.ArmParts[i].Timing == RAPID)
                Maneuvers[(int)EnemyPartsType.ERapid].Add(me.CharaBase_data.ArmParts[i]);
            else if (me.CharaBase_data.ArmParts[i].Timing == JUDGE)
                Maneuvers[(int)EnemyPartsType.EJudge].Add(me.CharaBase_data.ArmParts[i]);
            else if (me.CharaBase_data.ArmParts[i].Timing == DAMAGE)
                Maneuvers[(int)EnemyPartsType.Edamage].Add(me.CharaBase_data.ArmParts[i]);

            me.CharaBase_data.BodyParts.Add(kihon.Base_Body_parts[i]);

            if (me.CharaBase_data.ArmParts[i].Timing == ACTION)
                Maneuvers[(int)EnemyPartsType.EAction].Add(me.CharaBase_data.BodyParts[i]);
            else if (me.CharaBase_data.ArmParts[i].Timing == RAPID)
                Maneuvers[(int)EnemyPartsType.ERapid].Add(me.CharaBase_data.BodyParts[i]);
            else if (me.CharaBase_data.ArmParts[i].Timing == JUDGE)
                Maneuvers[(int)EnemyPartsType.EJudge].Add(me.CharaBase_data.BodyParts[i]);
            else if (me.CharaBase_data.ArmParts[i].Timing == DAMAGE)
                Maneuvers[(int)EnemyPartsType.Edamage].Add(me.CharaBase_data.BodyParts[i]);

            me.CharaBase_data.LegParts.Add(kihon.Base_Leg_parts[i]);

            if (me.CharaBase_data.ArmParts[i].Timing == ACTION)
                Maneuvers[(int)EnemyPartsType.EAction].Add(me.CharaBase_data.LegParts[i]);
            else if (me.CharaBase_data.ArmParts[i].Timing == RAPID)
                Maneuvers[(int)EnemyPartsType.ERapid].Add(me.CharaBase_data.LegParts[i]);
            else if (me.CharaBase_data.ArmParts[i].Timing == JUDGE)
                Maneuvers[(int)EnemyPartsType.EJudge].Add(me.CharaBase_data.LegParts[i]);
            else if (me.CharaBase_data.ArmParts[i].Timing == DAMAGE)
                Maneuvers[(int)EnemyPartsType.Edamage].Add(me.CharaBase_data.LegParts[i]);
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

                    if (me.CharaBase_data.HeadParts[i].Timing == ACTION)
                        Maneuvers[(int)EnemyPartsType.EAction].Add(me.CharaBase_data.HeadParts[i]);
                    else if (me.CharaBase_data.HeadParts[i].Timing == RAPID)
                        Maneuvers[(int)EnemyPartsType.ERapid].Add(me.CharaBase_data.HeadParts[i]);
                    else if (me.CharaBase_data.HeadParts[i].Timing == JUDGE)
                        Maneuvers[(int)EnemyPartsType.EJudge].Add(me.CharaBase_data.HeadParts[i]);
                    else if (me.CharaBase_data.HeadParts[i].Timing == DAMAGE)
                        Maneuvers[(int)EnemyPartsType.Edamage].Add(me.CharaBase_data.HeadParts[i]);

                }
                if (SITE == ARM)
                {
                    me.CharaBase_data.ArmParts.Add(Enemy.Wepons[ARM].Parts[i].Maneuver);

                    if (me.CharaBase_data.ArmParts[i].Timing == ACTION)
                        Maneuvers[(int)EnemyPartsType.EAction].Add(me.CharaBase_data.ArmParts[i]);
                    else if (me.CharaBase_data.ArmParts[i].Timing == RAPID)
                        Maneuvers[(int)EnemyPartsType.ERapid].Add(me.CharaBase_data.ArmParts[i]);
                    else if (me.CharaBase_data.ArmParts[i].Timing == JUDGE)
                        Maneuvers[(int)EnemyPartsType.EJudge].Add(me.CharaBase_data.ArmParts[i]);
                    else if (me.CharaBase_data.ArmParts[i].Timing == DAMAGE)
                        Maneuvers[(int)EnemyPartsType.Edamage].Add(me.CharaBase_data.ArmParts[i]);

                }
                if (SITE == BODY)
                {
                    me.CharaBase_data.BodyParts.Add(Enemy.Wepons[BODY].Parts[i].Maneuver);

                    if (me.CharaBase_data.ArmParts[i].Timing == ACTION)
                        Maneuvers[(int)EnemyPartsType.EAction].Add(me.CharaBase_data.BodyParts[i]);
                    else if (me.CharaBase_data.ArmParts[i].Timing == RAPID)
                        Maneuvers[(int)EnemyPartsType.ERapid].Add(me.CharaBase_data.BodyParts[i]);
                    else if (me.CharaBase_data.ArmParts[i].Timing == JUDGE)
                        Maneuvers[(int)EnemyPartsType.EJudge].Add(me.CharaBase_data.BodyParts[i]);
                    else if (me.CharaBase_data.ArmParts[i].Timing == DAMAGE)
                        Maneuvers[(int)EnemyPartsType.Edamage].Add(me.CharaBase_data.BodyParts[i]);

                }
                if (SITE == LEG)
                {
                    me.CharaBase_data.LegParts.Add(Enemy.Wepons[LEG].Parts[i].Maneuver);

                    if (me.CharaBase_data.ArmParts[i].Timing == ACTION)
                        Maneuvers[(int)EnemyPartsType.EAction].Add(me.CharaBase_data.LegParts[i]);
                    else if (me.CharaBase_data.ArmParts[i].Timing == RAPID)
                        Maneuvers[(int)EnemyPartsType.ERapid].Add(me.CharaBase_data.LegParts[i]);
                    else if (me.CharaBase_data.ArmParts[i].Timing == JUDGE)
                        Maneuvers[(int)EnemyPartsType.EJudge].Add(me.CharaBase_data.LegParts[i]);
                    else if (me.CharaBase_data.ArmParts[i].Timing == DAMAGE)
                        Maneuvers[(int)EnemyPartsType.Edamage].Add(me.CharaBase_data.LegParts[i]);
                }

            }
        }

    }

    public void EnemyAI_Action()
    {
        //のちにPLのみ取得するよう依頼
        List<Doll_blu_Nor> PlayerDolls = ManagerAccessor.Instance.battleSystem.GetCharaObj();

        // 敵キャラのエリアと選択されたマニューバの射程を絶対値で比べて、射程内であれば攻撃するか選択するコマンドを表示する
        // 敵キャラのエリアの絶対値が攻撃の最大射程以下且つ、
        // 敵キャラのエリアの絶対値が攻撃の最小射程以上なら攻撃する

        //使用武具for文
        for(int ActManeuvers=0;ActManeuvers!=Maneuvers[(int)EnemyPartsType.EAction].Count;ActManeuvers++)
        { 
            for (int i = 0; i != PlayerDolls.Count; i++)
            {
                //破損判定
                if (!Maneuvers[(int)EnemyPartsType.EAction][ActManeuvers].isDmage)
                {
                    //敵の位置検索 & 射程比較
                    if (Maneuvers[(int)EnemyPartsType.EAction][ActManeuvers].MinRange != 10 &&                                   //ここarea考えろ
                    (Mathf.Abs(PlayerDolls[i].area) <= Mathf.Abs(Maneuvers[(int)EnemyPartsType.EAction][ActManeuvers].MaxRange + me.potition) &&
                     Mathf.Abs(PlayerDolls[i].area) >= Mathf.Abs(Maneuvers[(int)EnemyPartsType.EAction][ActManeuvers].MinRange + me.potition)))
                    {
                        //使用武具更新
                        if (UseManever == null)
                        {
                            UseManever = Maneuvers[(int)EnemyPartsType.EAction][ActManeuvers];
                            //優先度更新
                            UseManever.EnemyAI[(int)EnemyPartsType.EAction] = 0;
                        }
                        else if (UseManever.EnemyAI[(int)EnemyPartsType.EAction] < Maneuvers[(int)EnemyPartsType.EAction][ActManeuvers].EnemyAI[(int)EnemyPartsType.EAction])
                            UseManever = Maneuvers[(int)EnemyPartsType.EAction][ActManeuvers];
                    }
                }
            }
        }
    }
}
