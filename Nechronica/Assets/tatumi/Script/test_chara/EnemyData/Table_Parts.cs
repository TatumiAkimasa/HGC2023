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
        Guard ,
        Judge ,
        Move ,
        Count ,
        Insanity  ,   // 狂気点が関与するものはこれを入れる
        Extra ,


        // オンリーワンの効果
        Protect,  // かばうの効果はこれで認識
        YobunnnaUde  ,  // 余分な腕、死の手はこれで認識
        Nikunotate,  // 肉の盾はこれで認識
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
