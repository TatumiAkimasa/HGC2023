using UnityEngine;
using System.Collections;
using UnityEditor;

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
            if (target_.EventType. == SetMonsterStatus.MonsterType.Warrior)
            {
                EditorGUILayout.HelpBox("Warriorは魔法が使えないのマジックポイントは設定できません", MessageType.Info, true);
            }
            else if (monster.Type == SetMonsterStatus.MonsterType.Witch)
            {
                EditorGUILayout.HelpBox("Witchはマジックポイントを0にできません。\nまた、体力より低いマジックポイントも設定できません。", MessageType.Info, true);
            }
            else if (monster.Type == SetMonsterStatus.MonsterType.Dragon)
            {
                EditorGUILayout.HelpBox("Dragonのパワーは固定なので設定できません", MessageType.Info, true);
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
