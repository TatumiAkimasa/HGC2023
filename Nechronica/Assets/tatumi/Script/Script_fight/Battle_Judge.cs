//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Battle_Judge : MonoBehaviour
//{
//    //���݂̑���\�v���C�l��
//    const int MAXCHARA = 1;

//    //�󂯎���
//    private BattleSystem battleSystem;
//    //�I���\�pbool
//    private bool[] JudgeOK_Chara;

//    // �L�����̃I�u�W�F�N�g�����ۂɎg�p���邽�߂̃N���X
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
//        //�I���\�g���Đݒ�
//        JudgeOK_Chara = new bool[battleSystem.GetCharaObj().Count];

//        //�S�L�����̒�����W���b�W�p�[�c���łĂ�z��𔻕�
//        for (int Charanum = 0; Charanum != battleSystem.GetCharaObj().Count; Charanum++)
//        {
//            // �e���ʃp�[�c�̃A�N�V�����A���s�b�h�^�C�~���O�̃p�[�c���擾
//            for (int i = 0; i < battleSystem.GetCharaObj().GetHeadParts().Count; i++)
//            {
//                //�W���b�W���A�g����Ƃ�
//                if (battleSystem.charaObject[Charanum].GetHeadParts()[i].Timing == CharaBase.JUDGE && !battleSystem.charaObject[Charanum].GetHeadParts()[i].isDmage)
//                {
//                    //�˒����̕��𔻕�
//                    if (Mathf.Abs(battleSystem.charaObject[Charanum].potition - TargetChara.potition) > battleSystem.charaObject[Charanum].GetHeadParts()[i].MaxRange)
//                        ;
//                    else if (Mathf.Abs(battleSystem.charaObject[Charanum].potition - TargetChara.potition) < battleSystem.charaObject[Charanum].GetHeadParts()[i].MinRange)
//                        ;
//                    //���������ŋZ�o�^��������K�X
//                    else
//                        JudgeOK_Chara[Charanum] = true;
//                }

//            }

//            for (int i = 0; i < battleSystem.charaObject[Charanum].GetArmParts().Count; i++)
//            {
//                //�W���b�W���A�g����Ƃ�
//                if (battleSystem.charaObject[Charanum].GetArmParts()[i].Timing == CharaBase.JUDGE && !battleSystem.charaObject[Charanum].GetArmParts()[i].isDmage)
//                {
//                    //�˒����̕��𔻕�
//                    if (Mathf.Abs(battleSystem.charaObject[Charanum].potition - TargetChara.potition) > battleSystem.charaObject[Charanum].GetArmParts()[i].MaxRange)
//                        ;
//                    else if (Mathf.Abs(battleSystem.charaObject[Charanum].potition - TargetChara.potition) < battleSystem.charaObject[Charanum].GetArmParts()[i].MinRange)
//                        ;
//                    //���������ŋZ�o�^��������K�X
//                    else
//                        JudgeOK_Chara[Charanum] = true;
//                }

//            }

//            for (int i = 0; i < battleSystem.charaObject[Charanum].GetLegParts().Count; i++)
//            {
//                //�W���b�W���A�g����Ƃ�
//                if (battleSystem.charaObject[Charanum].GetLegParts()[i].Timing == CharaBase.JUDGE && !battleSystem.charaObject[Charanum].GetLegParts()[i].isDmage)
//                {
//                    //�˒����̕��𔻕�
//                    if (Mathf.Abs(battleSystem.charaObject[Charanum].potition - TargetChara.potition) > battleSystem.charaObject[Charanum].GetLegParts()[i].MaxRange)
//                        ;
//                    else if (Mathf.Abs(battleSystem.charaObject[Charanum].potition - TargetChara.potition) < battleSystem.charaObject[Charanum].GetLegParts()[i].MinRange)
//                        ;
//                    //���������ŋZ�o�^��������K�X
//                    else
//                        JudgeOK_Chara[Charanum] = true;
//                }

//            }

//            for (int i = 0; i < battleSystem.charaObject[Charanum].GetBodyParts().Count; i++)
//            {
//                //�W���b�W���A�g����Ƃ�
//                if (battleSystem.charaObject[Charanum].GetBodyParts()[i].Timing == CharaBase.JUDGE && !battleSystem.charaObject[Charanum].GetBodyParts()[i].isDmage)
//                {
//                    //�˒����̕��𔻕�
//                    if (Mathf.Abs(battleSystem.charaObject[Charanum].potition - TargetChara.potition) > battleSystem.charaObject[Charanum].GetBodyParts()[i].MaxRange)
//                        ;
//                    else if (Mathf.Abs(battleSystem.charaObject[Charanum].potition - TargetChara.potition) < battleSystem.charaObject[Charanum].GetBodyParts()[i].MinRange)
//                        ;
//                    //���������ŋZ�o�^��������K�X
//                    else
//                        JudgeOK_Chara[Charanum] = true;
//                }
//            }

//        }

//        //�v���C��[������ł���q����
//        for (int i = 0; i != MAXCHARA; i++)
//        {
//            battleSystem.charaObject[Charanum].gameObject.GetComponent<BattleCommand>().�W���b�W�N���b�N���I�ȕ��{�������A�s�[��;
//        }

//        //NPC�̎q����
//        for (int i = MAXCHARA; i != battleSystem.charaObject.Count(); i++)
//        {
//            battleSystem.charaObject[Charanum].gameObject.GetComponent<BattleCommand>().�G�W���b�W����;
//        }
//    }
//}
