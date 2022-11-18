using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class MultiSite
{
    [Header("��")]
    public Text[] Text=new Text [2];

    public MultiSite(Text[] _multisite)
    {
        Text = _multisite;
    }
}

[System.Serializable]
public class MultiSite2
{
    [Header("�i")]
    public MultiSite[] Step=new MultiSite[3];

    public MultiSite2(MultiSite[] _multisite)
    {
        Step = _multisite;
    }
}

public class Wepon_Maneger : ClassData_
{
    [SerializeField, Header("�������z��-����")]
    public MultiSite2[] Site_;

    [SerializeField]
    //������/���x��/���Ă���x��
    Toggle[,,] Wepon = new Toggle[3,3,3];

    [Header("�s���p�[�c���Text")]
    public Text[] ErrorText;

    public int[] Wepon_limit = new int[3];

    private int Reset_num = 0;

    private const int MAX_BONUS_WEPON = 3;
    private const int MAX_WEPON_NUM = 3;

    public int [] Bonus_Parts_int = new int [MAX_BONUS_WEPON + 1];
  

    // Start is called before the first frame update
    void Awake()
    {
        Maneger_Accessor.Instance.weponManeger_cs = this;
    }

    private int Bounus_Parts(int Level,int Type)
    {
        if (Level == 2)
            return Type;
        else
            return 3;
    }

    public void Add_Wepon(Toggle add,int Type,int Level)
    {
        //���ꂼ��I�ׂȂ����̂�I�����Ƃ��ɉ������Ȃ��悤�ɂ���v�Z�p�ϐ�
        int Max_Wepon_num = (Wepon_limit[Type] - 1) / 3;
        int parts_num_add = (Wepon_limit[Type] - 1) % 3;
        //���̕���E���x���őI�ׂ�鐔�ۑ��p�ϐ�
        int Serect_parts = 1;

        //���������l��0�Ȃ牽�����Ȃ�
        if (Wepon_limit[Type] == 0)
        {
            add.isOn = false;
            return;
        }
        //�l1�̎�2,3�I�񂾂�r��
        else if (Wepon_limit[Type] < Level + Max_Wepon_num + 1)
        {
            add.isOn = false;
            return;
        }

        if(parts_num_add>=Level)
        {
            Serect_parts += Max_Wepon_num;
        }
            
        //                  �@�I�ׂ鐔    + Skill�ɂ�鑝����
        for (int i = 0; i != Serect_parts + Bonus_Parts_int[Bounus_Parts(Level,Type)]; i++)
        {
            //�f�[�^�Ȃ��̏ꍇ
            if (Wepon[Type, Level, i] == null)
            {
                NameChange(add, true);
                Wepon[Type, Level, i] = add;

                WeponLimit_TextChange();
                return;
            }
            //2��ڂ̎����𔲂�
            else if(Wepon[Type, Level, i]==add)
            {
                NameChange(add, false);
                Wepon[Type, Level, i].isOn = false;
                Wepon[Type, Level, i] = null;

                WeponLimit_TextChange();
                return;
            }
           
        }

       

        if (Serect_parts + Bonus_Parts_int[Bounus_Parts(Level, Type)] <= Reset_num)
            Reset_num = 0;

        //�������x�����{�Œǉ�������
        //�������𔲂���
        NameChange(Wepon[Type, Level, Reset_num], false);
        Wepon[Type, Level, Reset_num].isOn = false;
        Wepon[Type, Level, Reset_num] = null;

        //�V�K�������
        Wepon[Type, Level, Reset_num] = add;
        NameChange(Wepon[Type, Level, Reset_num], true);

        Reset_num++;
        WeponLimit_TextChange();
    }

    public void Reset_wepon(int Type)
    {
        //���Z�b�g
        for (int Level = 0; Level != 3; Level++)
        {
            
            for (int i = 0; i != MAX_WEPON_NUM; i++)
            {
                if (Wepon[Type, Level, i] != null)
                {
                    NameChange(Wepon[Type, Level, i], false);
                    Wepon[Type, Level, i].isOn = false;
                    Wepon[Type, Level, i] = null;
                }

            }

        }

        Reset_num = 0;
    }

    private void NameChange(Toggle wepon,bool mode)
    {
        int SITE = -1;

        //�r
        if(wepon.name.EndsWith("A"))
        {
            SITE = ARM;
        }
        //��
        else if (wepon.name.EndsWith("H"))
        {
            SITE = HEAD;
        }
        //��
        else if (wepon.name.EndsWith("B"))
        {
            SITE = BODY;
        }
        //�r
        else if (wepon.name.EndsWith("L"))
        {
            SITE = LEG;
        }
        //�S����
        else if (wepon.name.EndsWith("S"))
        {
            return;
        }

        for (int i = 0; i != 2; i++)
        {
            for (int k = 0; k != 3; k++)
            {
                //����
                if (mode == true)
                {
                    //��񂪖������
                    if (Site_[SITE].Step[i].Text[k].text == "None")
                    {
                        Site_[SITE].Step[i].Text[k].text = wepon.GetComponent<WeponData_Set>().Get_Wepon_Text();
                        Site_[SITE].Step[i].Text[k].GetComponent<Wepon_Data_SaveSet>().SetParts(wepon.GetComponent<Wepon_Data_SaveSet>().GetParts());
                        return;
                    }
                    //����΂���[�B
                }
                //�L�����Z��
                else
                {
                    //�����񂪂���Ώ�����
                    if (Site_[SITE].Step[i].Text[k].text == wepon.GetComponent<WeponData_Set>().Get_Wepon_Text())
                    {
                        Site_[SITE].Step[i].Text[k].text = "None";
                        Site_[SITE].Step[i].Text[k].GetComponent<Wepon_Data_SaveSet>().Reset();
                        return;
                    }
                    //�Ȃ���΂���[�B
                }
            }
        }
    }

    private void WeponLimit_TextChange()
    {
        for(int Type = 0; Type!=3;Type++)
        {
            for(int Level=0;Level!=3;Level++)
            {
                //���ꂼ��I�ׂȂ����̂�I�����Ƃ��ɉ������Ȃ��悤�ɂ���v�Z�p�ϐ�
                int Max_Wepon_num = (Wepon_limit[Type] - 1) / 3;
                int parts_num_add = (Wepon_limit[Type] - 1) % 3;
                //���̕���E���x���őI�ׂ�鐔�ۑ��p�ϐ�
                int Serect_parts = 1;

                //���������l��0�Ȃ牽�����Ȃ�
                if (Wepon_limit[Type] == 0)
                {
                    Serect_parts = 0;
                }
                //�l1�̎�2,3�I�񂾂�r��
                else if (Wepon_limit[Type] < Level + Max_Wepon_num + 1)
                {
                    Serect_parts = 0;
                }

                if (parts_num_add >= Level)
                {
                    Serect_parts += Max_Wepon_num + Bounus_Parts(Level,Type);
                }

                for(int i=0;i!=3;i++)
                {
                    if (Wepon[Type, Level, i] != null)
                        Serect_parts--;
                }
                
                ErrorText[Type + Level].text = "LEVEL" + Level.ToString() + ":"+Serect_parts.ToString();
                
            }
        }
    }
}
