using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �V�X�e�������X�N���v�g�ւ̃A�N�Z�X���ȈՉ�����
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


    // �V�X�e�������X�N���v�g�Q��
    public BattleSystem battleSystem;
    public GetClickedGameObject getClickedObj;

}
