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

        // �p�[�c�̃}�j���[�o�Ƃ��Ă̊��蓖�Ă��Ă���^�C�~���O�ŕ�����
        // MOVE�^�C�~���O�̃}�j���[�o���A�N�V�����^�C�~���O�Ȃ̂Ő�Ƀ}�j���[�o�ɒǉ�
        AddManeuver(thisChara.HeadParts, CharaBase.MOVE);
        AddManeuver(thisChara.ArmParts, CharaBase.MOVE);
        AddManeuver(thisChara.BodyParts, CharaBase.MOVE);
        AddManeuver(thisChara.LegParts, CharaBase.MOVE);

        base.InitCommand(timing);
    }
}
