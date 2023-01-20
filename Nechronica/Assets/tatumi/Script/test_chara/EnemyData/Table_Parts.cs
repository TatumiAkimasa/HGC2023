using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "ScriptTable", menuName = "CreatePartsData")]//  Create����CreateShelter�Ƃ������j���[��\�����AShelter���쐬����
public class Table_Parts : ScriptableObject
{
    public enum EffctNumString
    {
        Damage,
        Guard ,
        Judge ,
        Move ,
        Count ,
        Insanity  ,   // ���C�_���֗^������̂͂��������
        Extra ,


        // �I�����[�����̌���
        Protect,  // ���΂��̌��ʂ͂���ŔF��
        YobunnnaUde  ,  // �]���Șr�A���̎�͂���ŔF��
        Nikunotate,  // ���̏��͂���ŔF��
    }




   
    [SerializeField, Header("EeffctNum�ݒ�")]
    public EffctNumString Eff_String;
    public int Eff_int;

    [SerializeField, Header("�p�[�c�f�[�^")]
    public CharaManeuver Maneuver;

    public Table_Parts(Table_Parts value)
    {
        this.Maneuver.EffectNum.Add(value.Eff_String.ToString(),value.Eff_int);
        this.Maneuver = value.Maneuver;
    }
}
