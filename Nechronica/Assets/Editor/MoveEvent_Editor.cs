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
//�Q�l https://kan-kikuchi.hatenablog.com/entry/InspectorEx , https://qiita.com/PETITDIGI/items/a40a7477579f14a5a8e6
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

        //���͂��ꂽ�����X�^�[�̎�ނ�ݒ�
       
        SerializedObject so = new SerializedObject(target_);

        SerializedProperty stringsProperty = so.FindProperty("EventType");

        EditorGUILayout.PropertyField(stringsProperty, true);

        so.ApplyModifiedProperties();


        //�X�V���ꂽ�甭���I
        //EditorGUI.BeginChangeCheck();
        EditorGUILayout.LabelField("1�Ԗڂ̐ݒ�---------------------------");

        for (int i = 0; i != target_.EventType.Count; i++)
        {
            //�e�^�C�v���\��
            if (target_.EventType[i].eventType == EventType.event_Type.Nomove)
            {
                EditorGUILayout.LabelField("�ҋ@�̐ݒ�");
                target_.EventType[i].ordes.WaitTime = EditorGUILayout.FloatField("�ҋ@����", target_.EventType[i].ordes.WaitTime);
            }
            else if (target_.EventType[i].eventType == EventType.event_Type.HorizonMove)
            {
                EditorGUILayout.LabelField("���E�̐ݒ�");
                target_.EventType[i].ordes.Horizon = EditorGUILayout.IntField("��:1�`-�E:-1", target_.EventType[i].ordes.Horizon);
                target_.EventType[i].ordes.Move_Time = EditorGUILayout.IntField("�ړ�����", target_.EventType[i].ordes.Move_Time);
            }
            else if (target_.EventType[i].eventType == EventType.event_Type.VerticalMove)
            {
                EditorGUILayout.LabelField("�㉺�̐ݒ�");
                target_.EventType[i].ordes.Vertical = EditorGUILayout.IntField("��:1�`-��:-1", target_.EventType[i].ordes.Vertical);
                target_.EventType[i].ordes.Move_Time = EditorGUILayout.IntField("�ړ�����", target_.EventType[i].ordes.Move_Time);
            }
            else if (target_.EventType[i].eventType == EventType.event_Type.JumpMove)
            {
                EditorGUILayout.LabelField("�W�����v�̐ݒ�");
                target_.EventType[i].ordes.Jump_count = EditorGUILayout.IntField("�W�����v��", target_.EventType[i].ordes.Jump_count);
                target_.EventType[i].ordes.WaitTime = EditorGUILayout.FloatField("�W�����v�Ԋu", target_.EventType[i].ordes.WaitTime);
            }
            else if (target_.EventType[i].eventType == EventType.event_Type.TalkStart)
            {
                for(int k=0;k!= target_.EventType[i].ordes.Talk.Length;k++)
                    target_.EventType[i].ordes.Talk[k] = EditorGUILayout.TextField(k.ToString()+"�Ԗڂ̃Z���t", target_.EventType[i].ordes.Talk[k]);

            }
            else if (target_.EventType[i].eventType == EventType.event_Type.TalkEnd)
            {

            }
            else
            {
                Debug.LogError("�o�^����ĂȂ��C�x���gType");
            }

            EditorGUILayout.Space();
            EditorGUILayout.LabelField((i+2).ToString()+"�Ԗڂ̐ݒ�---------------------------");
        }



        EditorUtility.SetDirty(target);
        Debug.Log("������");
    }

}
