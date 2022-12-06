using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * �L�����̌�����Ԃ�ύX����Controller
 */
public class AnimationStateController : MonoBehaviour
{
    Animator animator;

    public Image image;

    public Sprite[] sprites;

    public bool SetEvent;

    void Start()
    {
        // ������
        // �R���g���[�����Z�b�g�����I�u�W�F�N�g�ɕR�t���Ă���
        // Animator���擾����
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
                // �L�[���͂������Animator��state���Z�b�g����
                setStateToAnimator(vector: action.Value);
                return;
            }
        }
        // ���͂���Vector2�C���X�^���X���쐬
        Vector2 vector = new Vector2(
            (int)Input.GetAxis("Horizontal"),
            (int)Input.GetAxis("Vertical"));

        // �L�[���͂������Ă���ꍇ�́A���͂���쐬����Vector2��n��
        // �L�[���͂��Ȃ���� null
        setStateToAnimator(vector: vector != Vector2.zero ? vector : (Vector2?)null);

    }

    /**
     * Animator�ɏ�Ԃ��Z�b�g����
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
     * ����̃L�[�̓��͂�����΃L�[�ɂ��킹��Vector2�C���X�^���X��Ԃ�
     * �Ȃ����null��Ԃ�   
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
