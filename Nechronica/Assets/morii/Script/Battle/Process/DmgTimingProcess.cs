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

    // 定数
    const int HEAD = 10;
    const int ARM  = 9;
    const int BODY = 8;
    const int LEG  = 7;
    //--------------------------------

    private bool siteSelect = false;    // ダイスロールの値が10より多いときにtrueにする

    private int addDamage = 0;          // ダメージタイミングのマニューバ二位夜追加ダメージ
    private int giveDamage = 0;         // 与えるダメージ
    private int dmgGuard = 0;           // 与えるダメージをこの変数の値分減らす

    private bool isAddEffectStep;       // 追加効果があるか確認するステップ
    private int exprosionSite;          // 爆発による追加ダメージの部位選択  

    private Unity.Mathematics.Random randoms;

    Doll_blu_Nor selectedAllyChara;

    [SerializeField] private Button nextButton;
    [SerializeField] private GameObject confirmatButton;    // 発動するかどうかの確認するボタンのゲームオブジェクト
    [SerializeField] private GameObject damageButtons;      // ダメージタイミングのボタンの親オブジェクト

    [SerializeField] private GameObject siteSelectHead;  // 部位選択のボタン
    [SerializeField] private GameObject siteSelectArm;   // 部位選択のボタン
    [SerializeField] private GameObject siteSelectBody;  // 部位選択のボタン
    [SerializeField] private GameObject siteSelectLeg;   // 部位選択のボタン

    private int rollResult;             // ダイスロールの結果の値
    private Doll_blu_Nor damageChara;   // ダメージを受ける予定のキャラ
    private CharaManeuver actManeuver;     // アクションタイミングで発動されたコマンドの格納場所

    private bool isStandbyCutRoll = false;     // 切断判定待機
    private bool isCutRoll = false;     // 切断判定待機
    [SerializeField] private int  cutRollResult = 0;          // 切断判定のリザルト
    private int cutSite = 0;                 // 切断されそうな部位
    [SerializeField] private Text rollResultText;
    [SerializeField] private Button diceRollButton; // ダイスロールを行うボタン
    [SerializeField] private Image diceRollButtonImg;       // ダイス演出などに使うボタンの画像
    [SerializeField] private Animator diceRollAnim;         // ダイスロールのアニメ

    private List<Doll_blu_Nor> damageCharas = new List<Doll_blu_Nor>();

    private int continuousAtk = 0;      // 連撃
    public int SetContinuousAtk(int num) => continuousAtk = num;
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
        else if(isStandbyCutRoll)
        {
            // 用意if分分け。敵か味方か。
            // 敵の場合は敵の切断判定処理へ移行
            // 味方の場合はプレイヤーが切断判定の処理を行う
            if (isCutRoll)
            {
                isStandbyCutRoll = false;
                isCutRoll = false;

                CutRollJudge();
            }
        }
        else if(isAddEffectStep)
        {
            JudgeAddEffect();
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

    public void OnClickDiceRoll()
    {
        diceRollAnim.gameObject.SetActive(true);
        rollResultText.text = "";   // 文字が邪魔なので一旦空データを代入
        randoms = new Unity.Mathematics.Random((uint)Random.Range(0, 468446876));

        // その後の操作を邪魔しないようにfalseにしておく
        diceRollButtonImg.raycastTarget = false;
        diceRollButton.enabled = false;
        StartCoroutine(RollAnimStandby(diceRollAnim, callBack =>
        {
            cutRollResult = randoms.NextInt(1, 11);
            cutRollResult = 8;
            rollResultText.text = cutRollResult.ToString();
            isCutRoll = true;
        }));

    }

    public void OnClickHead()
    {
        siteSelect = true;
        // rollResultの値を変えて、攻撃する部位を選択
        rollResult = HEAD;
        SiteSelectButtonsActive(false);
    }

    public void OnClickArm()
    {
        siteSelect = true;
        // rollResultの値を変えて、攻撃する部位を選択
        rollResult = ARM;
        SiteSelectButtonsActive(false);
    }

    public void OnClickBody()
    {
        siteSelect = true;
        // rollResultの値を変えて、攻撃する部位を選択
        rollResult = BODY;
        SiteSelectButtonsActive(false);
    }

    public void OnClickLeg()
    {
        siteSelect = true;
        // rollResultの値を変えて、攻撃する部位を選択
        rollResult = LEG;
        SiteSelectButtonsActive(false);
    }

    /// <summary>
    /// 部位選択の有無を判定する処理
    /// </summary>
    public void OnClickNextButton()
    {
        // 最終的なダメージの結果をだし、攻撃されたキャラクターがダメージを受ける
        // オートタイミングのものも合わせて加算する予定
        isStandbyCharaSelect = false;

        giveDamage = actManeuver.EffectNum[EffNum.Damage] + addDamage - dmgGuard;

        // rollResultが10より多い場合は攻撃するキャラがどこの部位に当てるか決められる
        // 要if文分け。サヴァントかホラーかレギオンか
        if (rollResult > 10)
        {
            // ダイスロールの結果が10より上の場合の追加ダメージ処理
            giveDamage = giveDamage + rollResult - 10;
            SiteSelectButtonsActive(true);

            // 部位選択待機
            StartCoroutine(SelectDamageSite(callBack=>
            {
                isAddEffectStep = true;
            }));
        }
        else if (rollResult == 6)
        {
            rollResult = 10;
            isAddEffectStep = true;
        }
        else
        {
            // 部位選択がなければそのまま追加効果判定へ移行させる
            isAddEffectStep = true;
        }
    }



    /// <summary>
    /// どこの部位にダメージが入るか
    /// </summary>
    /// <param name="site"></param>
    void SortDamageParts(int site)
    {
        if(actManeuver.Atk.isAllAttack)
        {
            for(int i=0;i<ManagerAccessor.Instance.battleSystem.GetCharaObj().Count;i++)
            {
                if(ManagerAccessor.Instance.battleSystem.GetCharaObj()[i].area==damageChara.area)
                {
                    damageCharas.Add(ManagerAccessor.Instance.battleSystem.GetCharaObj()[i]);
                }
            }

            SortDamageParts(site, ref damageCharas);
        }
        else
        {
            if (site == HEAD)
            {
                damageChara.HeadParts = GiveDamageParts(damageChara.HeadParts);
            }
            else if (site == ARM)
            {
                damageChara.ArmParts = GiveDamageParts(damageChara.ArmParts);
            }
            else if (site == BODY)
            {
                damageChara.BodyParts = GiveDamageParts(damageChara.BodyParts);
            }
            else if (site == LEG)
            {
                damageChara.LegParts = GiveDamageParts(damageChara.LegParts);
            }

            if (exprosionSite != 0)
            {
                int buf = exprosionSite;
                exprosionSite = 0;
                SortDamageParts(buf);
            }
            else
            {
                StartCoroutine(ManeuverAnimation(actManeuver, callBack => EndFlowProcess()));
            }
        }
    }

    void SortDamageParts(int site, ref List<Doll_blu_Nor> listChara)
    {
        if (site == HEAD)
        {
            for(int i=0;i<listChara.Count;i++)
            {
                listChara[i].HeadParts = GiveDamageParts(listChara[i].HeadParts);
            }
        }
        else if (site == ARM)
        {
            for (int i = 0; i < listChara.Count; i++)
            {
                listChara[i].ArmParts = GiveDamageParts(listChara[i].ArmParts);
            }
        }
        else if (site == BODY)
        {
            for (int i = 0; i < listChara.Count; i++)
            {
                listChara[i].BodyParts = GiveDamageParts(listChara[i].BodyParts);
            }
        }
        else if (site == LEG)
        {
            for (int i = 0; i < listChara.Count; i++)
            {
                listChara[i].LegParts = GiveDamageParts(listChara[i].LegParts);
            }
        }

        if (exprosionSite != 0)
        {
            int buf = exprosionSite;
            exprosionSite = 0;
            SortDamageParts(buf, ref damageCharas);
        }
        else
        {
            StartCoroutine(ManeuverAnimation(actManeuver, callBack => EndFlowProcess()));
        }
    }

    /// <summary>
    /// 実際にダメージを入れる
    /// </summary>
    /// <param name="site"></param>
    List<CharaManeuver> GiveDamageParts(List<CharaManeuver> site)
    {
        for (int i = 0; i < giveDamage; i++)
        {
            if (i >= site.Count)
            {
                break;
            }
            else if (!site[i].isDmage)
            {
                site[i].isDmage = true;
            }
        }

        return site;
    }

    /// <summary>
    /// 追加効果があるかどうか判定する処理
    /// </summary>
    void JudgeAddEffect()
    {
        if (actManeuver.Atk.isExplosion)
        {
            StartCoroutine(SelectExplosionSite(callBack =>
            {
                isAddEffectStep = false;
                SortDamageParts(rollResult);
            }));
        }
        else if (actManeuver.Atk.isCutting)
        {
            isStandbyCutRoll = true;
            diceRollButton.gameObject.SetActive(true);
        }
        else
        {
            SortDamageParts(rollResult);
        }

        isAddEffectStep = false;
    }


    /// <summary>
    /// アニメーション終了まで待つ処理
    /// </summary>
    /// <param name="maneuver"></param>
    /// <param name="callBack"></param>
    /// <returns></returns>
    public IEnumerator ManeuverAnimation(CharaManeuver maneuver, System.Action<bool> callBack)
    {
        GameObject instance;
        if (maneuver.AnimEffect!=null)
        {
            instance = Instantiate(maneuver.AnimEffect, damageChara.transform.position, Quaternion.identity, damageChara.transform);
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

        if(instance!=null)
        {
            Destroy(instance);
        }


        callBack(true);
    }

    public IEnumerator SelectDamageSite(System.Action<bool> callBack)
    {
        while(!siteSelect)
        {
            yield return null;
        }

        siteSelect = false;
        callBack(true);
    }

    /// <summary>
    /// 爆発による追加ダメージがどこに入るか、また選択の有無があれば選択させるための処理
    /// </summary>
    /// <param name="callBack"></param>
    /// <returns></returns>
    public IEnumerator SelectExplosionSite(System.Action<bool> callBack)
    {
        int roll = rollResult;

        if(rollResult==HEAD)
        {
            exprosionSite = ARM;
        }
        else if(rollResult==ARM)
        {
            siteSelectHead.gameObject.SetActive(true);
            siteSelectBody.gameObject.SetActive(true);

            while (!siteSelect)
            {
                yield return null;
            }

            exprosionSite = rollResult;
            
        }
        else if (rollResult == BODY)
        {
            siteSelectArm.gameObject.SetActive(true);
            siteSelectLeg.gameObject.SetActive(true);

            while (!siteSelect)
            {
                yield return null;
            }

            exprosionSite = rollResult;
        }
        else if (rollResult == LEG)
        {
            exprosionSite = BODY;
        }

        rollResult = roll;
        siteSelect = false;
        callBack(true);
    }

    /// <summary>
    /// 切断判定の成否。成功してたら99ダメージという判定にし、すべてのパーツを破損させる
    /// </summary>
    public void CutRollJudge()
    {
        if(cutRollResult>=6)
        {
            giveDamage = 99;
            SortDamageParts(rollResult);
        }
        else
        {
            SortDamageParts(rollResult);
        }
    }

    public void SiteSelectButtonsActive(bool isActive)
    {
        siteSelectHead.gameObject.SetActive(isActive);
        siteSelectArm.gameObject.SetActive(isActive);
        siteSelectBody.gameObject.SetActive(isActive);
        siteSelectLeg.gameObject.SetActive(isActive);
    }


    /// <summary>
    /// カウントの流れ終了時の処理.
    /// 連撃があるならジャッジタイミングへ移行し、もう一度同じ処理をする。
    /// </summary>
    void EndFlowProcess()
    {
        // もろもろを初期化
        addDamage = 0;
        giveDamage = 0;
        dmgGuard = 0;
        siteSelect = false;

        diceRollButton.gameObject.SetActive(false);
        diceRollButtonImg.raycastTarget = true;
        diceRollButton.enabled = true;
        rollResultText.text = "ダイスロール";
        diceRollAnim.gameObject.SetActive(false);

        if (continuousAtk < actManeuver.Atk.Num_per_Action)
        {
            damageButtons.SetActive(false);
            isStandbyEnemySelect = false;
            isStandbyCharaSelect = false;

            //ここでジャッジタイミングへ移行
            ProcessAccessor.Instance.jdgTiming.SetActChara(actingChara);
            ProcessAccessor.Instance.jdgTiming.ActMneuver = actManeuver;
            ProcessAccessor.Instance.jdgTiming.IsStandbyDiceRoll = true;
            ProcessAccessor.Instance.jdgTiming.AtkTargetEnemy = damageChara.gameObject;
            ProcessAccessor.Instance.jdgTiming.GetJudgeButton().SetActive(true);
            ProcessAccessor.Instance.jdgTiming.GetDiceRollButton().gameObject.SetActive(true);
            ProcessAccessor.Instance.jdgTiming.SetContinuousAtk(continuousAtk++);
            if (actingChara.gameObject.CompareTag("EnemyChara")/*||自動ダイスロール的な？設定参照用*/)
            {
                ProcessAccessor.Instance.jdgTiming.OnClickDiceRoll();
            }
        }
        else
        {
            
            continuousAtk = 0;
            // 連撃の数のカウントをジャッジ側で管理できないのでここで初期化
            SetContinuousAtk(continuousAtk);
            // 行動したキャラを表示から消す
            ManagerAccessor.Instance.battleSystem.DeleteMoveChara();
            ManagerAccessor.Instance.battleSystem.BattleExe = true;
            nextButton.gameObject.SetActive(false);
            
        }
        
    }

    /// <summary>
    /// ダイスの回転が終わるのを待つメソッド
    /// </summary>
    /// <returns></returns>
    private IEnumerator RollAnimStandby(Animator anim, System.Action<bool> callBack)
    {
        while (!anim.GetCurrentAnimatorStateInfo(0).IsName("End"))
        {
            yield return null;
        }

        callBack(true);
    }

}
