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
    public const int RAPID  = 1;
    public const int JUDGE  = 2;               // 子オブジェクト取得のための定数
    public const int DAMAGE = 3;               // 子オブジェクト取得のための定数
    public const int STATUS = 0;               // 敵のステータスを取得するための定数
    public const int BUTTONS = 1;              // 敵のボタンを取得するための定数
    public const int ATKBUTTONS = 1;           // アタックボタンとかの子オブジェクトを取得するための定数
    public const int DICEROLL = 1;             // ダイスロールボタンを取得するための定数
    //----------------------------------------------------------

    protected CinemachineVirtualCamera CharaCamera;   // キャラに持たせるプレハブのクローンのカメラ

    protected CinemachineVirtualCamera saveCharaCamera;   // Charaカメラの内容保存用変数

    [SerializeField] protected new Camera camera;                      // メインカメラ
    [SerializeField] protected CinemachineVirtualCamera cinemaCamera;  // クローン生成元のシネマカメラ
    [SerializeField] private CinemachineVirtualCamera MainCamera;    // 全体を映すシネマカメラ
    [SerializeField] protected BattleSystem battleSystem;              // バトルシステムとの変数受け渡し用
    [SerializeField] protected Transform childCommand;                 // プレイアブルキャラのコマンドオブジェクト

    [SerializeField] protected Text timingText;                 // プレイアブルキャラのコマンドオブジェクト
    public void SetTimingText(string set) { timingText.text = set; }
    
    // ほかスクリプトからも値を変更する変数
    protected bool isStandbyCharaSelect = false;

    public bool StandbyCharaSelect
    {
        get { return isStandbyCharaSelect; }
        set { isStandbyCharaSelect = value; }
    }

    protected bool isStandbyEnemySelect = false;
    public bool StandbyEnemySelect
    {
        get { return isStandbyEnemySelect; }
        set { isStandbyEnemySelect = value; }
    }

    protected bool skillSelected = false;

    public bool SkillSelected
    {
        get { return skillSelected; }
        set { skillSelected = value; }
    }
    [SerializeField]
    protected Doll_blu_Nor actingChara;                                   // 攻撃などの行動をしようとしているキャラ
    public void SetActChara(Doll_blu_Nor doll) => actingChara = doll;
    public Doll_blu_Nor ActingChara
    {
        set { actingChara = value; }
        get { return actingChara; }
    }


    protected CharaManeuver dollManeuver;         // 選択されたドールのマニューバ格納用変数
    public void SetManeuver(CharaManeuver set) { dollManeuver = set; }

    protected int movingArea = 0;                   // 選択されたドールの所属エリア格納用変数
    public void SetArea(int set) { movingArea = set; }
    public int GetArea() => movingArea;

    protected bool isSelectedChara = false;

    //------------------------------------


    private void Awake()
    {
        ManagerAccessor.Instance.getClickedObj = this;
    }

    private void Update()
    {
        // バトル処理ステップ
        if(isSelectedChara)
        {
            SkillSelectStandby();
        }
        else if (isStandbyEnemySelect)
        {
            EnemySelectStandby();
        }
        else if(isStandbyCharaSelect)
        {
            CharaSelectStandby();
        }
    }

    protected virtual void CharaSelectStandby()
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
                isStandbyCharaSelect = false;
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
            isSelectedChara = false;
            isStandbyCharaSelect = true;
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
                if(CharaCamera!=null)
                {
                    Destroy(CharaCamera.gameObject);
                }
                //priorityを元の数値にする
                cinemaCamera.Priority = 10;
                isSelectedChara = false;
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
    protected virtual void ZoomUpObj(GameObject clicked)
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

    /// <summary>
    /// 戻るを押したらカメラが離れていく処理
    /// </summary>
    protected virtual void ZoomOutObj()
    {
        // 全体を表示させるカメラを優先にする。
        CharaCamera.Priority = 0;
        // コマンドを消す
        //childCommand.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        if(childCommand!=null)
        {
            childCommand.gameObject.SetActive(false);
        }
        // 複製したプレハブカメラを消す。
        StartCoroutine(DstroyCamera());
    }

    /// <summary>
    /// マニューバを使用したキャラクターの射程内に選択されたキャラがいるかどうか
    /// </summary>
    /// <param name="targetChara">マニューバのターゲットに選択されたキャラ</param>
    /// <param name="maneuver">マニューバの情報</param>
    /// <param name="exeChara">マニューバを使用したキャラ</param>
    /// <returns>射程内であればtrueで返す</returns>
    public bool RangeCheck(Doll_blu_Nor targetChara, CharaManeuver maneuver, Doll_blu_Nor exeChara)
    {
        if(maneuver.Timing==CharaBase.ACTION)
        {
            if ((Mathf.Abs(targetChara.area) <= Mathf.Abs(maneuver.MaxRange + exeChara.area) &&
                 Mathf.Abs(targetChara.area) >= Mathf.Abs(maneuver.MinRange + exeChara.area)) &&
               !maneuver.isDmage)
            {
                return true;
            }
        }
        else
        {
            if ((Mathf.Abs(targetChara.area) <= Mathf.Abs(maneuver.MaxRange + exeChara.area) &&
                 Mathf.Abs(targetChara.area) >= Mathf.Abs(maneuver.MinRange + exeChara.area)) &&
               (!maneuver.isUse && !maneuver.isDmage))
            {
                return true;
            }
        }

        return false;
    }
}
