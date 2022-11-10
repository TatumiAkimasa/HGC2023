using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class Dice : MonoBehaviour
{
    // �����̕����V�[�h�l
    [SerializeField] private uint[] _seeds;

    // �Č��\�ȗ����̓�����Ԃ�ێ�����C���X�^���X
    private Unity.Mathematics.Random[] _randoms;
   
    //test�p
    [SerializeField,Header("Debug�p")] private Text text;

    private void Start()
    {
        if (_seeds.Length != _randoms.Length)
            Debug.LogError("�����_���̗v�f������v���Ă܂���");
        else
        {
            for (int i = 0; i != _randoms.Length; i++)
            {
                // �Č��\�ȗ�����������
                _randoms[i] = new Unity.Mathematics.Random(_seeds[i]);
            }
        }
       
    }

    //1~10�̃����_���Ȃ�������Ԃ�
    public int DiceRoll(int number)
    {
        return _randoms[number].NextInt(1, 10);
    }

    //Seed�l����x���߂��ȏ㖳���ɓr���ύX����Ǝ��ʂ̂ŕ�����邱��
    //public void SeedChange()
    //{
    //    _random1 = new Unity.Mathematics.Random(_seed2);
    //}
}
