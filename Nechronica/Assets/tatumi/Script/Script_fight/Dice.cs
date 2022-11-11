using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class Dice : MonoBehaviour
{
    // 乱数の複数シード値
    [SerializeField] private uint[] _seeds;

    // 再現可能な乱数の内部状態を保持するインスタンス
    private Unity.Mathematics.Random[] _randoms;
   
    //test用
    [SerializeField,Header("Debug用")] private Text text;

    private void Start()
    {
        if (_seeds.Length != _randoms.Length)
            Debug.LogError("ランダムの要素数が一致してません");
        else
        {
            for (int i = 0; i != _randoms.Length; i++)
            {
                // 再現可能な乱数を初期化
                _randoms[i] = new Unity.Mathematics.Random(_seeds[i]);
            }
        }
       
    }

    //1~10のランダムなすうじを返す
    public int DiceRoll(int number)
    {
        return _randoms[number].NextInt(1, 10);
    }

    //Seed値を一度決めた以上無理に途中変更すると死ぬので複数作ること
    //public void SeedChange()
    //{
    //    _random1 = new Unity.Mathematics.Random(_seed2);
    //}
}
