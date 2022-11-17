using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class JdgTimingProcess : GetClickedGameObject
{
    private bool isDiceRoll;
    private bool isStandbyDiceRoll;
    public bool IsStandbyDiceRoll
    {
        get { return isStandbyDiceRoll; }
        set { isStandbyDiceRoll = value; }
    }

    private bool isJudgeTiming = false;


    [SerializeField]
    protected Text text;

    protected Unity.Mathematics.Random randoms;       // 再現可能な乱数の内部状態を保持するインスタンス

    protected int rollResult = 0;                     // ダイスロールの結果を格納する変数


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
                isJudgeTiming = true;
            }
        }
    }


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
                    if (isJudgeTiming)
                    {
                        // 選択したキャラのコマンドのオブジェクトを取得
                        childCommand = move.transform.GetChild(CANVAS).transform.GetChild(JUDGE);
                        // 技コマンドもろもろを表示
                        childCommand.gameObject.SetActive(true);
                    }
                }
                else if (move.CompareTag("EnemyChara"))
                {
                    // ステータスを取得し、表示。後にZoomOutObjで使うのでメンバ変数に格納
                    childCommand = move.transform.GetChild(CANVAS);
                    childCommand.gameObject.SetActive(true);

                    // 敵キャラのエリアと選択されたマニューバの射程を絶対値で比べて、射程内であれば攻撃するか選択するコマンドを表示する
                    // 敵キャラのエリアの絶対値が攻撃の最大射程以下且つ、
                    // 敵キャラのエリアの絶対値が攻撃の最小射程以上なら攻撃する
                    if (Mathf.Abs(move.GetComponent<Doll_blu_Nor>().potition) <= Mathf.Abs(dollManeuver.MaxRange + dollArea) &&
                        Mathf.Abs(move.GetComponent<Doll_blu_Nor>().potition) >= Mathf.Abs(dollManeuver.MinRange + dollArea))
                    {

                    }

                    // 攻撃する～を選んだら、selectedChara、standbyCharaSelectをfalseにし、ダイスロールへ
                    // ダイスロール後、マニューバ、敵Obj(move)、ダイスの値を引数とし、ダメージタイミングへ
                }
            }
        }
    }

    public void OnClickDiceRoll()
    {
        randoms = new Unity.Mathematics.Random((uint)Random.Range(0, 468446876));
        rollResult = randoms.NextInt(1, 10);
        text.text = rollResult.ToString();
        isDiceRoll = true;
    }

}

