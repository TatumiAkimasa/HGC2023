using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Cinemachine;

public class GetClickedGameObject : MonoBehaviour
{
    // 定数-----------------------------------------------------
    const int CharaPriority = 20;       // シネマカメラの優先度用定数。キャラをズームする用のカメラの優先度を最優先にする
    const int ACTION = 0;               // 子オブジェクト取得のための定数
    //----------------------------------------------------------

    [SerializeField]
    private GameObject clickedGameObject;           // クリックしたゲームオブジェクト

    private CinemachineVirtualCamera CharaCamera;   // キャラに持たせるプレハブのクローンのカメラ


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

    private bool selectedChara = false;

    private void Update()
    {
        //左クリックで
        if (Input.GetMouseButtonDown(0) && CharaCamera == null && !selectedChara)
        {
            //Clickした箇所にレイを飛ばす。
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();

            //ヒットしたオブジェクトを取得
            if (Physics.Raycast(ray, out hit))
            {
                clickedGameObject = hit.collider.gameObject;
                Debug.Log(clickedGameObject.name);
            }

            //クリックしたゲームオブジェクトが味方キャラなら
            if (clickedGameObject.CompareTag("AllyChara"))
            {
                CharaSelect();
                // ここでコマンド表示
                StartCoroutine(MoveStandby(clickedGameObject));
            }

            
        }
        else if(selectedChara)
        {
            SkillSelectStandby();

        }
    }

    /// <summary>
    /// キャラが選択された後に近づくメソッド
    /// </summary>
    /// <returns></returns>
    public void CharaSelect()
    {
        
        Debug.Log("どうでしょうか");
        // クリックしたオブジェクトの座標情報を取得
        Vector3 clickedObjPos = clickedGameObject.transform.position;
        // 取得した座標情報から少し離れた位置に座標を調整
        clickedObjPos.z -= 10.0f;
        clickedObjPos.x -= 2.5f;

        // シネマカメラのプレハブを生成しクリックしたオブジェクトを親オブジェクトにする
        CharaCamera = Instantiate(cinemaCamera, clickedObjPos, Quaternion.identity);
        CharaCamera.transform.parent = clickedGameObject.transform;

        // 生成したプレハブのバーチャルカメラがメインカメラになるようプライオリティを設定
        CharaCamera.Priority = CharaPriority;

        selectedChara = true;
        return;
    }

    public void SkillSelectStandby()
    {
        //技を発動した時点でこのメソッドを抜けたい感じある。
        //選択した技コマンドを取得する必要ありか…

        // 右クリックで
        if (Input.GetMouseButtonDown(1) && CharaCamera != null)
        {
            // 全体を表示させるカメラを優先にする。
            CharaCamera.Priority = 0;
            // コマンドを消す
            childCommand.gameObject.SetActive(false);
            // 複製したプレハブカメラを消す。
            StartCoroutine(DstroyCamera());
        }
        // else if(技コマンド選択で…
        //{
        //    DestroyCamera();
        //    battleSystem.BattleProcess(選んだ技コマンド);
        //    battleSystem.rayGuard.SetActive(true);
        //    selsectedChara=false;
        //}

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
                // 必要な情報は取得した後なので初期化しておく
                clickedGameObject = null;
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
                // 選択したキャラのコマンドのオブジェクトを取得
                childCommand = move.transform.GetChild(ACTION);
                //技コマンドもろもろを表示
                childCommand.gameObject.SetActive(true);
            }
        }
    }
}
