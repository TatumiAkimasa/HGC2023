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
    public int Expected_FatalDamage;//致命傷ダメージ数

    private void Start()
    {
        opponentManeuver.EffectNum.Add(EffNum.Damage, 1);

        //部位×4+Skill1
        Maneuvers = new List<CharaManeuver>[(int)EnemyPartsType.EPartsMax];
        for (int i = 0; i != Maneuvers.Length; i++)
            Maneuvers[i] = new List<CharaManeuver>();

        //初期武装追記
        for (int i = 0; i != 3; i++)
        {
           
            //me.HeadParts.Add(kihon.Base_Head_parts[i]);

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

        TableParts_EffctUp(Enemy);

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

    }

    public void EnemyAI_Action()
    {
        List<Doll_blu_Nor> PlayerDolls = new List<Doll_blu_Nor>();
        Doll_blu_Nor target = null;
       //PLのみ取得
        for (int i=0;i< ManagerAccessor.Instance.battleSystem.GetCharaObj().Count;i++)
        {
            if(ManagerAccessor.Instance.battleSystem.GetCharaObj()[i].gameObject.CompareTag("AllyChara"))
            {
                PlayerDolls.Add(ManagerAccessor.Instance.battleSystem.GetCharaObj()[i]);
            }
        }
        

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
                        target = PlayerDolls[i];

                    }
                }
            }
        }

        //全ACTION吟味し、一番有効値が高い物を使用する
        ProcessAccessor.Instance.actTiming.ExeAtkManeuver(target, UseManever, me);
        return;
    }
    //移動逃げ
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
                if (Maneuvers[(int)EnemyPartsType.ERapid][ActManeuvers].EffectNum[EffNum.Move] == 0)
                {
                    ;//今のところない？
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
                    if (Maneuvers[(int)EnemyPartsType.ERapid][ActManeuvers].EffectNum[EffNum.Move] > Mathf.Abs(differenceRange))
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
                                if (4 >= Opponent.area + UseManever.EffectNum[EffNum.Move])
                                {
                                    //とりま地獄側へ移動
                                    Debug.Log("Enemy:地獄にとばす");
                                    ProcessAccessor.Instance.rpdTiming.AddRapidManeuver(me, Opponent, UseManever);
                                    return;
                                }
                                else if(0 <= Opponent.area - UseManever.EffectNum[EffNum.Move])
                                {
                                    //無理ならしぶしぶ反対へ
                                    Debug.Log("Enemy:天国にとばす");
                                    ProcessAccessor.Instance.rpdTiming.AddRapidManeuver(me, Opponent, UseManever);
                                    return;
                                }

                            }
                            else
                            {
                                //天国側へ移動(移動して場外に行くかも判定)
                                if (differenceRange > 0)
                                {
                                    if (0 <= Opponent.area - UseManever.EffectNum[EffNum.Move])
                                    {
                                        Debug.Log("Enemy:天国にとばす");
                                        ProcessAccessor.Instance.rpdTiming.AddRapidManeuver(me, Opponent, UseManever);
                                        return;
                                    }
                                }
                                //地獄側へ移動
                                else if (differenceRange < 0)
                                {
                                    if (4 >= Opponent.area + UseManever.EffectNum[EffNum.Move])
                                    {
                                        Debug.Log("Enemy:地獄にとばす");
                                        ProcessAccessor.Instance.rpdTiming.AddRapidManeuver(me, Opponent, UseManever);
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
                                if (4 >= me.area + UseManever.EffectNum[EffNum.Move])
                                {
                                    //とりま地獄側へ移動
                                    Debug.Log("Enemy:地獄にとばす");
                                    return;
                                }
                                else if (0 <= me.area - UseManever.EffectNum[EffNum.Move])
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
                                    if (0 <= me.area - UseManever.EffectNum[EffNum.Move])
                                    {
                                        Debug.Log("Enemy:天国にとばす");
                                        return;
                                    }
                                }
                                //地獄側へ移動
                                else if (differenceRange < 0)
                                {
                                    if (4 >= me.area + UseManever.EffectNum[EffNum.Move])
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
                            PlayerDolls[i].GetComponent<ObjEnemy>().HelpMoveRapid(me, Opponent,differenceRange);
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
                    PlayerDolls[i].GetComponent<ObjEnemy>().HelpMoveRapid(me, Opponent,differenceRange);
                }
            }

        }

        //田中さんが書いた処理なので田中さんに聞いてください
        Debug.Log("なんでここまでこれたんや...");
        return;
    }
    //妨害処理
    public void EnemyAI_Judge(CharaManeuver OpponentManeuver, Doll_blu_Nor Opponent,int DiceRoll,int TargetParts,int Power)
    {
       //もう一度要求のパターン
        if (Power != 0)
            ;
        //ダイス値が即対応できる範囲(6以下で致命傷判断を入れる)
        else if (DiceRoll >= 6)
        {
            //致命傷1(現在のパーツ数で許容できるダメージ量か)判断。問題ないならスルー
            if (TargetParts == HEAD)
            {
                if (OpponentManeuver.Moving == 0 && OpponentManeuver.EffectNum[EffNum.Damage] < me.HeadParts.Count)
                    return;
            }
            else if (TargetParts == ARM)
            {
                if (OpponentManeuver.Moving == 0 && OpponentManeuver.EffectNum[EffNum.Damage] < me.ArmParts.Count)
                    return;
            }
            else if (TargetParts == LEG)
            {
                if (OpponentManeuver.Moving == 0 && OpponentManeuver.EffectNum[EffNum.Damage] < me.LegParts.Count)
                    return;
            }
            else if (TargetParts == BODY)
            {
                if (OpponentManeuver.Moving == 0 && OpponentManeuver.EffectNum[EffNum.Damage] < me.BodyParts.Count)
                    return;
            }

            //致命傷2(許容できるダメージ量か)判断。問題ないならスルー
            if (opponentManeuver.EffectNum[EffNum.Damage] < Expected_FatalDamage)
                return;
        }


        //致命傷の場合（どうしてもさけたい)
        //全員いるわ
        List<Doll_blu_Nor> PlayerDolls = ManagerAccessor.Instance.battleSystem.GetCharaObj();

        int MaxOpponentRange = OpponentManeuver.MaxRange;
        int MinopponentRange = OpponentManeuver.MinRange;

        // 敵キャラのエリアと選択されたマニューバの射程を絶対値で比べて、射程内であれば攻撃するか選択するコマンドを表示する
        // 敵キャラのエリアの絶対値が攻撃の最大射程以下且つ、
        // 敵キャラのエリアの絶対値が攻撃の最小射程以上なら攻撃する
        //使用武具for文
        for (int ActManeuvers = 0; ActManeuvers != Maneuvers[(int)EnemyPartsType.EJudge].Count; ActManeuvers++)
        {
            //破損判定&使用判定
            if (!Maneuvers[(int)EnemyPartsType.EJudge][ActManeuvers].isDmage && !Maneuvers[(int)EnemyPartsType.EJudge][ActManeuvers].isDmage)
            {
                if (Maneuvers[(int)EnemyPartsType.EJudge][ActManeuvers].MinRange != 10 &&
            (Mathf.Abs(Opponent.area) <= Mathf.Abs(Maneuvers[(int)EnemyPartsType.EJudge][ActManeuvers].MaxRange + me.area) &&
             Mathf.Abs(Opponent.area) >= Mathf.Abs(Maneuvers[(int)EnemyPartsType.EJudge][ActManeuvers].MinRange + me.area)))
                {
                    //使用武具更新
                    UseManever = UseManuber_Change((int)EnemyPartsType.EJudge, ActManeuvers, UseManever);
                   
                    //マニューバー適応結果がまだ命中圏内の場合
                    if (5 < DiceRoll - UseManever.EffectNum["Judge"] - Power)
                    {
                        Debug.Log("もう一度妨害できるかチャレンジ！");
                        return;
                        //もう一度処理を繰り返す
                        EnemyAI_Judge(OpponentManeuver, Opponent, DiceRoll, TargetParts, UseManever.EffectNum["Judge"]);
                        //要求（行動
                        ProcessAccessor.Instance.jdgTiming.ExeJudgManeuver(UseManever, me);

                    }
                    else
                    {
                        //要求（行動
                        Debug.Log("妨害チャレンジ完了");
                        ProcessAccessor.Instance.jdgTiming.ExeJudgManeuver(UseManever, me);
                        return;
                    }

                }

            }
           
        }

        //自身では対応不可
        Debug.Log("Enemy:JudgeHelp!");
        return;
        //全味方に
        for (int i = 0; i != PlayerDolls.Count; i++)
        {
            //他の味方に救援を送る
            Power += PlayerDolls[i].GetComponent<ObjEnemy>().HelpJudge_OP(Opponent,OpponentManeuver.EffectNum["Action"] - Power);

            //許容値なら
            if (6 > DiceRoll - Power)
                break;
        }

        //わんちゃん繰り返し他の味方に妨害重ねてもらう処理イルカも
        return;
    }
    //支援処理
    public void EnemyAI_Judge(CharaManeuver OpponentManeuver, Doll_blu_Nor Opponent, int DiceRoll, int Power)
    {
        int MaxOpponentRange = OpponentManeuver.MaxRange;
        int MinopponentRange = OpponentManeuver.MinRange;

        //全員いるわ
        List<Doll_blu_Nor> PlayerDolls = ManagerAccessor.Instance.battleSystem.GetCharaObj();

        //使用武具for文
        for (int ActManeuvers = 0; ActManeuvers != Maneuvers[(int)EnemyPartsType.EJudge].Count; ActManeuvers++)
        {
            //破損判定&使用判定
            if (!Maneuvers[(int)EnemyPartsType.EJudge][ActManeuvers].isDmage && !Maneuvers[(int)EnemyPartsType.EJudge][ActManeuvers].isDmage)
            {
                //自身の位置と射程比較
                if ( (Mathf.Abs(Opponent.area) <= Mathf.Abs(Maneuvers[(int)EnemyPartsType.EJudge][ActManeuvers].MaxRange + me.area) &&
                      Mathf.Abs(Opponent.area) >= Mathf.Abs(Maneuvers[(int)EnemyPartsType.EJudge][ActManeuvers].MinRange + me.area)))
                {
                    //使用武具更新
                    UseManever = UseManuber_Change((int)EnemyPartsType.EJudge, ActManeuvers, UseManever);
                   
                    //マニューバー適応結果がまだ命中圏内の場合
                    if (5 > DiceRoll - UseManever.EffectNum["Judge"] - Power)
                    {
                        //もう一度処理を繰り返す
                        
                        //要求（行動

                        return;
                    }
                    else
                    {
                        EnemyAI_Judge(OpponentManeuver, Opponent, DiceRoll, UseManever.EffectNum["Judge"]);

                        //要求（行動
                        return;
                    }

                }
            }

        }

        //自身では対応不可
        Debug.Log("Enemy:Help!");
        return;
        //全味方に
        for (int i = 0; i != PlayerDolls.Count; i++)
        {
            //他の味方に救援を送る
            Power += PlayerDolls[i].GetComponent<ObjEnemy>().HelpJudge_ME(me, OpponentManeuver.EffectNum["Action"] - Power);

            //許容値なら
            if (6 > DiceRoll - Power)
                break;
        }

        //わんちゃん繰り返し他の味方に妨害重ねてもらう処理イルカも
        return;
    }
    //ダメージタイミングの場合
    public void EnemyAI_Damage(CharaManeuver OpponentManeuver, Doll_blu_Nor Opponent,bool ATorDF)
    {
        //全員いるわ
        List<Doll_blu_Nor> PlayerDolls = ManagerAccessor.Instance.battleSystem.GetCharaObj();

        //使用武具for文
        for (int ActManeuvers = 0; ActManeuvers != Maneuvers[(int)EnemyPartsType.Edamage].Count; ActManeuvers++)
        {
            //破損判定&使用判定
            if (!Maneuvers[(int)EnemyPartsType.Edamage][ActManeuvers].isDmage && !Maneuvers[(int)EnemyPartsType.Edamage][ActManeuvers].isDmage)
            {
                //選択したものがガード系の技なら
                if (Maneuvers[(int)EnemyPartsType.Edamage][ActManeuvers].EffectNum.ContainsKey(EffNum.Guard) && ATorDF)
                {
                    //使用武具更新
                    UseManever = UseManuber_Change((int)EnemyPartsType.Edamage, ActManeuvers, UseManever);

                }
                //ダメージ追加系の場合
                else if(Maneuvers[(int)EnemyPartsType.Edamage][ActManeuvers].EffectNum.ContainsKey(EffNum.Damage) && !ATorDF)
                {
                    //使用武具更新
                    UseManever = UseManuber_Change((int)EnemyPartsType.Edamage, ActManeuvers, UseManever);

                }

                
            }

        }

        //上記でマニューバーがあれば送る
        if(UseManever!=null)
        ProcessAccessor.Instance.dmgTiming.ExeManeuver(UseManever, me);

        //防御のタイミングかつ助けてほしい場合HELPする
        if (UseManever==null&&ATorDF)
        {
            //自身では対応不可
            Debug.Log("Enemy:Help!");
            return;
            //全味方にかばう要請
            for (int i = 0; i != PlayerDolls.Count; i++)
            {
                //他の味方に救援を送る
                PlayerDolls[i].GetComponent<ObjEnemy>().HelpDamege(me);

            }
        }
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

    //移動補助目的HELP(RAPID
    public void HelpMoveRapid(Doll_blu_Nor Follow, Doll_blu_Nor Opponent, int NeedRange)
    {
        for (int ActManeuvers = 0; ActManeuvers != Maneuvers[(int)EnemyPartsType.ERapid].Count; ActManeuvers++)
        {
            //破損判定&使用判定
            if (!Maneuvers[(int)EnemyPartsType.ERapid][ActManeuvers].isDmage && !Maneuvers[(int)EnemyPartsType.ERapid][ActManeuvers].isDmage)
            {
                //敵の位置検索 & 射程比較(敵を動かす)
                if (Maneuvers[(int)EnemyPartsType.ERapid][ActManeuvers].MinRange != 10 &&
            (Mathf.Abs(Opponent.area) <= Mathf.Abs(Maneuvers[(int)EnemyPartsType.ERapid][ActManeuvers].MaxRange + me.area) &&
             Mathf.Abs(Opponent.area) >= Mathf.Abs(Maneuvers[(int)EnemyPartsType.ERapid][ActManeuvers].MinRange + me.area)))
                {
                    //宣言（対象に）+=天国へ,-は地獄へ飛ばす
                    //return 相手にラピットかけて回避する的な関数？に送る（自分,自分まにゅ,対象味方）
                }
                //敵の位置検索 & 射程比較(味方を動かす)
                else if (Maneuvers[(int)EnemyPartsType.ERapid][ActManeuvers].MinRange != 10 &&
            (Mathf.Abs(Follow.area) <= Mathf.Abs(Maneuvers[(int)EnemyPartsType.ERapid][ActManeuvers].MaxRange + me.area) &&
             Mathf.Abs(Follow.area) >= Mathf.Abs(Maneuvers[(int)EnemyPartsType.ERapid][ActManeuvers].MinRange + me.area)))
                {
                    //宣言（対象に）+=天国へ,-は地獄へ飛ばす
                    //return 相手にラピットかけて回避する的な関数？に送る（自分,自分まにゅ,対象味方）
                }
                //余分な計で便乗行動権回復勢(Actionとかで管理するかも？)
                //if()
            }
        }

    }

    //妨害目的HELP(JUDGE
    public int HelpJudge_OP(Doll_blu_Nor Opponent,int NeedPower)
    {
        for (int ActManeuvers = 0; ActManeuvers != Maneuvers[(int)EnemyPartsType.EJudge].Count; ActManeuvers++)
        {
            //破損判定&使用判定
            if (!Maneuvers[(int)EnemyPartsType.EJudge][ActManeuvers].isDmage && !Maneuvers[(int)EnemyPartsType.EJudge][ActManeuvers].isDmage)
            {
                //敵の位置検索 & 射程比較(敵を動かす)
                if (Maneuvers[(int)EnemyPartsType.EJudge][ActManeuvers].MinRange != 10 &&
            (Mathf.Abs(Opponent.area) <= Mathf.Abs(Maneuvers[(int)EnemyPartsType.EJudge][ActManeuvers].MaxRange + me.area) &&
             Mathf.Abs(Opponent.area) >= Mathf.Abs(Maneuvers[(int)EnemyPartsType.EJudge][ActManeuvers].MinRange + me.area)))
                {
                    //妨害まにゅばー
                    //宣言（対象に）

                    //妨害値を加えた状態で受けている側の奴にもう一度判断させる。
                    return Maneuvers[(int)EnemyPartsType.EJudge][ActManeuvers].EffectNum["Judge"];
                }
            }
        }

        return 0;

    }

    public int HelpJudge_ME(Doll_blu_Nor Follow,int NeedPower)
    {
        for (int ActManeuvers = 0; ActManeuvers != Maneuvers[(int)EnemyPartsType.EJudge].Count; ActManeuvers++)
        {
            //破損判定&使用判定
            if (!Maneuvers[(int)EnemyPartsType.EJudge][ActManeuvers].isDmage && !Maneuvers[(int)EnemyPartsType.EJudge][ActManeuvers].isDmage)
            {
                //支援まにゅばー
                //宣言（対象に）

                //妨害値を加えた状態で受けている側の奴にもう一度判断させる。
                return Maneuvers[(int)EnemyPartsType.EJudge][ActManeuvers].EffectNum["Judge"];
            }
        }

        return 0;

    }

    public void HelpDamege(Doll_blu_Nor Follow)
    {
        for (int ActManeuvers = 0; ActManeuvers != Maneuvers[(int)EnemyPartsType.ERapid].Count; ActManeuvers++)
        {
            //破損判定&使用判定
            if (!Maneuvers[(int)EnemyPartsType.ERapid][ActManeuvers].isDmage && !Maneuvers[(int)EnemyPartsType.ERapid][ActManeuvers].isDmage)
            {
                //敵の位置検索 & 射程比較(味方を動かす)-なお今のところ「庇う」ぐらい
                if (Maneuvers[(int)EnemyPartsType.ERapid][ActManeuvers].MinRange != 10 &&
            (Mathf.Abs(Follow.area) <= Mathf.Abs(Maneuvers[(int)EnemyPartsType.ERapid][ActManeuvers].MaxRange + me.area) &&
             Mathf.Abs(Follow.area) >= Mathf.Abs(Maneuvers[(int)EnemyPartsType.ERapid][ActManeuvers].MinRange + me.area)))
                {
                    //ROLL処理
                    return;
                }
            }
        }
    }

    //public List<CharaManeuver> GetDamageUPList(int TargetParts, int sonsyoukazu)
    //{
    //    List<CharaManeuver> DamageParts = new List<CharaManeuver>();
    //    int[] Discarded_num = new int[sonsyoukazu];

    //    for (int i = 0; i != sonsyoukazu; i++)
    //        Discarded_num = 0;

    //    int nowsonsyou = 0;
    //    if (TargetParts == HEAD)
    //    {
    //        //必須廃棄
    //        for (int i = 0; i != me.HeadParts.Count; i++)
    //        {
    //            //選考0,損傷して欲しい物
    //            if (i == 1200000000)
    //            {
    //                if (74)
    //                    DamageParts.Add(me.HeadParts[i]);
    //                Discarded_num[nowsonsyou] = 1500;
    //                nowsonsyou++;
    //            }
    //            //選考1,はらわた系統
    //            else if (me.HeadParts[i].EnemyAI == null)
    //            {
    //                DamageParts.Add(me.HeadParts[i]);
    //                Discarded_num[nowsonsyou] = 1000;
    //                nowsonsyou++;
    //            }

    //        }

    //        //泣く泣く廃棄
    //        for (int i = 0; i != me.HeadParts.Count; i++)
    //        {
    //            //選考2-1,使用済みか
    //            //選考2-2,使用済みなら廃棄期待値増加
    //            //選考3,未使用で効果期待値が低い物
    //        }
    //    }

    //    else if (TargetParts == ARM)
    //        Maneuvers[(int)EnemyPartsType.ERapid].Add(me.LegParts[i]);
    //    else if (TargetParts == BODY)
    //        Maneuvers[(int)EnemyPartsType.EJudge].Add(me.LegParts[i]);
    //    else if (TargetParts == LEG)
    //        Maneuvers[(int)EnemyPartsType.Edamage].Add(me.LegParts[i]);
    //}

    private CharaManeuver UseManuber_Change(int ActionType,int ActManuber,CharaManeuver NowManuver)
    {
        //使用武具更新
        //何も決まってない場合
        if (NowManuver == null)
        {
            return Maneuvers[ActionType][ActManuber];
           
        }
        //あって、かつ優先度が高い場合
        else if (NowManuver.EnemyAI[ActionType] < Maneuvers[ActionType][ActManuber].EnemyAI[ActionType])
            return Maneuvers[ActionType][ActManuber];

        //特になにもない場合
        return NowManuver;
    }

    private bool DiscardedManuber_comparison(int a,int[] aa)
    {
        //必須廃棄
        for (int i = 0; i != aa.Length; i++)
        {
            //上からマニューバーをなけりゃとりあえず登録
            if(aa[i]==0)
                return true;
            //候補が既存のものより優先度が高ければ入れ替え
            else if (aa[i] < a)
                return true;
        }
        return false;
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
