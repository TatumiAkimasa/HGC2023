using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WeponData_Set : CharaBase
{
    [SerializeField]
    private int EffectNum,Cost,Timing,MinRange,MaxRange,Weight, AtkType, Num_per_Action;//基礎設定値

    [SerializeField]
    private Text Name;//パーツ名

    [SerializeField]
    private bool isExplosion, isCotting, isAllAttack, isSuccession;//使用したかどうか

    public CharaManeuver Set_Parts = new CharaManeuver { };
    public ManeuverEffectsAtk Set_Eff = new ManeuverEffectsAtk { };


    private void Start()
    {
        Set_Parts.Cost = Cost;
        Set_Parts.EffectNum = EffectNum;
        Set_Parts.isDmage = false;
        Set_Parts.isUse = false;
        Set_Parts.MaxRange = MaxRange;
        Set_Parts.MinRange = MinRange;
        Set_Parts.Name = Name.text;
        Set_Parts.Timing = Timing;
        Set_Parts.Weight = Weight;

        Set_Eff.AtkType = AtkType;
        Set_Eff.isAllAttack = isAllAttack;
        Set_Eff.isCotting = isCotting;
        Set_Eff.isExplosion = isExplosion;
        Set_Eff.isSuccession = isSuccession;
        Set_Eff.Num_per_Action = Num_per_Action;

    }
}
