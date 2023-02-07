using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePartsSelect : BattleCommand
{
    [SerializeField] int partsSelect;

    private void Start()
    {
        thisChara = this.GetComponent<Doll_blu_Nor>();

        InitParts(partsSelect);
    }

    protected override void InitParts(int parts)
    {
        base.InitParts(parts);
    }
}
