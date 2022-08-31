//include = using
using UnityEngine;
using UnityEngine.UI;

//○○:継承名
//また、クラス名とファイル名があってないとエラーが出る。
public class MOve_chara : MonoBehaviour
{
    public float speed,Pjump; // 動く速さ
  
    private Rigidbody rb; // Rididbody
   
    //初期化
    void Start()
    {
        // Rigidbody を取得
        rb = GetComponent<Rigidbody>();

       
    }

    //アクション（毎回実行）
    void Update()
    {
        // カーソルキーの入力を取得
        var moveHorizontal = Input.GetAxis("Horizontal");
        var moveVertical = Input.GetAxis("Vertical");

        //var=変数(型は特になし)という意

        // カーソルキーの入力に合わせて移動方向を設定
        var movement = new Vector3(moveHorizontal, 0, moveVertical);

        // Ridigbody に力を与えて玉を動かす（剛体）
        rb.AddForce(movement * speed);

        if(Input.GetKey(KeyCode.Space))
        {
            // カーソルキーの入力に合わせて移動方向を設定
            var jump = new Vector3(0, 1, 0);

            // Ridigbody に力を与えて玉を動かす（剛体）
            rb.AddForce(jump * Pjump);
        }

    }

   
}