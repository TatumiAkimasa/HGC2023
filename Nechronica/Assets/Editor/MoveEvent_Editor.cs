using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//エディター設定
using UnityEditor;

//対象クラス名(Script)
[CustomEditor(typeof(MoveEvent_Chara))]
//参考 https://kan-kikuchi.hatenablog.com/entry/InspectorEx , https://qiita.com/PETITDIGI/items/a40a7477579f14a5a8e6

//※Unity上もしくは、実際のフォルダ操作でAseetsの中にEditorフォルダを新規作成しそのなかにこのEditorScriptを入れる事

//継承先はEditor
public class MoveEvent_Editor : Editor
{
    //対象クラス名(Script)宣言(長くなるから)
    private MoveEvent_Chara target_;
  
    //代入
    private void Awake()
    {
        //代入の仕方ちょい特殊(GameObject型に変換)
        target_ = target as MoveEvent_Chara;
    }

    public override void OnInspectorGUI()
    {
        //入力されたList表示
        //インスペクターを弄るためのObj型（？）
        SerializedObject so = new SerializedObject(target_);
        //Listや配列はこのように変数名から検索
        SerializedProperty stringsProperty = so.FindProperty("EventTypes");
        //表示するためのもの
        EditorGUILayout.PropertyField(stringsProperty, true);
        so.ApplyModifiedProperties();


        //更新されたら発動！(いらん)
        //EditorGUI.BeginChangeCheck();

        //空間に文字表示
        EditorGUILayout.LabelField("0番目の設定---------------------------");

        //Listなので要素数分項目が増える
        for (int i = 0; i != target_.EventTypes.Count; i++)
        {
            //各タイプ毎表示
            if (target_.EventTypes[i].eventType == EventType.event_Type.Nomove)
            {
                EditorGUILayout.LabelField("待機の設定");

                //変える変数の対象先　　　　　　　　　                 Float型の変更で名前、  変える変数の対象先　
                target_.EventTypes[i].ordes.WaitTime = EditorGUILayout.FloatField("待機時間", target_.EventTypes[i].ordes.WaitTime);
                //変える変数の対象先　　　　　　　　　                     Int型の変更で名前、        変える変数の対象先　
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
                //string(配列)の場合（疑似的）※上の(25行目)Listのやり方のやつはClassなりなんなりの中に入ってるやつはできない
                EditorGUILayout.LabelField("トークの設定");

                //要素数分回す
                for (int k=0;k!= target_.EventTypes[i].ordes.Talk.Length;k++)
                    //要素数分Text枠を増やす
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
            EditorGUILayout.LabelField((i+1).ToString()+"番目の設定---------------------------");
        }



        EditorUtility.SetDirty(target);
       
    }

}
