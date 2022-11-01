using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GetClickedGameObject : MonoBehaviour
{
    // 定数-----------------------------------------------------
    const int CharaPriority = 20;       // シネマカメラの優先度用定数。キャラをズームする用のカメラの優先度を最優先にする
    const int ACTION = 0;               // 子オブジェクト取得のための定数
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

    private CharaManeuver dollManeuver;         // 選択されたドールのマニューバ格納用変数
    public void SetManeuver(CharaManeuver set) { dollManeuver = set; }

    private int dollArea = 0;                   // 選択されたドールの所属エリア格納用変数
    public void SetArea(int set) { dollArea = set; }

    //------------------------------------

    private bool selectedChara = false;

    private void Update()
    {
        if(selectedChara)
        {
            SkillSelectStandby();
        }
        else if(standbyCharaSelect)
        {
            CharaSelectStandby();
        }

        if(standbyEnemySelect)
        {
            EnemySelectStandby();
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
                // 敵キャラのエリアと選択されたマニューバの射程を絶対値で比べて、
                if (Mathf.Abs(dollArea - clickedObj.GetComponent<Doll_blu_Nor>().potition) <= Mathf.Abs(dollManeuver.MaxRange-dollManeuver.MinRange))
                {
                    ZoomUpObj(clickedObj);
                    // ここでコマンド表示
                    StartCoroutine(MoveStandby(clickedObj));
                }
            }
        }
    }

    public void SkillSelectStandby()
    {
        // 右クリックで
        if ((Input.GetMouseButtonDown(1) || skillSelected) && CharaCamera != null)
        {
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
                    // 選択したキャラのコマンドのオブジェクトを取得
                    childCommand = move.transform.GetChild(ACTION).transform.GetChild(ACTION);
                    // 技コマンドもろもろを表示
                    childCommand.gameObject.SetActive(true);
                }
                else if(move.CompareTag("EnemyChara"))
                {
                    // ここで 攻撃する～みたいなコマンドを表示
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
}
