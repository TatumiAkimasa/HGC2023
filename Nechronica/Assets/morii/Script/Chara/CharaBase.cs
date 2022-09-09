using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaBase : MonoBehaviour
{
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

    private int MaxCount;                       //�J�E���g�ő�l
    private int NowCount;                       //���݂̃J�E���g
    private int AllWeight;                      //�d��
}

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

public class ManeuverEffectsAtk
{
    public int AtkType;       //�U������
    public  bool isExplosion;   //�����U�����ǂ���
    public bool isCotting;     //�ؒf�U�����ǂ���
    public bool isAllAttack;   //�S�̍U�����ǂ���
    public bool isSuccession;  //�A�����ǂ���
    public int Num_per_Action;//�A����
}


