using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckUp_chara : CheckUp_Base
{
    [SerializeField, Header("�K�v�A�C�e����")]
    protected string Item_Name;

    [SerializeField, Header("�C�x���g�ɒǉ����邩")]
    protected bool EventStart = false;

    [SerializeField, Header("�Ή��C�x���g�ԍ�")]
    protected int Event_num;

    [SerializeField, Header("�������̑䎌")]
    protected string[] Event_OK;
    [SerializeField, Header("���s���̑䎌")]
    protected string[] Event_OUT;

    [SerializeField, Header("��ʏ��UI���g����")]
    protected bool OverStrObj;

    [SerializeField, Header("Event������폜")]
    protected GameObject ParentObj;

    [SerializeField, Header("�ړI�X�V�pUIobj")]
    protected GameObject UI;

    protected Item item_data;

}


