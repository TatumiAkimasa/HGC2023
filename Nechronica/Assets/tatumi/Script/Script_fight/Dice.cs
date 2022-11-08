using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class Dice : MonoBehaviour
{
    [System.Serializable]
    private struct RandomInfo
    {
        /// <summary>
        /// 次の乱数に更新するボタン
        /// </summary>
        public Button button;

        /// <summary>
        /// 乱数を表示するテキスト
        /// </summary>
        public Text text;

        /// <summary>
        /// 乱数の種
        /// </summary>
        public uint seed;

        /// <summary>
        /// 乱数生成器
        /// </summary>
        [System.NonSerialized]
        public Unity.Mathematics.Random _random;
    }

    /// <summary>
    /// 乱数情報
    /// </summary>
    [SerializeField] private RandomInfo[] _randomInfo;

    /// <summary>
    /// 初期化
    /// </summary>
    private void Awake()
    {
        for (int i = 0; i < _randomInfo.Length; i++)
        {
            RandomInfo info = _randomInfo[i];

            // 乱数生成器作成
            info._random = new Unity.Mathematics.Random(info.seed);

            // 更新ボタンが押されたとき
            info.button.onClick.AddListener(() =>
            {
                // 独立した乱数生成器から[0, 100)の範囲の値を取得＆表示
                info.text.text = info._random.NextInt(0, 100).ToString();
            });
        }
    }

}
