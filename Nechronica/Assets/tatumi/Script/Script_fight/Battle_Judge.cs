//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Battle_Judge : MonoBehaviour
//{
//    //現在の操作可能プレイ人数
//    const int MAXCHARA = 1;

//    //受け取り口
//    private BattleSystem battleSystem;
//    //選択可能用bool
//    private bool[] JudgeOK_Chara;

//    // キャラのオブジェクトを実際に使用するためのクラス
//    [SerializeField]
//    private List<Doll_blu_Nor> charaObject = new List<Doll_blu_Nor>();

//    // Start is called before the first frame update
//    void Start()
//    {
//        battleSystem = ManagerAccessor.Instance.battleSystem;

//    }

//    // Update is called once per frame
//    void Update()
//    {

//    }

//    public void JudgeTiming(Doll_blu_Nor TargetChara)
//    {
//        //選択可能枠を再設定
//        JudgeOK_Chara = new bool[battleSystem.GetCharaObj().Count];

//        //全キャラの中からジャッジパーツが打てる奴らを判別
//        for (int Charanum = 0; Charanum != battleSystem.GetCharaObj().Count; Charanum++)
//        {
//            // 各部位パーツのアクション、ラピッドタイミングのパーツを取得
//            for (int i = 0; i < battleSystem.GetCharaObj().GetHeadParts().Count; i++)
//            {
//                //ジャッジかつ、使えるとき
//                if (battleSystem.charaObject[Charanum].GetHeadParts()[i].Timing == CharaBase.JUDGE && !battleSystem.charaObject[Charanum].GetHeadParts()[i].isDmage)
//                {
//                    //射程内の物を判別
//                    if (Mathf.Abs(battleSystem.charaObject[Charanum].potition - TargetChara.potition) > battleSystem.charaObject[Charanum].GetHeadParts()[i].MaxRange)
//                        ;
//                    else if (Mathf.Abs(battleSystem.charaObject[Charanum].potition - TargetChara.potition) < battleSystem.charaObject[Charanum].GetHeadParts()[i].MinRange)
//                        ;
//                    //多分ここで技登録がいい希ガス
//                    else
//                        JudgeOK_Chara[Charanum] = true;
//                }

//            }

//            for (int i = 0; i < battleSystem.charaObject[Charanum].GetArmParts().Count; i++)
//            {
//                //ジャッジかつ、使えるとき
//                if (battleSystem.charaObject[Charanum].GetArmParts()[i].Timing == CharaBase.JUDGE && !battleSystem.charaObject[Charanum].GetArmParts()[i].isDmage)
//                {
//                    //射程内の物を判別
//                    if (Mathf.Abs(battleSystem.charaObject[Charanum].potition - TargetChara.potition) > battleSystem.charaObject[Charanum].GetArmParts()[i].MaxRange)
//                        ;
//                    else if (Mathf.Abs(battleSystem.charaObject[Charanum].potition - TargetChara.potition) < battleSystem.charaObject[Charanum].GetArmParts()[i].MinRange)
//                        ;
//                    //多分ここで技登録がいい希ガス
//                    else
//                        JudgeOK_Chara[Charanum] = true;
//                }

//            }

//            for (int i = 0; i < battleSystem.charaObject[Charanum].GetLegParts().Count; i++)
//            {
//                //ジャッジかつ、使えるとき
//                if (battleSystem.charaObject[Charanum].GetLegParts()[i].Timing == CharaBase.JUDGE && !battleSystem.charaObject[Charanum].GetLegParts()[i].isDmage)
//                {
//                    //射程内の物を判別
//                    if (Mathf.Abs(battleSystem.charaObject[Charanum].potition - TargetChara.potition) > battleSystem.charaObject[Charanum].GetLegParts()[i].MaxRange)
//                        ;
//                    else if (Mathf.Abs(battleSystem.charaObject[Charanum].potition - TargetChara.potition) < battleSystem.charaObject[Charanum].GetLegParts()[i].MinRange)
//                        ;
//                    //多分ここで技登録がいい希ガス
//                    else
//                        JudgeOK_Chara[Charanum] = true;
//                }

//            }

//            for (int i = 0; i < battleSystem.charaObject[Charanum].GetBodyParts().Count; i++)
//            {
//                //ジャッジかつ、使えるとき
//                if (battleSystem.charaObject[Charanum].GetBodyParts()[i].Timing == CharaBase.JUDGE && !battleSystem.charaObject[Charanum].GetBodyParts()[i].isDmage)
//                {
//                    //射程内の物を判別
//                    if (Mathf.Abs(battleSystem.charaObject[Charanum].potition - TargetChara.potition) > battleSystem.charaObject[Charanum].GetBodyParts()[i].MaxRange)
//                        ;
//                    else if (Mathf.Abs(battleSystem.charaObject[Charanum].potition - TargetChara.potition) < battleSystem.charaObject[Charanum].GetBodyParts()[i].MinRange)
//                        ;
//                    //多分ここで技登録がいい希ガス
//                    else
//                        JudgeOK_Chara[Charanum] = true;
//                }
//            }

//        }

//        //プレイやーが操作できる子たち
//        for (int i = 0; i != MAXCHARA; i++)
//        {
//            battleSystem.charaObject[Charanum].gameObject.GetComponent<BattleCommand>().ジャッジクリック許可的な物＋押せるよアピール;
//        }

//        //NPCの子たち
//        for (int i = MAXCHARA; i != battleSystem.charaObject.Count(); i++)
//        {
//            battleSystem.charaObject[Charanum].gameObject.GetComponent<BattleCommand>().敵ジャッジ許可;
//        }
//    }
//}
