using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "ScriptTable", menuName = "CreatePartsData")]//  Create����CreateShelter�Ƃ������j���[��\�����AShelter���쐬����
public class Table_Parts : ScriptableObject
{
    public enum effstring
    {
        Damage,
        Guard,
        Judge,
        Move, 
        Count,
        Insanit, // ���C�_���֗^������̂͂��������
        Extra,

        // �I�����[�����̌���
        Protect,    // ���΂��̌��ʂ͂���ŔF��
        YobunnnaUde,    // �]���Șr�A���̎�͂���ŔF��
        Nikunotate,   // ���̏��͂���ŔF��
    }


    [System.Serializable]
    public struct EffctNum_
    {
        public effstring Eff_String;
        public int Eff_int;
    }

    [SerializeField, Header("EeffctNum�ݒ�")]
    public List<EffctNum_> effcrnums;

    [SerializeField, Header("�p�[�c�f�[�^")]
    public CharaManeuver Maneuver;

    public Table_Parts(Table_Parts value)
    {
        
        this.Maneuver = value.Maneuver;

    }


}
