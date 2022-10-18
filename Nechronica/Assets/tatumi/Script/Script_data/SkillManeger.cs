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

    public int[] parts=new int[9];


    private string keyword = "x";
    private string keyword2 = "X";

    [SerializeField]
    private int    Button_num = 0;

    [SerializeField]
    Button[] buttons = new Button[3];

    Button[] Parent_Skill = new Button[2];

    public Wepon_Maneger wepon_Maneger;
    public Chara_data_input Chara_intpu_cs;

    // Start is called before the first frame update
    void Start()
    {
        //初期化
        for(int i=0;i!=9;i++)
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
        //エラー回避
        if (Parent_Skill[MorS] != null)
        {
            //選択色
            ColorBlock cb = a.colors;

            //前に選択したものと同一職でないなら処理(MAIN&SUB)
            if (a.name != Parent_Skill[MorS].name)
            {
                cb.normalColor = Color.yellow;
                cb.selectedColor = Color.yellow;
                a.colors = cb;

                //既存元に戻す
                cb.normalColor = Color.white;
                cb.selectedColor = Color.white;
                Parent_Skill[MorS].colors = cb;

                //代入
                Parent_Skill[MorS] = a;

            }

        }
    }

    public void SetPSKill(Button a)
    {

        int item = -1;

        for(int i=0;i!=3;i++)
        {
            if (buttons[i] == a)
                item = i;
        }

        if (buttons[Button_num] != null)
        {
            //選択色
            ColorBlock cb = a.colors;

            //同一職
            if (a.name.Contains(keyword) && a.name.Contains(keyword2))
            {
                if (item == -1)
                {
                    cb.normalColor = Color.yellow;
                    cb.highlightedColor = Color.yellow;
                    cb.selectedColor = Color.yellow;
                    a.colors = cb;

                    //既存元に戻す
                    cb.normalColor = Color.white;
                    cb.highlightedColor = Color.white;
                    cb.selectedColor = Color.white;
                    buttons[Button_num].colors = cb;

                    if (buttons[Button_num].GetComponent<Wepon_Data_SaveSet>() != null)
                        Chara_intpu_cs.Doll_data.CharaBase_data.Skill.Remove(buttons[Button_num].GetComponent<Wepon_Data_SaveSet>().GetParts());

                    //代入
                    buttons[Button_num] = a;

                    //出力
                    if (Button_num == 0)
                        Output_text.text = a.GetComponentInChildren<Text>().text;
                    else if (Button_num == 1)
                        Output_text2.text = a.GetComponentInChildren<Text>().text;
                    else
                        Output_text3.text = a.GetComponentInChildren<Text>().text;

                    Button_num++;


                    if (Button_num == 3)
                        Button_num = 0;

                    Chara_intpu_cs.Doll_data.CharaBase_data.Skill.Add(a.GetComponent<Wepon_Data_SaveSet>().GetParts());

                    if (a.gameObject.GetComponent<Wepon_Bonus_Add>() != null)
                        a.gameObject.GetComponent<Wepon_Bonus_Add>().Bounus_Wepon_add(1);
                        

                }
               // 同じもの選択
                else
                {
                    //既存元に戻す
                    cb.normalColor = Color.white;
                    cb.highlightedColor = Color.white;
                    cb.selectedColor = Color.white;
                    buttons[item].colors = cb;

                    Chara_intpu_cs.Doll_data.CharaBase_data.Skill.Remove(buttons[item].GetComponent<Wepon_Data_SaveSet>().GetParts());

                    if (a.gameObject.GetComponent<Wepon_Bonus_Add>() != null)
                        a.gameObject.GetComponent<Wepon_Bonus_Add>().Bounus_Wepon_add(0);

                    buttons[item] = this.gameObject.GetComponent<Button>();

                    //出力
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
                if (item == -1)
                {
                    cb.normalColor = Color.yellow;
                    cb.highlightedColor = Color.yellow;
                    cb.selectedColor = Color.yellow;
                    a.colors = cb;

                    //既存元に戻す
                    cb.normalColor = Color.white;
                    cb.highlightedColor = Color.white;
                    cb.selectedColor = Color.white;
                    buttons[Button_num].colors = cb;

                    if (buttons[Button_num].GetComponent<Wepon_Data_SaveSet>() != null)
                        Chara_intpu_cs.Doll_data.CharaBase_data.Skill.Remove(buttons[Button_num].GetComponent<Wepon_Data_SaveSet>().GetParts());

                    //代入
                    buttons[Button_num] = a;

                    //出力
                    if (Button_num == 0)
                        Output_text.text = a.GetComponentInChildren<Text>().text;
                    else
                        Output_text2.text = a.GetComponentInChildren<Text>().text;

                    Button_num++;

                    if (Button_num >= 2)
                        Button_num = 0;

                    Chara_intpu_cs.Doll_data.CharaBase_data.Skill.Add(a.GetComponent<Wepon_Data_SaveSet>().GetParts());

                    if (a.gameObject.GetComponent<Wepon_Bonus_Add>() != null)
                    {
                        a.gameObject.GetComponent<Wepon_Bonus_Add>().Bounus_Wepon_add(1);
                    }

                }
                //同じもの選択
                else
                {
                    //既存元に戻す
                    cb.normalColor = Color.white;
                    cb.highlightedColor = Color.white;
                    cb.selectedColor = Color.white;
                    buttons[item].colors = cb;

                    Chara_intpu_cs.Doll_data.CharaBase_data.Skill.Remove(buttons[item].GetComponent<Wepon_Data_SaveSet>().GetParts());

                    if (a.gameObject.GetComponent<Wepon_Bonus_Add>() != null)
                        a.gameObject.GetComponent<Wepon_Bonus_Add>().Bounus_Wepon_add(0);

                    buttons[item] = this.gameObject.GetComponent<Button>();

                    //出力
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

                        //既存元に戻す
                        cb.normalColor = Color.white;
                        cb.highlightedColor = Color.white;
                        cb.selectedColor = Color.white;
                        buttons[2].colors = cb;

                        if (buttons[2].GetComponent<Wepon_Data_SaveSet>() != null)
                            Chara_intpu_cs.Doll_data.CharaBase_data.Skill.Remove(buttons[2].GetComponent<Wepon_Data_SaveSet>().GetParts());

                        //代入
                        buttons[2] = a;

                        //出力
                        Output_text3.text = a.GetComponentInChildren<Text>().text;

                        Chara_intpu_cs.Doll_data.CharaBase_data.Skill.Add(a.GetComponent<Wepon_Data_SaveSet>().GetParts());

                        if (a.gameObject.GetComponent<Wepon_Bonus_Add>() != null)
                            a.gameObject.GetComponent<Wepon_Bonus_Add>().Bounus_Wepon_add(0);

                    }
                    //同じもの選択
                    else
                    {
                        //既存元に戻す
                        cb.normalColor = Color.white;
                        cb.highlightedColor = Color.white;
                        cb.selectedColor = Color.white;
                        buttons[2].colors = cb;

                        Chara_intpu_cs.Doll_data.CharaBase_data.Skill.Remove(buttons[2].GetComponent<Wepon_Data_SaveSet>().GetParts());

                        if (a.gameObject.GetComponent<Wepon_Bonus_Add>() != null)
                            a.gameObject.GetComponent<Wepon_Bonus_Add>().Bounus_Wepon_add(1);

                        buttons[2] = this.gameObject.GetComponent<Button>();

                        //出力
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

        ClassPoint_Update();

        for (int i = 0; i < Cskils.transform.childCount; i++)
        {
            //取得
            GameObject test = Cskils.transform.GetChild(i).gameObject;

            //選択許可命令
            if (test.name.Contains(keyword) && test.name.Contains(keyword2))
            {
               
                test.SetActive(true);
            }
            else if (test.name.Contains(keyword)||test.name.Contains(keyword2))
            {
               
                test.SetActive(true);

                //星スキル封印
                if (test.name.Contains("7"))
                {
                   
                    test.SetActive(false);
                    //skill初期化
                    for (int k = 0; k != 3; k++)
                    {
                        if (test.name.Contains(buttons[k].name))
                        {
                            //選択色
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
                //skill初期化
                for (int k = 0; k != 3; k++)
                {
                    if (test.name.Contains(buttons[k].name))
                    {
                        //選択色
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

    public string GetKeyWord_main()
    {
        return keyword;
    }

    public string GetKeyWord_sub()
    {
        return keyword2;
    }

    public int GetArmament()
    {
        int i= int.Parse(wepon_text.text);
        return i;
    }

    public int GetVariantt()
    {
        return int.Parse(bio_text.text);
    }

    public int GetAlter()
    {
        return int.Parse(mac_text.text);
    }

    public void ClassPoint_Update()
    {
        wepon_text.text = (parts[0] + parts[3] + parts[6]).ToString();
        wepon_Maneger.Wepon_limit[0] = parts[0] + parts[3] + parts[6];
        wepon_Maneger.Reset_wepon(ARMAMENT);

        bio_text.text = (parts[1] + parts[4] + parts[7]).ToString();
        wepon_Maneger.Wepon_limit[1] = parts[1] + parts[4] + parts[7];
        wepon_Maneger.Reset_wepon(VARIANT);

        mac_text.text = (parts[2] + parts[5] + parts[8]).ToString();
        wepon_Maneger.Wepon_limit[2] = parts[2] + parts[5] + parts[8];
        wepon_Maneger.Reset_wepon(ALTER);
    }
}
