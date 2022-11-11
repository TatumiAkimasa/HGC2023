using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class DiceRoll : MonoBehaviour
{
    // 再現可能な乱数の内部状態を保持するインスタンス
    private Unity.Mathematics.Random randoms;

    //test用
    [SerializeField, Header("Debug用")] private Text text;

    public void NCRoll()
    {
        randoms = new Unity.Mathematics.Random((uint)Random.Range(0, 468446876));
        text.text = randoms.NextInt(1, 10).ToString();
    }

    //Seed値を一度決めた以上無理に途中変更すると死ぬので複数作ること
    //public void SeedChange()
    //{
    //    _random1 = new Unity.Mathematics.Random(_seed2);
    //}
}