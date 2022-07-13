using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GetClickedGameObject : MonoBehaviour
{
    [SerializeField]
    private GameObject clickedGameObject;

    private CinemachineVirtualCamera CharaCamera;   //

    [SerializeField]
    private new Camera camera;                      // メインカメラ

    [SerializeField]
    private CinemachineVirtualCamera cinemaCamera;  // クローン生成元のシネマカメラ

    [SerializeField]
    private CinemachineVirtualCamera MainCamera;    // 全体を映すシネマカメラ


    void Update()
    {
        //左クリックで
        if (Input.GetMouseButtonDown(0) && CharaCamera==null)
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

            if(clickedGameObject.CompareTag("PlayableChara"))
            {
                // クリックしたオブジェクトの座標情報を取得
                Vector3 clickedObjPos = clickedGameObject.transform.position;
                // 取得した座標情報から少し離れた位置に座標を変更
                clickedObjPos.z -= 10.0f;
                clickedObjPos.x -= 2.5f;

                // シネマカメラのプレハブを生成しクリックしたオブジェクトを親オブジェクトにする
                CharaCamera = Instantiate(cinemaCamera, clickedObjPos, Quaternion.identity);
                CharaCamera.transform.parent = clickedGameObject.transform;

                // 生成したプレハブカメラのプライオリティをメインカメラに設定
                CharaCamera.Priority = MainCamera.Priority;
            }
        }
        // 右クリックで
        else if(Input.GetMouseButtonDown(1) && CharaCamera != null)
        {
            // 
            cinemaCamera.Priority = MainCamera.Priority;

            StartCoroutine(DstroyCamera());
        }

        IEnumerator DstroyCamera()
        {
            for(int i=0;i<2;i++)
            {
                if(i==0)
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
}
