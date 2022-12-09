using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Doll_blueprint
{
    public string Name;                    //�h�[���� 
    public string hide_hint;              //�Î�
    public string Death_year;              //���N
    public string temper;                  //�|�W�V����
    public short[] Memory;                 //�L���̂�����
    public string MainClass, SubClass;     //�E��
    public short Armament, Variant, Alter; //����,�ψ�,����
    public int InitArea;                 //�����ʒu
    
    public List<Item> Item;              //�����A�C�e��
    public CharaBase_SaveData CharaBase_data;
    public Chara_Field_SaveData CharaField_data;
}

[System.Serializable]
public class Chara_Field_SaveData
{
    public string Scene_Name;
    public float[] Pos = new float[3];
    public string PosStr;
    public EventFlag[] Event = new EventFlag[2];
    public string[] Time = new string[2];
}

[System.Serializable]
public class EventFlag
{
    public string str;
    public bool flag;
}

[System.Serializable]
public class Item
{
    public string Tiltle;
    public string str;
}

[System.Serializable]
public class CharaBase_SaveData
{
    //�^�C�~���O�̒萔
    public const int COUNT = -1;
    public const int AUTO = 0;
    public const int ACTION = 1;
    public const int MOVE = 2;
    public const int RAPID = 3;
    public const int JUDGE = 4;
    public const int DAMAGE = 5;

    //�Q�b�^�[
    public int GetMaxCount() => maxCount;
    public int GetNowCount() => nowCount;
    public int GetWeight() => allWeight;

    public List<CharaManeuver> GetHeadParts() => HeadParts; // ���p�[�c�Q��
    public List<CharaManeuver> GetArmParts() => ArmParts;   // �r�p�[�c�Q��
    public List<CharaManeuver> GetBodyParts() => BodyParts; // ���̃p�[�c�Q��
    public List<CharaManeuver> GetLegParts() => LegParts;   // �r�p�[�c�Q��

    public int GetALLParts() => HeadParts.Count + ArmParts.Count + BodyParts.Count + LegParts.Count;

    public List<CharaManeuver> HeadParts;      // ���̃p�[�c
    public List<CharaManeuver> ArmParts;       // �r�̃p�[�c
    public List<CharaManeuver> BodyParts;      // ���̃p�[�c
    public List<CharaManeuver> LegParts;       // �r�̃p�[�c
    public List<CharaManeuver> Skill;       // �r�̃p�[�c

    //�ő�s���l�v�Z
    public void MaxCountCal()
    {
        for (int i = 0; i < HeadParts.Count; i++)
        {
            //�ő�s���l���Z�p�[�c���j�����Ă��Ȃ���΍ő�s���l���Z
            if (!HeadParts[i].isDmage && HeadParts[i].Timing == COUNT)
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




