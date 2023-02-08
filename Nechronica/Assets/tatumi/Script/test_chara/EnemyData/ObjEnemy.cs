using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjEnemy : ClassData_
{
    [SerializeField]
    private Table_Enemy Enemy;

    [SerializeField]
    private kihonnpatu kihon;

    public Doll_blu_Nor me;
  
    public List<CharaManeuver>[] Maneuvers;
    private CharaManeuver UseManever;

    //public CharaManeuver opponentManeuver;
    //public Doll_blu_Nor opponent;
    public int Expected_FatalDamage;//致命傷ダメージ数
    public bool DOOLmode;//敵の分類がドールか否か
    public int armynum;//レギオンの場合の人数
    public bool arrmyFlag=false;

    private void Start()
    {
        //部位×4+Skill1
        Maneuvers = new List<CharaManeuver>[(int)EnemyPartsType.EPartsMax];
        for (int i = 0; i != Maneuvers.Length; i++)
            Maneuvers[i] = new List<CharaManeuver>();

        if (DOOLmode)
        {
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
        }
       
        TableParts_EffctUp(Enemy);
        Debug.Log(Maneuvers[(int)EnemyPartsType.EAction].Count);

        //データから解析し、マニューバーを追加する
        //追加武装追記
        for (int SITE = 0; SITE != Enemy.Wepons.Count; SITE++)
        {
            for (int i = 0; i != Enemy.Wepons[SITE].Parts.Count; i++)
            {
                CharaManeuver AddManuver = null;

                //ドールなら部位を分ける
                if (DOOLmode)
                {
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
                }
                //ドールでないなら頭に集約
                else
                {
                    //追記するマニューバー
                    AddManuver = Enemy.Wepons[SITE].Parts[i].Maneuver;
                    me.HeadParts.Add(Enemy.Wepons[SITE].Parts[i].Maneuver);
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
        Debug.Log(Maneuvers[(int)EnemyPartsType.EAction].Count);
    }

    public void EnemyAI_Action()
    {
        //初期化
        UseManever = null;

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

        Debug.Log("はいってるよね？");

        // 敵キャラのエリアと選択されたマニューバの射程を絶対値で比べて、射程内であれば攻撃するか選択するコマンドを表示する
        // 敵キャラのエリアの絶対値が攻撃の最大射程以下且つ、
        // 敵キャラのエリアの絶対値が攻撃の最小射程以上なら攻撃する

        Debug.Log(Maneuvers[(int)EnemyPartsType.EAction].Count);

        //使用武具for文
        for (int ActManeuvers=0;ActManeuvers!=Maneuvers[(int)EnemyPartsType.EAction].Count;ActManeuvers++)
        {
            Debug.Log(Maneuvers[(int)EnemyPartsType.EAction][ActManeuvers].Name);
            for (int i = 0; i != PlayerDolls.Count; i++)
            {
                Debug.Log(i);
                //破損判定
                if (!Maneuvers[(int)EnemyPartsType.EAction][ActManeuvers].isDmage)
                {
                   

                    //敵の位置検索 & 射程比較
                    if (Maneuvers[(int)EnemyPartsType.EAction][ActManeuvers].MinRange != 10 &&                                  
                    (Mathf.Abs(PlayerDolls[i].area) <= Mathf.Abs(Maneuvers[(int)EnemyPartsType.EAction][ActManeuvers].MaxRange + me.area) &&
                     Mathf.Abs(PlayerDolls[i].area) >= Mathf.Abs(Maneuvers[(int)EnemyPartsType.EAction][ActManeuvers].MinRange + me.area)))
                    {
                        //使用武具更新
                        UseManever = UseManuber_Change((int)EnemyPartsType.EAction, ActManeuvers, UseManever);
                        target = PlayerDolls[i];
                        Debug.Log("AIはうごいえる？");
                    }
                }
            }
        }

        //全ACTION吟味し、一番有効値が高い物を使用する
        if (UseManever != null)
            ProcessAccessor.Instance.actTiming.ExeAtkManeuver(target, UseManever, me);

        Debug.Log("AIはうごいえる？");
        return;
    }
    //移動逃げ
    public void EnemyAI_Rapid(CharaManeuver OpponentManeuver, Doll_blu_Nor Opponent)
    {
        //初期化
        UseManever = null;

        //全員いるわ
        List<Doll_blu_Nor> PlayerDolls = ManagerAccessor.Instance.battleSystem.GetCharaObj();

        int MaxOpponentRange = OpponentManeuver.MaxRange;
        int MinopponentRange = OpponentManeuver.MinRange;

        int differenceRange = 0;
        bool MeorOp = true;

        //相手の武具から射程にどっち方向かつどれだけ余裕があるか
        //例:狙われている武具射程が2~0で今の状況が射程の数字でいうところの2,1,0のどれかを算出
        differenceRange = Mathf.Abs(Opponent.area - me.area) - MaxOpponentRange;
        if (Mathf.Abs(differenceRange) < Mathf.Abs(Mathf.Abs(Opponent.area - me.area) - MinopponentRange))
            differenceRange = Mathf.Abs(Opponent.area - me.area) - MinopponentRange;

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
                    //この時点で数字=射程差,+=天国側,-地獄側が分かる,0の場合は別途中身で判定
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
                            UseManever = UseManuber_Change((int)EnemyPartsType.ERapid, ActManeuvers, UseManever);
                            MeorOp = true;

                        }
                        //自身に及ぼす場合
                        else if (Maneuvers[(int)EnemyPartsType.ERapid][ActManeuvers].MinRange == 10 || (Maneuvers[(int)EnemyPartsType.ERapid][ActManeuvers].MaxRange == 0 && Maneuvers[(int)EnemyPartsType.ERapid][ActManeuvers].MinRange == 0))
                        {
                            //使用武具更新
                            UseManever = UseManuber_Change((int)EnemyPartsType.ERapid, ActManeuvers, UseManever);
                            MeorOp = false;
                        }
                    }
                }
            }

        }

        if (UseManever != null)
        {
            //自分に使うときの影響処理
            if (MeorOp == false)
            {
                var item = Opponent;
                Opponent = me;
                me = item;
            }

            bool direction = false;

            if (differenceRange > 0)
                direction = false;
            else
                direction = true;

            //相手の射程が0の場合
            if (4 >= Opponent.area + UseManever.EffectNum[EffNum.Move] && direction == false)
            {
                //とりま地獄側へ移動
                Debug.Log("Enemy:地獄にとばす");
                ProcessAccessor.Instance.rpdTiming.SetDirection(false);
                ProcessAccessor.Instance.rpdTiming.AddRapidManeuver(me, Opponent, UseManever);
                return;
            }
            else if (0 <= Opponent.area - UseManever.EffectNum[EffNum.Move] && direction == true)
            {
                //無理ならしぶしぶ反対へ
                Debug.Log("Enemy:天国にとばす");
                ProcessAccessor.Instance.rpdTiming.SetDirection(true);
                ProcessAccessor.Instance.rpdTiming.AddRapidManeuver(me, Opponent, UseManever);
                return;
            }




            //逃げたいが端で逃げられない場合諦める
            //自分に使うときの影響処理
            if (MeorOp == false)
            {
                var item = Opponent;
                Opponent = me;
                me = item;
            }

            //自身では対応不可
            Debug.Log("Enemy:Help!");
           
            //動いた分計算用
            int movenum = 0;

            //全味方に
            for (int i = 0; i != PlayerDolls.Count; i++)
            {
                //他の味方に救援を送る
                movenum+=PlayerDolls[i].GetComponent<ObjEnemy>().HelpMoveRapid(me, Opponent, differenceRange, direction);

                if ((Mathf.Abs(me.area + movenum) <= Mathf.Abs(OpponentManeuver.MaxRange + Opponent.area) &&
                     Mathf.Abs(me.area + movenum) >= Mathf.Abs(OpponentManeuver.MinRange + Opponent.area)))
                {
                    //相手の攻撃の射程内ならまだ救援信号
                    ;
                }
                else
                    break;
            }

        }

        //避けれない無理★
        return;
    }
    //妨害処理
    public void EnemyAI_Judge(CharaManeuver OpponentManeuver, Doll_blu_Nor Opponent,int DiceRoll,int TargetParts,int Power)
    {
        //初期化
        UseManever = null;

        //もう一度要求のパターン
        if (Power != 0)
            ;
        //ダイス値が即対応できる範囲(6以下で致命傷判断を入れる)
        else if (DiceRoll >= 6)
        {
            //致命傷1(現在のパーツ数で許容できるダメージ量か)判断。問題ないならスルー
            if (DOOLmode)
            {
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
            }
            
            //致命傷2(許容できるダメージ量か)判断。問題ないならスルー
            if (OpponentManeuver.EffectNum[EffNum.Damage] < Expected_FatalDamage)
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
            //支援タイプ
            if (UseManever.EffectNum[EffNum.Judge] < 0)
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

                    }

                }
            }
           
        }

        if (UseManever != null)
        {
            //マニューバー適応結果がまだ命中圏内の場合
            if (5 < DiceRoll - UseManever.EffectNum["Judge"] - Power)
            {
                Debug.Log("もう一度妨害できるかチャレンジ！");
                //もう一度処理を繰り返す
                EnemyAI_Judge(OpponentManeuver, Opponent, DiceRoll, TargetParts, UseManever.EffectNum["Judge"]);
                //要求（行動
                ProcessAccessor.Instance.jdgTiming.ExeJudgManeuver(UseManever, me);
                return;
            }
            else
            {
                //要求（行動
                Debug.Log("妨害チャレンジ完了");
                ProcessAccessor.Instance.jdgTiming.ExeJudgManeuver(UseManever, me);
                return;
            }
        }

        //自身では対応不可
        Debug.Log("Enemy:JudgeHelp!");
        
        //全味方に
        for (int i = 0; i != PlayerDolls.Count; i++)
        {
            //他の味方に救援を送る
            Power += PlayerDolls[i].GetComponent<ObjEnemy>().HelpJudge_OP(Opponent,OpponentManeuver.EffectNum[EffNum.Damage] - Power);

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
        //初期化
        UseManever = null;

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
                //支援タイプ
                if (UseManever.EffectNum[EffNum.Judge] > 0)
                {
                    //自身の位置と射程比較
                    if ((Mathf.Abs(Opponent.area) <= Mathf.Abs(Maneuvers[(int)EnemyPartsType.EJudge][ActManeuvers].MaxRange + me.area) &&
                      Mathf.Abs(Opponent.area) >= Mathf.Abs(Maneuvers[(int)EnemyPartsType.EJudge][ActManeuvers].MinRange + me.area)))
                    {
                        //使用武具更新
                        UseManever = UseManuber_Change((int)EnemyPartsType.EJudge, ActManeuvers, UseManever);

                    }
                }
            }

        }

        if (UseManever != null)
        {
            //マニューバー適応結果が命中圏内の場合
            if (5 < DiceRoll + UseManever.EffectNum["Judge"] + Power)
            {
                //要求（行動
                ProcessAccessor.Instance.jdgTiming.ExeJudgManeuver(UseManever, Opponent);
                return;
            }
            //圏外
            else
            {
                //もう一度処理を繰り返す
                EnemyAI_Judge(OpponentManeuver, Opponent, DiceRoll, UseManever.EffectNum["Judge"]);
                //要求（行動
                ProcessAccessor.Instance.jdgTiming.ExeJudgManeuver(UseManever, Opponent);
                return;
            }
        }

        //自身では対応不可
        Debug.Log("Enemy:Help!");
       
        //全味方に
        for (int i = 0; i != PlayerDolls.Count; i++)
        {
            //他の味方に救援を送る
            Power += PlayerDolls[i].GetComponent<ObjEnemy>().HelpJudge_ME(me, OpponentManeuver.EffectNum["Action"] - Power);

            //許容値なら
            if (6 > DiceRoll - Power)
                break;
        }

        
        return;
    }
    //ダメージタイミングの場合
    public void EnemyAI_Damage(CharaManeuver OpponentManeuver, Doll_blu_Nor Opponent,bool ATorDF)
    {
        //初期化
        UseManever = null;

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

    //ゲームオブジェクトごと消す処理。
    //サヴァント・ホラーはスルーする
    public void Deletme()
    {
        if(!DOOLmode&&arrmyFlag)
        Destroy(this.gameObject);
    }

    //移動補助目的HELP(RAPID
    public int HelpMoveRapid(Doll_blu_Nor Follow, Doll_blu_Nor Opponent, int NeedRange,bool Needdirection)
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
                    //宣言（敵に）+=天国へ,-は地獄へ飛ばす
                    if (4 >= Opponent.area + UseManever.EffectNum[EffNum.Move] && Needdirection == false)
                    {
                        //とりま地獄側へ移動
                        Debug.Log("Enemy:地獄にとばす");
                        ProcessAccessor.Instance.rpdTiming.SetDirection(false);
                        ProcessAccessor.Instance.rpdTiming.AddRapidManeuver(me, Opponent, Maneuvers[(int)EnemyPartsType.ERapid][ActManeuvers]);
                        return UseManever.EffectNum[EffNum.Move];
                    }
                    else if (0 <= Opponent.area - UseManever.EffectNum[EffNum.Move] && Needdirection == true)
                    {
                        //無理ならしぶしぶ反対へ
                        Debug.Log("Enemy:天国にとばす");
                        ProcessAccessor.Instance.rpdTiming.SetDirection(true);
                        ProcessAccessor.Instance.rpdTiming.AddRapidManeuver(me, Opponent, Maneuvers[(int)EnemyPartsType.ERapid][ActManeuvers]);
                        return -UseManever.EffectNum[EffNum.Move];
                    }
                    
                }
                //敵の位置検索 & 射程比較(味方を動かす)
                else if (Maneuvers[(int)EnemyPartsType.ERapid][ActManeuvers].MinRange != 10 &&
            (Mathf.Abs(Follow.area) <= Mathf.Abs(Maneuvers[(int)EnemyPartsType.ERapid][ActManeuvers].MaxRange + me.area) &&
             Mathf.Abs(Follow.area) >= Mathf.Abs(Maneuvers[(int)EnemyPartsType.ERapid][ActManeuvers].MinRange + me.area)))
                {
                    //宣言（味方に）+=天国へ,-は地獄へ飛ばす
                    if (4 >= Opponent.area + UseManever.EffectNum[EffNum.Move] && Needdirection == false)
                    {
                        //とりま地獄側へ移動
                        Debug.Log("Enemy:味方を地獄にとばす");
                        ProcessAccessor.Instance.rpdTiming.SetDirection(false);
                        ProcessAccessor.Instance.rpdTiming.AddRapidManeuver(me, Follow, Maneuvers[(int)EnemyPartsType.ERapid][ActManeuvers]);
                        return UseManever.EffectNum[EffNum.Move];
                    }
                    else if (0 <= Opponent.area - UseManever.EffectNum[EffNum.Move] && Needdirection == true)
                    {
                        //無理ならしぶしぶ反対へ
                        Debug.Log("Enemy:味方を天国にとばす");
                        ProcessAccessor.Instance.rpdTiming.SetDirection(true);
                        ProcessAccessor.Instance.rpdTiming.AddRapidManeuver(me, Follow, Maneuvers[(int)EnemyPartsType.ERapid][ActManeuvers]);
                        return -UseManever.EffectNum[EffNum.Move];
                    }
                    
                }
               
            }
        }

        //逃げたいがそもそも使えるものが無かったり、端で逃げさせられない場合諦める
        return 0;

    }

    //妨害目的HELP(JUDGE
    public int HelpJudge_OP(Doll_blu_Nor Opponent,int NeedPower)
    {
        for (int ActManeuvers = 0; ActManeuvers != Maneuvers[(int)EnemyPartsType.EJudge].Count; ActManeuvers++)
        {
            //破損判定&使用判定
            if (!Maneuvers[(int)EnemyPartsType.EJudge][ActManeuvers].isDmage && !Maneuvers[(int)EnemyPartsType.EJudge][ActManeuvers].isDmage)
            {
                //支援タイプ
                if (Maneuvers[(int)EnemyPartsType.EJudge][ActManeuvers].EffectNum[EffNum.Judge] < 0)
                {
                    //敵の位置検索 & 射程比較(敵を動かす)
                    if (Maneuvers[(int)EnemyPartsType.EJudge][ActManeuvers].MinRange != 10 &&
            (Mathf.Abs(Opponent.area) <= Mathf.Abs(Maneuvers[(int)EnemyPartsType.EJudge][ActManeuvers].MaxRange + me.area) &&
             Mathf.Abs(Opponent.area) >= Mathf.Abs(Maneuvers[(int)EnemyPartsType.EJudge][ActManeuvers].MinRange + me.area)))
                    {
                        //妨害まにゅばー
                        //要求（行動
                        ProcessAccessor.Instance.jdgTiming.ExeJudgManeuver(Maneuvers[(int)EnemyPartsType.EJudge][ActManeuvers], Opponent);

                        //妨害値を加えた状態で受けている側の奴にもう一度判断させる。
                        return Maneuvers[(int)EnemyPartsType.EJudge][ActManeuvers].EffectNum["Judge"];
                    }
                }
            }
        }

        return 0;

    }
    //支援目的HELP(JUDGE
    public int HelpJudge_ME(Doll_blu_Nor Follow,int NeedPower)
    {
        for (int ActManeuvers = 0; ActManeuvers != Maneuvers[(int)EnemyPartsType.EJudge].Count; ActManeuvers++)
        {
            //支援タイプ
            if (Maneuvers[(int)EnemyPartsType.EJudge][ActManeuvers].EffectNum[EffNum.Judge] > 0)
            {
                //破損判定&使用判定
                if (!Maneuvers[(int)EnemyPartsType.EJudge][ActManeuvers].isDmage && !Maneuvers[(int)EnemyPartsType.EJudge][ActManeuvers].isDmage)
                {
                    //敵の位置検索 & 射程比較(敵を動かす)
                    if (Maneuvers[(int)EnemyPartsType.EJudge][ActManeuvers].MinRange != 10 &&
            (Mathf.Abs(Follow.area) <= Mathf.Abs(Maneuvers[(int)EnemyPartsType.EJudge][ActManeuvers].MaxRange + me.area) &&
             Mathf.Abs(Follow.area) >= Mathf.Abs(Maneuvers[(int)EnemyPartsType.EJudge][ActManeuvers].MinRange + me.area)))
                    {
                        //支援まにゅばー
                        //要求（行動
                        ProcessAccessor.Instance.jdgTiming.ExeJudgManeuver(Maneuvers[(int)EnemyPartsType.EJudge][ActManeuvers], Follow);

                        //妨害値を加えた状態で受けている側の奴にもう一度判断させる。
                        return Maneuvers[(int)EnemyPartsType.EJudge][ActManeuvers].EffectNum["Judge"];
                    }
                }
            }
        }

        return 0;

    }
    //多分庇うぐらいしかない
    public void HelpDamege(Doll_blu_Nor Follow)
    {
        for (int ActManeuvers = 0; ActManeuvers != Maneuvers[(int)EnemyPartsType.Edamage].Count; ActManeuvers++)
        {
            //破損判定&使用判定
            if (!Maneuvers[(int)EnemyPartsType.Edamage][ActManeuvers].isDmage && !Maneuvers[(int)EnemyPartsType.Edamage][ActManeuvers].isDmage)
            {
                //敵の位置検索 & 射程比較(味方を動かす)-なお今のところ「庇う」ぐらい
                if (Maneuvers[(int)EnemyPartsType.Edamage][ActManeuvers].MinRange != 10 &&
            (Mathf.Abs(Follow.area) <= Mathf.Abs(Maneuvers[(int)EnemyPartsType.Edamage][ActManeuvers].MaxRange + me.area) &&
             Mathf.Abs(Follow.area) >= Mathf.Abs(Maneuvers[(int)EnemyPartsType.Edamage][ActManeuvers].MinRange + me.area)))
                {
                    //ROLL処理
                    ProcessAccessor.Instance.dmgTiming.ExeManeuver(Maneuvers[(int)EnemyPartsType.Edamage][ActManeuvers], Follow);
                    return;
                }
            }
        }
    }

    //ダメージ部位選択本関数-------------------------------------------------------------------
    //部位選択できる場合
    //不随効果は考えれない
    //敵がサヴァント型の場合かつ、こちらが受けるパーツを選択する場合使用
    public List<CharaManeuver> GetDamageUPList_ALL(int sonsyoukazu)
    {
        int num = 0;
        int tmp = 0;
        int[] lowtmp = new int[2];

        if (num < me.HeadParts.Count && me.HeadParts.Count!=0)
        {
            num = HEAD;
            tmp = me.HeadParts.Count;
        } 
        else if(num>me.HeadParts.Count)
        {
            lowtmp[0] = me.HeadParts.Count;
            lowtmp[1] = HEAD;
        }
        if (num < me.ArmParts.Count && me.ArmParts.Count != 0)
        {
            num = ARM;
            tmp = me.ArmParts.Count;
        }
        else if (num > me.ArmParts.Count)
        {
            lowtmp[0] = me.ArmParts.Count;
            lowtmp[1] = ARM;
        }
        if (num < me.BodyParts.Count && me.BodyParts.Count!=0)
        {
            num = BODY;
            tmp = me.BodyParts.Count;
        }
        else if (num > me.BodyParts.Count)
        {
            lowtmp[0] = me.BodyParts.Count;
            lowtmp[1] = BODY;
        }
        if (num < me.LegParts.Count&&me.LegParts.Count!=0)
        {
            num = LEG;
            tmp = me.LegParts.Count;
        }
        else if (num > me.LegParts.Count)
        {
            lowtmp[0] = me.LegParts.Count;
            lowtmp[1] = LEG;
        }

        //損傷数が予定受ける部位より他部位の方が総ダメージが低くなる場合そちら優先
        if ((tmp - sonsyoukazu) < lowtmp[0])
            num = lowtmp[1];

        return GetDamageUPList(num,sonsyoukazu);
    }

    //部位選択された場合
    //もしくは、ホラー、レギオンの場合にこいつを選択（なおTarget先変えれないパターンあると思うからその場合は連絡
    public List<CharaManeuver> GetDamageUPList(int TargetParts, int sonsyoukazu)
    {
        List<CharaManeuver> DamageParts = new List<CharaManeuver>();
        int[] Discarded_num = new int[sonsyoukazu];

        for (int i = 0; i != sonsyoukazu; i++)
            Discarded_num[i] = 0;

        //レギオンの場合NULLデータを返す
        if (armynum != 0)
        {
            armynum -= sonsyoukazu;

            if (armynum < 1)
                arrmyFlag = true;
            return null;
        }

        if (TargetParts == HEAD)
            DiscardedManuber_omoto(Discarded_num, DamageParts, me.HeadParts);
        else if (TargetParts == ARM)
            DiscardedManuber_omoto(Discarded_num, DamageParts, me.ArmParts);
        else if (TargetParts == BODY)
            DiscardedManuber_omoto(Discarded_num, DamageParts, me.BodyParts);
        else if (TargetParts == LEG)
            DiscardedManuber_omoto(Discarded_num, DamageParts, me.LegParts);

       

        return DamageParts;
    }
    //------------------------------------------------------------------------------------------------

    //優先率順で使用。HELP時は無視
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

    //ダメージ部位関数大本。
    private void DiscardedManuber_omoto(int[] aa, List<CharaManeuver> DamageList, List<CharaManeuver> SiteList)
    {
        //必須廃棄分（足りなくても可能）
        //優先度廃棄度が高い奴から廃棄される
        for (int i = 0; i != SiteList.Count; i++)
        {
            //選考0,損傷して欲しい物
            if (false)
            {
                DiscardedManuber_comparison(100, aa, SiteList[i], DamageList);

            }
            //選考1,はらわた系統
            else if (SiteList[i].EnemyAI.Count == 0)
            {
                DiscardedManuber_comparison(80, aa, SiteList[i], DamageList);

            }
            else if (SiteList[i].isUse)
            {
                //損傷優先度が高い状態からスタート
                DiscardedManuber_comparison(10 - SiteList[i].EnemyAI[4], aa, SiteList[i], DamageList);

            }
            else if (!SiteList[i].isUse)
            {
                
                //損傷優先度が低い状態からスタート
                DiscardedManuber_comparison(2 - SiteList[i].EnemyAI[4], aa, SiteList[i], DamageList);
                

            }
        }
    }

    //ダメージ部位関数サブ。
    private void DiscardedManuber_comparison(int a,int[] aa,CharaManeuver NowManuver,List<CharaManeuver> DamageList)
    {
        int Lownum = 99999;
        int Lownum2 = 0;
        //廃棄優先度にて判断
        for (int i = 0; i != aa.Length; i++)
        {
            //上からマニューバーをなけりゃとりあえず登録
            if(aa[i]==0)
            {
                DamageList.Add(NowManuver);
                aa[i] = a;
                break;
            }
            //候補が既存のものより優先度が高ければ入れ替え(List内の一番低い物を選別)
            else if (Lownum > aa[i])
            {
                Lownum = aa[i];
                Lownum2 = i;
            }

            
        }

        //ひくいやつとこうかん(低くなければスルー)
        //ここまでくる＝損傷数まで入った時だけ
        if (a > Lownum)
        {
            aa[Lownum2] = a;
            DamageList[Lownum2] = NowManuver;
        }
            return;
    }

    //相手の部位指定
    public int SiteDamageUP(Doll_blu_Nor jij)
    {
        //PL側の有効値もしくは、損傷期待値を参照すればいける
        //けど今はランダムで返す
        return UnityEngine.Random.Range(0, 3);

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
