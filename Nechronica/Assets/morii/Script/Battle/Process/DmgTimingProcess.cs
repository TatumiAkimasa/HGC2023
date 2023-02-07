using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DmgTimingProcess : GetClickedGameObject
{
    //-------------------------------
    // �ق�����񃁃�
    // �U������enemy
    // �^�[�Q�b�g�ɂȂ��Ă閡���L����
    //-------------------------------

    // �萔
    public const int HEAD = 10;
    public const int ARM  = 9;
    public const int BODY = 8;
    public const int LEG  = 7;
    //--------------------------------

    private bool siteSelect = false;    // �_�C�X���[���̒l��10��葽���Ƃ���true�ɂ���

    private bool deleteAddEff = false;  // ���̏��Ȃǂɂ��ǉ����ʂ̑ŏ������ʂ̃}�j���[�o���������ꂽ���ǂ����̗L��
    private bool deleteCutEff = false;  // �I�[�g�^�C�~���O�Ȃǂɂ��ؒf�ŏ������ʂ̃}�j���[�o���������ꂽ���ǂ����̗L��
    private bool deleteExproEff = false;// �I�[�g�^�C�~���O�Ȃǂɂ�锚���ŏ������ʂ̃}�j���[�o���������ꂽ���ǂ����̗L��
    private bool deleteFDEff = false;   // �I�[�g�^�C�~���O�Ȃǂɂ��]�|�ŏ������ʂ̃}�j���[�o���������ꂽ���ǂ����̗L���iFD��FallDown�̗��j

    private int addDamage = 0;          // �_���[�W�^�C�~���O�̃}�j���[�o��ʖ�ǉ��_���[�W
    private int giveDamage = 0;         // �^����_���[�W
    private int dmgGuard = 0;           // �^����_���[�W�����̕ϐ��̒l�����炷

    private bool isAddEffectStep;       // �ǉ����ʂ����邩�m�F����X�e�b�v
    private int exprosionSite;          // �����ɂ��ǉ��_���[�W�̕��ʑI��  

    private Unity.Mathematics.Random randoms;

    Doll_blu_Nor selectedAllyChara;

    [SerializeField] private Button nextButton;
    [SerializeField] private GameObject confirmatButton;    // �������邩�ǂ����̊m�F����{�^���̃Q�[���I�u�W�F�N�g
    [SerializeField] private GameObject damageButtons;      // �_���[�W�^�C�~���O�̃{�^���̐e�I�u�W�F�N�g

    [SerializeField] private GameObject siteSelectHead;  // ���ʑI���̃{�^��
    [SerializeField] private GameObject siteSelectArm;   // ���ʑI���̃{�^��
    [SerializeField] private GameObject siteSelectBody;  // ���ʑI���̃{�^��
    [SerializeField] private GameObject siteSelectLeg;   // ���ʑI���̃{�^��

    private GameObject reflectParts;        // ���߁[�����󂯂镔�ʂ̃o�b�t�@
    private bool isStanbyReflectSelect;     // �v���C���[���_���[�W���󂯂��p�[�c��I������̂�҂��߂̕ϐ�
    private int damageSelectCnt;            // �_���[�W���󂯂��p�[�c�𐔂���
    private string siteName;               // �_���[�W���󂯂镔�ʂ̖��O

    public int DamageSelectCnt
    {
        get { return damageSelectCnt; }
        set { damageSelectCnt = value; }
    }

    private int rollResult;             // �_�C�X���[���̌��ʂ̒l
    private Doll_blu_Nor damageChara;   // �_���[�W���󂯂�\��̃L����
    private CharaManeuver actManeuver;     // �A�N�V�����^�C�~���O�Ŕ������ꂽ�R�}���h�̊i�[�ꏊ

    private bool isStandbyCutRoll = false;     // �ؒf����ҋ@
    private bool isCutRoll = false;     // �ؒf����ҋ@
    [SerializeField] private int  cutRollResult = 0;          // �ؒf����̌��ʂ̒l

    [SerializeField] private Text rollResultText;
    [SerializeField] private Button diceRollButton; // �_�C�X���[�����s���{�^��
    [SerializeField] private Image diceRollButtonImg;       // �_�C�X���o�ȂǂɎg���{�^���̉摜
    [SerializeField] private Animator diceRollAnim;         // �_�C�X���[���̃A�j��

    private List<Doll_blu_Nor> damageCharas = new List<Doll_blu_Nor>();
    private List<Doll_blu_Nor> damageAllyCharas = new List<Doll_blu_Nor>();

    private int continuousAtk = 0;      // �A��
    public int SetContinuousAtk(int num) => continuousAtk = num;
    public GameObject GetConfirmatButton() => confirmatButton;
    public GameObject GetDamageButtons() => damageButtons;
    public void SetDamageChara(Doll_blu_Nor value) => damageChara = value;
    public void SetRollResult(int value) => rollResult = value;
    public CharaManeuver ActMneuver
    {
        get { return actManeuver; }
        set { actManeuver = value; }
    }

    public int GiveDamage
    {
        get { return giveDamage; }
        set { giveDamage = value; }
    }

    private void Awake()
    {
        ProcessAccessor.Instance.dmgTiming = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(isStandbyCharaSelect)
        {
            CharaSelectStandby();
        }
        else if(isStandbyCutRoll)
        {
            // �p��if�������B�G���������B
            // �G�̏ꍇ�͓G�̐ؒf���菈���ֈڍs
            // �����̏ꍇ�̓v���C���[���ؒf����̏������s��
            if (isCutRoll)
            {
                isStandbyCutRoll = false;
                isCutRoll = false;

                CutRollJudge();
            }
        }
        else if(isAddEffectStep)
        {
            JudgeAddEffect();
        }
        else if(isStanbyReflectSelect)
        {
            // �񐔕��_���[�W���󂯂�p�[�c��I��������_���[�W�^�C�~���O���I������
            if (reflectParts == null && damageAllyCharas.Count != 0)
            {
                ReflectPartsIndecation(damageAllyCharas[0].gameObject, siteName);
            }

            if(giveDamage==damageSelectCnt)
            {
                reflectParts.SetActive(false);
                if (damageAllyCharas.Count != 0)
                {
                    damageAllyCharas.RemoveAt(0);
                    reflectParts = null;
                }
                else
                {
                    isStanbyReflectSelect = false;
                    EndFlowProcess();
                }
            }
        }
    }

    /// <summary>
    /// �J�������߂Â��Ă���R�}���h��\�����郁�\�b�h
    /// </summary>
    /// <param name="charaCommand">�N���b�N���ꂽ�L�����̎q�I�u�W�F�N�g�i�R�}���h�j</param>
    /// <returns></returns>
    override protected IEnumerator MoveStandby(GameObject move)
    {
        for (int i = 0; i < 2; i++)
        {
            //�J�������N���b�N�����L�����ɋ߂Â��܂ő҂�
            if (i == 0)
            {
                yield return new WaitForSeconds(0.75f);
            }
            else
            {
                if (move.CompareTag("AllyChara"))
                {
                    isStandbyCharaSelect = false;
                    selectedAllyChara = move.GetComponent<Doll_blu_Nor>();
                    // �I�������L�����̃R�}���h�̃I�u�W�F�N�g���擾
                    childCommand = move.transform.GetChild(CANVAS).transform.GetChild(DAMAGE);
                    // �Z�R�}���h��������\��
                    childCommand.gameObject.SetActive(true);
                }
            }
        }
    }


    protected override void ZoomOutObj()
    {
        base.ZoomOutObj();
        nextButton.gameObject.SetActive(true);
    }

    protected override void ZoomUpObj(GameObject clicked)
    {
        base.ZoomUpObj(clicked);
        nextButton.gameObject.SetActive(false);
    }

    public void OnClickExe()
    {
        ExeManeuver(dollManeuver, selectedAllyChara);
    }


    /// <summary>
    /// ���������}�j���[�o������������̂��m�F����
    /// </summary>
    /// <param name="maneuver">�}�j���[�o�̓��e</param>
    /// <param name="dmgExeChara">�}�j���[�o���g�p�����L�����̏��</param>
    public void ExeManeuver(CharaManeuver maneuver, Doll_blu_Nor dmgExeChara)
    {
        // �_���[�W�𑝉�����}�j���[�o�[�̏���
        if(maneuver.EffectNum.ContainsKey(EffNum.Damage))
        {
            DamageUPProcess(maneuver,dmgExeChara);
        }
        // �h��l�𑝉�����}�j���[�o�[�̏���
        else if(maneuver.EffectNum.ContainsKey(EffNum.Guard))   
        {
            GuardProcess(maneuver, dmgExeChara);
        }
        
        else   // ��̓�ɊY�����Ȃ��ꍇ�A�ŗL�̌��ʂƎg�p����
        {

        }

        // �vif�������B����ȃR�X�g�ǂ������f����
        dmgExeChara.NowCount -= maneuver.Cost;

        // ���J�E���g�œ����L�������_���[�W�^�C�~���O�̃}�j���[�o�[�𔭓������ꍇ�A���J�E���g�ɍs�����ł��Ȃ��Ȃ�̂ō��̕\������ѓ�����\��̃L�����̃��X�g����폜����
        if (dmgExeChara.NowCount==ManagerAccessor.Instance.battleSystem.NowCount)
        {
            ManagerAccessor.Instance.battleSystem.DeleteMoveChara(selectedAllyChara.Name);
        }

        confirmatButton.SetActive(false);
        if(dmgExeChara.gameObject.CompareTag("AllyChara"))
        {
            ZoomOutObj();
        }
    }

    private void DamageUPProcess(CharaManeuver maneuver, Doll_blu_Nor dmgExeChara)
    {
        // �^����_���[�W���オ��n�̏���
        // �˒������g�݂̂̏ꍇ�A�_���[�W��^����L�����ƃ_���[�W�^�C�~���O�œ����L�������������ǂ������ׂ�
        if (maneuver.MinRange == 10)
        {
            if(actingChara == dmgExeChara)
            {
                addDamage += maneuver.EffectNum[EffNum.Damage];
            }
        }
        // �G�L�����̃G���A�ƑI�����ꂽ�}�j���[�o�̎˒����Βl�Ŕ�ׂāA�˒����ł���΍U�����邩�I������R�}���h��\������
        // �G�L�����̃G���A�̐�Βl���U���̍ő�˒��ȉ����A
        // �G�L�����̃G���A�̐�Βl���U���̍ŏ��˒��ȏ�Ȃ甭������
        else if (!RangeCheck(actingChara, maneuver, dmgExeChara))
        {
            addDamage += maneuver.EffectNum[EffNum.Damage];
        }
        else
        {
            // �˒�������܂��񌩂����ȕ\�L����
        }
    }

    private void GuardProcess(CharaManeuver maneuver, Doll_blu_Nor dmgExeChara)
    {
        // �h��Ƃ��̏���
        // �˒������g�݂̂̏ꍇ�A�_���[�W���󂯂�L�����ƃ_���[�W�^�C�~���O�œ����L�������������ǂ������ׂ�
        if (maneuver.MinRange == 10)
        {
            if (damageChara == dmgExeChara)
            {
                dmgGuard += maneuver.EffectNum[EffNum.Guard];
            }
        }
        else if (RangeCheck(damageChara, maneuver, dmgExeChara))
        {
            if(maneuver.EffectNum.ContainsKey(EffNum.Guard))
            {
                dmgGuard += maneuver.EffectNum[EffNum.Guard];
            }
        }
        else
        {
            // �˒�������܂��񌩂����ȕ\�L����
        }
    }

    private void EXProcess(CharaManeuver maneuver, Doll_blu_Nor dmgExeChara)
    {
        // ���΂��̏���
        if (dollManeuver.EffectNum.ContainsKey(EffNum.Protect))
        {
            // �������΂��̏������邩��
            // �vif�������B����ȃR�X�g�ǂ������f����
            selectedAllyChara.NowCount -= dollManeuver.Cost;
        }
        else if (dollManeuver.EffectNum.ContainsKey(EffNum.Nikunotate))
        {
            if (RangeCheck(damageChara, maneuver, dmgExeChara))
            {
                deleteAddEff = true;
            }
            else
            {
                // �˒�������܂���݂����ȕ\�L����
            }
        }
    }

    public void OnClickDiceRoll()
    {
        diceRollAnim.gameObject.SetActive(true);
        rollResultText.text = "";   // �������ז��Ȃ̂ň�U��f�[�^����
        randoms = new Unity.Mathematics.Random((uint)Random.Range(0, 468446876));   //�K���ɃV�[�h�l�ݒ�

        // ���̌�̑�����ז����Ȃ��悤��false�ɂ��Ă���
        diceRollButtonImg.raycastTarget = false;
        diceRollButton.enabled = false;
        StartCoroutine(RollAnimStandby(diceRollAnim, callBack =>
        {
            cutRollResult = randoms.NextInt(1, 11);
            cutRollResult = 8;
            rollResultText.text = cutRollResult.ToString();
            isCutRoll = true;
        }));

    }

    public void OnClickHead()
    {
        siteSelect = true;
        // rollResult�̒l��ς��āA�U�����镔�ʂ�I��
        rollResult = HEAD;
        SiteSelectButtonsActive(false);
    }

    public void OnClickArm()
    {
        siteSelect = true;
        // rollResult�̒l��ς��āA�U�����镔�ʂ�I��
        rollResult = ARM;
        SiteSelectButtonsActive(false);
    }

    public void OnClickBody()
    {
        siteSelect = true;
        // rollResult�̒l��ς��āA�U�����镔�ʂ�I��
        rollResult = BODY;
        SiteSelectButtonsActive(false);
    }

    public void OnClickLeg()
    {
        siteSelect = true;
        // rollResult�̒l��ς��āA�U�����镔�ʂ�I��
        rollResult = LEG;
        SiteSelectButtonsActive(false);
    }

    /// <summary>
    /// ���ʑI���̗L���𔻒肷�鏈��
    /// </summary>
    public void OnClickNextButton()
    {
        // �ŏI�I�ȃ_���[�W�̌��ʂ������A�U�����ꂽ�L�����N�^�[���_���[�W���󂯂�
        // �I�[�g�^�C�~���O�̂��̂����킹�ĉ��Z����\��
        isStandbyCharaSelect = false;

        giveDamage = actManeuver.EffectNum[EffNum.Damage] + addDamage - dmgGuard;

        // rollResult��10��葽���ꍇ�͍U������L�������ǂ��̕��ʂɓ��Ă邩���߂���
        // �vif�������B�T���@���g���z���[�����M�I����
        if (rollResult > 10 )
        {
            // �_�C�X���[���̌��ʂ�10����̏ꍇ�̒ǉ��_���[�W����
            giveDamage = giveDamage + rollResult - 10;
            SiteSelectButtonsActive(true);

            // ���ʑI��ҋ@
            StartCoroutine(SelectDamageSite(callBack=>
            {
                isAddEffectStep = true;
            }));
        }
        else if (rollResult == 6)
        {
            rollResult = 10;
            isAddEffectStep = true;
        }
        else
        {
            // �U���Ώە��ʂɃp�[�c���c���Ă��Ȃ���Ε��ʑI���Ɉڍs
            if(SiteRemainingParts(rollResult))
            {
                SiteSelectButtonsActive(true);
                // ���ʑI��ҋ@
                StartCoroutine(SelectDamageSite(callBack =>
                {
                    isAddEffectStep = true;
                }));
            }
            else
            {
                // ���ʑI�����Ȃ���΂��̂܂ܒǉ����ʔ���ֈڍs������
                isAddEffectStep = true;
            }
        }
    }



    /// <summary>
    /// �ǂ��̕��ʂɃ_���[�W�����邩
    /// </summary>
    /// <param name="site"></param>
    void SortDamageParts(int site)
    {
        if(actManeuver.Atk.isAllAttack�@&& !deleteAddEff)
        {
            for(int i=0;i<ManagerAccessor.Instance.battleSystem.GetCharaObj().Count;i++)
            {
                if(ManagerAccessor.Instance.battleSystem.GetCharaObj()[i].area==damageChara.area)
                {
                    damageCharas.Add(ManagerAccessor.Instance.battleSystem.GetCharaObj()[i]);
                }
            }

            SortDamageParts(site, ref damageCharas);
        }
        else
        {
            if (site == HEAD)
            {
                if(damageChara.CompareTag("EnemyChara"))
                {
                    damageChara.GetComponent<ObjEnemy>().GetDamageUPList(0, giveDamage);
                }
                else
                {
                    isStanbyReflectSelect = true;
                    ReflectPartsIndecation(damageChara.gameObject, "Head");
                }
                //damageChara.HeadParts = GiveDamageParts(damageChara.HeadParts);
            }
            else if (site == ARM)
            {
                if (damageChara.CompareTag("EnemyChara"))
                {
                    damageChara.GetComponent<ObjEnemy>().GetDamageUPList(1, giveDamage);
                }
                else
                {
                    isStanbyReflectSelect = true;
                    ReflectPartsIndecation(damageChara.gameObject, "Arm");
                }
                //damageChara.ArmParts = GiveDamageParts(damageChara.ArmParts);
            }
            else if (site == BODY)
            {
                if (damageChara.CompareTag("EnemyChara"))
                {
                    damageChara.GetComponent<ObjEnemy>().GetDamageUPList(2, giveDamage);
                }
                else
                {
                    isStanbyReflectSelect = true;
                    ReflectPartsIndecation(damageChara.gameObject, "Body");
                }
                //damageChara.BodyParts = GiveDamageParts(damageChara.BodyParts);
            }
            else if (site == LEG)
            {
                if (damageChara.CompareTag("EnemyChara"))
                {
                    damageChara.GetComponent<ObjEnemy>().GetDamageUPList(3, giveDamage);
                }
                else
                {
                    isStanbyReflectSelect = true;
                    ReflectPartsIndecation(damageChara.gameObject, "Leg");
                }
                //damageChara.LegParts = GiveDamageParts(damageChara.LegParts);
            }

            // �ǉ����ʂɓ]�|������U���ł���
            // �ǉ����ʂ�ŏ�������Ă��Ȃ���Ԃ�
            // �����ɂ��ǉ��_���[�W���I���A�������͂Ȃ��ꍇ�ɏ���
            if (actManeuver.Atk.isFallDown && exprosionSite == 0 && !DeleteEffCheck(deleteAddEff, deleteFDEff))
            {
                if (damageChara.NowCount == ManagerAccessor.Instance.battleSystem.NowCount)
                {
                    ManagerAccessor.Instance.battleSystem.DeleteMoveChara(damageChara.Name);
                }
                damageChara.NowCount -= 2;
            }

            if (exprosionSite != 0)
            {
                int buf = exprosionSite;
                exprosionSite = 0;
                SortDamageParts(buf);
            }
            else if(!isStanbyReflectSelect)
            {
                StartCoroutine(ManeuverAnimation(actManeuver, callBack => EndFlowProcess()));
            }
        }
    }

    /// <summary>
    /// �S�̍U���ɂ��_���[�W���󂯂�
    /// </summary>
    /// <param name="site"></param>
    /// <param name="listChara"></param>
    void SortDamageParts(int site, ref List<Doll_blu_Nor> listChara)
    {
        if (site == HEAD)
        {
            for(int i=0;i<listChara.Count;i++)
            {
                if(listChara[i].CompareTag("EnemyChara"))
                {
                    listChara[i].GetComponent<ObjEnemy>().GetDamageUPList(0, giveDamage);
                }
                else
                {
                    isStanbyReflectSelect = true;
                    damageAllyCharas.Add(listChara[i]);
                    siteName = "Head";
                }
            }
        }
        else if (site == ARM)
        {
            for (int i = 0; i < listChara.Count; i++)
            {
                if (listChara[i].CompareTag("EnemyChara"))
                {
                    listChara[i].GetComponent<ObjEnemy>().GetDamageUPList(1, giveDamage);
                }
                else
                {
                    isStanbyReflectSelect = true; 
                    damageAllyCharas.Add(listChara[i]);
                    siteName = "Arm";
                }
            }
        }
        else if (site == BODY)
        {
            for (int i = 0; i < listChara.Count; i++)
            {
                if (listChara[i].CompareTag("EnemyChara"))
                {
                    listChara[i].GetComponent<ObjEnemy>().GetDamageUPList(2, giveDamage);
                }
                else
                {
                    isStanbyReflectSelect = true;
                    damageAllyCharas.Add(listChara[i]);
                    siteName = "Body";
                }
            }
        }
        else if (site == LEG)
        {
            for (int i = 0; i < listChara.Count; i++)
            {
                if (listChara[i].CompareTag("EnemyChara"))
                {
                    listChara[i].GetComponent<ObjEnemy>().GetDamageUPList(3, giveDamage);
                }
                else
                {
                    isStanbyReflectSelect = true;
                    damageAllyCharas.Add(listChara[i]);
                    siteName = "Leg";
                }
                listChara[i].LegParts = GiveDamageParts(listChara[i].LegParts);
            }
        }

        // �ǉ����ʂɓ]�|������U������
        // �ǉ����ʂ�ŏ�������Ă��Ȃ���Ԃ�
        // �����ɂ��ǉ��_���[�W���I���A�������͂Ȃ��ꍇ�ɏ���
        if (actManeuver.Atk.isFallDown && exprosionSite == 0 && !deleteFDEff)
        {
            for (int i = 0; i < listChara.Count; i++)
            {
                if(listChara[i].NowCount == ManagerAccessor.Instance.battleSystem.NowCount)
                {
                    ManagerAccessor.Instance.battleSystem.DeleteMoveChara(listChara[i].Name);
                }
                listChara[i].NowCount -= 2;
            }
        }

        if (exprosionSite != 0)
        {
            int buf = exprosionSite;
            exprosionSite = 0;
            SortDamageParts(buf, ref damageCharas);
        }
        else
        {
            StartCoroutine(ManeuverAnimation(actManeuver, callBack => EndFlowProcess()));
        }
    }

    /// <summary>
    /// ���ۂɃ_���[�W������
    /// </summary>
    /// <param name="site"></param>
    List<CharaManeuver> GiveDamageParts(List<CharaManeuver> site)
    {
        for (int i = 0; i < giveDamage; i++)
        {
            if (i >= site.Count)
            {
                break;
            }
            else if (!site[i].isDmage)
            {
                site[i].isDmage = true;
            }
        }

        return site;
    }

    /// <summary>
    /// �ǉ����ʂ����邩�ǂ������肷�鏈��
    /// </summary>
    void JudgeAddEffect()
    {
        // �ǉ����ʑŏ������������Ă��邩�ǂ���
        if(deleteAddEff)
        {
            SortDamageParts(rollResult);
        }
        else
        {
            if (actManeuver.Atk.isExplosion && !deleteExproEff)
            {
                // �����ɂ���Ăǂ��̕��ʂɃ_���[�W�����邩���肷�鏈���B
                // �U�����ʂ��r�A���̂ǂ��炩�Ȃ�I�������o��
                StartCoroutine(SelectExplosionSite(callBack =>
                {
                    isAddEffectStep = false;
                    SortDamageParts(rollResult);
                }));
            }
            else if (actManeuver.Atk.isCutting && !deleteCutEff)
            {
                isStandbyCutRoll = true;
                if(damageChara.CompareTag("EnemyChara"))
                {
                    OnClickDiceRoll();
                }
                else
                {
                    diceRollButton.gameObject.SetActive(true);
                }
            }
            else
            {
                SortDamageParts(rollResult);
            }
        }

        isAddEffectStep = false;
    }


    /// <summary>
    /// �A�j���[�V�����I���܂ő҂���
    /// </summary>
    /// <param name="maneuver"></param>
    /// <param name="callBack"></param>
    /// <returns></returns>
    public IEnumerator ManeuverAnimation(CharaManeuver maneuver, System.Action<bool> callBack)
    {
        GameObject instance;
        if (maneuver.AnimEffect!=null)
        {
            instance = Instantiate(maneuver.AnimEffect, damageChara.transform.position, Quaternion.identity, damageChara.transform);
            EffctEnd effctEnd = instance.GetComponent<EffctEnd>();

            while (!effctEnd.AnimEnd)
            {
                yield return null;  
            }
        }
        else
        {
            yield break;
        }

        if(instance!=null)
        {
            Destroy(instance);
        }


        callBack(true);
    }

    /// <summary>
    /// �_���[�W��^���镔�ʂ�I������̂�҂���
    /// </summary>
    /// <param name="callBack"></param>
    /// <returns></returns>
    public IEnumerator SelectDamageSite(System.Action<bool> callBack)
    {
        while(!siteSelect)
        {
            yield return null;
        }

        siteSelect = false;
        callBack(true);
    }

    public void ReflectPartsIndecation(GameObject chara, string partsName)
    {
        Transform children = chara.transform.GetChild(0).GetComponentInChildren<Transform>();
        foreach (Transform child in children)
        {
            if(child.name==partsName)
            {
                reflectParts = child.gameObject;
                reflectParts.SetActive(true);
                break;
            }
        }
    }

    /// <summary>
    /// �ǉ����ʂ�ł������}�j���[�o�����邩�ǂ����`�F�b�N���郁�\�b�h
    /// </summary>
    /// <param name="isSuccess"></param>
    /// <returns></returns>
    public bool DeleteEffCheck(params bool[] isSuccess)
    {
        foreach(bool isOk in isSuccess)
        {
            if (isOk)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// �����ɂ��ǉ��_���[�W���ǂ��ɓ��邩�A�܂��I���̗L��������ΑI�������邽�߂̏���
    /// </summary>
    /// <param name="callBack"></param>
    /// <returns></returns>
    public IEnumerator SelectExplosionSite(System.Action<bool> callBack)
    {
        int roll = rollResult;

        if(rollResult==HEAD)
        {
            exprosionSite = ARM;
        }
        else if(rollResult==ARM)
        {
            siteSelectHead.gameObject.SetActive(true);
            siteSelectBody.gameObject.SetActive(true);

            while (!siteSelect)
            {
                yield return null;
            }

            exprosionSite = rollResult;
            
        }
        else if (rollResult == BODY)
        {
            siteSelectArm.gameObject.SetActive(true);
            siteSelectLeg.gameObject.SetActive(true);

            while (!siteSelect)
            {
                yield return null;
            }

            exprosionSite = rollResult;
        }
        else if (rollResult == LEG)
        {
            exprosionSite = BODY;
        }

        rollResult = roll;
        siteSelect = false;
        callBack(true);
    }

    /// <summary>
    /// �ؒf����̐��ہB�������Ă���99�_���[�W�Ƃ�������ɂ��A���ׂẴp�[�c��j��������
    /// </summary>
    public void CutRollJudge()
    {
        if(cutRollResult>=6)
        {
            giveDamage = 99;
            SortDamageParts(rollResult);
        }
        else
        {
            SortDamageParts(rollResult);
        }
    }

    /// <summary>
    /// �G�Ƀ_���[�W��^���鎞�A�ǂ̃p�[�c�Ƀ_���[�W��^���邩��I�ԏ���
    /// </summary>
    /// <param name="isActive"></param>
    public void SiteSelectButtonsActive(bool isActive)
    {
        if(!SiteRemainingParts(HEAD))
        {
            siteSelectHead.gameObject.SetActive(isActive);
        }
        if (!SiteRemainingParts(ARM))
        {
            siteSelectArm.gameObject.SetActive(isActive);
        }
        if (!SiteRemainingParts(BODY))
        {
            siteSelectBody.gameObject.SetActive(isActive);
        }
        if (!SiteRemainingParts(LEG))
        {
            siteSelectLeg.gameObject.SetActive(isActive);
        }
    }

    /// <summary>
    /// �p�[�c�ɕ��ʂ��c���Ă��邩�ǂ����m�F����
    /// </summary>
    /// <param name="site">�c���Ă邩�ǂ����m�F����������</param>
    /// <returns>�c���Ă����false�A�c���Ă��Ȃ����true�ŕԂ�</returns>
    private bool SiteRemainingParts(int site)
    {
        if(site==HEAD)
        {
            for(int i=0;i< damageChara.HeadParts.Count;i++)
            {
                if(!damageChara.HeadParts[i].isDmage)
                {
                    return false;
                }
            }
        }
        else if (site == ARM)
        {
            for (int i = 0; i < damageChara.ArmParts.Count; i++)
            {
                if (!damageChara.ArmParts[i].isDmage)
                {
                    return false;
                }
            }
        }
        else if (site == BODY)
        {
            for (int i = 0; i < damageChara.BodyParts.Count; i++)
            {
                if (!damageChara.BodyParts[i].isDmage)
                {
                    return false;
                }
            }
        }
        else if (site == LEG)
        {
            for (int i = 0; i < damageChara.LegParts.Count; i++)
            {
                if (!damageChara.LegParts[i].isDmage)
                {
                    return false;
                }
            }
        }

        return true;
    }

    /// <summary>
    /// �J�E���g�̗���I�����̏���.
    /// �A��������Ȃ�W���b�W�^�C�~���O�ֈڍs���A������x��������������B
    /// </summary>
    void EndFlowProcess()
    {
        // ��������������
        addDamage = 0;
        giveDamage = 0;
        dmgGuard = 0;
        siteSelect = false;

        diceRollButton.gameObject.SetActive(false);
        diceRollButtonImg.raycastTarget = true;
        diceRollButton.enabled = true;
        rollResultText.text = "�_�C�X���[��";
        diceRollAnim.gameObject.SetActive(false);
        isStandbyCutRoll = false;

        if (continuousAtk < actManeuver.Atk.Num_per_Action)
        {
            damageButtons.SetActive(false);
            isStandbyEnemySelect = false;
            isStandbyCharaSelect = false;

            //�����ŃW���b�W�^�C�~���O�ֈڍs
            ProcessAccessor.Instance.jdgTiming.SetActChara(actingChara);
            ProcessAccessor.Instance.jdgTiming.ActMneuver = actManeuver;
            ProcessAccessor.Instance.jdgTiming.IsStandbyDiceRoll = true;
            ProcessAccessor.Instance.jdgTiming.AtkTargetEnemy = damageChara.gameObject;
            ProcessAccessor.Instance.jdgTiming.GetJudgeButton().SetActive(true);
            ProcessAccessor.Instance.jdgTiming.GetDiceRollButton().gameObject.SetActive(true);
            ProcessAccessor.Instance.jdgTiming.SetContinuousAtk(continuousAtk++);
            if (actingChara.gameObject.CompareTag("EnemyChara")/*||�����_�C�X���[���I�ȁH�ݒ�Q�Ɨp*/)
            {
                ProcessAccessor.Instance.jdgTiming.OnClickDiceRoll();
            }
        }
        else
        {
            
            continuousAtk = 0;
            // �A���̐��̃J�E���g���W���b�W���ŊǗ��ł��Ȃ��̂ł����ŏ�����
            SetContinuousAtk(continuousAtk);
            // �s�������L������\���������
            ManagerAccessor.Instance.battleSystem.DeleteMoveChara(actingChara.Name);
            ManagerAccessor.Instance.battleSystem.BattleExe = true;
            damageButtons.gameObject.SetActive(false);
            
        }
        
    }

    /// <summary>
    /// �_�C�X�̉�]���I���̂�҂��\�b�h
    /// </summary>
    /// <returns></returns>
    private IEnumerator RollAnimStandby(Animator anim, System.Action<bool> callBack)
    {
        while (!anim.GetCurrentAnimatorStateInfo(0).IsName("End"))
        {
            yield return null;
        }

        callBack(true);
    }

    public void OnClickBack()
    {
        ZoomOutObj();
        confirmatButton.SetActive(false);
        isStandbyCharaSelect = true;
    }


}
