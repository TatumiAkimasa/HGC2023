using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActCommands : BattleCommand
{
    private void Awake()
    {
        CommandAccessor.Instance.actCommands = this;
    }

    private void Start()
    {
        thisChara = this.GetComponent<Doll_blu_Nor>();

        InitCommand(CharaBase.ACTION);
    }

    protected override void InitCommand(int timing)
    {

        // パーツのマニューバとしての割り当てられているタイミングで分ける
        // MOVEタイミングのマニューバもアクションタイミングなので先にマニューバに追加
        AddManeuver(thisChara.HeadParts, CharaBase.MOVE);
        AddManeuver(thisChara.ArmParts, CharaBase.MOVE);
        AddManeuver(thisChara.BodyParts, CharaBase.MOVE);
        AddManeuver(thisChara.LegParts, CharaBase.MOVE);

        base.InitCommand(timing);
    }
}
