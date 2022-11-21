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

        for (int i = 0; i != target_.EventTypes.Count; i++)
        {
            //�e�^�C�v���\��
            if (target_.EventTypes[i].eventType == EventType.event_Type.Nomove)
            {
                EditorGUILayout.LabelField("�ҋ@�̐ݒ�");
                target_.EventTypes[i].ordes.WaitTime = EditorGUILayout.FloatField("�ҋ@����", target_.EventTypes[i].ordes.WaitTime);
                target_.EventTypes[i].ordes.ObjChara_Num = EditorGUILayout.IntField("�ΏۃL�����ԍ�", target_.EventTypes[i].ordes.ObjChara_Num);
            }
            else if (target_.EventTypes[i].eventType == EventType.event_Type.HorizonMove)
            {
                EditorGUILayout.LabelField("���E�̐ݒ�");
                target_.EventTypes[i].ordes.Horizon = EditorGUILayout.IntField("��:1�`-�E:-1", target_.EventTypes[i].ordes.Horizon);
                target_.EventTypes[i].ordes.Move_Time = EditorGUILayout.IntField("�ړ�����", target_.EventTypes[i].ordes.Move_Time);
                target_.EventTypes[i].ordes.ObjChara_Num = EditorGUILayout.IntField("�ΏۃL�����ԍ�", target_.EventTypes[i].ordes.ObjChara_Num);
            }
            else if (target_.EventTypes[i].eventType == EventType.event_Type.VerticalMove)
            {
                EditorGUILayout.LabelField("�㉺�̐ݒ�");
                target_.EventTypes[i].ordes.Vertical = EditorGUILayout.IntField("��:1�`-��:-1", target_.EventTypes[i].ordes.Vertical);
                target_.EventTypes[i].ordes.Move_Time = EditorGUILayout.IntField("�ړ�����", target_.EventTypes[i].ordes.Move_Time);
                target_.EventTypes[i].ordes.ObjChara_Num = EditorGUILayout.IntField("�ΏۃL�����ԍ�", target_.EventTypes[i].ordes.ObjChara_Num);
            }
            else if (target_.EventTypes[i].eventType == EventType.event_Type.JumpMove)
            {
                EditorGUILayout.LabelField("�W�����v�̐ݒ�");
                target_.EventTypes[i].ordes.Jump_count = EditorGUILayout.IntField("�W�����v��", target_.EventTypes[i].ordes.Jump_count);
                target_.EventTypes[i].ordes.WaitTime = EditorGUILayout.FloatField("�W�����v�Ԋu", target_.EventTypes[i].ordes.WaitTime);
                target_.EventTypes[i].ordes.ObjChara_Num = EditorGUILayout.IntField("�ΏۃL�����ԍ�", target_.EventTypes[i].ordes.ObjChara_Num);
            }
            else if (target_.EventTypes[i].eventType == EventType.event_Type.TalkStart)
            {
                for(int k=0;k!= target_.EventTypes[i].ordes.Talk.Length;k++)
                    target_.EventTypes[i].ordes.Talk[k] = EditorGUILayout.TextField(k.ToString()+"�Ԗڂ̃Z���t", target_.EventTypes[i].ordes.Talk[k]);
                target_.EventTypes[i].ordes.ObjChara_Num = EditorGUILayout.IntField("�ΏۃL�����ԍ�", target_.EventTypes[i].ordes.ObjChara_Num);

            }
            else if (target_.EventTypes[i].eventType == EventType.event_Type.CameraMove)
            {
                target_.OutCamera = EditorGUILayout.IntField("�ڂ�ς���J�����ԍ�", target_.OutCamera);
            }
            else
            {
                Debug.LogError("�o�^����ĂȂ��C�x���gType");
            }

            EditorGUILayout.Space();
            EditorGUILayout.LabelField((i+2).ToString()+"�Ԗڂ̐ݒ�---------------------------");
        }



        EditorUtility.SetDirty(target);
       
    }

}
