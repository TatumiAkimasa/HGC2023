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

    private void Awake()
    {
        target_ = target as MoveEvent_Player;
    }

    public override void OnInspectorGUI()
    {
        //�X�V���ꂽ�甭���I
        EditorGUI.BeginChangeCheck();

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
                EditorGUILayout.LabelField("��b�̐ݒ�");
                var str = serializedObject.FindProperty("talk");
                target_.EventType[i].ordes.Talk = new ExampleCustomList(listProp);

                target_.EventType[i].ordes.WaitTime = EditorGUILayout.FloatField("�W�����v�Ԋu", target_.EventType[i].ordes.WaitTime);
            }
            else if (target_.EventType[i].eventType == EventType.event_Type.TalkEnd)
            {

            }
            else
            {
                Debug.LogError("�o�^����ĂȂ��C�x���gType");
            }
        }


        //HP�͂ǂ̃^�C�v�ł��ݒ�
        monster.HP = Mathf.Max(1, EditorGUILayout.FloatField("�̗�", monster.HP));

        //��m��MP��0�Ȃ̂Őݒ肵�Ȃ�
        if (monster.Type != SetMonsterStatus.MonsterType.Warrior)
        {
            monster.MP = Mathf.Max(0, EditorGUILayout.FloatField("�}�W�b�N�|�C���g", monster.MP));
        }

        //�h���S���̃p���[�͌Œ�Ȃ̂Őݒ肵�Ȃ�
        if (monster.Type != SetMonsterStatus.MonsterType.Dragon)
        {
            monster.Power = Mathf.Max(1, EditorGUILayout.FloatField("�p���[�I", monster.Power));
        }

        //���@�g����MP >= HP
        if (monster.Type == SetMonsterStatus.MonsterType.Witch)
        {
            monster.MP = Mathf.Max(monster.MP, monster.HP);
        }

        EditorUtility.SetDirty(target);
    }

}
