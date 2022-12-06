using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessAccessor
{
    private static ProcessAccessor instance = null;

    public static ProcessAccessor Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ProcessAccessor();
            }
            return instance;
        }
    }


    // �V�X�e�������X�N���v�g�Q��
    public ActTimingProcess actTiming;
    public JdgTimingProcess jdgTiming;
    public DmgTimingProcess dmgTiming;
    public MoveActProcess actTimingMove;
}
