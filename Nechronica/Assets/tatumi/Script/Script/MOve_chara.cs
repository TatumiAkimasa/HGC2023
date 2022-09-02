
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
    }

  
}