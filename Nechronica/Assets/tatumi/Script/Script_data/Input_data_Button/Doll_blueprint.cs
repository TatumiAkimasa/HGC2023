using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharaManeuver))]
[System.Serializable]
public class Doll_blueprint : CharaBase
{
    public string Name;                    //�h�[���� 
    public string hide_hint;              //�Î�
    public string Death_year;              //���N
    public string temper;                  //�|�W�V����
    public short[] Memory;                 //�L���̂�����
    public string MainClass, SubClass;     //�E��
    public short Armament, Variant, Alter; //����,�ψ�,����
    public short potition;                 //�����z�u
    //---------------------------------------------------������������
  
}


