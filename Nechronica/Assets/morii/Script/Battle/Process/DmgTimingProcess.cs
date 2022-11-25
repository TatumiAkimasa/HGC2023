using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DmgTimingProcess : GetClickedGameObject
{

    private int giveDamage;
    public int GiveDamage
    {
        get { return giveDamage; }
        set { giveDamage = value; }
    }

    // Update is called once per frame
    void Update()
    {
        if(standbyCharaSelect)
        {
            CharaSelectStandby();
        }
    }

    /// <summary>
    /// カメラが近づいてからコマンドを表示するメソッド
    /// </summary>
    /// <param name="charaCommand">クリックされたキャラの子オブジェクト（コマンド）</param>
    /// <returns></returns>
    override protected IEnumerator MoveStandby(GameObject move)
    {
        for (int i = 0; i < 2; i++)
        {
            //カメラがクリックしたキャラに近づくまで待つ
            if (i == 0)
            {
                yield return new WaitForSeconds(0.75f);
            }
            else
            {
                if (move.CompareTag("AllyChara"))
                {
                    standbyCharaSelect = false;
                    // 選択したキャラのコマンドのオブジェクトを取得
                    childCommand = move.transform.GetChild(CANVAS).transform.GetChild(DAMAGE);
                    // 技コマンドもろもろを表示
                    childCommand.gameObject.SetActive(true);
                }
            }
        }
    }
}
