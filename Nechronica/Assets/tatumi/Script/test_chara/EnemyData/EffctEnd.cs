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

        print("パーティクルの再生が終了したよ！");
    }
}
