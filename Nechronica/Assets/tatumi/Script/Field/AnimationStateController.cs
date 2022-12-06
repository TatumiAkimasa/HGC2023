using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * キャラの向き状態を変更するController
 */
public class AnimationStateController : MonoBehaviour
{
    Animator animator;

    public Image image;

    public Sprite[] sprites;

    public bool SetEvent;

    void Start()
    {
        // 初期化
        // コントローラをセットしたオブジェクトに紐付いている
        // Animatorを取得する
        this.animator = GetComponent<Animator>();
    }


    void Update()
    {
        if (SetEvent)
        {
            this.animator.speed = 0.0f;
            return;
        }

        if (Input.anyKeyDown)
        {
            Vector2? action = this.actionKeyDown();
            if (action.HasValue)
            {
                // キー入力があればAnimatorにstateをセットする
                setStateToAnimator(vector: action.Value);
                return;
            }
        }
        // 入力からVector2インスタンスを作成
        Vector2 vector = new Vector2(
            (int)Input.GetAxis("Horizontal"),
            (int)Input.GetAxis("Vertical"));

        // キー入力が続いている場合は、入力から作成したVector2を渡す
        // キー入力がなければ null
        setStateToAnimator(vector: vector != Vector2.zero ? vector : (Vector2?)null);

    }

    /**
     * Animatorに状態をセットする
     *    
     */
    private void setStateToAnimator(Vector2? vector)
    {
        if (!Input.GetButton("Horizontal"))
        {
            if (!Input.GetButton("Vertical"))
            {
                this.animator.speed = 0.0f;
                return;
            }
        }

        if (!vector.HasValue)
        {
            return;
        }
       
        this.animator.speed = 1.0f;

      
        this.animator.SetFloat("X", vector.Value.x);
        this.animator.SetFloat("Y", vector.Value.y);

    }

    public void setStateEventToAnimator(Vector2? vector)
    {
        this.animator.speed = 1.0f;
        this.animator.SetFloat("X", vector.Value.x);
        this.animator.SetFloat("Y", vector.Value.y);
    }

    /**
     * 特定のキーの入力があればキーにあわせたVector2インスタンスを返す
     * なければnullを返す   
     */
    private Vector2? actionKeyDown()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow)) return Vector2.up;
        if (Input.GetKeyDown(KeyCode.LeftArrow)) return Vector2.left;
        if (Input.GetKeyDown(KeyCode.DownArrow)) return Vector2.down;
        if (Input.GetKeyDown(KeyCode.RightArrow)) return Vector2.right;
        return null;
    }

    public void Change_Sprite(int number)
    {
        image.sprite = sprites[number];
    }
}
