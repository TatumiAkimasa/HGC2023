using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventMoveChara : MOve_chara
{
    public IEnumerator Event_Horimove(int Horizontaltime, int RightorLeft)
    {
        //�ړ��b��������
        for (int i = 0; i != Horizontaltime; i++)
        {
            moveDirection = new Vector3(1.0f * RightorLeft, 0.0f, 0.0f);

            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection = moveDirection * speed;
            yield return null;//1�t���[���ҋ@
        }

        yield return null;//1�t���[���ҋ@
    }

    public IEnumerator Event_Vertimove(int Vertical, int UporDown)
    {
        //�ړ��b��������
        for (int i = 0; i != Vertical; i++)
        {
            moveDirection = new Vector3(0.0f, 0.0f, 1.0f * UporDown);

            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection = moveDirection * speed;
            yield return null;//1�t���[���ҋ@
        }

        yield return null;//1�t���[���ҋ@
    }

    public IEnumerator Event_jump(int count, float time)
    {
        //�ړ��b��������
        for (int i = 0; i != count; i++)
        {
            moveDirection.y = jumpSpeed;
            yield return new WaitForSeconds(time);//time�b�ҋ@
        }

        yield return null;//1�t���[���ҋ@
    }
}
