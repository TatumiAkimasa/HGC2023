using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventMoveChara : MOve_chara
{
    public IEnumerator Event_Horimove(int Horizontaltime, int RightorLeft)
    {
        //移動秒数分動く
        for (int i = 0; i != Horizontaltime; i++)
        {
            moveDirection = new Vector3(1.0f * RightorLeft, 0.0f, 0.0f);

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

    public IEnumerator Event_jump(int count, float time)
    {
        //移動秒数分動く
        for (int i = 0; i != count; i++)
        {
            moveDirection.y = jumpSpeed;
            yield return new WaitForSeconds(time);//time秒待機
        }

        yield return null;//1フレーム待機
    }
}
