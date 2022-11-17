
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MOve_chara : MonoBehaviour
{
    public float speed = 10.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 320.0f;

    private Vector3 moveDirection = Vector3.zero;
  
    private CharacterController controller;

    private float my_y;

    [SerializeField]
    GameObject Menu;

    [SerializeField]
    GameObject mission;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection = moveDirection * speed;

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }
        else
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal") * speed, my_y, Input.GetAxis("Vertical") * speed);
            
        }

        moveDirection.y = moveDirection.y - (gravity * Time.deltaTime);
        my_y = moveDirection.y;
        controller.Move(moveDirection * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.M))
        {
            Menu.SetActive(true);
            mission.SetActive(false);
        }
    }

    public IEnumerator Event_Horimove(int Horizontaltime,int RightorLeft)
    {
        //移動秒数分動く
        for(int i=0;i!= Horizontaltime;i++)
        {
            moveDirection = new Vector3(1.0f*RightorLeft, 0.0f, 0.0f);

            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection = moveDirection * speed;
            yield return null;//1フレーム待機
        }

        yield return null;//1フレーム待機
    }

    public IEnumerator Event_Vertimove(int Vertical, int UporDown)
    {
        //移動秒数分動く
        for (int i = 0; i != Vertical; i++)
        {
            moveDirection = new Vector3(0.0f, 0.0f, 1.0f * UporDown);

            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection = moveDirection * speed;
            yield return null;//1フレーム待機
        }

        yield return null;//1フレーム待機
    }

    public IEnumerator Event_jump(int count,float time)
    {
        //移動秒数分動く
        for (int i = 0; i != count; i++)
        {
            moveDirection.y = jumpSpeed;
            yield return new WaitForSeconds(time);//0.5秒待機
        }

        yield return null;//1フレーム待機
    }
}