using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassData_ : MonoBehaviour
{
    public struct ClassData 
    {
        
        public string name;
        public int[] parts;
    }

    //�萔
    protected const int ARMAMENT = 0;     //����
    protected  const int VARIANT = 1;      //�ψ�
    protected  const int ALTER = 2;        //����

    //����
    protected const int HEAD = 0;
    protected const int ARM = 1;
    protected const int BODY = 2;
    protected const int LEG = 3;

    //�G���[
    protected const int NAME = 0;     
    protected const int CLASS = 1;      
    protected const int PARTS = 2;        
    protected const int SKILL = 3;        
    protected const int POTITIONSKILL = 4;
    protected const int POTITION = 5;

    protected const int MAXPARTS = 15;

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
