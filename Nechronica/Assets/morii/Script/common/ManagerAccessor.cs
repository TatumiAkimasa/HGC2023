using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// システム根幹スクリプトへのアクセスを簡易化する
/// </summary>
public class ManagerAccessor
{
    private static ManagerAccessor instance = null;


    public static ManagerAccessor Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ManagerAccessor();
            }
            return instance;
        }
    }


    // システム根幹スクリプト参照
    public BattleSystem battleSystem;
    public GetClickedGameObject getClickedObj;

}
