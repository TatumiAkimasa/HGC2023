using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WeponData_Set : MonoBehaviour
{

    [SerializeField]
    private Text Name;//パーツ名

    //見た目--------------------------------------------------------------

    [SerializeField]
    private Sprite[] sprites;
    [SerializeField]
    private Image change_image;

   
    public int Type, level;

    private int nowType = 0;

    //引数2個出来んためこっちで制御
    //データの実際に
    public void Wepon_add()
    {
        Maneger_Accessor.Instance.weponManeger_cs.Add_Wepon(this.GetComponent<Toggle>(), Type,level);
    }

    public string Get_Wepon_Text()
    {
        return Name.text;
    }

    public void poti_change()
    {
        if (change_image != null)
        {
            if (Input.GetMouseButton(1)&&this.GetComponent<Toggle>().isOn==false)
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
