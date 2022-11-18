using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class GetClickedGameObject : MonoBehaviour
{
    // 定数-----------------------------------------------------
    public const int CANVAS = 0;
    public const int CharaPriority = 20;       // シネマカメラの優先度用定数。キャラをズームする用のカメラの優先度を最優先にする
    public const int ACTION = 0;               // 子オブジェクト取得のための定数
    public const int JUDGE  = 1;               // 子オブジェクト取得のための定数
    public const int DAMAGE = 2;               // 子オブジェクト取得のための定数
    public const int STATUS = 0;               // 敵のステータスを取得するための定数
    public const int BUTTONS = 1;              // 敵のボタンを取得するための定数
    public const int ATKBUTTONS = 0;           // アタックボタンとかの子オブジェクトを取得するための定数
    public const int DICEROLL = 1;             // ダイスロールボタンを取得するための定数
    //----------------------------------------------------------

    protected CinemachineVirtualCamera CharaCamera;   // キャラに持たせるプレハブのクローンのカメラ

    protected CinemachineVirtualCamera saveCharaCamera;   // Charaカメラの内容保存用変数

    [SerializeField]
    protected new Camera camera;                      // メインカメラ

    [SerializeField]
    protected CinemachineVirtualCamera cinemaCamera;  // クローン生成元のシネマカメラ

    [SerializeField]
    private CinemachineVirtualCamera MainCamera;    // 全体を映すシネマカメラ

    [SerializeField]
    protected BattleSystem battleSystem;              // バトルシステムとの変数受け渡し用

    [SerializeField]
    protected Transform childCommand;                 // プレイアブルキャラのコマンドオブジェクト

    [SerializeField] protected Button exeButton;
    public Button ExeButton
    {
        get { return exeButton; }
    }

    // ほかスクリプトからも値を変更する変数
    protected bool standbyCharaSelect = false;

    public bool StandbyCharaSelect
    {
        get { return standbyCharaSelect; }
        set { standbyCharaSelect = value; }
    }

    protected bool standbyEnemySelect = false;
    public bool StandbyEnemySelect
    {
        get { return standbyEnemySelect; }
        set { standbyEnemySelect = value; }
    }

    protected bool skillSelected = false;

    public bool SkillSelected
    {
        get { return skillSelected; }
        set { skillSelected = value; }
    }



    protected CharaManeuver dollManeuver;         // 選択されたドールのマニューバ格納用変数
    public void SetManeuver(CharaManeuver set) { dollManeuver = set; }

    protected int targetArea = 0;                   // 選択されたドールの所属エリア格納用変数
    public void SetArea(int set) { targetArea = set; }
    public int GetArea() => targetArea;

    protected bool selectedChara = false;

    //------------------------------------


    private void Awake()
    {
        ManagerAccessor.Instance.getClickedObj = this;
    }

    private void Update()
    {
        // バトル処理ステップ
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
    }

    protected virtual void CharaSelectStandby()
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
                standbyCharaSelect = false;
                // ここでコマンド表示
                StartCoroutine(MoveStandby(clickedObj));
            }
        }
    }

    protected virtual void EnemySelectStandby()
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

    protected virtual void SkillSelectStandby()
    {
        // 右クリックで
        if ((Input.GetMouseButtonDown(1) || skillSelected) && CharaCamera != null)
        {
            // マニューバを選ぶコマンドまで表示されていたらそのコマンドだけ消す
            ZoomOutObj();
            // キャラ選択待機状態にする
            selectedChara = false;
            standbyCharaSelect = true;
        }
    }



    // カメラが完全に離れてから消すためのコルーチン
    protected IEnumerator DstroyCamera()
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
    protected virtual IEnumerator MoveStandby(GameObject move)
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
                
            }
        }
    }

    /// <summary>
    /// クリックしたObjを取得し、返す
    /// </summary>
    /// <returns></returns>
    protected GameObject ShotRay()
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
    protected void ZoomUpObj(GameObject clicked)
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

    protected void ZoomOutObj()
    {
        // 全体を表示させるカメラを優先にする。
        CharaCamera.Priority = 0;
        // コマンドを消す
        childCommand.gameObject.SetActive(false);
        // 複製したプレハブカメラを消す。
        StartCoroutine(DstroyCamera());
    }

    protected void OnClickBack()
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
