using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class Dice : MonoBehaviour
{
    [System.Serializable]
    private struct RandomInfo
    {
        /// <summary>
        /// ���̗����ɍX�V����{�^��
        /// </summary>
        public Button button;

        /// <summary>
        /// ������\������e�L�X�g
        /// </summary>
        public Text text;

        /// <summary>
        /// �����̎�
        /// </summary>
        public uint seed;

        /// <summary>
        /// ����������
        /// </summary>
        [System.NonSerialized]
        public Unity.Mathematics.Random _random;
    }

    /// <summary>
    /// �������
    /// </summary>
    [SerializeField] private RandomInfo[] _randomInfo;

    /// <summary>
    /// ������
    /// </summary>
    private void Awake()
    {
        for (int i = 0; i < _randomInfo.Length; i++)
        {
            RandomInfo info = _randomInfo[i];

            // ����������쐬
            info._random = new Unity.Mathematics.Random(info.seed);

            // �X�V�{�^���������ꂽ�Ƃ�
            info.button.onClick.AddListener(() =>
            {
                // �Ɨ��������������킩��[0, 100)�͈̔͂̒l���擾���\��
                info.text.text = info._random.NextInt(0, 100).ToString();
            });
        }
    }

}
