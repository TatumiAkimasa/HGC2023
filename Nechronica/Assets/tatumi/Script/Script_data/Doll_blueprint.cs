using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public short potition;                 //�����z�u
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
    //�Q�b�^�[
    public int GetMaxCount() => MaxCount;
    public int GetNowCount() => NowCount;
    public int GetWeight() => AllWeight;
    public int GetALLParts()=> HeadParts.Count + ArmParts.Count + BodyParts.Count + LegParts.Count;
    

    public List<CharaManeuver> GetHeadParts() => HeadParts; //���p�[�c�Q��
    public List<CharaManeuver> GetArmParts() => ArmParts;   //�r�p�[�c�Q��
    public List<CharaManeuver> GetBodygParts() => BodyParts;//���̃p�[�c�Q��
    public List<CharaManeuver> GetLegParts() => LegParts;   //�r�p�[�c�Q��
    public List<CharaManeuver> GetSkillParts() => Skill;      //SKILL�̃p�[�c
   

    public List<CharaManeuver> HeadParts;      //���̃p�[�c
    public List<CharaManeuver> ArmParts;       //�r�̃p�[�c
    public List<CharaManeuver> BodyParts;      //���̃p�[�c
    public List<CharaManeuver> LegParts;       //�r�̃p�[�c
    public List<CharaManeuver> Skill;          //SKILL�̃p�[�c

    private int MaxCount;                       //�J�E���g�ő�l
    private int NowCount;                       //���݂̃J�E���g
    private int AllWeight;                      //�d��
}

[System.Serializable]
public class CharaManeuver_SaveData
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
public class ManeuverEffectsAtk_SaveData
{
    public int AtkType;       //�U������
    public bool isExplosion;   //�����U�����ǂ���
    public bool isCotting;     //�ؒf�U�����ǂ���
    public bool isAllAttack;   //�S�̍U�����ǂ���
    public bool isSuccession;  //�A�����ǂ���
    public int Num_per_Action;//�A����
}







