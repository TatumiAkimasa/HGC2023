using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCommand : MonoBehaviour
{
    [SerializeField]
    private Doll_blu_Nor thisChara;     // 自身を参照するための変数

    [SerializeField]
    private List<List<CharaManeuver>> thisManeuvers;  // 自身が持っているマニューバを保存 

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
