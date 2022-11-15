using UnityEngine;
using System.Collections;
using UnityEditor;

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
            if (target_.EventType. == SetMonsterStatus.MonsterType.Warrior)
            {
                EditorGUILayout.HelpBox("Warrior�͖��@���g���Ȃ��̃}�W�b�N�|�C���g�͐ݒ�ł��܂���", MessageType.Info, true);
            }
            else if (monster.Type == SetMonsterStatus.MonsterType.Witch)
            {
                EditorGUILayout.HelpBox("Witch�̓}�W�b�N�|�C���g��0�ɂł��܂���B\n�܂��A�̗͂��Ⴂ�}�W�b�N�|�C���g���ݒ�ł��܂���B", MessageType.Info, true);
            }
            else if (monster.Type == SetMonsterStatus.MonsterType.Dragon)
            {
                EditorGUILayout.HelpBox("Dragon�̃p���[�͌Œ�Ȃ̂Őݒ�ł��܂���", MessageType.Info, true);
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
