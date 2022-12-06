using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BattleSpone : MonoBehaviour
{
    // �萔---------------------------
    public const int RAKUEN   = 4;
    public const int HANAZONO = 3;
    public const int RENGOKU  = 2;
    public const int JIGOKU   = 1;
    public const int NARAKU   = 0;

    //--------------------------------

    [SerializeField]
    private GameObject[] allySponeField = new GameObject[5];             // �����L������z�u���邽�߂�nullObj�����O�ɗp��

    [SerializeField]
    private GameObject[] enemySponeField = new GameObject[5];             // �����L������z�u���邽�߂�nullObj�����O�ɗp��

    [SerializeField]
    private BattleSystem battleSystem;

    [SerializeField]
    private List<Doll_blu_Nor> charaObject = new List<Doll_blu_Nor>();

    private int[] charaCount = new int[5];         // �����L�����\���G���A�̂ǂ��ɕ\�����邩���f����悤
    private int[] enemyCount = new int[5];         // �G�L�����\���G���A�̂ǂ��ɕ\�����邩���f����悤

    private void Awake()
    {
        ManagerAccessor.Instance.battleSpone = this;
    }

    public List<Doll_blu_Nor> CharaSpone(List<Doll_blu_Nor> array)
    {
        // array�̏���charaObject�Ɋi�[
        for(int i=0;i<array.Count;i++)
        {
            charaObject.Add(array[i]);
        }

        for (int i=0;i<charaObject.Count;i++)
        {
            // �G�L�����������L���������f
            if(charaObject[i].gameObject.CompareTag("AllyChara"))
            {
                switch(charaObject[i].area)
                {
                    case RAKUEN:
                        charaObject[i].transform.position = allySponeField[RAKUEN].transform.GetChild(charaCount[RAKUEN]).position;
                        charaCount[RAKUEN]++;
                        break;
                    case HANAZONO:
                        charaObject[i].transform.position = allySponeField[HANAZONO].transform.GetChild(charaCount[HANAZONO]).position;
                        charaCount[HANAZONO]++;
                        break;
                    case RENGOKU:
                        charaObject[i].transform.position = allySponeField[RENGOKU].transform.GetChild(charaCount[RENGOKU]).position;
                        charaCount[RENGOKU]++;
                        break;
                }
            }
            if(charaObject[i].gameObject.CompareTag("EnemyChara"))
            {
                switch (charaObject[i].area)
                {
                    case RAKUEN:
                        charaObject[i].transform.position = enemySponeField[RAKUEN].transform.GetChild(enemyCount[RAKUEN]).position;
                        enemyCount[RAKUEN]++;
                        break;
                    case HANAZONO:
                        charaObject[i].transform.position = enemySponeField[HANAZONO].transform.GetChild(enemyCount[HANAZONO]).position;
                        enemyCount[HANAZONO]++;
                        break;
                    case RENGOKU:
                        charaObject[i].transform.position = enemySponeField[RENGOKU].transform.GetChild(enemyCount[RENGOKU]).position;
                        enemyCount[RENGOKU]++;
                        break;
                    case JIGOKU:
                        charaObject[i].transform.position = enemySponeField[JIGOKU].transform.GetChild(enemyCount[JIGOKU]).position;
                        enemyCount[JIGOKU]++;
                        break;
                    case NARAKU:
                        charaObject[i].transform.position = enemySponeField[NARAKU].transform.GetChild(enemyCount[NARAKU]).position;
                        enemyCount[NARAKU]++;
                        break;
                }
            }

        }

        return charaObject;
    }

    /// <summary>
    /// �L�������ړ������郁�\�b�h
    /// </summary>
    /// <param name="chara">�ړ�����L����</param>
    /// <param name="moveAmount">�ړ���</param>
    public void CharaMove(Doll_blu_Nor chara, int moveAmount)
    {
        if(chara.gameObject.CompareTag("AllyChara"))
        {
            chara.transform.position = allySponeField[chara.area + moveAmount].transform.GetChild(charaCount[chara.area + moveAmount]).position;
            // ���G���A����ړ�����̂ŁA���G���A�̃L�������݃J�E���g��1���炷
            charaCount[chara.area]--;
            // �ړ�������̃G���A�̃L�������݃J�E���g��1���₷
            charaCount[chara.area + moveAmount]++;
            chara.area = chara.area + moveAmount;
        }
        else if (chara.gameObject.CompareTag("EnemyChara"))
        {
            chara.transform.position = enemySponeField[chara.area + moveAmount].transform.GetChild(charaCount[chara.area + moveAmount]).position;
            // ���G���A����ړ�����̂ŁA���G���A�̃L�������݃J�E���g��1���炷
            charaCount[chara.area]--;
            // �ړ�������̃G���A�̃L�������݃J�E���g��1���₷
            chara.area = chara.area + moveAmount;
        }
    }
}

[System.Serializable]
class NullCharaObj
{
    public GameObject[] chara = new GameObject[4];
}