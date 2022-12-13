using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveActProcess : GetClickedGameObject
{
    private bool standbyTargetSelect = false;
    public bool SetStandbyTarget(bool isStandby) => standbyTargetSelect = isStandby;

    Doll_blu_Nor moveChara;         // 実際に移動するキャラ

    [SerializeField] private GameObject moveButtons;            // 移動させるボタンを表示するためのもの


    private void Awake()
    {
        ProcessAccessor.Instance.actTimingMove = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(skillSelected)
        {
            RangeJudgment();
        }
        else if(standbyTargetSelect)
        {
            TargetSelectStandby();
        }
    }

    /// <summary>
    /// 射程判断用メソッド。自身が移動するかキャラを選んで移動させるか選択
    /// </summary>
    public void RangeJudgment()
    {
        // 射程が自身のみならそのまま移動するかの選択ボタンを出す。
        if(dollManeuver.MinRange==10)
        {
            moveChara = actingChara;
            moveButtons.SetActive(true);
        }
        // 自身でなければ動かす対象を選ぶまでの待機状態へ移行
        else
        {
            standbyTargetSelect = true;
        }
        // もうこのメソッドでする仕事はないのでこのメソッドに入らないようにする
        skillSelected = false;
    }

    /// <summary>
    /// 射程が自身でない場合にキャラクターを選ぶ処理
    /// </summary>
    public void TargetSelectStandby()
    {
        //左クリックで
        if (Input.GetMouseButtonDown(0))
        {
            // プレイアブルキャラに向いていたカメラ情報を保存
            saveCharaCamera = CharaCamera;

            GameObject clickedObj = ShotRay();

            //クリックしたゲームオブジェクトが選べるキャラなら
            if (clickedObj.CompareTag("AllyChara") || clickedObj.CompareTag("EnemyChara"))
            {
                ZoomUpObj(clickedObj);
                // ここでコマンド表示
                StartCoroutine(MoveStandby(clickedObj));
            }
        }
    }

    /// <summary>
    /// 選んだキャラが射程内なら、選んだキャラを移動予定のキャラ変数に格納し、移動ボタンを表示する
    /// </summary>
    /// <param name="move"></param>
    /// <returns></returns>
    protected override IEnumerator MoveStandby(GameObject move)
    {
        for (int i = 0; i < 2; i++)
        {
            //カメラがクリックしたキャラに近づくまで待つ
            if (i == 0)
            {
                yield return new WaitForSeconds(0.75f);
            }
            else
            {
                // 敵キャラのエリアと選択されたマニューバの射程を絶対値で比べて、射程内であれば攻撃するか選択するコマンドを表示する
                // 敵キャラのエリアの絶対値が攻撃の最大射程以下且つ、
                // 敵キャラのエリアの絶対値が攻撃の最小射程以上なら攻撃する
                if (dollManeuver.MinRange != 10 &&
                    (Mathf.Abs(move.GetComponent<Doll_blu_Nor>().area) <= Mathf.Abs(dollManeuver.MaxRange + actingChara.area) &&
                     Mathf.Abs(move.GetComponent<Doll_blu_Nor>().area) >= Mathf.Abs(dollManeuver.MinRange + actingChara.area)))
                {
                    moveChara = move.GetComponent<Doll_blu_Nor>();
                    // 要if文分け。移動量が複数あるならいったんどれだけ移動するかを選択するボタンを出す

                    moveButtons.SetActive(true);
                }

            }
        }
    }

    // 楽園方向に移動するボタン用のメソッド
    public void OnClickParadiseMove()
    {
        MoveChara(true,moveChara,false);
        ProcessAccessor.Instance.actTiming.SkillSelected = true;
    }

    // 奈落方向に移動するボタン用のメソッド
    public void OnClickAbyssMove()
    {
        MoveChara(false,moveChara,false);
        ProcessAccessor.Instance.actTiming.SkillSelected = true;
    }



    /// <summary>
    /// 
    /// </summary>
    /// <param name="direction">trueで楽園方向に移動、falseで奈落方向に移動</param>
    /// <param name="beMovedChara">移動させられるキャラ</param>
    /// <param name="isRapid">ラピッドタイミングかどうか判断</param>
    public void MoveChara(bool direction, Doll_blu_Nor beMovedChara, bool isRapid)
    {
        if ((beMovedChara.area == BattleSpone.NARAKU && !direction)||
             beMovedChara.area == BattleSpone.RAKUEN &&  direction)
        {
            // これ以上移動できませんみたいな表記する
        }
        else
        {
            // このクラスに入る前にActTimingProcessクラスでカメラの処理をしているのでActTimingProcessクラスでズームアウトの処理を行う
            ProcessAccessor.Instance.actTiming.ZoomOutRequest();
            // 実際にキャラを動かす
            if(direction)
            {
                ManagerAccessor.Instance.battleSpone.CharaMove(moveChara, 1);
            }
            else
            {
                ManagerAccessor.Instance.battleSpone.CharaMove(moveChara, -1);
            }
            
            moveButtons.SetActive(false);
            actingChara.NowCount -= dollManeuver.Cost;
            // ここ、アニメーション終わってからの処理にしたいなぁ
            // 行動したキャラを表示から消す
            if(isRapid)
            {
                ProcessAccessor.Instance.rpdTiming.ExemaneuverProcess = true;
            }
            else
            {

                ManagerAccessor.Instance.battleSystem.DeleteMoveChara();
                ManagerAccessor.Instance.battleSystem.BattleExe = true;
            }
        }
    }
}
