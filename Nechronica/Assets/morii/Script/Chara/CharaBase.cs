using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class CharaBase : MonoBehaviour
{
    //�萔----------------------------------
    //�^�C�~���O�̒萔
    public const int COUNT  = -1;
    public const int AUTO   = 0;
    public const int ACTION = 1;
    public const int MOVE   = 2;
    public const int RAPID  = 3;
    public const int JUDGE  = 4;
    public const int DAMAGE = 5;

    //�U���^�C�v�̒萔
    public const int MELEE = 0;         // �����U��
    public const int VIOLENCE = 1;      // ���e�U��
    public const int SHOOTING = 2;      // �ˌ��U��
    //--------------------------------------

    //�Q�b�^�[
    public int GetMaxCount() => maxCount;
    public int GetNowCount() => nowCount;
    public int GetWeight() => allWeight;
    public List<CharaManeuver> GetPotisionSkill() => positionSkill; // �|�W�V�����X�L���Q��
    public List<CharaManeuver> GetClassSkill() => classSkill;   // �N���X�X�L��

    [SerializeField] protected List<CharaManeuver> headParts;       // ���̃p�[�c
    [SerializeField] protected List<CharaManeuver> armParts;        // �r�̃p�[�c
    [SerializeField] protected List<CharaManeuver> bodyParts;       // ���̃p�[�c
    [SerializeField] protected List<CharaManeuver> legParts;        // �r�̃p�[�c

    public List<CharaManeuver> HeadParts
    {
        get { return headParts; }
        set { headParts = value; }
    }
    public List<CharaManeuver> ArmParts
    {
        get { return armParts; }
        set { armParts = value; }
    }
    public List<CharaManeuver> BodyParts
    {
        get { return bodyParts; }
        set { bodyParts = value; }
    }
    public List<CharaManeuver> LegParts
    {
        get { return legParts; }
        set { legParts = value; }
    }

    public List<CharaManeuver> positionSkill;   // �|�W�V�����X�L��

    public List<CharaManeuver> classSkill;      // �N���X�X�L��
                                                  
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
        for (int i = 0; i < LegParts.Count; i++)
        {
            //�ő�s���l���Z
            if (!LegParts[i].isDmage && LegParts[i].Timing == COUNT)
            {
                maxCount += classSkill[i].EffectNum[EffNum.Count];
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
    [SerializeField]protected int nowCount;                       // ���݂̃J�E���g
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
    public string Name;            // �p�[�c�� /
    public string AnimName;        // �A�j���[�V����ID
    public GameObject AnimEffect;  // �A�j���[�V�����Đ��p
    //public int EffectNum;        // ���ʒl
    public Dictionary<string, int> EffectNum = new Dictionary<string, int>();          // ���ʒl
    public int Cost;               // �R�X�g
    public int Timing;             // �����^�C�~���O
    public int MinRange;           // �˒��̍ŏ��l
    public int MaxRange;           // �˒��̍ő�l
    public int Weight;             // �d��
    public int Moving;             // �ړ���(0�ňړ����Ȃ�)
    public bool isUse;             // �g�p�������ǂ���
    public bool isDmage;           // �j���������ǂ���
    public ManeuverEffectsAtk Atk; // �U���n
    [NamedArrayAttribute(new string[] { "�U��", "�h��", "�x���E��", "�W�Q" ,"�����D��x"})]
    public List<short> EnemyAI; //�G�s���D�揇��
}

[System.Serializable]
public class ManeuverEffectsAtk
{
    public int atkType = -1;        // �U������
    public bool isExplosion;   // �����U�����ǂ���
    public bool isCutting;     // �ؒf�U�����ǂ���
    public bool isAllAttack;   // �S�̍U�����ǂ���
    public bool isFallDown;    // �]�|���ǂ���
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
    public const string YobunnnaUde = "YobunnnaUde";   // �]���Șr�A���̎�͂���ŔF��
    public const string Nikunotate = "Nikunotate";  // ���̏��͂���ŔF��


}

[System.Serializable]
public class AnimationName
{
    public const string Null = "Null";
    public const string Ago  = "Ago";
    public const string Kobushi = "Kobushi";

}


