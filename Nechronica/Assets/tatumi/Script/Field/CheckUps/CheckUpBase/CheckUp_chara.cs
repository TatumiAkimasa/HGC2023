using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckUp_chara : CheckUp_Base
{
    [SerializeField, Header("必要アイテム名")]
    protected string Item_Name;

    [SerializeField, Header("イベントに追加するか")]
    protected bool EventStart = false;

    [SerializeField, Header("対応イベント番号")]
    protected int Event_num;

    [SerializeField, Header("成功時の台詞")]
    protected string[] Event_OK;
    [SerializeField, Header("失敗時の台詞")]
    protected string[] Event_OUT;

    [SerializeField, Header("画面上のUIを使うか")]
    protected bool OverStrObj;

    [SerializeField, Header("Event完了後削除")]
    protected GameObject ParentObj;

    [SerializeField, Header("目的更新用UIobj")]
    protected GameObject UI;

    protected Item item_data;

}


