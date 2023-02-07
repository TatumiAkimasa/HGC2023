using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class ActTimingProcess : GetClickedGameObject
{
    // 定数--------------------------
    private const bool ALLY  = true;
    private const bool ENEMY = false;
    //-------------------------------

    // キャラクターのアクションコマンドを取得
    private BattleCommand actCharaCommand;
    private ActCommands actCommand;
    private RpdCommand rpdCommand;
    private bool isAllyOrEnemy;                       // 選んだキャラが敵か味方かの判断

    private GameObject atkTargetEnemy;                // 攻撃する敵オブジェクトを格納場所
    public GameObject AtkTargetEnemy
    {
        set { atkTargetEnemy = value; }
        get { return atkTargetEnemy; }
    }

    [SerializeField] protected Button exeButton;
    public Button ExeButton
    {
        get { return exeButton; }
    }


    // Start is called before the first frame update
    void Awake()
    {
        ProcessAccessor.Instance.actTiming = this;
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
        else if (isStandbyCharaSelect)
        {
            CharaSelectStandby();
        }
        
    }

    // カメラの動き、キャラ選択関連のメソッド--------------------------------------------------

    /// <summary>
    /// キャラ選択待機状態時に動く関数
    /// クリックするまで特に何も処理を行わない
    /// クリックしたらその場所にレイを飛ばし、オブジェクトを取得
    /// </summary>
    protected override void CharaSelectStandby()
    {
        //左クリックで
        if (Input.GetMouseButtonDown(0) && CharaCamera == null && !isSelectedChara)
        {
            GameObject clickedObj = ShotRay();


            if(clickedObj==null)
            {
                return;
            }
            //クリックしたゲームオブジェクトが味方キャラなら
            else if (clickedObj.CompareTag("AllyChara")　|| clickedObj.CompareTag("EnemyChara"))
            {
                // コマンドを表示し、選んだキャラに近づく
                actCharaCommand = clickedObj.GetComponent<BattleCommand>();
                actCommand = clickedObj.GetComponent<ActCommands>();
                rpdCommand = clickedObj.GetComponent<RpdCommand>();
                ZoomUpObj(clickedObj);
                // ここでコマンド表示
                StartCoroutine(MoveStandby(clickedObj));
            }
        }
    }

    /// <summary>
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
            if (clickedObj.CompareTag("EnemyChara"))
            {
                ZoomUpObj(clickedObj);
                // ここでコマンド表示
                StartCoroutine(MoveStandby(clickedObj));
            }
        }
    }

    /// <summary>
    /// スキル選択待機状態
    /// ここではカメラが元に戻る処理だけ行う
    /// 元に戻るタイミングはスキルが選択されたタイミング、戻るボタンが押されたタイミング
    /// </summary>
    protected override void SkillSelectStandby()
    {
        // 右クリックで
        if ((Input.GetMouseButtonDown(1) || skillSelected) && CharaCamera != null)
        {
            if(isAllyOrEnemy==ALLY)
            {
                // マニューバを選ぶコマンドまで表示されていたらそのコマンドだけ消す
                if (ProcessAccessor.Instance.actTimingMove.GetMoveButtons().activeSelf)
                {
                    ProcessAccessor.Instance.actTimingMove.GetMoveButtons().SetActive(false);
                }
                else if (actCommand.GetCommands().activeSelf)
                {
                    actCommand.GetCommands().SetActive(false);
                }
                else if(rpdCommand.GetCommands().activeSelf)
                { 
                    rpdCommand.GetCommands().SetActive(false);
                }
                else
                {
                    MyZoomOutObj(actCharaCommand.GetActSelect());
                    // キャラ選択待機状態にする
                    isSelectedChara = false;
                    if(skillSelected)
                    {
                        skillSelected = false;
                    }
                }
            }
            else
            {
                ZoomOutObj();
            }
        }
    }

    public void ZoomOutRequest()
    {
        ZoomOutObj();
    }

    private void MyZoomOutObj(GameObject hideCommand)
    {
        // 全体を表示させるカメラを優先にする。
        CharaCamera.Priority = 0;
        // コマンドを消す
        hideCommand.SetActive(false);
        // 複製したプレハブカメラを消す。
        StartCoroutine(DstroyCamera());
    }

    /// <summary>
    /// 敵選択の時に戻るを押したら実行されるメソッド
    /// </summary>
    protected void OnClickBack()
    {
        // カメラを元の位置に戻し、UIを消す
        ZoomOutObj();
        this.transform.GetChild(CANVAS).transform.GetChild(0).gameObject.SetActive(false);
        // childCommandの中身をなくす
        if (childCommand != null)
        {
            childCommand = null;
        }
    }

    //-----------------------------------------------------------------------------------------

    /// <summary>
    /// カメラが近づいた後の処理。敵と味方で処理が異なる
    /// 味方-------------------
    /// コマンドを表示するだけ
    /// 敵---------------------
    /// 敵オブジェクトを取得し、ステータスを表示
    /// その後、攻撃ボタンを表示する
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
                if (move.CompareTag("AllyChara"))
                {
                    isAllyOrEnemy = ALLY;
                    for (int j=0;j< ManagerAccessor.Instance.battleSystem.GetMoveChara().Count;j++)
                    {
                        if (move.name == ManagerAccessor.Instance.battleSystem.GetMoveChara()[j].name)
                        {
                            // 技コマンドもろもろを表示
                            actCharaCommand.GetActSelect().SetActive(true);
                            break;
                        }
                    }
                    
                }
                else if (move.CompareTag("EnemyChara"))
                {
                    // ステータスを取得し、表示。後にOnClickAtkで使うのでメンバ変数に格納
                    childCommand = move.transform.GetChild(CANVAS);
                    childCommand.gameObject.SetActive(true);
                    isAllyOrEnemy = ENEMY;

                    // 敵キャラのエリアと選択されたマニューバの射程を絶対値で比べて、射程内であれば攻撃するか選択するコマンドを表示する
                    // 敵キャラのエリアの絶対値が攻撃の最大射程以下且つ、
                    // 敵キャラのエリアの絶対値が攻撃の最小射程以上なら攻撃する
                    if (isStandbyEnemySelect && dollManeuver.MinRange != 10 && RangeCheck(move.GetComponent<Doll_blu_Nor>(), dollManeuver, actingChara))
                    {
                        atkTargetEnemy = move;
                        atkTargetEnemy.transform.GetChild(CANVAS).gameObject.SetActive(true);

                        exeButton.onClick.AddListener(() => OnClickAtkRequest());

                        this.transform.GetChild(CANVAS).transform.GetChild(ATKBUTTONS).gameObject.SetActive(true);
                    }
                }
                isSelectedChara = true;
            }
        }
    }

    /// <summary>
    /// 次のジャッジタイミングに移行をリクエストするボタン
    /// </summary>
    private void OnClickAtkRequest()
    {
        ExeAtkManeuver(atkTargetEnemy.GetComponent<Doll_blu_Nor>(), dollManeuver, actingChara);
    }

    /// <summary>
    /// 待機をするボタン
    /// </summary>
    public void OnClickStandby()
    {
        ExeStandby(actingChara);
    }


    /// <summary>
    /// ジャッジタイミングへ移行するメソッド
    /// </summary>
    /// <param name="enemy">攻撃対象</param>
    /// <param name="maneuver">攻撃内容</param>
    /// <param name="actChara">攻撃を行うキャラ</param>
    public void ExeAtkManeuver(Doll_blu_Nor enemy, CharaManeuver maneuver,Doll_blu_Nor actChara)
    {
        // actCharaのタグがAllyCharaの場合カメラやUIが出ているので、カメラを元の位置に戻しUIを消す
        if(actChara.gameObject.CompareTag("AllyChara"))
        {
            ZoomOutObj();
            this.transform.GetChild(CANVAS).transform.GetChild(ATKBUTTONS).gameObject.SetActive(false);
        }


        isSelectedChara = false;
        isStandbyEnemySelect = false;
        isStandbyCharaSelect = false;

        // ここからジャッジに入る
        ProcessAccessor.Instance.rpdTiming.SetActChara(actChara);
        ProcessAccessor.Instance.rpdTiming.ActMneuver = maneuver;
        ProcessAccessor.Instance.rpdTiming.AtkTargetEnemy = enemy.gameObject;
        ProcessAccessor.Instance.rpdTiming.StandbyCharaSelect = true;
        ProcessAccessor.Instance.rpdTiming.SetRapidButton(true); 
        ProcessAccessor.Instance.rpdTiming.SetTimingText("ラピッド");
        if (rpdCommand!=null)
        {
            rpdCommand.GetCommands().SetActive(true);
        }
        
        //actChara.GetComponent<RpdCommand>

        // ここでラピッドタイミングのマニューバーを敵が発動するかどうか判断
        //enemy.GetComponent<ObjEnemy>().EnemyAI_Rapid(maneuver, actChara);

        // 要if分分け。特殊なコストでなければコストを減少させる
        // 行動値を減少させる
        actChara.NowCount -= maneuver.Cost;
    }

    /// <summary>
    /// 待機をおこなう処理
    /// </summary>
    public void ExeStandby(Doll_blu_Nor chara)
    {
        ManagerAccessor.Instance.battleSystem.DeleteMoveChara(chara.Name);
        chara.NowCount -= 1;
        MyZoomOutObj(actCharaCommand.GetActSelect());
        ManagerAccessor.Instance.battleSystem.BattleExe = true;
    }

}
