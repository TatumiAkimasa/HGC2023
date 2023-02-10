using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PartsList : CharaBase
{
    [SerializeField]
    //基礎パーツ
    protected CharaManeuver noumiso_H, medama_H, ago_H, hunnu_H,
                            kobusi_A, ude_A, kata_A,
                            harawata_B, harawata2_B, harawata3_B,
                            hone_L, hone1_L, hone2_L, asi_L,

                            
                            shotGun_U, doubleGun_U, bearGun_U, scope_H, kanhu_H,
                            meitou_U, kanhu2_H, tale_B, uroko_B, wirelille_A;
    //TIMING------------------^p^

    //0=オート,1=アクション,2=ラピッド,3=ジャッジ,4=ダメージ(処理順でわける)
    //攻撃範囲-------------------------------
    //最小(10=自身)
    //最最大(10=自身)
    //重さ------------------------------------
    //(基礎は今のところ1で固定)
    // Start is called before the first frame update
    public void InitParts()
    {
        //頭パーツ-------------------
        ago_H.Name = "あご";
        ago_H.AnimName = AnimationName.Ago;
        ago_H.AnimEffect = NonResources.Load<GameObject>("Assets/morii/Prefab/Anim/"+ ago_H.AnimName + ".prefab");
        ago_H.EffectNum.Add(EffNum.Damage, 3);
        ago_H.Cost = 2;
        ago_H.Timing = ACTION;
        ago_H.MinRange = 0;
        ago_H.MaxRange = 0;
        ago_H.Weight = 1;
        ago_H.EnemyAI.Add(2);
        ago_H.EnemyAI.Add(2);
        ago_H.EnemyAI.Add(2);
        ago_H.EnemyAI.Add(2);
        ago_H.EnemyAI.Add(2);
        ago_H.EnemyAI.Add(10);

        noumiso_H.Name = "のうみそ";
        noumiso_H.AnimName = AnimationName.Null;
        noumiso_H.EffectNum.Add(EffNum.Count, 2);
        noumiso_H.Cost = 0;
        noumiso_H.Timing = COUNT;
        noumiso_H.MinRange = 10;
        noumiso_H.MaxRange = 10;
        noumiso_H.Weight = 1;
       

        medama_H.Name = "めだま";
        medama_H.AnimName = AnimationName.Null;
        medama_H.EffectNum.Add(EffNum.Count, 1);
        medama_H.Cost = 0;
        medama_H.Timing = COUNT;
        medama_H.MinRange = 10;
        medama_H.MaxRange = 10;
        medama_H.Weight = 1;



        //腕---------------------------------
        kobusi_A.Name = "こぶし";
        kobusi_A.AnimName = AnimationName.Kobushi;
        kobusi_A.AnimEffect = NonResources.Load<GameObject>("Assets/morii/Prefab/Anim/" + kobusi_A.AnimName + ".prefab");
        kobusi_A.EffectNum.Add(EffNum.Damage, 1);
        kobusi_A.Cost = 2;
        kobusi_A.Timing = ACTION;
        kobusi_A.MinRange = 0;
        kobusi_A.MaxRange = 0;
        kobusi_A.Weight = 1;

        kobusi_A.EnemyAI.Add(2);
        kobusi_A.EnemyAI.Add(2);
        kobusi_A.EnemyAI.Add(2);
        kobusi_A.EnemyAI.Add(2);
        kobusi_A.EnemyAI.Add(10);


        kata_A.Name = "かた";
        kata_A.AnimName = AnimationName.Null;
        kata_A.EffectNum.Add(EffNum.Move, 1);
        kata_A.Cost = 4;
        kata_A.Timing = MOVE;
        kata_A.MinRange = 10;
        kata_A.MaxRange = 10;
        kata_A.Weight = 1;
        kata_A.EnemyAI.Add(2);
        kata_A.EnemyAI.Add(2);
        kata_A.EnemyAI.Add(2);
        kata_A.EnemyAI.Add(2);
        kata_A.EnemyAI.Add(10);

        ude_A.Name = "うで";
        ude_A.AnimName = AnimationName.Null;
        ude_A.EffectNum.Add(EffNum.Judge, 1);
        ude_A.Cost = 1;
        ude_A.Timing = JUDGE;
        ude_A.MinRange = 0;
        ude_A.MaxRange = 0;
        ude_A.Weight = 1;
        ude_A.EnemyAI.Add(2);
        ude_A.EnemyAI.Add(2);
        ude_A.EnemyAI.Add(2);
        ude_A.EnemyAI.Add(2);
        ude_A.EnemyAI.Add(10);
        //胴----------------------------

        harawata_B.Name = "はらわた";
        harawata_B.AnimName = AnimationName.Null;
        harawata_B.EffectNum.Add(EffNum.Extra, 0);
        harawata_B.Cost = 0;
        harawata_B.Timing = AUTO;
        harawata_B.MinRange = 10;
        harawata_B.MaxRange = 10;
        harawata_B.Weight = 1;

        harawata2_B.Name = "はらわた";
        harawata2_B.AnimName = AnimationName.Null;
        harawata2_B.EffectNum.Add(EffNum.Extra, 0);
        harawata2_B.Cost = 0;
        harawata2_B.Timing = AUTO;
        harawata2_B.MinRange = 10;
        harawata2_B.MaxRange = 10;
        harawata2_B.Weight = 1;

        harawata3_B.Name = "せぼね";
        harawata3_B.AnimName = AnimationName.Null;
        harawata3_B.EffectNum.Add(EffNum.Extra, 0);
        harawata3_B.Cost = 0;
        harawata3_B.Timing = AUTO;
        harawata3_B.MinRange = 10;
        harawata3_B.MaxRange = 10;
        harawata3_B.Weight = 1;

        //脚------------------------------

        hone_L.Name = "ほね";
        hone_L.AnimName = AnimationName.Null;
        hone_L.EffectNum.Add(EffNum.Move, 1);
        hone_L.Cost = 3;
        hone_L.Timing = MOVE;
        hone_L.MinRange = 10;
        hone_L.MaxRange = 10;
        hone_L.Weight = 1;
        hone_L.EnemyAI.Add(2);
        hone_L.EnemyAI.Add(2);
        hone_L.EnemyAI.Add(2);
        hone_L.EnemyAI.Add(2);
        hone_L.EnemyAI.Add(10);

        hone2_L.Name = "ほね";
        hone2_L.AnimName = AnimationName.Null;
        hone2_L.EffectNum.Add(EffNum.Move, 1);
        hone2_L.Cost = 3;
        hone2_L.Timing = MOVE;
        hone2_L.MinRange = 10;
        hone2_L.MaxRange = 10;
        hone2_L.Weight = 1;
        hone2_L.EnemyAI.Add(2);
        hone2_L.EnemyAI.Add(2);
        hone2_L.EnemyAI.Add(2);
        hone2_L.EnemyAI.Add(2);
        hone2_L.EnemyAI.Add(10);

        asi_L.Name = "あし";
        asi_L.AnimName = AnimationName.Null;
        asi_L.EffectNum.Add(EffNum.Judge, -1);
        asi_L.Cost = 1;
        asi_L.MinRange = 0;
        asi_L.MaxRange = 0;
        asi_L.Timing = JUDGE;
        asi_L.Weight = 1;
        asi_L.EnemyAI.Add(2);
        asi_L.EnemyAI.Add(2);
        asi_L.EnemyAI.Add(2);
        asi_L.EnemyAI.Add(2);
        asi_L.EnemyAI.Add(10);

        hone1_L.Name = "ほね";
        hone1_L.AnimName = AnimationName.Null;
        hone1_L.EffectNum.Add(EffNum.Move, 1);
        hone1_L.Cost = 3;
        hone1_L.Timing = MOVE;
        hone1_L.MinRange = 10;
        hone1_L.MaxRange = 10;
        hone1_L.Weight = 1;
        hone1_L.EnemyAI.Add(2);
        hone1_L.EnemyAI.Add(2);
        hone1_L.EnemyAI.Add(2);
        hone1_L.EnemyAI.Add(2);
        hone1_L.EnemyAI.Add(10);

        //追加
        shotGun_U.Name = "ショットガン";
        shotGun_U.AnimName = AnimationName.Null;
        shotGun_U.EffectNum.Add(EffNum.Damage, 2);
        shotGun_U.Cost = 2;
        shotGun_U.Timing = ACTION;
        shotGun_U.MinRange = 0;
        shotGun_U.MaxRange = 1;
        shotGun_U.Weight = 1;
        shotGun_U.Atk.isExplosion = true;

        doubleGun_U.Name = "二丁拳銃";
        doubleGun_U.AnimName = AnimationName.Null;
        doubleGun_U.EffectNum.Add(EffNum.Damage, 2);
        doubleGun_U.Cost = 3;
        doubleGun_U.Timing = ACTION;
        doubleGun_U.MinRange = 1;
        doubleGun_U.MaxRange = 1;
        doubleGun_U.Weight = 1;
        doubleGun_U.Atk.Num_per_Action = 1;

        bearGun_U.Name = "熊撃ち銃";
        bearGun_U.AnimName = AnimationName.Null;
        bearGun_U.EffectNum.Add(EffNum.Damage, 3);
        bearGun_U.Cost = 3;
        bearGun_U.Timing = ACTION;
        bearGun_U.MinRange = 0;
        bearGun_U.MaxRange = 2;
        bearGun_U.Weight = 1;

        scope_H.Name = "スコープ";
        scope_H.AnimName = AnimationName.Null;
        scope_H.EffectNum.Add(EffNum.Judge, 2);
        scope_H.Cost = 0;
        scope_H.Timing = JUDGE;
        scope_H.MinRange = 10;
        scope_H.MaxRange = 10;
        scope_H.Weight = 1;
        
        kanhu_H.Name = "カンフー";
        kanhu_H.AnimName = AnimationName.Null;
        kanhu_H.EffectNum.Add(EffNum.Count, 1);
        kanhu_H.Cost = 0;
        kanhu_H.Timing = COUNT;
        kanhu_H.MinRange = 10;
        kanhu_H.MaxRange = 10;
        kanhu_H.Weight = 1;

        //追加
        meitou_U.Name = "名刀";
        meitou_U.AnimName = AnimationName.Null;
        meitou_U.EffectNum.Add(EffNum.Damage, 3);
        meitou_U.Cost = 2;
        meitou_U.Timing = ACTION;
        meitou_U.MinRange = 0;
        meitou_U.MaxRange = 0;
        meitou_U.Weight = 1;
        meitou_U.Atk.isCutting = true;

        kanhu2_H.Name = "カンフー";
        kanhu2_H.AnimName = AnimationName.Null;
        kanhu2_H.EffectNum.Add(EffNum.Count, 1);
        kanhu2_H.Cost = 0;
        kanhu2_H.Timing = COUNT;
        kanhu2_H.MinRange = 10;
        kanhu2_H.MaxRange = 10;
        kanhu2_H.Weight = 1;

        tale_B.Name = "しっぽ";
        tale_B.AnimName = AnimationName.Null;
        tale_B.EffectNum.Add(EffNum.Count, 1);
        tale_B.Cost = 0;
        tale_B.Timing = COUNT;
        tale_B.MinRange = 10;
        tale_B.MaxRange = 10;
        tale_B.Weight = 1;

        uroko_B.Name = "鋼鉄";
        uroko_B.AnimName = AnimationName.Null;
        uroko_B.EffectNum.Add(EffNum.Guard, 2);
        uroko_B.Cost = 0;
        uroko_B.Timing = DAMAGE;
        uroko_B.MinRange = 10;
        uroko_B.MaxRange = 10;
        uroko_B.Weight = 1;

        wirelille_A.Name = "ワイヤーリール";
        wirelille_A.AnimName = AnimationName.Null;
        wirelille_A.EffectNum.Add(EffNum.Move, 1);
        wirelille_A.Cost = 3;
        wirelille_A.Timing = RAPID;
        wirelille_A.MinRange = 10;
        wirelille_A.MaxRange = 10;
        wirelille_A.Weight = 1;
    }


}
