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
    const int HEAD = 1;
    const int ARM  = 2;
    const int BODY = 3;
    const int LEG  = 4;
    //--------------------------------

    private bool siteSelect = false;    // ダイスロールの値が10より多いときにtrueにする
    private int siteSelectNum = 0;      // 部位選択を数値で決める

    private int addDamage = 0;          // ダメージタイミングのマニューバ二位夜追加ダメージ
    private int giveDamage = 0;         // 与えるダメージ
    private int dmgGuard = 0;           // 与えるダメージをこの変数の値分減らす

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
            if (isCutRoll)
            {
                isStandbyCutRoll = false;
                isCutRoll = false;

                
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
            rollResult = randoms.NextInt(1, 11);
            rollResult = 11;
            rollResultText.text = rollResult.ToString();
            isCutRoll = true;
        }));

    }

    public void OnClickHead()
    {
        siteSelect = true;
        siteSelectNum = HEAD;
        SiteSelectButtonsActive(false);
    }

    public void OnClickArm()
    {
        siteSelect = true;
        siteSelectNum = ARM;
        SiteSelectButtonsActive(false);
    }

    public void OnClickBody()
    {
        siteSelect = true;
        siteSelectNum = BODY;
        SiteSelectButtonsActive(false);
    }

    public void OnClickLeg()
    {
        siteSelect = true;
        siteSelectNum = LEG;
        SiteSelectButtonsActive(false);
    }

    public void OnClickNextButton()
    {
        // 最終的なダメージの結果をだし、攻撃されたキャラクターがダメージを受ける
        // オートタイミングのものも合わせて加算する予定
        isStandbyCharaSelect = false;

        giveDamage = actManeuver.EffectNum[EffNum.Damage] + addDamage - dmgGuard;

        

        // rollResultが10より多い場合は攻撃するキャラがどこの部位に当てるか決められるが今は仮に頭とする
        // 要if文分け。サヴァントかホラーかレギオンか
        if (rollResult > 10)
        {
            // ダイスロールの結果が10より上の場合の追加ダメージ処理
            int addDmg = rollResult - 10;
            giveDamage = giveDamage + addDmg;
            SiteSelectButtonsActive(true);

            StartCoroutine(SelectDamageSite(callBack =>
            {
                SortDamageParts(siteSelectNum);
            }));
        }
        else if (rollResult == 10)
        {
            SortDamageParts(HEAD);
        }
        else if (rollResult == 9)
        {
            SortDamageParts(ARM);
        }
        else if (rollResult == 8)
        {
            SortDamageParts(BODY);
        }
        else if (rollResult == 7)
        {
            SortDamageParts(LEG);
        }
        else if (rollResult == 6)
        {
            // 与えるダメージがパーツの数より多い場合、要素数より多い数を参照しないようにする。
            if (giveDamage > damageChara.HeadParts.Count)
            {
                giveDamage = damageChara.HeadParts.Count;
            }

            // 相手が選ぶ。今は仮に頭にダメージが入るようにする
            for (int i = 0; i < giveDamage; i++)
            {
                if (!damageChara.HeadParts[i].isDmage)
                {
                    damageChara.HeadParts[i].isDmage = true;
                }
            }
        }

        siteSelect = false;
        siteSelectNum = 0;
    }



    /// <summary>
    /// どこの部位にダメージが入るか
    /// </summary>
    /// <param name="site"></param>
    void SortDamageParts(int site)
    {
        randoms = new Unity.Mathematics.Random((uint)Random.Range(0, 468446876));
        if (site==HEAD)
        {
            damageChara.HeadParts = DamagingParts(damageChara.HeadParts);
            // 爆発による追加ダメージ
            if (actManeuver.Atk.isExplosion)
            {
                DamagingParts(damageChara.ArmParts);
            }
        }
        else if (site == ARM)
        {
            int isChoices = randoms.NextInt(1, 2);
            damageChara.ArmParts = DamagingParts(damageChara.ArmParts);
            if(isChoices==1)
            {
                DamagingParts(damageChara.HeadParts);
            }
            else
            {
                DamagingParts(damageChara.BodyParts);
            }

        }
        else if (site == BODY)
        {
            int isChoices = randoms.NextInt(1, 2);
            damageChara.BodyParts = DamagingParts(damageChara.BodyParts);
            if (isChoices == 1)
            {
                DamagingParts(damageChara.ArmParts);
            }
            else
            {
                DamagingParts(damageChara.LegParts);
            }
        }
        else if (site == LEG)
        {
            damageChara.LegParts = DamagingParts(damageChara.LegParts);
            // 爆発による追加ダメージ
            if (actManeuver.Atk.isExplosion)
            {
                DamagingParts(damageChara.BodyParts);
            }
        }

        if(actManeuver.Atk.isCutting)
        {
            cutSite = site;
            isStandbyCutRoll = true;
            diceRollButton.gameObject.SetActive(true);
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
    List<CharaManeuver> DamagingParts(List<CharaManeuver> site)
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

        callBack(true);
    }

    public void CutRollJudge()
    {
        if(cutRollResult>=6)
        {
            giveDamage = 99;
            if(cutSite==HEAD)
            {
                
            }
            else if (cutSite == ARM)
            {

            }
            else if (cutSite == BODY)
            {

            }
            else if (cutSite == LEG)
            {

            }
        }

        StartCoroutine(ManeuverAnimation(actManeuver, callBack => EndFlowProcess()));
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
        siteSelectNum = 0;
        siteSelect = false;

        if (continuousAtk<actManeuver.Atk.Num_per_Action)
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
