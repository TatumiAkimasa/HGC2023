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

    public Doll_blu_Nor me;

    private List<CharaManeuver>[] Maneuvers;
    private CharaManeuver UseManever;

    private void Start()
    {
        //部位×4+Skill1
        Maneuvers = new List<CharaManeuver>[(int)EnemyPartsType.EPartsMax];

        //初期武装追記
        for (int i = 0; i != kihon.GET_MAX_BASE_PARTS(); i++)
        {
            me.HeadParts.Add(kihon.Base_Head_parts[i]);

            if (me.HeadParts[i].Timing == ACTION)
                Maneuvers[(int)EnemyPartsType.EAction].Add(me.HeadParts[i]);
            else if (me.HeadParts[i].Timing == RAPID)
                Maneuvers[(int)EnemyPartsType.ERapid].Add(me.HeadParts[i]);
            else if (me.HeadParts[i].Timing == JUDGE)
                Maneuvers[(int)EnemyPartsType.EJudge].Add(me.HeadParts[i]);
            else if (me.HeadParts[i].Timing == DAMAGE)
                Maneuvers[(int)EnemyPartsType.Edamage].Add(me.HeadParts[i]);

            me.ArmParts.Add(kihon.Base_Arm_parts[i]);

            if (me.ArmParts[i].Timing == ACTION)
                Maneuvers[(int)EnemyPartsType.EAction].Add(me.ArmParts[i]);
            else if (me.ArmParts[i].Timing == RAPID)
                Maneuvers[(int)EnemyPartsType.ERapid].Add(me.ArmParts[i]);
            else if (me.ArmParts[i].Timing == JUDGE)
                Maneuvers[(int)EnemyPartsType.EJudge].Add(me.ArmParts[i]);
            else if (me.ArmParts[i].Timing == DAMAGE)
                Maneuvers[(int)EnemyPartsType.Edamage].Add(me.ArmParts[i]);

            me.BodyParts.Add(kihon.Base_Body_parts[i]);

            if (me.ArmParts[i].Timing == ACTION)
                Maneuvers[(int)EnemyPartsType.EAction].Add(me.BodyParts[i]);
            else if (me.ArmParts[i].Timing == RAPID)
                Maneuvers[(int)EnemyPartsType.ERapid].Add(me.BodyParts[i]);
            else if (me.ArmParts[i].Timing == JUDGE)
                Maneuvers[(int)EnemyPartsType.EJudge].Add(me.BodyParts[i]);
            else if (me.ArmParts[i].Timing == DAMAGE)
                Maneuvers[(int)EnemyPartsType.Edamage].Add(me.BodyParts[i]);

            me.LegParts.Add(kihon.Base_Leg_parts[i]);

            if (me.ArmParts[i].Timing == ACTION)
                Maneuvers[(int)EnemyPartsType.EAction].Add(me.LegParts[i]);
            else if (me.ArmParts[i].Timing == RAPID)
                Maneuvers[(int)EnemyPartsType.ERapid].Add(me.LegParts[i]);
            else if (me.ArmParts[i].Timing == JUDGE)
                Maneuvers[(int)EnemyPartsType.EJudge].Add(me.LegParts[i]);
            else if (me.ArmParts[i].Timing == DAMAGE)
                Maneuvers[(int)EnemyPartsType.Edamage].Add(me.LegParts[i]);
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
                    me.HeadParts.Add(Enemy.Wepons[HEAD].Parts[i].Maneuver);

                    if (me.HeadParts[i].Timing == ACTION)
                        Maneuvers[(int)EnemyPartsType.EAction].Add(me.HeadParts[i]);
                    else if (me.HeadParts[i].Timing == RAPID)
                        Maneuvers[(int)EnemyPartsType.ERapid].Add(me.HeadParts[i]);
                    else if (me.HeadParts[i].Timing == JUDGE)
                        Maneuvers[(int)EnemyPartsType.EJudge].Add(me.HeadParts[i]);
                    else if (me.HeadParts[i].Timing == DAMAGE)
                        Maneuvers[(int)EnemyPartsType.Edamage].Add(me.HeadParts[i]);

                }
                if (SITE == ARM)
                {
                    me.ArmParts.Add(Enemy.Wepons[ARM].Parts[i].Maneuver);

                    if (me.ArmParts[i].Timing == ACTION)
                        Maneuvers[(int)EnemyPartsType.EAction].Add(me.ArmParts[i]);
                    else if (me.ArmParts[i].Timing == RAPID)
                        Maneuvers[(int)EnemyPartsType.ERapid].Add(me.ArmParts[i]);
                    else if (me.ArmParts[i].Timing == JUDGE)
                        Maneuvers[(int)EnemyPartsType.EJudge].Add(me.ArmParts[i]);
                    else if (me.ArmParts[i].Timing == DAMAGE)
                        Maneuvers[(int)EnemyPartsType.Edamage].Add(me.ArmParts[i]);

                }
                if (SITE == BODY)
                {
                    me.BodyParts.Add(Enemy.Wepons[BODY].Parts[i].Maneuver);

                    if (me.ArmParts[i].Timing == ACTION)
                        Maneuvers[(int)EnemyPartsType.EAction].Add(me.BodyParts[i]);
                    else if (me.ArmParts[i].Timing == RAPID)
                        Maneuvers[(int)EnemyPartsType.ERapid].Add(me.BodyParts[i]);
                    else if (me.ArmParts[i].Timing == JUDGE)
                        Maneuvers[(int)EnemyPartsType.EJudge].Add(me.BodyParts[i]);
                    else if (me.ArmParts[i].Timing == DAMAGE)
                        Maneuvers[(int)EnemyPartsType.Edamage].Add(me.BodyParts[i]);

                }
                if (SITE == LEG)
                {
                    me.LegParts.Add(Enemy.Wepons[LEG].Parts[i].Maneuver);

                    if (me.ArmParts[i].Timing == ACTION)
                        Maneuvers[(int)EnemyPartsType.EAction].Add(me.LegParts[i]);
                    else if (me.ArmParts[i].Timing == RAPID)
                        Maneuvers[(int)EnemyPartsType.ERapid].Add(me.LegParts[i]);
                    else if (me.ArmParts[i].Timing == JUDGE)
                        Maneuvers[(int)EnemyPartsType.EJudge].Add(me.LegParts[i]);
                    else if (me.ArmParts[i].Timing == DAMAGE)
                        Maneuvers[(int)EnemyPartsType.Edamage].Add(me.LegParts[i]);
                }

            }
        }

    }

    public CharaManeuver EnemyAI_Action()
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
                    (Mathf.Abs(PlayerDolls[i].area) <= Mathf.Abs(Maneuvers[(int)EnemyPartsType.EAction][ActManeuvers].MaxRange + me.area) &&
                     Mathf.Abs(PlayerDolls[i].area) >= Mathf.Abs(Maneuvers[(int)EnemyPartsType.EAction][ActManeuvers].MinRange + me.area)))
                    {
                        //使用武具更新
                        if (UseManever == null)
                        {
                            UseManever = Maneuvers[(int)EnemyPartsType.EAction][ActManeuvers];
                            //優先度更新
                            //UseManever.EnemyAI[(int)EnemyPartsType.EAction] = 0;
                        }
                        else if (UseManever.EnemyAI[(int)EnemyPartsType.EAction] < Maneuvers[(int)EnemyPartsType.EAction][ActManeuvers].EnemyAI[(int)EnemyPartsType.EAction])
                            UseManever = Maneuvers[(int)EnemyPartsType.EAction][ActManeuvers];
                    }
                }
            }
        }

        //田中さんが書いた処理なので田中さんに聞いてください
        return UseManever;
    }

    public CharaManeuver EnemyAI_NonAction(EnemyPartsType PartsType)
    {
        //全員とる
        List<Doll_blu_Nor> PlayerDolls = ManagerAccessor.Instance.battleSystem.GetCharaObj();

        // 敵キャラのエリアと選択されたマニューバの射程を絶対値で比べて、射程内であれば攻撃するか選択するコマンドを表示する
        // 敵キャラのエリアの絶対値が攻撃の最大射程以下且つ、
        // 敵キャラのエリアの絶対値が攻撃の最小射程以上なら攻撃する

        //使用武具for文
        for (int ActManeuvers = 0; ActManeuvers != Maneuvers[(int)PartsType].Count; ActManeuvers++)
        {
            for (int i = 0; i != PlayerDolls.Count; i++)
            {
                //破損判定
                if (!Maneuvers[(int)PartsType][ActManeuvers].isDmage)
                {
                    //敵の位置検索 & 射程比較
                    if (Maneuvers[(int)PartsType][ActManeuvers].MinRange != 10 &&                                   
                    (Mathf.Abs(PlayerDolls[i].area) <= Mathf.Abs(Maneuvers[(int)PartsType][ActManeuvers].MaxRange + me.area) &&
                     Mathf.Abs(PlayerDolls[i].area) >= Mathf.Abs(Maneuvers[(int)PartsType][ActManeuvers].MinRange + me.area)))
                    {
                        //使用武具更新
                        if (UseManever == null)
                        {
                            UseManever = Maneuvers[(int)PartsType][ActManeuvers];
                            //優先度更新
                            //UseManever.EnemyAI[(int)EnemyPartsType.EAction] = 0;
                        }
                        else if (UseManever.EnemyAI[(int)PartsType] < Maneuvers[(int)PartsType][ActManeuvers].EnemyAI[(int)PartsType])
                            UseManever = Maneuvers[(int)PartsType][ActManeuvers];
                    }
                }
            }
        }

        //田中さんが書いた処理なので田中さんに聞いてください
        return UseManever;
    }

    public void ActionTiming()
    {
        ;
    }

    public void RapidTiming()
    {
        ;
    }

    public void JudgeTiming()
    {
        ;
    }

    public void DamageTiming()
    {
        ;
    }
}

[System.Serializable]
public enum EnemyPartsType
{
    EAuto = -1,
    EAction = 0,
    ERapid,
    EJudge,
    Edamage,
    ESkill,
    EPartsMax = 5,
}
