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

    public CharaManeuver opponentManeuver;
    public Doll_blu_Nor opponent;

    private void Start()
    {
        //部位×4+Skill1
        Maneuvers = new List<CharaManeuver>[(int)EnemyPartsType.EPartsMax];
        for (int i = 0; i != Maneuvers.Length; i++)
            Maneuvers[i] = new List<CharaManeuver>();

        //初期武装追記
        for (int i = 0; i != 3; i++)
        {
           
            //me.HeadParts.Add(kihon.Base_Head_parts[i]);

            Maneuvers[(int)EnemyPartsType.EAction].Add(me.HeadParts[i]);

            if (me.HeadParts[i].Timing == ACTION)
                Maneuvers[(int)EnemyPartsType.EAction].Add(me.HeadParts[i]);
            else if (me.HeadParts[i].Timing == RAPID)
                Maneuvers[(int)EnemyPartsType.ERapid].Add(me.HeadParts[i]);
            else if (me.HeadParts[i].Timing == JUDGE)
                Maneuvers[(int)EnemyPartsType.EJudge].Add(me.HeadParts[i]);
            else if (me.HeadParts[i].Timing == DAMAGE)
                Maneuvers[(int)EnemyPartsType.Edamage].Add(me.HeadParts[i]);

            //me.ArmParts.Add(kihon.Base_Arm_parts[i]);

            if (me.ArmParts[i].Timing == ACTION)
                Maneuvers[(int)EnemyPartsType.EAction].Add(me.ArmParts[i]);
            else if (me.ArmParts[i].Timing == RAPID)
                Maneuvers[(int)EnemyPartsType.ERapid].Add(me.ArmParts[i]);
            else if (me.ArmParts[i].Timing == JUDGE)
                Maneuvers[(int)EnemyPartsType.EJudge].Add(me.ArmParts[i]);
            else if (me.ArmParts[i].Timing == DAMAGE)
                Maneuvers[(int)EnemyPartsType.Edamage].Add(me.ArmParts[i]);

            //me.BodyParts.Add(kihon.Base_Body_parts[i]);

            if (me.BodyParts[i].Timing == ACTION)
                Maneuvers[(int)EnemyPartsType.EAction].Add(me.BodyParts[i]);
            else if (me.BodyParts[i].Timing == RAPID)
                Maneuvers[(int)EnemyPartsType.ERapid].Add(me.BodyParts[i]);
            else if (me.BodyParts[i].Timing == JUDGE)
                Maneuvers[(int)EnemyPartsType.EJudge].Add(me.BodyParts[i]);
            else if (me.BodyParts[i].Timing == DAMAGE)
                Maneuvers[(int)EnemyPartsType.Edamage].Add(me.BodyParts[i]);

           // me.LegParts.Add(kihon.Base_Leg_parts[i]);

            if (me.LegParts[i].Timing == ACTION)
                Maneuvers[(int)EnemyPartsType.EAction].Add(me.LegParts[i]);
            else if (me.LegParts[i].Timing == RAPID)
                Maneuvers[(int)EnemyPartsType.ERapid].Add(me.LegParts[i]);
            else if (me.LegParts[i].Timing == JUDGE)
                Maneuvers[(int)EnemyPartsType.EJudge].Add(me.LegParts[i]);
            else if (me.LegParts[i].Timing == DAMAGE)
                Maneuvers[(int)EnemyPartsType.Edamage].Add(me.LegParts[i]);
        }

        //データから解析し、マニューバーを追加する
        //追加武装追記
        for (int SITE = 0; SITE != 4; SITE++)
        {
            for (int i = 0; i != Enemy.Wepons[SITE].Parts.Count; i++)
            {
                CharaManeuver AddManuver = null;
                //None情報を抜きにして整理
                if (SITE == HEAD)
                {
                    //追記するマニューバー
                    AddManuver = Enemy.Wepons[HEAD].Parts[i].Maneuver;
                    me.HeadParts.Add(AddManuver);
                    
                }
                if (SITE == ARM)
                {
                    //追記するマニューバー
                    AddManuver = Enemy.Wepons[ARM].Parts[i].Maneuver;
                    me.ArmParts.Add(Enemy.Wepons[ARM].Parts[i].Maneuver);

                }
                if (SITE == BODY)
                {
                    //追記するマニューバー
                    AddManuver = Enemy.Wepons[BODY].Parts[i].Maneuver;
                    me.BodyParts.Add(Enemy.Wepons[BODY].Parts[i].Maneuver);

                }
                if (SITE == LEG)
                {
                    //追記するマニューバー
                    AddManuver = Enemy.Wepons[LEG].Parts[i].Maneuver;
                    me.LegParts.Add(Enemy.Wepons[LEG].Parts[i].Maneuver);
                }
                //追記
                if (AddManuver.Timing == ACTION)
                    Maneuvers[(int)EnemyPartsType.EAction].Add(AddManuver);
                else if (AddManuver.Timing == RAPID)
                    Maneuvers[(int)EnemyPartsType.ERapid].Add(AddManuver);
                else if (AddManuver.Timing == JUDGE)
                    Maneuvers[(int)EnemyPartsType.EJudge].Add(AddManuver);
                else if (AddManuver.Timing == DAMAGE)
                    Maneuvers[(int)EnemyPartsType.Edamage].Add(AddManuver);
                else
                    ;//登録の必要ないタイミング
            }
        }

        EnemyAI_Rapid(opponentManeuver, opponent);
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
                    if (Maneuvers[(int)EnemyPartsType.EAction][ActManeuvers].MinRange != 10 &&                                  
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

    public void EnemyAI_Rapid(CharaManeuver OpponentManeuver, Doll_blu_Nor Opponent)
    {
        //全員いるわ
        List<Doll_blu_Nor> PlayerDolls = ManagerAccessor.Instance.battleSystem.GetCharaObj();

        int MaxOpponentRange = OpponentManeuver.MaxRange;
        int MinopponentRange = OpponentManeuver.MinRange;

        int differenceRange = 0;

        // 敵キャラのエリアと選択されたマニューバの射程を絶対値で比べて、射程内であれば攻撃するか選択するコマンドを表示する
        // 敵キャラのエリアの絶対値が攻撃の最大射程以下且つ、
        // 敵キャラのエリアの絶対値が攻撃の最小射程以上なら攻撃する
        //使用武具for文
        for (int ActManeuvers = 0; ActManeuvers != Maneuvers[(int)EnemyPartsType.ERapid].Count; ActManeuvers++)
        {
            //破損判定&使用判定
            if (!Maneuvers[(int)EnemyPartsType.ERapid][ActManeuvers].isDmage && !Maneuvers[(int)EnemyPartsType.ERapid][ActManeuvers].isDmage)
            {
                //移動以外のマニューバー
                if (Maneuvers[(int)EnemyPartsType.ERapid][ActManeuvers].Moving == 0)
                {

                }
                //移動まにゅばー
                else
                {
                    //相手の武具から射程にどっち方向かつどれだけ余裕があるか
                    //例:狙われている武具射程が2~0で今の状況が射程の数字でいうところの2,1,0のどれかを算出
                    differenceRange = Mathf.Abs(Opponent.area - me.area) - MaxOpponentRange;
                    if (Mathf.Abs(differenceRange) < Mathf.Abs(Mathf.Abs(Opponent.area - me.area) - MinopponentRange))
                        differenceRange = Mathf.Abs(Opponent.area - me.area) - MinopponentRange;

                    //この時点で数字=射程差,+=天国側,-地獄側が分かる,0の場合は別途
                    //今検証中のPartsが射程外になるまで効果が及ぼせるか判定
                    //射程が1以上余裕がある場合
                    if (Maneuvers[(int)EnemyPartsType.ERapid][ActManeuvers].Moving > Mathf.Abs(differenceRange))
                    {
                        //効果が及ぼせるとき
                        //敵の位置検索 & 射程比較(相手を動かす)
                        if (Maneuvers[(int)EnemyPartsType.ERapid][ActManeuvers].MinRange != 10 &&
                    (Mathf.Abs(Opponent.area) <= Mathf.Abs(Maneuvers[(int)EnemyPartsType.ERapid][ActManeuvers].MaxRange + me.area) &&
                     Mathf.Abs(Opponent.area) >= Mathf.Abs(Maneuvers[(int)EnemyPartsType.ERapid][ActManeuvers].MinRange + me.area)))
                        {
                            //使用武具更新
                            if (UseManever == null)
                            {
                                UseManever = Maneuvers[(int)EnemyPartsType.ERapid][ActManeuvers];
                                //優先度更新
                                //UseManever.EnemyAI[(int)EnemyPartsType.EAction] = 0;
                            }
                            else if (UseManever.EnemyAI[(int)EnemyPartsType.ERapid] < Maneuvers[(int)EnemyPartsType.ERapid][ActManeuvers].EnemyAI[(int)EnemyPartsType.ERapid])
                                UseManever = Maneuvers[(int)EnemyPartsType.ERapid][ActManeuvers];

                            //相手の射程が0の場合
                            if (Mathf.Abs(differenceRange) == 0)
                            {
                                if (4 >= Opponent.area + UseManever.Moving)
                                {
                                    //とりま地獄側へ移動
                                    Debug.Log("Enemy:地獄にとばす");
                                    return;
                                }
                                else if(0 <= Opponent.area - UseManever.Moving)
                                {
                                    //無理ならしぶしぶ反対へ
                                    Debug.Log("Enemy:天国にとばす");
                                    return;
                                }

                            }
                            else
                            {
                                //天国側へ移動(移動して場外に行くかも判定)
                                if (differenceRange > 0)
                                {
                                    if (0 <= Opponent.area - UseManever.Moving)
                                    {
                                        Debug.Log("Enemy:天国にとばす");
                                        return;
                                    }
                                }
                                //地獄側へ移動
                                else if (differenceRange < 0)
                                {
                                    if (4 >= Opponent.area + UseManever.Moving)
                                    {
                                        Debug.Log("Enemy:地獄にとばす");
                                        return;
                                    }
                                }
                            }

                        }
                        //自身に及ぼす場合
                        else
                        {
                            //使用武具更新
                            if (UseManever == null)
                            {
                                UseManever = Maneuvers[(int)EnemyPartsType.ERapid][ActManeuvers];
                                //優先度更新
                                //UseManever.EnemyAI[(int)EnemyPartsType.EAction] = 0;
                            }
                            else if (UseManever.EnemyAI[(int)EnemyPartsType.ERapid] < Maneuvers[(int)EnemyPartsType.ERapid][ActManeuvers].EnemyAI[(int)EnemyPartsType.ERapid])
                                UseManever = Maneuvers[(int)EnemyPartsType.ERapid][ActManeuvers];

                            //相手の射程が0の場合
                            if (Mathf.Abs(differenceRange) == 0)
                            {
                                if (4 >= me.area + UseManever.Moving)
                                {
                                    //とりま地獄側へ移動
                                    Debug.Log("Enemy:地獄にとばす");
                                    return;
                                }
                                else if (0 <= me.area - UseManever.Moving)
                                {
                                    //無理ならしぶしぶ反対へ
                                    Debug.Log("Enemy:天国にとばす");
                                    return;
                                }
                            }
                            else
                            {
                                //天国側へ移動(移動して場外に行くかも判定)
                                if (differenceRange > 0)
                                {
                                    if (0 <= me.area - UseManever.Moving)
                                    {
                                        Debug.Log("Enemy:天国にとばす");
                                        return;
                                    }
                                }
                                //地獄側へ移動
                                else if (differenceRange < 0)
                                {
                                    if (4 >= me.area + UseManever.Moving)
                                    {
                                        Debug.Log("Enemy:地獄にとばす");
                                        return;
                                    }
                                }
                            }
                        }
                    }
                    //自身では対応不可
                    else
                    {
                        Debug.Log("Enemy:Help!");
                        return;
                        //全味方に
                        for (int i = 0; i != PlayerDolls.Count; i++)
                        {
                            //他の味方に救援を送る
                            PlayerDolls[i].GetComponent<ObjEnemy>().HelpMoveRapid(me, differenceRange);
                        }
                    }
                }
            }
            //自身では対応不可
            else
            {
                Debug.Log("Enemy:Help!");
                return;
                //全味方に
                for (int i = 0; i != PlayerDolls.Count; i++)
                {
                    //他の味方に救援を送る
                    PlayerDolls[i].GetComponent<ObjEnemy>().HelpMoveRapid(me, differenceRange);
                }
            }

        }

        //田中さんが書いた処理なので田中さんに聞いてください
        Debug.Log("なんでここまでこれたんや...");
        return;
    }

    public void EnemyAI_Judge(CharaManeuver OpponentManeuver, Doll_blu_Nor Opponent)
    {
        //全員いるわ
        List<Doll_blu_Nor> PlayerDolls = ManagerAccessor.Instance.battleSystem.GetCharaObj();

        int MaxOpponentRange = OpponentManeuver.MaxRange;
        int MinopponentRange = OpponentManeuver.MinRange;

        int differenceRange = 0;

        // 敵キャラのエリアと選択されたマニューバの射程を絶対値で比べて、射程内であれば攻撃するか選択するコマンドを表示する
        // 敵キャラのエリアの絶対値が攻撃の最大射程以下且つ、
        // 敵キャラのエリアの絶対値が攻撃の最小射程以上なら攻撃する
        //使用武具for文
        for (int ActManeuvers = 0; ActManeuvers != Maneuvers[(int)EnemyPartsType.ERapid].Count; ActManeuvers++)
        {
            //破損判定&使用判定
            if (!Maneuvers[(int)EnemyPartsType.ERapid][ActManeuvers].isDmage && !Maneuvers[(int)EnemyPartsType.ERapid][ActManeuvers].isDmage)
            {
                //移動以外のマニューバー
                if (Maneuvers[(int)EnemyPartsType.ERapid][ActManeuvers].Moving == 0)
                {

                }
                //移動まにゅばー
                else
                {
                    //相手の武具から射程にどっち方向かつどれだけ余裕があるか
                    //例:狙われている武具射程が2~0で今の状況が射程の数字でいうところの2,1,0のどれかを算出
                    differenceRange = Mathf.Abs(Opponent.area - me.area) - MaxOpponentRange;
                    if (Mathf.Abs(differenceRange) < Mathf.Abs(Mathf.Abs(Opponent.area - me.area) - MinopponentRange))
                        differenceRange = Mathf.Abs(Opponent.area - me.area) - MinopponentRange;

                    //この時点で数字=射程差,+=天国側,-地獄側が分かる,0の場合は別途
                    //今検証中のPartsが射程外になるまで効果が及ぼせるか判定
                    //射程が1以上余裕がある場合
                    if (Maneuvers[(int)EnemyPartsType.ERapid][ActManeuvers].Moving > Mathf.Abs(differenceRange))
                    {
                        //効果が及ぼせるとき
                        //敵の位置検索 & 射程比較(相手を動かす)
                        if (Maneuvers[(int)EnemyPartsType.ERapid][ActManeuvers].MinRange != 10 &&
                    (Mathf.Abs(Opponent.area) <= Mathf.Abs(Maneuvers[(int)EnemyPartsType.ERapid][ActManeuvers].MaxRange + me.area) &&
                     Mathf.Abs(Opponent.area) >= Mathf.Abs(Maneuvers[(int)EnemyPartsType.ERapid][ActManeuvers].MinRange + me.area)))
                        {
                            //使用武具更新
                            if (UseManever == null)
                            {
                                UseManever = Maneuvers[(int)EnemyPartsType.ERapid][ActManeuvers];
                                //優先度更新
                                //UseManever.EnemyAI[(int)EnemyPartsType.EAction] = 0;
                            }
                            else if (UseManever.EnemyAI[(int)EnemyPartsType.ERapid] < Maneuvers[(int)EnemyPartsType.ERapid][ActManeuvers].EnemyAI[(int)EnemyPartsType.ERapid])
                                UseManever = Maneuvers[(int)EnemyPartsType.ERapid][ActManeuvers];

                            //相手の射程が0の場合
                            if (Mathf.Abs(differenceRange) == 0)
                            {
                                if (4 >= Opponent.area + UseManever.Moving)
                                {
                                    //とりま地獄側へ移動
                                    Debug.Log("Enemy:地獄にとばす");
                                    return;
                                }
                                else if (0 <= Opponent.area - UseManever.Moving)
                                {
                                    //無理ならしぶしぶ反対へ
                                    Debug.Log("Enemy:天国にとばす");
                                    return;
                                }

                            }
                            else
                            {
                                //天国側へ移動(移動して場外に行くかも判定)
                                if (differenceRange > 0)
                                {
                                    if (0 <= Opponent.area - UseManever.Moving)
                                    {
                                        Debug.Log("Enemy:天国にとばす");
                                        return;
                                    }
                                }
                                //地獄側へ移動
                                else if (differenceRange < 0)
                                {
                                    if (4 >= Opponent.area + UseManever.Moving)
                                    {
                                        Debug.Log("Enemy:地獄にとばす");
                                        return;
                                    }
                                }
                            }

                        }
                        //自身に及ぼす場合
                        else
                        {
                            //使用武具更新
                            if (UseManever == null)
                            {
                                UseManever = Maneuvers[(int)EnemyPartsType.ERapid][ActManeuvers];
                                //優先度更新
                                //UseManever.EnemyAI[(int)EnemyPartsType.EAction] = 0;
                            }
                            else if (UseManever.EnemyAI[(int)EnemyPartsType.ERapid] < Maneuvers[(int)EnemyPartsType.ERapid][ActManeuvers].EnemyAI[(int)EnemyPartsType.ERapid])
                                UseManever = Maneuvers[(int)EnemyPartsType.ERapid][ActManeuvers];

                            //相手の射程が0の場合
                            if (Mathf.Abs(differenceRange) == 0)
                            {
                                if (4 >= me.area + UseManever.Moving)
                                {
                                    //とりま地獄側へ移動
                                    Debug.Log("Enemy:地獄にとばす");
                                    return;
                                }
                                else if (0 <= me.area - UseManever.Moving)
                                {
                                    //無理ならしぶしぶ反対へ
                                    Debug.Log("Enemy:天国にとばす");
                                    return;
                                }
                            }
                            else
                            {
                                //天国側へ移動(移動して場外に行くかも判定)
                                if (differenceRange > 0)
                                {
                                    if (0 <= me.area - UseManever.Moving)
                                    {
                                        Debug.Log("Enemy:天国にとばす");
                                        return;
                                    }
                                }
                                //地獄側へ移動
                                else if (differenceRange < 0)
                                {
                                    if (4 >= me.area + UseManever.Moving)
                                    {
                                        Debug.Log("Enemy:地獄にとばす");
                                        return;
                                    }
                                }
                            }
                        }
                    }
                    //自身では対応不可
                    else
                    {
                        Debug.Log("Enemy:Help!");
                        return;
                        //全味方に
                        for (int i = 0; i != PlayerDolls.Count; i++)
                        {
                            //他の味方に救援を送る
                            PlayerDolls[i].GetComponent<ObjEnemy>().HelpMoveRapid(me, differenceRange);
                        }
                    }
                }
            }
            //自身では対応不可
            else
            {
                Debug.Log("Enemy:Help!");
                return;
                //全味方に
                for (int i = 0; i != PlayerDolls.Count; i++)
                {
                    //他の味方に救援を送る
                    PlayerDolls[i].GetComponent<ObjEnemy>().HelpMoveRapid(me, differenceRange);
                }
            }

        }

        //田中さんが書いた処理なので田中さんに聞いてください
        Debug.Log("なんでここまでこれたんや...");
        return;
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

    public void HelpMoveRapid(Doll_blu_Nor Follow, int NeedRange)
    {
        for (int ActManeuvers = 0; ActManeuvers != Maneuvers[(int)EnemyPartsType.ERapid].Count; ActManeuvers++)
        {
            //破損判定&使用判定
            if (!Maneuvers[(int)EnemyPartsType.ERapid][ActManeuvers].isDmage && !Maneuvers[(int)EnemyPartsType.ERapid][ActManeuvers].isDmage)
            {
                //移動まにゅばー選出（出来そうな)
                if (Maneuvers[(int)EnemyPartsType.ERapid][ActManeuvers].Moving > NeedRange)
                {
                    //宣言（対象に）
                    //return 相手にラピットかけて回避する的な関数？に送る（自分,自分まにゅ,対象味方）
                }
                //余分な計で便乗行動権回復勢(Actionとかで管理するかも？)
                //if()
            }
        }

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
