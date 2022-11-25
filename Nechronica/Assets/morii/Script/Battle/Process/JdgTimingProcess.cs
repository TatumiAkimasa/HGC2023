using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class JdgTimingProcess : GetClickedGameObject
{
    private bool isDiceRoll;
    private bool isStandbyDiceRoll;

    private int  giveDamage;
    public int GiveDamage
    {
        get { return giveDamage; }
        set { giveDamage = value; }
    }

    public bool IsStandbyDiceRoll
    {
        get { return isStandbyDiceRoll; }
        set { isStandbyDiceRoll = value; }
    }

    [SerializeField] private Text rollResultText;

    [SerializeField] private Button nextButton;     // ジャッジタイミングを終わらせるボタン
    [SerializeField] private Button diceRollButton; // ダイスロールを行うボタン

    [SerializeField] private GameObject buttons;    // 最後に発動するかどうかのボタン
    public GameObject JudgeButtons
    {
        get { return buttons; }
    }

    public Button DiceRollButton
    {
        get { return diceRollButton; }
    }

    public Text RollResultText
    {
        get { return rollResultText; }
    }

    private Unity.Mathematics.Random randoms;       // 再現可能な乱数の内部状態を保持するインスタンス
    private int rollResult = 0;                     // ダイスロールの結果を格納する変数
    private int movingCharaArea;                    // 攻撃途中の敵、味方オブジェクトのエリア
    public int MovingCharaArea
    {
        get { return movingCharaArea; }
        set { movingCharaArea = value; }
    }

    void Awake()
    {
        ProcessAccessor.Instance.jdgTiming = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (isStandbyDiceRoll)
        {
            if (isDiceRoll)
            {
                isStandbyDiceRoll = false;
                isDiceRoll = false;

                // ここからジャッジタイミング
                standbyCharaSelect = true;
                //passButton.gameObject.SetActive(true);
            }
        }
        else if(standbyCharaSelect)
        {
            CharaSelectStandby();
        }
    }

    //override protected void CharaSelectStandby()
    //{
    //    //左クリックで
    //    if (Input.GetMouseButtonDown(0) && CharaCamera == null && !selectedChara)
    //    {
    //        GameObject clickedObj = ShotRay();

    //        //クリックしたゲームオブジェクトが味方キャラなら
    //        if (clickedObj.CompareTag("AllyChara"))
    //        {
    //            clickedObj.GetComponent<BattleCommand>().SetNowSelect(true);
    //            ZoomUpObj(clickedObj);
    //            selectedChara = true;
    //            // ここでコマンド表示
    //            StartCoroutine(MoveStandby(clickedObj));
    //        }
    //    }
    //}


    /// <summary>
    /// カメラが近づいてからコマンドを表示するメソッド
    /// </summary>
    /// <param name="charaCommand">クリックされたキャラの子オブジェクト（コマンド）</param>
    /// <returns></returns>
    override protected IEnumerator MoveStandby(GameObject move)
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
                    standbyCharaSelect = false;
                    // 選択したキャラのコマンドのオブジェクトを取得
                    childCommand = move.transform.GetChild(CANVAS).transform.GetChild(JUDGE);
                    // 技コマンドもろもろを表示
                    childCommand.gameObject.SetActive(true);
                }
            }
        }
    }

    /// <summary>
    /// ジャッジタイミングを終わらせる処理
    /// </summary>
    public void OnClickPass()
    {
        // ダメージタイミングに移行
    }

    /// <summary>
    /// ダイスロールするボタン
    /// </summary>
    public void OnClickDiceRoll()
    {
        randoms = new Unity.Mathematics.Random((uint)Random.Range(0, 468446876));
        rollResult = randoms.NextInt(1, 11);
        rollResultText.text = rollResult.ToString();
        isDiceRoll = true;
    }

    public void OnClickExeManeuver()
    {
        // 敵キャラのエリアと選択されたマニューバの射程を絶対値で比べて、射程内であれば攻撃するか選択するコマンドを表示する
        // 敵キャラのエリアの絶対値が攻撃の最大射程以下且つ、
        // 敵キャラのエリアの絶対値が攻撃の最小射程以上なら発動する
        if (dollManeuver.MinRange != 10 &&
            (Mathf.Abs(movingCharaArea) <= Mathf.Abs(dollManeuver.MaxRange + targetArea) &&
             Mathf.Abs(movingCharaArea) >= Mathf.Abs(dollManeuver.MinRange + targetArea))&&
             (!dollManeuver.isUse && !dollManeuver.isDmage))
        {
            rollResult += dollManeuver.EffectNum[EffNum.Judge];
            rollResultText.text = rollResult.ToString();
            dollManeuver.isUse = true; 
            // スキルを発動したらコマンドボタンを非表示にすし、キャラ選択待機状態にもどす
            JudgeButtons.SetActive(false);
            skillSelected = false;
            ZoomOutObj();
            standbyCharaSelect = true;
        }
        else
        {
            // 射程が足りませんみたいな表記をする
        }
    }

    /// <summary>
    /// このメソッドが動いたらダメージタイミングに移行する
    /// </summary>
    public void OnClickNext()
    {
        if(rollResult>=6)
        {
            int addDmg = 0;

            if (rollResult > 10)
            {
                addDmg = rollResult - 10;
            }

            ProcessAccessor.Instance.dmgTiming.GiveDamage = giveDamage + addDmg;
        }
        else if(rollResult==1)
        {
            // 大失敗の処理
        }
        else
        {
            // 次のカウントに行く処理
        }

    }

}

