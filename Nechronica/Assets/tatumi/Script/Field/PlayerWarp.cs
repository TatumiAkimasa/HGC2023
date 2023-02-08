using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWarp : CheckUp_Base
{
    public Transform WarpZone;
    void Update()
    {
        if (PL != null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {//　CharacterControllerコンポーネントを一旦無効化する
                PL.GetComponent<CharacterController>().enabled = false;
                //　キャラクターの位置を変更する
                PL.transform.position = WarpZone.position;
                //　CharacterControllerコンポーネントを有効化する
                PL.GetComponent<CharacterController>().enabled = true;
                PL = null;
            }
        }
    }
}
