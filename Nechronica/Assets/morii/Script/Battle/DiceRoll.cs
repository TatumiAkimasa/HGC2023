using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class DiceRoll : MonoBehaviour
{
    // �Č��\�ȗ����̓�����Ԃ�ێ�����C���X�^���X
    private Unity.Mathematics.Random randoms;

    //test�p
    [SerializeField, Header("Debug�p")] private Text text;

    public void NCRoll()
    {
        randoms = new Unity.Mathematics.Random((uint)Random.Range(0, 468446876));
        text.text = randoms.NextInt(1, 10).ToString();
    }

    //Seed�l����x���߂��ȏ㖳���ɓr���ύX����Ǝ��ʂ̂ŕ�����邱��
    //public void SeedChange()
    //{
    //    _random1 = new Unity.Mathematics.Random(_seed2);
    //}
}