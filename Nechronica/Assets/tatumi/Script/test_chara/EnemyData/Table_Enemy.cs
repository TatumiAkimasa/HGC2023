using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MultiWepon
{
    [Header("中身パーツ")]
    public List<Table_Parts> Parts;

    public MultiWepon(List<Table_Parts> _multisite)
    {
        Parts = _multisite;
    }
}

[CreateAssetMenu(fileName = "ScriptTable", menuName = "CreateEnemyData")]//  CreateからCreateShelterというメニューを表示し、Shelterを作成する
public class Table_Enemy : ScriptableObject
{
    [SerializeField, Header("部位(0~3,頭~脚-4,SKILL)")]
    public List<MultiWepon> Wepons;

   
}
