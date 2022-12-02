using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class CharaBase : MonoBehaviour
{
    //�^�C�~���O�̒萔
    public const int COUNT  = -1;
    public const int AUTO   = 0;
    public const int ACTION = 1;
    public const int MOVE   = 2;
    public const int RAPID  = 3;
    public const int JUDGE  = 4;
    public const int DAMAGE = 5;

    //�Q�b�^�[
    public int GetMaxCount() => maxCount;
    public int GetNowCount() => nowCount;
    public int GetWeight() => allWeight;

    public List<CharaManeuver> GetHeadParts() => HeadParts; // ���p�[�c�Q��
    public List<CharaManeuver> GetArmParts() => ArmParts;   // �r�p�[�c�Q��
    public List<CharaManeuver> GetBodyParts() => BodyParts; // ���̃p�[�c�Q��
    public List<CharaManeuver> GetLegParts() => LegParts;   // �r�p�[�c�Q��

    public List<CharaManeuver> HeadParts;      // ���̃p�[�c
    public List<CharaManeuver> ArmParts;       // �r�̃p�[�c
    public List<CharaManeuver> BodyParts;      // ���̃p�[�c
    public List<CharaManeuver> LegParts;       // �r�̃p�[�c
                                                  
    //�ő�s���l�v�Z
    public void MaxCountCal()
    {
        for(int i=0;i<HeadParts.Count;i++)
        {
            //�ő�s���l���Z�p�[�c���j�����Ă��Ȃ���΍ő�s���l���Z
            if(!HeadParts[i].isDmage&& HeadParts[i].Timing==COUNT)
            {
                maxCount += HeadParts[i].EffectNum[EffNum.Count];
            }
        }
        for (int i = 0; i < ArmParts.Count; i++)
        {
            //�ő�s���l���Z�p�[�c���j�����Ă��Ȃ���΍ő�s���l���Z
            if (!ArmParts[i].isDmage && ArmParts[i].Timing == COUNT)
            {
                maxCount += ArmParts[i].EffectNum[EffNum.Count];
            }
        }
        for (int i = 0; i < BodyParts.Count; i++)
        {
            //�ő�s���l���Z�p�[�c���j�����Ă��Ȃ���΍ő�s���l���Z
            if (!BodyParts[i].isDmage && BodyParts[i].Timing == COUNT)
            {
                maxCount += BodyParts[i].EffectNum[EffNum.Count];
            }
        }
        for (int i = 0; i < LegParts.Count; i++)
        {
            //�ő�s���l���Z�p�[�c���j�����Ă��Ȃ���΍ő�s���l���Z
            if (!LegParts[i].isDmage && LegParts[i].Timing == COUNT)
            {
                maxCount += LegParts[i].EffectNum[EffNum.Count];
            }
        }
    }

    /// <summary>
    /// �s���͉񕜃��\�b�h
    /// </summary>
    public void IncreaseNowCount()
    {
        nowCount += maxCount;
    }

    protected int maxCount = 6;                   // �J�E���g�ő�l ���[������Ƃ��ƍő�s���l��6����̂�6�ŏ�����
    protected int nowCount;                       // ���݂̃J�E���g
    public int NowCount
    {
        get { return nowCount; }
        set { nowCount = value; }
    }
    protected int allWeight;                      // �d��

    [SerializeField]
    protected Image CharaImg;
    [SerializeField]
    protected Text CharaName;
}   
    
[System.Serializable]
public class CharaManeuver
{
    public string Name;            // �p�[�c��
    //public int EffectNum;        // ���ʒl
    public Dictionary<string,int> EffectNum = new Dictionary<string, int>();          // ���ʒl
    public int Cost;               // �R�X�g
    public int Timing;             // �����^�C�~���O
    public int MinRange;           // �˒��̍ŏ��l
    public int MaxRange;           // �˒��̍ő�l
    public int Weight;             // �d��
    public int Moving;             // �ړ���(0�ňړ����Ȃ�)
    public bool isUse;             // �g�p�������ǂ���
    public bool isDmage;           // �j���������ǂ���
    public ManeuverEffectsAtk Atk; // �U���n
}

[System.Serializable]
public class ManeuverEffectsAtk
{
    public int atkType;        // �U������
    public bool isExplosion;   // �����U�����ǂ���
    public bool isCutting;     // �ؒf�U�����ǂ���
    public bool isAllAttack;   // �S�̍U�����ǂ���
    public bool isSuccession;  // �A�����ǂ���
    public int Num_per_Action; // �A����
}

[System.Serializable]
public class EffNum
{
    public const string Damage   = "Damage";
    public const string Guard    = "Guard";
    public const string Judge    = "Judge";
    public const string Move     = "Move";
    public const string Count    = "Count";
    public const string Insanity = "Insanity";      // ���C�_���֗^������̂͂��������
    public const string Extra    = "Extra";
    
    
    // �I�����[�����̌���
    public const string Protect  = "Protect";       // ���΂��̌��ʂ͂���ŔF��
}


