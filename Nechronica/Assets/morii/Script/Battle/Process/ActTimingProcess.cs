using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class ActTimingProcess : GetClickedGameObject
{
    private GameObject atkTargetEnemy;              // 攻撃する敵オブジェクトを格納場所

    // Start is called before the first frame update
    void Awake()
    {
        ProcessAccessor.Instance.actTiming = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (selectedChara)
        {
            SkillSelectStandby();
        }
        else if (standbyEnemySelect)
        {
            EnemySelectStandby();
        }
        else if (standbyCharaSelect)
        {
            CharaSelectStandby();
        }
        
    }

    protected override void CharaSelectStandby()
    {
        //左クリックで
        if (Input.GetMouseButtonDown(0) && CharaCamera == null && !selectedChara)
        {
            GameObject clickedObj = ShotRay();

            //クリックしたゲームオブジェクトが味方キャラなら
            if (clickedObj.CompareTag("AllyChara"))
            {
                clickedObj.GetComponent<BattleCommand>().SetNowSelect(true);
                ZoomUpObj(clickedObj);
                selectedChara = true;
                // ここでコマンド表示
                StartCoroutine(MoveStandby(clickedObj));
            }
        }
    }

    protected override void EnemySelectStandby()
    {
        //左クリックで
        if (Input.GetMouseButtonDown(0))
        {
            // プレイアブルキャラに向いていたカメラ情報を保存
            saveCharaCamera = CharaCamera;

            GameObject clickedObj = ShotRay();

            //クリックしたゲームオブジェクトが敵キャラなら
            if (clickedObj.CompareTag("EnemyChara"))
            {
                ZoomUpObj(clickedObj);
                // ここでコマンド表示
                StartCoroutine(MoveStandby(clickedObj));
            }
        }
    }

    protected override void SkillSelectStandby()
    {
        // 右クリックで
        if ((Input.GetMouseButtonDown(1) || skillSelected) && CharaCamera != null)
        {
            // マニューバを選ぶコマンドまで表示されていたらそのコマンドだけ消す
            ZoomOutObj();
            // キャラ選択待機状態にする
            selectedChara = false;
        }
    }

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
                if (move.CompareTag("AllyChara"))
                {
                    // 選択したキャラのコマンドのオブジェクトを取得
                    childCommand = move.transform.GetChild(CANVAS).transform.GetChild(ACTION);
                    // 技コマンドもろもろを表示
                    childCommand.gameObject.SetActive(true);
                }
                else if (move.CompareTag("EnemyChara"))
                {
                    // ステータスを取得し、表示。後にZoomOutObjで使うのでメンバ変数に格納
                    childCommand = move.transform.GetChild(CANVAS);
                    childCommand.gameObject.SetActive(true);

                    // 敵キャラのエリアと選択されたマニューバの射程を絶対値で比べて、射程内であれば攻撃するか選択するコマンドを表示する
                    // 敵キャラのエリアの絶対値が攻撃の最大射程以下且つ、
                    // 敵キャラのエリアの絶対値が攻撃の最小射程以上なら攻撃する
                    if (dollManeuver.MinRange != 10 &&
                        (Mathf.Abs(move.GetComponent<Doll_blu_Nor>().potition) <= Mathf.Abs(dollManeuver.MaxRange + targetArea) &&
                         Mathf.Abs(move.GetComponent<Doll_blu_Nor>().potition) >= Mathf.Abs(dollManeuver.MinRange + targetArea)))
                    {
                        atkTargetEnemy = move;
                        atkTargetEnemy.transform.GetChild(CANVAS).gameObject.SetActive(true);

                        exeButton.onClick.AddListener(() => OnClickAtk(move.GetComponent<Doll_blu_Nor>()));

                        this.transform.GetChild(CANVAS).transform.GetChild(ATKBUTTONS).gameObject.SetActive(true);
                    }
                }
            }
        }
    }

    public void OnClickAtk(Doll_blu_Nor enemy)
    {
        // カメラを元の位置に戻し、UIを消す
        ZoomOutObj();
        enemy.transform.GetChild(CANVAS).gameObject.SetActive(false);
        this.transform.GetChild(CANVAS).transform.GetChild(ATKBUTTONS).gameObject.SetActive(false);

        selectedChara = false;
        standbyEnemySelect = false;
        standbyCharaSelect = false;

        // ここジャッジから入る
        ProcessAccessor.Instance.jdgTiming.enabled = true;
        ProcessAccessor.Instance.jdgTiming.DiceRollButton.gameObject.SetActive(true);
        ProcessAccessor.Instance.jdgTiming.MovingCharaArea = targetArea;
        ProcessAccessor.Instance.jdgTiming.IsStandbyDiceRoll = true;

        // ジャッジに入ってからバトルプロセスが動かないように非アクティブにする
        this.enabled = false;

        // Debug用
        //battleSystem.DamageTiming(dollManeuver, enemy);
    }
}
