//include = using
using UnityEngine;
using UnityEngine.UI;

//����:�p����
//�܂��A�N���X���ƃt�@�C�����������ĂȂ��ƃG���[���o��B
public class MOve_chara : MonoBehaviour
{
    public float speed,Pjump; // ��������
  
    private Rigidbody rb; // Rididbody
   
    //������
    void Start()
    {
        // Rigidbody ���擾
        rb = GetComponent<Rigidbody>();

       
    }

    //�A�N�V�����i������s�j
    void Update()
    {
        // �J�[�\���L�[�̓��͂��擾
        var moveHorizontal = Input.GetAxis("Horizontal");
        var moveVertical = Input.GetAxis("Vertical");

        //var=�ϐ�(�^�͓��ɂȂ�)�Ƃ�����

        // �J�[�\���L�[�̓��͂ɍ��킹�Ĉړ�������ݒ�
        var movement = new Vector3(moveHorizontal, 0, moveVertical);

        // Ridigbody �ɗ͂�^���ċʂ𓮂����i���́j
        rb.AddForce(movement * speed);

        if(Input.GetKey(KeyCode.Space))
        {
            // �J�[�\���L�[�̓��͂ɍ��킹�Ĉړ�������ݒ�
            var jump = new Vector3(0, 1, 0);

            // Ridigbody �ɗ͂�^���ċʂ𓮂����i���́j
            rb.AddForce(jump * Pjump);
        }

    }

   
}