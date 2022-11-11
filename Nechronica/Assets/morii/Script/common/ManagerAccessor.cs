using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �e�}�l�[�W���[�N���X�ւ̃A�N�Z�X���ȈՉ�����
/// </summary>
public class ManagerAccessor : MonoBehaviour
{
    private static ManagerAccessor instance = null;


    // �}�l�[�W���̎Q��
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
