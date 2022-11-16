using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class GetClickedGameObject : MonoBehaviour
{
    // 定数-----------------------------------------------------
    const int CANVAS = 0;

    const int CharaPriority = 20;       // シネマカメラの優先度用定数。キャラをズームする用のカメラの優先度を最優先にする
    const int ACTION = 0;               // 子オブジェクト取得のための定数
    const int JUDGE  = 1;               // 子オブジェクト取得のための定数
    const int DAMAGE = 2;               // 子オブジェクト取得のための定数

    const int STATUS = 0;               // 敵のステータスを取得するための定数
    const int BUTTONS = 1;              // 敵のボタンを取得するための定数

    const int ATKBUTTONS = 0;           // アタックボタンとかの子オブジェクトを取得するための定数
    const int DICEROLL = 1;             // ダイスロールボタンを取得するための定数
    //----------------------------------------------------------

    private CinemachineVirtualCamera CharaCamera;   // キャラに持たせるプレハブのクローンのカメラ

    private CinemachineVirtualCamera saveCharaCamera;   // Charaカメラの内容保存用変数

    [SerializeField]
    private new Camera camera;                      // メインカメラ

    [SerializeField]
    private CinemachineVirtualCamera cinemaCamera;  // クローン生成元のシネマカメラ

    [SerializeField]
    private CinemachineVirtualCamera MainCamera;    // 全体を映すシネマカメラ

    [SerializeField]
    private BattleSystem battleSystem;              // バトルシステムとの変数受け渡し用

    [SerializeField]
    private Transform childCommand;                 // プレイアブルキャラのコマンドオブジェクト

    private GameObject atkTargetEnemy;              // 攻撃する敵オブジェクトを格納場所

    private Unity.Mathematics.Random randoms;       // 再現可能な乱数の内部状態を保持するインスタンス

    private int rollResult = 0;                     // ダイスロールの結果を格納する変数

    [SerializeField]
    private Button atkButton;
    [SerializeField]
    private Button backButton;

    // ほかスクリプトからも値を変更する変数
    private bool standbyCharaSelect = false;

    public bool StandbyCharaSelect
    {
        get { return standbyCharaSelect; }
        set { standbyCharaSelect = value; }
    }

    private bool standbyEnemySelect = false;
    public bool StandbyEnemySelect
    {
        get { return standbyEnemySelect; }
        set { standbyEnemySelect = value; }
    }

    private bool skillSelected = false;

    public bool SkillSelected
    {
        get { return skillSelected; }
        set { skillSelected = value; }
    }

    private bool isDiceRoll;
    private bool isStandbyDiceRoll;
    private bool isJudgeTiming=false;


    private CharaManeuver dollManeuver;         // 選択されたドールのマニューバ格納用変数
    public void SetManeuver(CharaManeuver set) { dollManeuver = set; }

    private int dollArea = 0;                   // 選択されたドールの所属エリア格納用変数
    public void SetArea(int set) { dollArea = set; }
    public int GetArea() => dollArea;

    [SerializeField]
    private Text text;

    //------------------------------------

    private bool selectedChara = false;

    private void Awake()
    {
        ManagerAccessor.Instance.getClickedObj = this;
        backButton.onClick.AddListener(OnClickBack);
    }

    private void Update()
    {
        if(selectedChara)
        {
            SkillSelectStandby();
        }
        else if (standbyEnemySelect)
        {
            EnemySelectStandby();
        }
        else if(standbyCharaSelect)
        {
            CharaSelectStandby();
        }
        else if(isStandbyDiceRoll)
        {
            if(isDiceRoll)
            {
                isStandbyDiceRoll = false;
                isDiceRoll = false;

                // ここからジャッジタイミング
                standbyCharaSelect = true;
                isJudgeTiming = true;
            }
        }
    }

    void CharaSelectStandby()
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

    public void EnemySelectStandby()
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

    public void SkillSelectStandby()
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



    // カメラが完全に離れてから消すためのコルーチン
    IEnumerator DstroyCamera()
    {
        for (int i = 0; i < 2; i++)
        {
            if (i == 0)
            {
                yield return new WaitForSeconds(0.75f);
            }
            else
            {
                Destroy(CharaCamera.gameObject);
                //priorityを元の数値にする
                cinemaCamera.Priority = 10;
                selectedChara = false;
            }
        }
    }


    /// <summary>
    /// カメラが近づいてからコマンドを表示するメソッド
    /// </summary>
    /// <param name="charaCommand">クリックされたキャラの子オブジェクト（コマンド）</param>
    /// <returns></returns>
    public IEnumerator MoveStandby(GameObject move)
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
                if(move.CompareTag("AllyChara"))
                {
                    if(isJudgeTiming)
                    {
                        // 選択したキャラのコマンドのオブジェクトを取得
                        childCommand = move.transform.GetChild(CANVAS).transform.GetChild(JUDGE);
                        // 技コマンドもろもろを表示
                        childCommand.gameObject.SetActive(true);
                    }
                    else
                    {
                        // 選択したキャラのコマンドのオブジェクトを取得
                        childCommand = move.transform.GetChild(CANVAS).transform.GetChild(ACTION);
                        // 技コマンドもろもろを表示
                        childCommand.gameObject.SetActive(true);
                    }
                }
                else if(move.CompareTag("EnemyChara"))
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
                        atkTargetEnemy = move;
                        atkTargetEnemy.transform.GetChild(CANVAS).gameObject.SetActive(true);

                        atkButton.onClick.AddListener(() => OnClickAtk(move.GetComponent<Doll_blu_Nor>()));

                        this.transform.GetChild(CANVAS).transform.GetChild(ATKBUTTONS).gameObject.SetActive(true);
                    }
                    
                    // 攻撃する～を選んだら、selectedChara、standbyCharaSelectをfalseにし、ダイスロールへ
                    // ダイスロール後、マニューバ、敵Obj(move)、ダイスの値を引数とし、ダメージタイミングへ
                }
            }
        }
    }

    /// <summary>
    /// クリックしたObjを取得し、返す
    /// </summary>
    /// <returns></returns>
    GameObject ShotRay()
    {
        //Clickした箇所にレイを飛ばす。
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();

        //ヒットしたオブジェクトを取得
        if (Physics.Raycast(ray, out hit))
        {
            return hit.collider.gameObject;
        }

        return null;
    }

    /// <summary>
    /// キャラが選択された後、カメラが選択されたキャラに近づくメソッド
    /// </summary>
    /// <param name="clicked"></param>
    void ZoomUpObj(GameObject clicked)
    {
        // クリックしたオブジェクトの座標情報を取得
        Vector3 clickedObjPos = clicked.transform.position;
        // 取得した座標情報から少し離れた位置に座標を調整
        clickedObjPos.z -= 10.0f;
        clickedObjPos.x -= 2.5f;

        // シネマカメラのプレハブを生成しクリックしたオブジェクトを親オブジェクトにする
        CharaCamera = Instantiate(cinemaCamera, clickedObjPos, Quaternion.identity, clicked.transform);

        // 生成したプレハブのバーチャルカメラがメインカメラになるようプライオリティを設定
        CharaCamera.Priority = CharaPriority;
    }

    void ZoomOutObj()
    {
        // 全体を表示させるカメラを優先にする。
        CharaCamera.Priority = 0;
        // コマンドを消す
        childCommand.gameObject.SetActive(false);
        // 複製したプレハブカメラを消す。
        StartCoroutine(DstroyCamera());
    }

    void OnClickBack()
    {
        // childCommandの中身があるということはコマンドが表示されている状態なので、それを非表示にし、childCommandの中身をなくす
        if (childCommand != null)
        {
            childCommand.gameObject.SetActive(false);
            childCommand = null;
        }
        else
        {
            // カメラを元の位置に戻し、UIを消す
            ZoomOutObj();
            this.transform.GetChild(CANVAS).gameObject.SetActive(false);
        }
       
    }

    void OnClickAtk(Doll_blu_Nor enemy)
    {
        // カメラを元の位置に戻し、UIを消す
        ZoomOutObj();
        enemy.transform.GetChild(CANVAS).gameObject.SetActive(false);
        this.transform.GetChild(CANVAS).transform.GetChild(ATKBUTTONS).gameObject.SetActive(false);
        this.transform.GetChild(CANVAS).transform.GetChild(DICEROLL).gameObject.SetActive(true);

        selectedChara = false;
        standbyEnemySelect = false;
        standbyCharaSelect = false;

        // ここジャッジから入る
        isStandbyDiceRoll = true;

        battleSystem.DamageTiming(dollManeuver, enemy);
    }

    public void OnClickDiceRoll()
    {
        randoms = new Unity.Mathematics.Random((uint)Random.Range(0, 468446876));
        rollResult = randoms.NextInt(1, 10);
        text.text = rollResult.ToString();
        isDiceRoll = true;
    }

    public int JudgeTiming()
    {
        // 敵の介入（？）

        // 味方選択

        // 味方のジャッジ発動

        // ダイス結果に値を加算

        // もう一度同じ処理へ

        // passを押したら終了


        return 0;
    }
}
