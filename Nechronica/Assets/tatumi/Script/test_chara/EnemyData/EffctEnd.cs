using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffctEnd : MonoBehaviour
{
    bool animEnd = false;
    public bool AnimEnd
    {
        get { return animEnd; }
        set { animEnd = value; }
    }

    private void OnParticleSystemStopped()
    {
        animEnd = true;
        print("�p�[�e�B�N���̍Đ����I��������I");
    }
}
