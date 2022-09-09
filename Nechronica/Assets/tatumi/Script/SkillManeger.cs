using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillManeger : ClassData_
{
    [SerializeField]
    private GameObject Cskils;

    private GameObject[] ChildObject;

    [SerializeField]
    private Text wepon_text, bio_text,mac_text,Output_text2,Output_text3,Output_text;

    public int[] parts=new int[6];

   
    private string keyword = "x";
    private string keyword2 = "X";

    [SerializeField]
    private int    Button_num = 0;

    [SerializeField]
    Button[] buttons = new Button[3];

    Button[] Parent_Skill = new Button[2];

    public Wepon_Maneger wepon_Maneger;

    // Start is called before the first frame update
    void Start()
    {
        //������
        for(int i=0;i!=6;i++)
        {
            parts[i] = 0;
        }
        for (int i = 0; i != 3; i++)
        {
            buttons[i] = this.gameObject.GetComponent<Button>();

            if(i!=2)
                Parent_Skill[i] = this.gameObject.GetComponent<Button>();
        }

        keyword = "x";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetParent_SKill(Button a,int MorS)
    {
        //�G���[���
        if (Parent_Skill[MorS] != null)
        {
            //�I��F
            ColorBlock cb = a.colors;

            //�O�ɑI���������̂Ɠ���E�łȂ��Ȃ珈��(MAIN&SUB)
            if (a.name != Parent_Skill[MorS].name)
            {
                cb.normalColor = Color.yellow;
                cb.selectedColor = Color.yellow;
                a.colors = cb;

                //�������ɖ߂�
                cb.normalColor = Color.white;
                cb.selectedColor = Color.white;
                Parent_Skill[MorS].colors = cb;

                //���
                Parent_Skill[MorS] = a;

            }

        }
    }

    public void SetPSKill(Button a)
    {

        if (buttons[Button_num] != null)
        {
            //�I��F
            ColorBlock cb = a.colors;

            //����E
            if (a.name.Contains(keyword)&& a.name.Contains(keyword2))
            {
                if (a.name != buttons[Button_num - Button_num].name)
                {
                    cb.normalColor = Color.yellow;
                    cb.highlightedColor = Color.yellow;
                    cb.selectedColor = Color.yellow;
                    a.colors = cb;

                    //�������ɖ߂�
                    cb.normalColor = Color.white;
                    cb.highlightedColor = Color.white;
                    cb.selectedColor = Color.white;
                    buttons[Button_num].colors = cb;

                    //���
                    buttons[Button_num] = a;

                    //�o��
                    if (Button_num == 0)
                        Output_text.text = a.GetComponentInChildren<Text>().text;
                    else if(Button_num==1)
                        Output_text2.text = a.GetComponentInChildren<Text>().text;
                    else
                        Output_text3.text = a.GetComponentInChildren<Text>().text;

                    Button_num++;

                    if (Button_num == 3)
                        Button_num = 0;

                }
                //�������̑I��
                else
                {
                    //�������ɖ߂�
                    cb.normalColor = Color.white;
                    cb.highlightedColor = Color.white;
                    cb.selectedColor = Color.white;
                    buttons[Button_num - Button_num].colors = cb;

                    buttons[Button_num - Button_num] = this.gameObject.GetComponent<Button>();

                    //�o��
                    if (Button_num == 0)
                        Output_text.text = "None";
                    else if (Button_num == 1)
                        Output_text2.text = "None";
                    else
                        Output_text3.text = "None";
                }
            }
            //MainSkill
            else if (a.name.Contains(keyword))
            {
                if (a.name != buttons[Button_num-Button_num].name)
                {
                    cb.normalColor = Color.yellow;
                    cb.highlightedColor = Color.yellow;
                    cb.selectedColor = Color.yellow;
                    a.colors = cb;

                    //�������ɖ߂�
                    cb.normalColor = Color.white;
                    cb.highlightedColor = Color.white;
                    cb.selectedColor = Color.white;
                    buttons[Button_num].colors = cb;

                    //���
                    buttons[Button_num] = a;

                    //�o��
                    if (Button_num == 0)
                        Output_text.text = a.GetComponentInChildren<Text>().text;
                    else
                        Output_text2.text = a.GetComponentInChildren<Text>().text;

                    Button_num++;

                    if (Button_num >= 2)
                        Button_num = 0;

                }
                //�������̑I��
                else
                {
                    //�������ɖ߂�
                    cb.normalColor = Color.white;
                    cb.highlightedColor = Color.white;
                    cb.selectedColor = Color.white;
                    buttons[Button_num-Button_num].colors = cb;

                    buttons[Button_num - Button_num] = this.gameObject.GetComponent<Button>();

                    //�o��
                    if (Button_num == 0)
                        Output_text.text = "None";
                    else
                        Output_text2.text = "None";
                }

            }
            //SubSkill
            else if (a.name.Contains(keyword2))
            {
                if (buttons[2] != null)
                {
                    if (a.name != buttons[2].name)
                    {
                        cb.normalColor = Color.yellow;
                        cb.highlightedColor = Color.yellow;
                        cb.selectedColor = Color.yellow;
                        a.colors = cb;

                        //�������ɖ߂�
                        cb.normalColor = Color.white;
                        cb.highlightedColor = Color.white;
                        cb.selectedColor = Color.white;
                        buttons[2].colors = cb;

                        //���
                        buttons[2] = a;

                        //�o��
                        Output_text3.text = a.GetComponentInChildren<Text>().text;

                    }
                    //�������̑I��
                    else
                    {
                        //�������ɖ߂�
                        cb.normalColor = Color.white;
                        cb.highlightedColor = Color.white;
                        cb.selectedColor = Color.white;
                        buttons[2].colors = cb;

                        buttons[2] = this.gameObject.GetComponent<Button>();

                        //�o��
                        Output_text3.text = "None";
                    }
                }
            }

        }
    }

    public void Setparts(ClassData a,bool MorS)
    {
        if(MorS)
        {
            parts[0] = a.parts[0];
            parts[1] = a.parts[1];
            parts[2] = a.parts[2];

            //mainskill
            keyword = Classname_JtoE(a.name);
        }
        else
        {
            parts[3] = a.parts[0];
            parts[4] = a.parts[1];
            parts[5] = a.parts[2];

            //subskill
            keyword2 = Classname_JtoE(a.name);
        }

       


        wepon_text.text = (parts[0]+parts[3]).ToString();
        wepon_Maneger.Wepon_limit[0] = parts[0] + parts[3];


        bio_text.text = (parts[1]+parts[4]).ToString();
        wepon_Maneger.Wepon_limit[1] = parts[1] + parts[4];

        mac_text.text = (parts[2]+parts[5]).ToString();
        wepon_Maneger.Wepon_limit[2] = parts[2] + parts[5];

        for (int i = 0; i < Cskils.transform.childCount; i++)
        {
            //�擾
            GameObject test = Cskils.transform.GetChild(i).gameObject;

            //�I��������
            if (test.name.Contains(keyword) && test.name.Contains(keyword2))
            {
               
                test.SetActive(true);
            }
            else if (test.name.Contains(keyword)||test.name.Contains(keyword2))
            {
               
                test.SetActive(true);

                //���X�L������
                if (test.name.Contains("7"))
                {
                   
                    test.SetActive(false);
                    //skill������
                    for (int k = 0; k != 3; k++)
                    {
                        if (test.name.Contains(buttons[k].name))
                        {
                            //�I��F
                            ColorBlock cb = buttons[k].colors;
                            cb.normalColor = Color.white;
                            cb.highlightedColor = Color.white;
                            cb.selectedColor = Color.white;
                            buttons[k].colors = cb;

                            if (k == 0)
                            {
                                Output_text.text = "None";

                            }
                            else if (k == 1)
                            {
                                Output_text2.text = "None";
                            }
                            else
                            {

                                Output_text3.text = "None";
                            }
                        }
                    }
                }
            }
            else
            {
                test.SetActive(false);
                //skill������
                for (int k = 0; k != 3; k++)
                {
                    if (test.name.Contains(buttons[k].name))
                    {
                        //�I��F
                        ColorBlock cb = buttons[k].colors;
                        cb.normalColor = Color.white;
                        cb.highlightedColor = Color.white;
                        cb.selectedColor = Color.white;
                        buttons[k].colors = cb;

                        if (k == 0)
                        {
                            Output_text.text = "None";
                            
                        }
                        else if (k == 1)
                        {
                            Output_text2.text = "None";
                        }
                        else
                        {
                            
                            Output_text3.text = "None";
                        }
                    }
                }
            }

        }
    }
}
