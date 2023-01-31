using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RpdTimingProcess : GetClickedGameObject
{
    List<MoveCharaList> standbyManeuver = new List<MoveCharaList>();

    private GameObject atkTargetEnemy;      // 攻撃する敵オブジェクトを格納場所
    private GameObject charaCommand;     // 選んだキャラのコマンド格納用

    Doll_blu_Nor selectedAllyChara;         // 選択した味方キャラ
    Doll_blu_Nor selectedTargetChara;       // 選択した敵キャラ

    bool moveDirection;
    public void SetDirection(bool isDirection) => moveDirection = isDirection;


    bool exeManeuverProcess = false;
    public bool ExemaneuverProcess
    {
        get { return exeManeuverProcess; }
        set { exeManeuverProcess = value; }
    }

    public GameObject AtkTargetEnemy
    {
        set { atkTargetEnemy = value; }
        get { return atkTargetEnemy; }
    }

    private CharaManeuver actManeuver;     // アクションタイミングで発動されたコマンドの格納場所

    public CharaManeuver ActMneuver
    {
        get { return actManeuver; }
        set { actManeuver = value; }
    }

    [SerializeField] private Button nextButton;     // ラピッドタイミングの処理を開始するボタン
    [SerializeField] private GameObject confirmatButton;    // 最後に発動するか確認するボタン
    [SerializeField] private GameObject rapidButtons;       // ジャッジタイミングで扱うボタンの親オブジェクト
    public GameObject GetConfirmatButton() => confirmatButton;
    public void SetRapidButton(bool isActive) => rapidButtons.SetActive(isActive);

    void Awake()
    {
        ProcessAccessor.Instance.rpdTiming = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (isSelectedChara)
        {
            SkillSelectStandby();
        }
        else if (isStandbyEnemySelect)
        {
            EnemySelectStandby();
        }
        else if (exeManeuverProcess)
        {
            startExeManeuverListProcess();
        }
        else if (isStandbyCharaSelect)
        {
            CharaSelectStandby();
        }
        
    }

    /// <summary>
    /// enemyとは名ばかりの
    /// 敵選択待機状態時に動く関数
    /// クリックするまで特に何も処理を行わない
    /// クリックしたらその場所にレイを飛ばし、オブジェクトを取得
    /// </summary>
    protected override void EnemySelectStandby()
    {
        //左クリックで
        if (Input.GetMouseButtonDown(0))
        {
            // プレイアブルキャラに向いていたカメラ情報を保存
            saveCharaCamera = CharaCamera;

            GameObject clickedObj = ShotRay();

            //クリックしたゲームオブジェクトが敵キャラなら
            if (clickedObj.CompareTag("EnemyChara") || clickedObj.CompareTag("AllyChara"))
            {
                ZoomUpObj(clickedObj);
                // ここでコマンド表示
                StartCoroutine(MoveStandby(clickedObj));
            }
        }
    }

    override protected void CharaSelectStandby()
    {
        //左クリックで
        if (Input.GetMouseButtonDown(0) && CharaCamera == null && !isSelectedChara)
        {
            GameObject clickedObj = ShotRay();

            //クリックしたゲームオブジェクトが味方キャラなら
            if (clickedObj.CompareTag("AllyChara"))
            {
                ZoomUpObj(clickedObj);
                isSelectedChara = true;
                childCommand = clickedObj.transform.GetChild(CANVAS).transform.GetChild(RAPID);
                
                // ここでコマンド表示
                StartCoroutine(MoveStandby(clickedObj));
            }
        }
    }

    /// <summary>
    /// カメラが近づいてからコマンドを表示するメソッド
    /// </summary>
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
                if (isStandbyCharaSelect)
                {
                    if (move.CompareTag("AllyChara"))
                    {
                        // コマンドを表示する
                        isStandbyCharaSelect = false;
                        selectedAllyChara = move.GetComponent<Doll_blu_Nor>();
                        childCommand.gameObject.SetActive(true);
                    }
                }
                else if(isStandbyEnemySelect)
                {
                    Doll_blu_Nor selectedChara= move.GetComponent<Doll_blu_Nor>();
                    // 選択されたキャラのエリアと選択されたマニューバの射程を絶対値で比べて、射程内であれば発動するか選択するコマンドを表示する
                    // 選択されたキャラのエリアの絶対値が攻撃の最大射程以下且つ、
                    // 選択されたキャラのエリアの絶対値が攻撃の最小射程以上なら発動する
                    if ((Mathf.Abs(selectedChara.area) <= Mathf.Abs(dollManeuver.MaxRange + selectedAllyChara.area) &&
                         Mathf.Abs(selectedChara.area) >= Mathf.Abs(dollManeuver.MinRange + selectedAllyChara.area)) &&
                        (!dollManeuver.isUse && !dollManeuver.isDmage))
                    {
                        selectedTargetChara = selectedChara;
                        if(dollManeuver.EffectNum.ContainsKey(EffNum.Move))
                        {
                            ProcessAccessor.Instance.actTimingMove.GetMoveButtons().SetActive(true);
                            ProcessAccessor.Instance.actTimingMove.IsRapid = true;
                        }
                        else
                        {
                            confirmatButton.SetActive(true);
                        }
                    }
                    else
                    {
                        // 射程が足りませんみたいな表記をする
                    }
                }
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
            isSelectedChara = false;
        }
    }

    public void OnClickBack()
    {
        ZoomOutObj();
        confirmatButton.SetActive(false);
        isStandbyCharaSelect = true;
    }

    /// <summary>
    /// 選択したマニューバーをリストに追加する処理
    /// </summary>
    public void OnClickManeuver()
    {
        // 処理順リストに格納
        // 射程が自身のみの場合、ターゲットキャラ自体の引数にも自身を格納する
        if (dollManeuver.MinRange == 10)
        {
            AddRapidManeuver(selectedAllyChara, selectedAllyChara, dollManeuver);
        }
        // マニューバーを実行するキャラ、ターゲットとなるキャラ、マニューバー自体を引数にし、格納
        else
        {
            AddRapidManeuver(selectedAllyChara, selectedTargetChara, dollManeuver);
        }
            
        // UIとかを消す
        ZoomOutObj();
        confirmatButton.SetActive(false);
        // 処理するリストに格納したので、後々使う変数を初期化
        selectedTargetChara = null;
        selectedAllyChara = null;
    }

    /// <summary>
    /// ラピッドマニューバーを処理リストに追加するメソッド
    /// </summary>
    /// <param name="moveChara">マニューバーを実行するキャラ</param>
    /// <param name="targetChara">マニューバーの効果を受けるキャラ</param>
    /// <param name="maneuver">マニューバー自体</param>
    public void AddRapidManeuver(Doll_blu_Nor moveChara, Doll_blu_Nor targetChara, CharaManeuver maneuver)
    {
        // 処理順リストに格納
        MoveCharaList buff = new MoveCharaList();
        buff.moveChara = moveChara;
        buff.targetChara = targetChara;
        buff.moveManeuver = maneuver;
        if (maneuver.EffectNum.ContainsKey(EffNum.Move)) buff.direction = moveDirection;
        standbyManeuver.Add(buff);
        isStandbyCharaSelect = true;
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
        exeManeuverProcess = true;
    }

    /// <summary>
    /// リストの中にあるマニューバーの処理を開始
    /// </summary>
    public void startExeManeuverListProcess()
    {
        if(standbyManeuver.Count!=0)
        {
            ProcessDivide(standbyManeuver.Last());
            // 一気に処理しないようにする
            //exeManeuverProcess = false;
            // 末尾の要素を削除
            standbyManeuver.RemoveAt(standbyManeuver.Count - 1);
        }
        else
        {
            rapidButtons.SetActive(false);
            exeManeuverProcess = false;
            isStandbyEnemySelect = false;
            isStandbyCharaSelect = false;

            Doll_blu_Nor cashedChara = atkTargetEnemy.GetComponent<Doll_blu_Nor>();

            // 攻撃されるキャラが射程外に出たりしていなければジャッジタイミングへ移行
            if ((Mathf.Abs(cashedChara.area) <= Mathf.Abs(actManeuver.MaxRange + actingChara.area) &&
                 Mathf.Abs(cashedChara.area) >= Mathf.Abs(actManeuver.MinRange + actingChara.area)) &&
                        !actManeuver.isDmage)
            {
                //ここでジャッジタイミングへ移行
                ProcessAccessor.Instance.jdgTiming.SetActChara(actingChara);
                ProcessAccessor.Instance.jdgTiming.ActMneuver = actManeuver;
                ProcessAccessor.Instance.jdgTiming.IsStandbyDiceRoll = true;
                ProcessAccessor.Instance.jdgTiming.AtkTargetEnemy = atkTargetEnemy.gameObject;
                ProcessAccessor.Instance.jdgTiming.GetDiceRollButton().gameObject.SetActive(true);
                if (actingChara.gameObject.CompareTag("EnemyChara")/*||自動ダイスロール的な？設定参照用*/)
                {
                    ProcessAccessor.Instance.jdgTiming.OnClickDiceRoll();
                }
                else if(actingChara.gameObject.CompareTag("AllyChara"))
                {
                    ProcessAccessor.Instance.jdgTiming.GetJudgeButton().SetActive(true);
                }
            }
            else
            {
                // ここ、アニメーション終わってからの処理にしたいなぁ
                // 行動したキャラを表示から消す
                ManagerAccessor.Instance.battleSystem.DeleteMoveChara();
                ManagerAccessor.Instance.battleSystem.BattleExe = true;
            }
            

        }
    }

    public void ProcessDivide(MoveCharaList charaList)
    {
        if(charaList.moveManeuver.EffectNum.ContainsKey(EffNum.Move))
        {
            ProcessAccessor.Instance.actTimingMove.MoveChara(charaList.direction, charaList.targetChara, true);
            charaList.moveChara.NowCount -= charaList.moveManeuver.Cost;
        }
        else if (charaList.moveManeuver.EffectNum.ContainsKey(EffNum.YobunnnaUde))
        {

        }
    }


}

public class MoveCharaList
{
    public Doll_blu_Nor  moveChara;
    public Doll_blu_Nor  targetChara;
    public CharaManeuver moveManeuver;

    // 移動マニューバ用
    public bool direction;
}