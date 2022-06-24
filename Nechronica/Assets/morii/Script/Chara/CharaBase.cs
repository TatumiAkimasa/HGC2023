using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaBase : MonoBehaviour
{
    //�Q�b�^�[
    public int GetNowCount()  => NowCount;
    public int GetWeight() => Weight;

    List<CharaManeuver> HeadParts;      //���̃p�[�c
    List<CharaManeuver> ArmParts;       //�r�̃p�[�c
    List<CharaManeuver> BodyParts;      //���̃p�[�c
    List<CharaManeuver> LegParts;       //�r�̃p�[�c

    int MaxCount;                       //�J�E���g�ő�l
    int NowCount;                       //���݂̃J�E���g

    int Weight;                         //�d��
}

public class CharaManeuver
{
    string Name;            //�p�[�c��
    int EffectNum;          //���ʒl
    int Cost;               //�R�X�g
    int Timing;             //�����^�C�~���O
    int MinRange;           //�˒��̍ŏ��l
    int MaxRange;           //�˒��̍ő�l
    bool isUse;             //�g�p�������ǂ���
    bool isDmage;           //�j���������ǂ���
    ManeuverEffectsAtk Atk; //�U���n
}

public class ManeuverEffectsAtk
{
    int  AtkType;       //�U������
    bool isExplosion;   //�����U�����ǂ���
    bool isCotting;     //�ؒf�U�����ǂ���
    bool isAllAttack;   //�S�̍U�����ǂ���
    bool isSuccession;  //�A�����ǂ���
    int  Num_per_Action;//�A����
}
