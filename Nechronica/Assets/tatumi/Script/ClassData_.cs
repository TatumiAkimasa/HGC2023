using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassData_ : MonoBehaviour
{
    public class ClassData
    {
        public string name;
        public int[] parts = new int[3];
    }

    public string Classname_JtoE(string a)
    {
        if (a == "�X�e�[�V�[")
        {
            return "Stacy";

        }
        else if (a == "�^�i�g�X")
        {
            return "Thanatos";

        }
        else if (a == "�S�V�b�N")
        {
            return "Gothic";

        }
        else if (a == "���N�C�G��")
        {
            return "Requiem";

        }
        else if (a == "�o���b�N")
        {
            return "Baroque";

        }
        else if (a == "���}�l�X�N")
        {
            return "Romanesque";

        }
        else if (a == "�T�C�P�f���b�N")
        {
            return "Psychedelic";
        }
        else
            return "error";
    }
}
