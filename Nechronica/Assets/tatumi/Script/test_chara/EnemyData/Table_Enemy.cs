using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MultiWepon
{
    [Header("���g�p�[�c")]
    public List<Table_Parts> Parts;

    public MultiWepon(List<Table_Parts> _multisite)
    {
        Parts = _multisite;
    }
}

[CreateAssetMenu(fileName = "ScriptTable", menuName = "CreateEnemyData")]//  Create����CreateShelter�Ƃ������j���[��\�����AShelter���쐬����
public class Table_Enemy : ScriptableObject
{
    [SerializeField, Header("����(0~3,��~�r-4,SKILL)")]
    public List<MultiWepon> Wepons;

   
}
