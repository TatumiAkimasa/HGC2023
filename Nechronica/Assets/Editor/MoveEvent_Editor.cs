using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//�G�f�B�^�[�ݒ�
using UnityEditor;

//�ΏۃN���X��(Script)
[CustomEditor(typeof(MoveEvent_Chara))]
//�Q�l https://kan-kikuchi.hatenablog.com/entry/InspectorEx , https://qiita.com/PETITDIGI/items/a40a7477579f14a5a8e6

//��Unity��������́A���ۂ̃t�H���_�����Aseets�̒���Editor�t�H���_��V�K�쐬�����̂Ȃ��ɂ���EditorScript�����鎖

//�p�����Editor
public class MoveEvent_Editor : Editor
{
    //�ΏۃN���X��(Script)�錾(�����Ȃ邩��)
    private MoveEvent_Chara target_;
  
    //���
    private void Awake()
    {
        //����̎d�����傢����(GameObject�^�ɕϊ�)
        target_ = target as MoveEvent_Chara;
    }

    public override void OnInspectorGUI()
    {
        //���͂��ꂽList�\��
        //�C���X�y�N�^�[��M�邽�߂�Obj�^�i�H�j
        SerializedObject so = new SerializedObject(target_);
        //List��z��͂��̂悤�ɕϐ������猟��
        SerializedProperty stringsProperty = so.FindProperty("EventTypes");
        //�\�����邽�߂̂���
        EditorGUILayout.PropertyField(stringsProperty, true);
        so.ApplyModifiedProperties();


        //�X�V���ꂽ�甭���I(�����)
        //EditorGUI.BeginChangeCheck();

        //��Ԃɕ����\��
        EditorGUILayout.LabelField("0�Ԗڂ̐ݒ�---------------------------");

        //List�Ȃ̂ŗv�f�������ڂ�������
        for (int i = 0; i != target_.EventTypes.Count; i++)
        {
            //�e�^�C�v���\��
            if (target_.EventTypes[i].eventType == EventType.event_Type.Nomove)
            {
                EditorGUILayout.LabelField("�ҋ@�̐ݒ�");

                //�ς���ϐ��̑Ώې�@�@�@�@�@�@�@�@�@                 Float�^�̕ύX�Ŗ��O�A  �ς���ϐ��̑Ώې�@
                target_.EventTypes[i].ordes.WaitTime = EditorGUILayout.FloatField("�ҋ@����", target_.EventTypes[i].ordes.WaitTime);
                //�ς���ϐ��̑Ώې�@�@�@�@�@�@�@�@�@                     Int�^�̕ύX�Ŗ��O�A        �ς���ϐ��̑Ώې�@
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
                //string(�z��)�̏ꍇ�i�^���I�j�����(25�s��)List�̂����̂��Class�Ȃ�Ȃ�Ȃ�̒��ɓ����Ă��͂ł��Ȃ�
                EditorGUILayout.LabelField("�g�[�N�̐ݒ�");

                //�v�f������
                for (int k=0;k!= target_.EventTypes[i].ordes.Talk.Length;k++)
                    //�v�f����Text�g�𑝂₷
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
            EditorGUILayout.LabelField((i+1).ToString()+"�Ԗڂ̐ݒ�---------------------------");
        }



        EditorUtility.SetDirty(target);
       
    }

}
