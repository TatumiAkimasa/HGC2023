using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DmgCommand : BattleCommand
{
    private void Awake()
    {
        CommandAccessor.Instance.dmgCommands = this;
    }
    private void Start()
    {
        thisChara = this.GetComponent<Doll_blu_Nor>();

        InitCommand(CharaBase.DAMAGE);
    }

    protected override void InitCommand(int timing)
    {
        base.InitCommand(timing);
    }
}
