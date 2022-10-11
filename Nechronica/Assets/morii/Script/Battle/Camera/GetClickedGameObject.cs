using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GetClickedGameObject : MonoBehaviour
{
    // 定数-----------------------------------------------------
    const int CharaPriority = 20;       //シネマカメラの優先度用定数。キャラをズームする用のカメラの優先度を最優先にする
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

    /// <summary>
    /// キャラが選択されるまで再起する関数
    /// </summary>
    /// <returns></returns>
    public void CharaSelectStandby()
    {
        //左クリックで
        if (Input.GetMouseButtonDown(0) && CharaCamera == null)
        {
            //下記変数を初期化
            clickedGameObject = null;

            //Clickした箇所にレイを飛ばす。
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();

            //ヒットしたオブジェクトを取得
            if (Physics.Raycast(ray, out hit))
            {
                clickedGameObject = hit.collider.gameObject;
            }

            //クリックしたゲームオブジェクトがプレイアブルキャラなら
            if (clickedGameObject.CompareTag("PlayableChara"))
            {
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
            }
        }
        // 右クリックで
        else if (Input.GetMouseButtonDown(1) && CharaCamera != null)
        {
            // 全体を表示させるカメラを優先にする。
            CharaCamera.Priority = 0;

            // 複製したプレハブカメラを消す。
            StartCoroutine(DstroyCamera());
        }
        else
        {
            CharaSelectStandby();
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
                }
            }
        }
    }
    void Update()
    {
        
    }
}
