using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCommand : MonoBehaviour
{
    [SerializeField]
    private Doll_blu_Nor thisChara;     // ���g���Q�Ƃ��邽�߂̕ϐ�

    [SerializeField]
    private List<List<CharaManeuver>> thisManeuvers;  // ���g�������Ă���}�j���[�o��ۑ� 

    private void Start()
    {
        for (int i = 0; i < thisChara.GetManeuver().Count; i++)
        {
            thisManeuvers.Add(null);
        }

        thisManeuvers = thisChara.GetManeuver();
    }

    public void OnClickBattle()
    {

    }

    public void OnClickStandby()
    {

    }

    public void OnClickAction()
    {

    }

    public void OnClickRapid()
    {

    }

    public void OnClickMove()
    {

    }
}
