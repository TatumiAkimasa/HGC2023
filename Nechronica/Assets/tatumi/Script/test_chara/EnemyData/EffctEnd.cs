using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffctEnd : MonoBehaviour
{
    private void OnParticleSystemStopped()
    {
        Debug.Log("パーティクルの再生が終了したよ！");
    }
}
