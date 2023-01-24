using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "ScriptTable", menuName = "CreatePartsData")]//  CreateからCreateShelterというメニューを表示し、Shelterを作成する
public class Table_Parts : ScriptableObject
{
    public enum effstring
    {
        Damage,
        Guard,
        Judge,
        Move, 
        Count,
        Insanit, // 狂気点が関与するものはこれを入れる
        Extra,

        // オンリーワンの効果
        Protect,    // かばうの効果はこれで認識
        YobunnnaUde,    // 余分な腕、死の手はこれで認識
        Nikunotate,   // 肉の盾はこれで認識
    }


    [System.Serializable]
    public struct EffctNum_
    {
        public effstring Eff_String;
        public int Eff_int;
    }

    [SerializeField, Header("EeffctNum設定")]
    public List<EffctNum_> effcrnums;

    [SerializeField, Header("パーツデータ")]
    public CharaManeuver Maneuver;

    public Table_Parts(Table_Parts value)
    {
        
        this.Maneuver = value.Maneuver;

    }


}
