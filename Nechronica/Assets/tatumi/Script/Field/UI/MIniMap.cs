using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MIniMap : MonoBehaviour
{
    //PLの場所、角度を取得(向きをMAPに反映はうっとうしいのでなし)
    public GameObject player;
    Vector3 trans;// Angle;

    //[SerializeField, Header("限界点（z=縦）,大きさが今のところ一定なので中央固定のX(横移動)")]
    //public float Set_x, END_z_front,END_z_back;
    
    // Start is called before the first frame update
    void Start()
    {
        //カメラの初期位置固定
        //trans.z = END_z_front;
        //trans.x = Set_x;
    }

    // Update is called once per frame
    void Update()
    {
       // Angle = player.transform.localEulerAngles;


        //手前に下がらない
        //if (END_z_front > player.transform.position.z)
        //    ;
        ////奥に行かない
        //else if (END_z_back < player.transform.position.z)
        //    ;
        ////上記以外
        //else
        //    trans.z = player.transform.position.z;


        //Angle.x = 90.0f;

        //高低差対応用
        trans.y = player.transform.position.y+12.0f;
        
        //反映
        //this.transform.rotation = Quaternion.Euler(Angle);
        //this.transform.position = trans;
    }
}
