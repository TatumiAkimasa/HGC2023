using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RpdCommand : BattleCommand
{
    public GameObject GetCommands()
    {
        return commands;
    }

    private void Awake()
    {
        CommandAccessor.Instance.rpdCommands = this;
    }

    private void Start()
    {
        thisChara = this.GetComponent<Doll_blu_Nor>();

        InitCommand(CharaBase.RAPID);
    }

    protected override void InitCommand(int timing)
    {
        base.InitCommand(timing);
    }
}