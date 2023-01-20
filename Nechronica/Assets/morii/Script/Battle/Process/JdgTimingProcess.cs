using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class JdgTimingProcess : GetClickedGameObject
{
    //-------------------------------
    // ほしい情報メモ
    // 攻撃するenemy
    // ターゲットになってる味方キャラ
    // ダイスロールの値
    //-------------------------------

    private bool isDiceRoll;
    private bool isStandbyDiceRoll;
    private int continuousAtk;

    private GameObject atkTargetEnemy;      // 攻撃する敵オブジェクトを格納場所

    Doll_blu_Nor selectedAllyChara;
    public GameObject AtkTargetEnemy
    {
        set { atkTargetEnemy = value; }
        get { return atkTargetEnemy; }
    }

    private CharaManeuver actManeuver;     // アクションタイミングで発動されたコマンドの格納場所
    [SerializeField]private Animator diceRollAnim;

    public CharaManeuver ActMneuver
    {
        get { return actManeuver; }
        set { actManeuver = value; }
    }

    public bool IsStandbyDiceRoll
    {
        get { return isStandbyDiceRoll; }
        set { isStandbyDiceRoll = value; }
    }

    [SerializeField] private Text plusNumText;      // パッシブなどによるダイスロールに行われる加算
    [SerializeField] private Button nextButton;     // ジャッジタイミングを終わらせるボタン
    [SerializeField] private Text rollResultText;
    [SerializeField] private Button diceRollButton; // ダイスロールを行うボタン
    [SerializeField] private Image diceRollButtonImg;       // ダイス演出などに使うボタンの画像
    [SerializeField] private GameObject confirmatButton;    // 最後に発動するか確認するボタン
    [SerializeField] private GameObject judgeButtons;       // ジャッジタイミングで扱うボタンの親オブジェクト

    public Button GetDiceRollButton() => diceRollButton;
    public GameObject GetConfirmatButton() => confirmatButton;
    public GameObject GetJudgeButton() => judgeButtons;
    public GameObject GetPlusNum() => plusNumText.gameObject;
    public int SetContinuousAtk(int num) => continuousAtk = num;


    private Unity.Mathematics.Random randoms;       // 再現可能な乱数の内部状態を保持するインスタンス
    private int rollResult = 0;                     // ダイスロールの結果を格納する変数


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
                isStandbyCharaSelect = true;
                nextButton.gameObject.SetActive(true);
            }
        }
        else if(isStandbyCharaSelect)
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
                    isStandbyCharaSelect = false;
                    selectedAllyChara = move.GetComponent<Doll_blu_Nor>();
                    // 選択したキャラのコマンドのオブジェクトを取得
                    childCommand = move.transform.GetChild(CANVAS).transform.GetChild(JUDGE);
                    // 技コマンドもろもろを表示
                    childCommand.gameObject.SetActive(true);
                }
            }
        }
    }

    public void OnClickBack()
    {
        ZoomOutObj();
        confirmatButton.SetActive(false);
        isStandbyCharaSelect = true;
    }

    /// <summary>
    /// ダイスロールするボタン
    /// </summary>
    public void OnClickDiceRoll()
    {
        diceRollAnim.gameObject.SetActive(true);
        rollResultText.text = "";   // 文字が邪魔なので一旦空データを代入
        randoms = new Unity.Mathematics.Random((uint)Random.Range(0, 468446876));
        
        // その後の操作を邪魔しないようにfalseにしておく
        diceRollButtonImg.raycastTarget = false;
        diceRollButton.enabled = false;
        StartCoroutine(RollAnimStandby(diceRollAnim,callBack => 
        {
            rollResult = randoms.NextInt(1, 11);
            rollResult = 11;
            rollResultText.text = rollResult.ToString();
            isDiceRoll = true;
        }));
        
    }

    /// <summary>
    /// ダイスの回転が終わるのを待つメソッド
    /// </summary>
    /// <returns></returns>
    private IEnumerator RollAnimStandby(Animator anim, System.Action<bool> callBack)
    {
        while(!anim.GetCurrentAnimatorStateInfo(0).IsName("End"))
        {
            yield return null;
        }

        callBack(true);
    }

    public void OnClickManeuver()
    {
        if(dollManeuver.MinRange==10 && actingChara == selectedAllyChara)
        {
            ExeJudgManeuver(dollManeuver, selectedAllyChara);
        }
        // 敵キャラのエリアと選択されたマニューバの射程を絶対値で比べて、射程内であれば攻撃するか選択するコマンドを表示する
        // 敵キャラのエリアの絶対値が攻撃の最大射程以下且つ、
        // 敵キャラのエリアの絶対値が攻撃の最小射程以上なら発動する
        if (dollManeuver.MinRange != 10 && RangeCheck(actingChara, dollManeuver, selectedAllyChara))
        {
            ExeJudgManeuver(dollManeuver, selectedAllyChara);
        }
        else
        {
            // 射程が足りませんみたいな表記をする
        }
    }

    /// <summary>
    /// 実際にジャッジタイミングの効果を発動する処理
    /// </summary>
    /// <param name="maneuver"></param>
    /// <param name="judgExeChara"></param>
    public void ExeJudgManeuver(CharaManeuver maneuver, Doll_blu_Nor judgExeChara)
    {
        // ダイスロールの結果に支援、妨害の値を反映
        rollResult += dollManeuver.EffectNum[EffNum.Judge];
        rollResultText.text = rollResult.ToString();
        maneuver.isUse = true;
        // スキルを発動したらコマンドボタンを非表示にし、キャラ選択待機状態にもどす
        confirmatButton.SetActive(false);
        skillSelected = false;
        if(judgExeChara.gameObject.CompareTag("AllyChara"))
        {
            ZoomOutObj();
        }
        isStandbyCharaSelect = true;

        // 行動値をコスト分減少させる
        judgExeChara.NowCount -= dollManeuver.Cost;
        if (judgExeChara.NowCount == ManagerAccessor.Instance.battleSystem.NowCount && judgExeChara != actingChara)
        {
            ManagerAccessor.Instance.battleSystem.DeleteMoveChara(judgExeChara.Name);
        }
    }


    protected override void ZoomOutObj()
    {
        base.ZoomOutObj();
        nextButton.gameObject.SetActive(true);
    }

    protected override void ZoomUpObj(GameObject clicked)
    {
        base.ZoomUpObj(clicked);
        nextButton.gameObject.SetActive(true);
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

            ProcessAccessor.Instance.dmgTiming.SetActChara(actingChara);
            ProcessAccessor.Instance.dmgTiming.ActMneuver = actManeuver;
            ProcessAccessor.Instance.dmgTiming.StandbyCharaSelect = true;
            ProcessAccessor.Instance.dmgTiming.SetDamageChara(atkTargetEnemy.GetComponent<Doll_blu_Nor>());
            ProcessAccessor.Instance.dmgTiming.SetRollResult(rollResult);
            ProcessAccessor.Instance.dmgTiming.GetDamageButtons().SetActive(true);
            diceRollButton.gameObject.SetActive(false);
            judgeButtons.SetActive(false);
            
        }
        else if(rollResult==1)
        {
            // 大失敗の処理
        }
        else
        {
            // アクションタイミングで行動したキャラを表示から消す
            ManagerAccessor.Instance.battleSystem.DeleteMoveChara();
            ManagerAccessor.Instance.battleSystem.BattleExe = true;
            nextButton.gameObject.SetActive(false);
            diceRollButton.gameObject.SetActive(false);
            judgeButtons.SetActive(false);
        }

        // 次のジャッジタイミングで使えるようにtrueにする
        diceRollButtonImg.raycastTarget = true;
        diceRollButton.enabled  = true;
        isStandbyCharaSelect = false;
        rollResultText.text = "ダイスロール";
        diceRollAnim.gameObject.SetActive(false);

        nextButton.gameObject.SetActive(false);
    }

}

