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
    public string[] AThings;

    private void Awake()
    {
        target_ = target as MoveEvent_Player;
        
    }

    public override void OnInspectorGUI()
    {

        //入力されたモンスターの種類を設定
       
        SerializedObject so = new SerializedObject(target_);

        SerializedProperty stringsProperty = so.FindProperty("EventType");

        EditorGUILayout.PropertyField(stringsProperty, true);

        so.ApplyModifiedProperties();


        //更新されたら発動！
        //EditorGUI.BeginChangeCheck();
        EditorGUILayout.LabelField("1番目の設定---------------------------");

        for (int i = 0; i != target_.EventTypes.Count; i++)
        {
            //各タイプ毎表示
            if (target_.EventTypes[i].eventType == EventType.event_Type.Nomove)
            {
                EditorGUILayout.LabelField("待機の設定");
                target_.EventTypes[i].ordes.WaitTime = EditorGUILayout.FloatField("待機時間", target_.EventTypes[i].ordes.WaitTime);
                target_.EventTypes[i].ordes.ObjChara_Num = EditorGUILayout.IntField("対象キャラ番号", target_.EventTypes[i].ordes.ObjChara_Num);
            }
            else if (target_.EventTypes[i].eventType == EventType.event_Type.HorizonMove)
            {
                EditorGUILayout.LabelField("左右の設定");
                target_.EventTypes[i].ordes.Horizon = EditorGUILayout.IntField("左:1～-右:-1", target_.EventTypes[i].ordes.Horizon);
                target_.EventTypes[i].ordes.Move_Time = EditorGUILayout.IntField("移動時間", target_.EventTypes[i].ordes.Move_Time);
                target_.EventTypes[i].ordes.ObjChara_Num = EditorGUILayout.IntField("対象キャラ番号", target_.EventTypes[i].ordes.ObjChara_Num);
            }
            else if (target_.EventTypes[i].eventType == EventType.event_Type.VerticalMove)
            {
                EditorGUILayout.LabelField("上下の設定");
                target_.EventTypes[i].ordes.Vertical = EditorGUILayout.IntField("上:1～-下:-1", target_.EventTypes[i].ordes.Vertical);
                target_.EventTypes[i].ordes.Move_Time = EditorGUILayout.IntField("移動時間", target_.EventTypes[i].ordes.Move_Time);
                target_.EventTypes[i].ordes.ObjChara_Num = EditorGUILayout.IntField("対象キャラ番号", target_.EventTypes[i].ordes.ObjChara_Num);
            }
            else if (target_.EventTypes[i].eventType == EventType.event_Type.JumpMove)
            {
                EditorGUILayout.LabelField("ジャンプの設定");
                target_.EventTypes[i].ordes.Jump_count = EditorGUILayout.IntField("ジャンプ回数", target_.EventTypes[i].ordes.Jump_count);
                target_.EventTypes[i].ordes.WaitTime = EditorGUILayout.FloatField("ジャンプ間隔", target_.EventTypes[i].ordes.WaitTime);
                target_.EventTypes[i].ordes.ObjChara_Num = EditorGUILayout.IntField("対象キャラ番号", target_.EventTypes[i].ordes.ObjChara_Num);
            }
            else if (target_.EventTypes[i].eventType == EventType.event_Type.TalkStart)
            {
                for(int k=0;k!= target_.EventTypes[i].ordes.Talk.Length;k++)
                    target_.EventTypes[i].ordes.Talk[k] = EditorGUILayout.TextField(k.ToString()+"番目のセリフ", target_.EventTypes[i].ordes.Talk[k]);
                target_.EventTypes[i].ordes.ObjChara_Num = EditorGUILayout.IntField("対象キャラ番号", target_.EventTypes[i].ordes.ObjChara_Num);

            }
            else if (target_.EventTypes[i].eventType == EventType.event_Type.CameraMove)
            {
                target_.OutCamera = EditorGUILayout.IntField("移り変わり先カメラ番号", target_.OutCamera);
            }
            else
            {
                Debug.LogError("登録されてないイベントType");
            }

            EditorGUILayout.Space();
            EditorGUILayout.LabelField((i+2).ToString()+"番目の設定---------------------------");
        }



        EditorUtility.SetDirty(target);
       
    }

}
