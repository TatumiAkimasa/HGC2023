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

    [SerializeField]
    private Sprite[] sprites;
    [SerializeField]
    private Image change_image;

    public CharaManeuver Set_Parts = new CharaManeuver { };
    public ManeuverEffectsAtk Set_Eff = new ManeuverEffectsAtk { };

    public Wepon_Maneger Wepon_maneger;
    public int Type,level;

    private int nowType=0;
  
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

    
    //引数2個出来んためこっちで制御
    public void Wepon_add()
    {
        Wepon_maneger.Add_Wepon(this.GetComponent<Toggle>(), Type,level);
    }

    public string Get_Wepon_Text()
    {
        return Name.text;
    }

    public void poti_change()
    {
        if (change_image != null)
        {
            if (Input.GetMouseButton(1))
            {
                    string now_name = this.gameObject.name.Substring(0, this.gameObject.name.Length - 1);

                    switch (nowType)
                    {
                        case 1:
                            {
                                this.gameObject.name = now_name + "A";
                                change_image.sprite = sprites[nowType];
                                break;
                            }
                        case 2:
                            {
                                this.gameObject.name = now_name + "B";
                                change_image.sprite = sprites[nowType];
                                break;
                            }
                        case 3:
                            {
                                this.gameObject.name = now_name + "L";
                                change_image.sprite = sprites[nowType];
                                break;
                            }
                        case 0:
                            {
                                this.gameObject.name = now_name + "H";
                                change_image.sprite = sprites[nowType];
                                break;
                            }
                    }
                    nowType++;
                   
                    if (nowType == 4)
                        nowType = 0;
               
            }
           
               
        }



    }

}
