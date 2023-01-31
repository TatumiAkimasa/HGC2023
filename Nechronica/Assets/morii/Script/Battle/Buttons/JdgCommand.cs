using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JdgCommand : BattleCommand
{
    private void Awake()
    {
        CommandAccessor.Instance.jdgCommands = this;
    }

    private void Start()
    {
        thisChara = this.GetComponent<Doll_blu_Nor>();

        InitCommand(CharaBase.JUDGE);
    }

    protected override void InitCommand(int timing)
    {
        base.InitCommand(timing);
    }
}
