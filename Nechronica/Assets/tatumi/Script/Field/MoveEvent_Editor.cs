using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Example : MonoBehaviour
{
    [SerializeField]
    private string[] talk;

}

[CustomEditor(typeof(MoveEvent_Player))]
//参考 https://kan-kikuchi.hatenablog.com/entry/InspectorEx , https://qiita.com/PETITDIGI/items/a40a7477579f14a5a8e6
public class MoveEvent_Editor : Editor
{
   

    private MoveEvent_Player target_;

    private void Awake()
    {
        target_ = target as MoveEvent_Player;
    }

    public override void OnInspectorGUI()
    {
        //更新されたら発動！
        EditorGUI.BeginChangeCheck();

        for (int i = 0; i != target_.EventType.Count; i++)
        {
            //各タイプ毎表示
            if (target_.EventType[i].eventType == EventType.event_Type.Nomove)
            {
                EditorGUILayout.LabelField("待機の設定");
                target_.EventType[i].ordes.WaitTime = EditorGUILayout.FloatField("待機時間", target_.EventType[i].ordes.WaitTime);
            }
            else if (target_.EventType[i].eventType == EventType.event_Type.HorizonMove)
            {
                EditorGUILayout.LabelField("左右の設定");
                target_.EventType[i].ordes.Horizon = EditorGUILayout.IntField("左:1～-右:-1", target_.EventType[i].ordes.Horizon);
                target_.EventType[i].ordes.Move_Time = EditorGUILayout.IntField("移動時間", target_.EventType[i].ordes.Move_Time);
            }
            else if (target_.EventType[i].eventType == EventType.event_Type.VerticalMove)
            {
                EditorGUILayout.LabelField("上下の設定");
                target_.EventType[i].ordes.Vertical = EditorGUILayout.IntField("上:1～-下:-1", target_.EventType[i].ordes.Vertical);
                target_.EventType[i].ordes.Move_Time = EditorGUILayout.IntField("移動時間", target_.EventType[i].ordes.Move_Time);
            }
            else if (target_.EventType[i].eventType == EventType.event_Type.JumpMove)
            {
                EditorGUILayout.LabelField("ジャンプの設定");
                target_.EventType[i].ordes.Jump_count = EditorGUILayout.IntField("ジャンプ回数", target_.EventType[i].ordes.Jump_count);
                target_.EventType[i].ordes.WaitTime = EditorGUILayout.FloatField("ジャンプ間隔", target_.EventType[i].ordes.WaitTime);
            }
            else if (target_.EventType[i].eventType == EventType.event_Type.TalkStart)
            {
                EditorGUILayout.LabelField("会話の設定");
                var str = serializedObject.FindProperty("talk");
                target_.EventType[i].ordes.Talk = new ExampleCustomList(listProp);

                target_.EventType[i].ordes.WaitTime = EditorGUILayout.FloatField("ジャンプ間隔", target_.EventType[i].ordes.WaitTime);
            }
            else if (target_.EventType[i].eventType == EventType.event_Type.TalkEnd)
            {

            }
            else
            {
                Debug.LogError("登録されてないイベントType");
            }
        }


        //HPはどのタイプでも設定
        monster.HP = Mathf.Max(1, EditorGUILayout.FloatField("体力", monster.HP));

        //戦士はMPが0なので設定しない
        if (monster.Type != SetMonsterStatus.MonsterType.Warrior)
        {
            monster.MP = Mathf.Max(0, EditorGUILayout.FloatField("マジックポイント", monster.MP));
        }

        //ドラゴンのパワーは固定なので設定しない
        if (monster.Type != SetMonsterStatus.MonsterType.Dragon)
        {
            monster.Power = Mathf.Max(1, EditorGUILayout.FloatField("パワー！", monster.Power));
        }

        //魔法使いはMP >= HP
        if (monster.Type == SetMonsterStatus.MonsterType.Witch)
        {
            monster.MP = Mathf.Max(monster.MP, monster.HP);
        }

        EditorUtility.SetDirty(target);
    }

}
