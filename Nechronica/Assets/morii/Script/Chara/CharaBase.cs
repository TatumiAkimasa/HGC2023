using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class CharaBase : MonoBehaviour
{
    //�^�C�~���O�̒萔
    protected const int COUNT  = -1;
    protected const int AUTO   = 0;
    protected const int ACTION = 1;
    protected const int RAPID  = 2;
    protected const int JUDGE  = 3;
    protected const int DAMAGE = 4;

    //�Q�b�^�[
    public int GetMaxCount() => MaxCount;
    public int GetNowCount() => NowCount;
    public int GetWeight() => AllWeight;

    public List<CharaManeuver> GetHeadParts() => HeadParts; //���p�[�c�Q��
    public List<CharaManeuver> GetArmParts() => ArmParts;   //�r�p�[�c�Q��
    public List<CharaManeuver> GetBodygParts() => BodyParts;//���̃p�[�c�Q��
    public List<CharaManeuver> GetLegParts() => LegParts;   //�r�p�[�c�Q��

    public List<CharaManeuver> HeadParts;      //���̃p�[�c
    public List<CharaManeuver> ArmParts;       //�r�̃p�[�c
    public List<CharaManeuver> BodyParts;      //���̃p�[�c
    public List<CharaManeuver> LegParts;       //�r�̃p�[�c

    //�ő�s���l�v�Z
    public void MaxCountCal()
    {
        for(int i=0;i<HeadParts.Count;i++)
        {
            //�ő�s���l���Z�p�[�c���j�����Ă��Ȃ���΍ő�s���l���Z
            if(!HeadParts[i].isDmage&& HeadParts[i].Timing==COUNT)
            {
                MaxCount += HeadParts[i].EffectNum;
            }
        }
        for (int i = 0; i < ArmParts.Count; i++)
        {
            //�ő�s���l���Z�p�[�c���j�����Ă��Ȃ���΍ő�s���l���Z
            if (!ArmParts[i].isDmage && ArmParts[i].Timing == COUNT)
            {
                MaxCount += ArmParts[i].EffectNum;
            }
        }
        for (int i = 0; i < BodyParts.Count; i++)
        {
            //�ő�s���l���Z�p�[�c���j�����Ă��Ȃ���΍ő�s���l���Z
            if (!BodyParts[i].isDmage && BodyParts[i].Timing == COUNT)
            {
                MaxCount += BodyParts[i].EffectNum;
            }
        }
        for (int i = 0; i < LegParts.Count; i++)
        {
            //�ő�s���l���Z�p�[�c���j�����Ă��Ȃ���΍ő�s���l���Z
            if (!LegParts[i].isDmage && LegParts[i].Timing == COUNT)
            {
                MaxCount += LegParts[i].EffectNum;
            }
        }
    }

    public void IncreaseNowCount()
    {
        NowCount += MaxCount;
    }

    protected int MaxCount = 6;                   //�J�E���g�ő�l ���[������Ƃ��ƍő�s���l��6����̂�6�ŏ�����
    protected int NowCount;                       //���݂̃J�E���g
    protected int AllWeight;                      //�d��

    [SerializeField]
    protected Image CharaImg;
    [SerializeField]
    protected Text CharaName;
}   
    
[System.Serializable]
public class CharaManeuver
{
    public string Name;            //�p�[�c��
    public int EffectNum;          //���ʒl
    public int Cost;               //�R�X�g
    public int Timing;             //�����^�C�~���O
    public int MinRange;           //�˒��̍ŏ��l
    public int MaxRange;           //�˒��̍ő�l
    public int Weight;             //�d��
    public int Moving;             //�ړ���(0�ňړ����Ȃ�)
    public bool isUse;             //�g�p�������ǂ���
    public bool isDmage;           //�j���������ǂ���
    public ManeuverEffectsAtk Atk; //�U���n
}

[System.Serializable]
public class ManeuverEffectsAtk
{
    public int AtkType;       //�U������
    public  bool isExplosion;   //�����U�����ǂ���
    public bool isCotting;     //�ؒf�U�����ǂ���
    public bool isAllAttack;   //�S�̍U�����ǂ���
    public bool isSuccession;  //�A�����ǂ���
    public int Num_per_Action;//�A����
}


