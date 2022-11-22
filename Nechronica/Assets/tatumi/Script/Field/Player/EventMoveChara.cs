using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventMoveChara : MOve_chara
{
    [SerializeField]
    public AnimationStateController myanim;

    public IEnumerator Event_Horimove(int Horizontaltime, int RightorLeft)
    {
        
        //移動秒数分動く
        for (int i = 0; i != Horizontaltime; i++)
        {
            moveDirection = new Vector3(0.5f * RightorLeft, 0.0f, 0.0f);

            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection = moveDirection * speed;
            controller.Move(moveDirection * Time.deltaTime);

            Vector2 animstate = new Vector2(1.0f * RightorLeft,0.0f);

            myanim.setStateEventToAnimator(animstate);
            yield return null;//1フレーム待機
        }

       
        yield return null;//1フレーム待機
    }

    public IEnumerator Event_Vertimove(int Vertical, int UporDown)
    {
        //移動秒数分動く
        for (int i = 0; i != Vertical; i++)
        {
            moveDirection = new Vector3(0.0f, 0.0f, 0.5f * UporDown);

            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection = moveDirection * speed;
            controller.Move(moveDirection * Time.deltaTime);

            Vector2 animstate = new Vector2(1.0f * UporDown, 0.0f);

            myanim.setStateEventToAnimator(animstate);
            yield return null;//1フレーム待機
        }

        yield return null;//1フレーム待機
    }

    public IEnumerator Event_jump(int count, float time)
    {
        //移動秒数分動く
        for (int i = 0; i != count; i++)
        {
            moveDirection.y = jumpSpeed;
            moveDirection.y = moveDirection.y - (gravity * Time.deltaTime);
            controller.Move(moveDirection * Time.deltaTime);
            yield return new WaitForSeconds(time);//time秒待機
        }

        yield return null;//1フレーム待機
    }
}
