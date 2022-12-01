using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DmgTimingProcess : GetClickedGameObject
{
    private int addDamage;
    private int giveDamage;

    [SerializeField] private Button nextButton;

    private int rollResult;             // ダイスロールの結果の値

    private Doll_blu_Nor damagedChara;  // ダメージを受ける予定のキャラ
    public Doll_blu_Nor DamagedChara
    {
        set { damagedChara = value; }
    }


    private CharaManeuver actManeuver;     // アクションタイミングで発動されたコマンドの格納場所

    public CharaManeuver ActMneuver
    {
        get { return actManeuver; }
        set { actManeuver = value; }
    }

    public int GiveDamage
    {
        get { return giveDamage; }
        set { giveDamage = value; }
    }

    // Update is called once per frame
    void Update()
    {
        if(standbyCharaSelect)
        {
            CharaSelectStandby();
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
                    standbyCharaSelect = false;
                    // 選択したキャラのコマンドのオブジェクトを取得
                    childCommand = move.transform.GetChild(CANVAS).transform.GetChild(DAMAGE);
                    // 技コマンドもろもろを表示
                    childCommand.gameObject.SetActive(true);
                }
            }
        }
    }

    private void ExeManeuver()
    {
        if(dollManeuver.EffectNum.ContainsKey(EffNum.Damage))
        {

        }
        else if(dollManeuver.EffectNum.ContainsKey(EffNum.Guard))   
        {

        }
        else if(dollManeuver.EffectNum.ContainsKey(EffNum.Extra))   // 固有の効果マジでどうしよう
        {

        }
    }

    private void DamageUPProcess()
    {
        // 与えるダメージが上がる系の処理
    }

    private void GuardProcess()
    {
        // 防御とかの処理
    }

    private void EXProcess()
    {
        // 背徳の悦びとか
    }

    public void OnClickNextButton()
    {
        // 最終的なダメージの結果をだし、攻撃されたキャラクターがダメージを受ける処理にしたい



        if (rollResult >= 10)
        {
            for (int i = 0; i < giveDamage; i++)
            {
                if (!damagedChara.GetHeadParts()[i].isDmage)
                {
                    damagedChara.GetHeadParts()[i].isDmage = true;
                }
            }
        }
        else if (rollResult == 9)
        {

        }
        else if (rollResult == 8)
        {

        }
        else if (rollResult == 7)
        {

        }
        else if (rollResult == 6)
        {

        }
    }
}
