using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DmgTimingProcess : GetClickedGameObject
{
    //-------------------------------
    // ほしい情報メモ
    // 攻撃するenemy
    // ターゲットになってる味方キャラ
    //-------------------------------

    private int addDamage = 0;          // ダメージタイミングのマニューバ二位夜追加ダメージ
    private int giveDamage = 0;         // 与えるダメージ
    private int dmgGuard = 0;           // 与えるダメージをこの変数の値分減らす

    Doll_blu_Nor selectedAllyChara;

    [SerializeField] private Button nextButton;
    [SerializeField] private GameObject confirmatButton;    // 発動するかどうかの確認するボタンのゲームオブジェクト
    [SerializeField] private GameObject damageButtons;      // ダメージタイミングのボタンの親オブジェクト

    private int rollResult;             // ダイスロールの結果の値
    private Doll_blu_Nor damageChara;   // ダメージを受ける予定のキャラ
    private CharaManeuver actManeuver;     // アクションタイミングで発動されたコマンドの格納場所

    public GameObject GetConfirmatButton() => confirmatButton;
    public GameObject GetDamageButtons() => damageButtons;
    public void SetDamageChara(Doll_blu_Nor value) => damageChara = value;
    public void SetRollResult(int value) => rollResult = value;
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

    private void Awake()
    {
        ProcessAccessor.Instance.dmgTiming = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(isStandbyCharaSelect)
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
                    isStandbyCharaSelect = false;
                    selectedAllyChara = move.GetComponent<Doll_blu_Nor>();
                    // 選択したキャラのコマンドのオブジェクトを取得
                    childCommand = move.transform.GetChild(CANVAS).transform.GetChild(DAMAGE);
                    // 技コマンドもろもろを表示
                    childCommand.gameObject.SetActive(true);
                }
            }
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
        nextButton.gameObject.SetActive(false);
    }

    public void OnClickExe()
    {
        ExeManeuver(dollManeuver, selectedAllyChara);
    }


    /// <summary>
    /// 発動したマニューバーが何をするのかの確認をする
    /// </summary>
    public void ExeManeuver(CharaManeuver maneuver, Doll_blu_Nor dmgExeChara)
    {
        // ダメージを増加するマニューバーの処理
        if(maneuver.EffectNum.ContainsKey(EffNum.Damage))
        {
            DamageUPProcess(maneuver,dmgExeChara);
        }
        // 防御値を増加するマニューバーの処理
        else if(dollManeuver.EffectNum.ContainsKey(EffNum.Guard))   
        {
            GuardProcess(maneuver, dmgExeChara);
        }
        else   // 上の二つに該当しない場合、固有の効果と使用する
        {

        }

        // 同カウントで動くキャラがダメージタイミングのマニューバーを発動した場合、同カウントに行動ができなくなるので左の表示および動ける予定のキャラのリストから削除する
        if(selectedAllyChara.NowCount==ManagerAccessor.Instance.battleSystem.NowCount)
        {
            ManagerAccessor.Instance.battleSystem.DeleteMoveChara(selectedAllyChara.Name);
        }

        confirmatButton.SetActive(false);
        ZoomOutObj();
    }

    private void DamageUPProcess(CharaManeuver maneuver, Doll_blu_Nor dmgExeChara)
    {
        // 与えるダメージが上がる系の処理
        // 射程が自身のみの場合、ダメージを与えるキャラとダメージタイミングで動くキャラが同じかどうか調べる
        if (dollManeuver.MinRange == 10)
        {
            if(actingChara == dmgExeChara)
            {
                addDamage += maneuver.EffectNum[EffNum.Damage];
                // 要if文分け。特殊なコストどうか判断する
                dmgExeChara.NowCount -= maneuver.Cost;
            }
        }
        // 敵キャラのエリアと選択されたマニューバの射程を絶対値で比べて、射程内であれば攻撃するか選択するコマンドを表示する
        // 敵キャラのエリアの絶対値が攻撃の最大射程以下且つ、
        // 敵キャラのエリアの絶対値が攻撃の最小射程以上なら発動する
        else if ((Mathf.Abs(actingChara.area) <= Mathf.Abs(maneuver.MaxRange + dmgExeChara.area)  &&
                  Mathf.Abs(actingChara.area) >= Mathf.Abs(maneuver.MinRange + dmgExeChara.area)) &&
                (!maneuver.isUse && !maneuver.isDmage))
        {
            addDamage += maneuver.EffectNum[EffNum.Damage];
            // 要if文分け。特殊なコストどうか判断する
            dmgExeChara.NowCount -= maneuver.Cost;
        }
        else
        {
            // 射程が足りません見たいな表記する
        }
    }

    private void GuardProcess(CharaManeuver maneuver, Doll_blu_Nor dmgExeChara)
    {
        // 防御とかの処理
        // 射程が自身のみの場合、ダメージを受けるキャラとダメージタイミングで動くキャラが同じかどうか調べる
        if (maneuver.MinRange == 10)
        {
            if (damageChara == dmgExeChara)
            {
                dmgGuard += maneuver.EffectNum[EffNum.Guard];
                // 要if文分け。特殊なコストどうか判断する
                dmgExeChara.NowCount -= maneuver.Cost;
            }
        }
        else if ((Mathf.Abs(damageChara.area) <= Mathf.Abs(maneuver.MaxRange + dmgExeChara.area) &&
                  Mathf.Abs(damageChara.area) >= Mathf.Abs(maneuver.MinRange + dmgExeChara.area)) &&
                (!maneuver.isUse && !maneuver.isDmage))
        {
            if(maneuver.EffectNum.ContainsKey(EffNum.Guard))
            {
                dmgGuard += maneuver.EffectNum[EffNum.Guard];
            }
            // 要if文分け。特殊なコストどうか判断する
            selectedAllyChara.NowCount -= maneuver.Cost;
        }
        else
        {
            // 射程が足りません見たいな表記する
        }
    }

    private void EXProcess()
    {
        // かばうの処理
        if (dollManeuver.EffectNum.ContainsKey(EffNum.Protect))
        {
            // ここかばうの処理入るかも
            // 要if文分け。特殊なコストどうか判断する
            selectedAllyChara.NowCount -= dollManeuver.Cost;
        }
        
    }

    public void OnClickNextButton()
    {
        // 最終的なダメージの結果をだし、攻撃されたキャラクターがダメージを受ける
        // オートタイミングのものも合わせて加算する予定
        bool isAnim = false;
        

        giveDamage = actManeuver.EffectNum[EffNum.Damage] + addDamage - dmgGuard;

        // rollResultが10より多い場合は攻撃するキャラがどこの部位に当てるか決められるが今は仮に頭とする
        // 要if文分け。サヴァントかホラーかレギオンか
        if (rollResult > 10)
        {
            // ダイスロールの結果が10より上の場合の追加ダメージ処理
            int addDmg = rollResult - 10;
            giveDamage = giveDamage + addDmg;

            // 与えるダメージがパーツの数より多い場合、要素数より多い数を参照しないようにする。
            if (giveDamage > damageChara.GetHeadParts().Count)
            {
                giveDamage = damageChara.GetHeadParts().Count;
            }

            for (int i = 0; i < giveDamage; i++)
            {
                if (!damageChara.GetHeadParts()[i].isDmage)
                {
                    damageChara.GetHeadParts()[i].isDmage = true;
                }
            }
        }
        else if (rollResult == 10)
        {
            // 与えるダメージがパーツの数より多い場合、要素数より多い数を参照しないようにする。
            if (giveDamage>damageChara.GetHeadParts().Count)
            {
                giveDamage = damageChara.GetHeadParts().Count;
            }

            for (int i = 0; i < giveDamage; i++)
            {
                if (!damageChara.GetHeadParts()[i].isDmage)
                {
                    damageChara.GetHeadParts()[i].isDmage = true;
                }
            }
        }
        else if (rollResult == 9)
        {
            // 与えるダメージがパーツの数より多い場合、要素数より多い数を参照しないようにする。
            if (giveDamage > damageChara.GetArmParts().Count)
            {                               
                giveDamage = damageChara.GetArmParts().Count;
            }

            for (int i = 0; i < giveDamage; i++)
            {
                if (!damageChara.GetArmParts()[i].isDmage)
                {
                    damageChara.GetArmParts()[i].isDmage = true;
                }
            }
        }
        else if (rollResult == 8)
        {
            // 与えるダメージがパーツの数より多い場合、要素数より多い数を参照しないようにする。
            if (giveDamage > damageChara.GetBodyParts().Count)
            {                               
                giveDamage = damageChara.GetBodyParts().Count;
            }

            for (int i = 0; i < giveDamage; i++)
            {
                if (!damageChara.GetBodyParts()[i].isDmage)
                {
                    damageChara.GetBodyParts()[i].isDmage = true;
                }
            }
        }
        else if (rollResult == 7)
        {
            // 与えるダメージがパーツの数より多い場合、要素数より多い数を参照しないようにする。
            if (giveDamage > damageChara.GetLegParts().Count)
            {                               
                giveDamage = damageChara.GetLegParts().Count;
            }

            for (int i = 0; i < giveDamage; i++)
            {
                if (!damageChara.GetLegParts()[i].isDmage)
                {
                    damageChara.GetLegParts()[i].isDmage = true;
                }
            }
        }
        else if (rollResult == 6)
        {
            // 与えるダメージがパーツの数より多い場合、要素数より多い数を参照しないようにする。
            if (giveDamage > damageChara.GetHeadParts().Count)
            {
                giveDamage = damageChara.GetHeadParts().Count;
            }

            // 相手が選ぶ。今は仮に頭にダメージが入るようにする
            for (int i = 0; i < giveDamage; i++)
            {
                if (!damageChara.GetHeadParts()[i].isDmage)
                {
                    damageChara.GetHeadParts()[i].isDmage = true;
                }
            }
        }

        // ここ、アニメーション終わってからの処理にしたいなぁ

        StartCoroutine(ManeuverAnimation(actManeuver, callBack =>
        {
            // 行動したキャラを表示から消す
            ManagerAccessor.Instance.battleSystem.DeleteMoveChara();
            ManagerAccessor.Instance.battleSystem.BattleExe = true;
            nextButton.gameObject.SetActive(false);
        }));
    }

    public IEnumerator ManeuverAnimation(CharaManeuver maneuver, System.Action<bool> callBack)
    {
        if(maneuver.AnimEffect!=null)
        {
            GameObject instance = Instantiate(maneuver.AnimEffect, damageChara.transform.position, Quaternion.identity, damageChara.transform);
            EffctEnd effctEnd = instance.GetComponent<EffctEnd>();

            while (!effctEnd.AnimEnd)
            {
                yield return null;  
            }
        }
        else
        {
            yield break;
        }

        callBack(true);
    }

}
