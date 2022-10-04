using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wepon_Data_SaveSet : MonoBehaviour
{
    //内部データ------------------------------------------------
    [SerializeField]
    private int EffectNum, Cost, Timing, MinRange, MaxRange, Weight, AtkType, Num_per_Action;//基礎設定値

    private string Name;//パーツ名

    [SerializeField]
    private bool isExplosion, isCotting, isAllAttack, isSuccession;//使用したかどうか

    //なんかこうしないと作れない😢
    public CharaManeuver Set_Parts = new CharaManeuver { };
    public ManeuverEffectsAtk Eff_Parts = new ManeuverEffectsAtk { };

    private void Start()
    {
        Name = this.GetComponent<WeponData_Set>().Get_Wepon_Text();

        Reset();

    }

    public CharaManeuver GetPrats() => Set_Parts;

    public void Reset()
    {
        Set_Parts.Cost = Cost;
        Set_Parts.EffectNum = EffectNum;
        Set_Parts.isDmage = false;
        Set_Parts.isUse = false;
        Set_Parts.MaxRange = MaxRange;
        Set_Parts.MinRange = MinRange;
        Set_Parts.Name = Name;
        Set_Parts.Timing = Timing;
        Set_Parts.Weight = Weight;

        Set_Parts.Atk = Eff_Parts;

        Set_Parts.Atk.AtkType = AtkType;
        Set_Parts.Atk.isAllAttack = isAllAttack;
        Set_Parts.Atk.isCotting = isCotting;
        Set_Parts.Atk.isExplosion = isExplosion;
        Set_Parts.Atk.isSuccession = isSuccession;
        Set_Parts.Atk.Num_per_Action = Num_per_Action;
    }

}
