using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjEnemy : ClassData_
{
    [SerializeField]
    private Table_Enemy Enemy;

    [SerializeField]
    private kihonnpatu kihon;

    [SerializeField]
    private int EnemyAI;                  

    public Doll_blueprint me;

    private void Start()
    {
        //���������ǋL
        for (int i = 0; i != kihon.GET_MAX_BASE_PARTS(); i++)
        {
            me.CharaBase_data.HeadParts.Add(kihon.Base_Head_parts[i]);

            me.CharaBase_data.ArmParts.Add(kihon.Base_Arm_parts[i]);

            me.CharaBase_data.BodyParts.Add(kihon.Base_Body_parts[i]);

            me.CharaBase_data.LegParts.Add(kihon.Base_Leg_parts[i]);
        }

        //�f�[�^�����͂��A�}�j���[�o�[��ǉ�����
        //�ǉ������ǋL
        for (int SITE = 0; SITE != 4; SITE++)
        {
            for (int i = 0; i != Enemy.Wepons[SITE].Parts.Count; i++)
            {
                //None���𔲂��ɂ��Đ���
                if (SITE == HEAD)
                {
                    me.CharaBase_data.HeadParts.Add(Enemy.Wepons[HEAD].Parts[i].Maneuver);

                }
                if (SITE == ARM)
                {
                    me.CharaBase_data.ArmParts.Add(Enemy.Wepons[ARM].Parts[i].Maneuver);

                }
                if (SITE == BODY)
                {
                    me.CharaBase_data.BodyParts.Add(Enemy.Wepons[BODY].Parts[i].Maneuver);

                }
                if (SITE == LEG)
                {
                    me.CharaBase_data.LegParts.Add(Enemy.Wepons[LEG].Parts[i].Maneuver);

                }

            }
        }
    }

    public void EnemyAI_Action()
    {
        //�̂���PL�̂ݎ擾����悤�˗�
        List<Doll_blu_Nor> PlayerDolls = ManagerAccessor.Instance.battleSystem.GetCharaObj();

        //PL
        //PlayerDolls

        // �G�L�����̃G���A�ƑI�����ꂽ�}�j���[�o�̎˒����Βl�Ŕ�ׂāA�˒����ł���΍U�����邩�I������R�}���h��\������
        // �G�L�����̃G���A�̐�Βl���U���̍ő�˒��ȉ����A
        // �G�L�����̃G���A�̐�Βl���U���̍ŏ��˒��ȏ�Ȃ�U������

        //�g�p����for��

        //�G�̈ʒu����&�˒���r
        //for (int i = 0; i != PlayerDolls.Count; i++)
        //{
            
        //    if (this.GetComponent<Doll_blu_Nor>()/*�g�p����ԍ�*/. != 10 &&
        //        (Mathf.Abs(PlayerDolls[i].area) <= Mathf.Abs(dollManeuver.MaxRange + actingChara.area) &&
        //         Mathf.Abs(PlayerDolls[i].area) >= Mathf.Abs(dollManeuver.MinRange + actingChara.area)))
        //    {
        //        atkTargetEnemy = move;
        //        atkTargetEnemy.transform.GetChild(CANVAS).gameObject.SetActive(true);

        //        exeButton.onClick.AddListener(() => OnClickAtk(move.GetComponent<Doll_blu_Nor>()));

        //        this.transform.GetChild(CANVAS).transform.GetChild(ATKBUTTONS).gameObject.SetActive(true);
        //    }
        //}
    }
}
