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

    /// <summary>
    /// キャラが選択されるまで再起する関数
    /// </summary>
    /// <returns></returns>
    public Task<int> CharaSelectStandby()
    {
        Debug.Log("ここ通ってる？");
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
                Debug.Log("キャラ取得したよ");
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

                StandbyChara(clickedGameObject);
                return Task.FromResult(0);
            }
        }
        
        return Task.Run(()=> CharaSelectStandby());
    }

    public void SkillSelectStandby()
    {
        //技を発動した時点でこのメソッドを抜けたい感じある。
        //選択した技コマンドを取得する必要ありか…

        // ここでコマンド出現
        // StandbyChara(clickedGameObject)

        // 右クリックで
        if (Input.GetMouseButtonDown(1) && CharaCamera != null)
        {
            // 全体を表示させるカメラを優先にする。
            CharaCamera.Priority = 0;

            // 複製したプレハブカメラを消す。
            StartCoroutine(DstroyCamera());

            // キャラセレクト大気に戻る
            CharaSelectStandby();
        }
        // else if(技コマンド選択で…
        //{
        //    BattleSystem.BattleProcess(選んだ技コマンド);
        //}
        else
        {
            SkillSelectStandby();
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
            }
        }
    }


    /// <summary>
    /// カメラが近づいてからコマンドを表示するメソッド
    /// </summary>
    /// <param name="charaCommand">クリックされたキャラの子オブジェクト（コマンド）</param>
    /// <returns></returns>
    public IEnumerator MoveStandby(Transform charaCommand)
    {
        while (true)
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
                    //技コマンドもろもろを表示
                    charaCommand.gameObject.SetActive(true);
                }
            }
        }
    }


    /// <summary>
    /// ClickedGameObjectメソッドで呼び出される。クリックされたキャラのコマンドを表示するためのメソッド
    /// </summary>
    /// <param name="move">クリックされたキャラ</param>
    void StandbyChara(GameObject move)
    {
        Transform childCommand;
        childCommand = move.transform.GetChild(ACTION);
        StartCoroutine(MoveStandby(childCommand));
    }

    void Update()
    {
        
    }
}
