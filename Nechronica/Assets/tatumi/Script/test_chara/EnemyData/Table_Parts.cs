using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "ScriptTable", menuName = "CreatePartsData")]//  CreateからCreateShelterというメニューを表示し、Shelterを作成する
public class Table_Parts : ScriptableObject
{
    public enum EffctNumString
    {
        Damage,
        Judge,
        Move,
        Countt,
        Extraa,
    }

   
    [SerializeField, Header("EeffctNum設定")]
    public EffctNumString Eff_String;
    public int Eff_int;

    [SerializeField, Header("パーツデータ")]
    public CharaManeuver Maneuver;

    public Table_Parts(Table_Parts value)
    {
        this.Maneuver.EffectNum.Add(value.Eff_String.ToString(),value.Eff_int);
        this.Maneuver = value.Maneuver;
    }
}
