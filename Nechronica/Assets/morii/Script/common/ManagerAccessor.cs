using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 各マネージャークラスへのアクセスを簡易化する
/// </summary>
public class ManagerAccessor : MonoBehaviour
{
    private static ManagerAccessor instance = null;


    // マネージャの参照
    public BattleSystem battleSystem;
    public GetClickedGameObject getClickedObj;

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

}
