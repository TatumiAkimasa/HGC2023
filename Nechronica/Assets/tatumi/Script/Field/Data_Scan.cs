using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data_Scan
{
    //シングルトンパターン
    private static Data_Scan instance = null;

    public static Data_Scan Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Data_Scan();
            }
            return instance;
        }

    }

    public Doll_blueprint[] my_data = new Doll_blueprint [1];

    public Save_Load_data save_Load_Data_cs;

    public MOve_chara Player_controller;

    //将来的に全データを一旦こいつに置く。
    public IEnumerator Init(System.Action<bool> action)
    {
        save_Load_Data_cs.ClickButtonLoad();

        while (save_Load_Data_cs.aa == null)
            yield return null;

        my_data[0]=save_Load_Data_cs.aa;

        action(true);
        yield return null;
    }

}
