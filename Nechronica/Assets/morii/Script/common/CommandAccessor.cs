using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandAccessor
{
    private static CommandAccessor instance = null;

    public static CommandAccessor Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new CommandAccessor();
            }
            return instance;
        }
    }


    // �V�X�e�������X�N���v�g�Q��
    public ActCommands actCommands;
    public RpdCommand rpdCommands;
    public JdgCommand jdgCommands;
    public DmgCommand dmgCommands;
}
