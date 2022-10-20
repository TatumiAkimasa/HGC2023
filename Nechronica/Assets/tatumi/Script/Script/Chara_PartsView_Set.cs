using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Chara_PartsView_Set : CharaBase
{
    public string Timing_Set(int timing)
    {
        if(timing==AUTO)
        {
            return "�I�[�g";
        }
        else if(timing == ACTION)
        {
            return "�A�N�V����";
        }
        else if (timing == RAPID)
        {
            return "���s�b�h";
        }
        else if (timing == JUDGE)
        {
            return "�W���b�W";
        }
        else if (timing == DAMAGE)
        {
            return "�_���[�W";
        }

        return "ERROR:"+timing.ToString();
    }
    
}
