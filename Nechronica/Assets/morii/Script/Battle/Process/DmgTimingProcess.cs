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
    const int HEAD = 10;
    const int ARM  = 9;
    const int BODY = 8;
    const int LEG  = 7;
    //--------------------------------

    private bool siteSelect = false;    // �_�C�X���[���̒l��10��葽���Ƃ���true�ɂ���

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

    private int rollResult;             // �_�C�X���[���̌��ʂ̒l
    private Doll_blu_Nor damageChara;   // �_���[�W���󂯂�\��̃L����
    private CharaManeuver actManeuver;     // �A�N�V�����^�C�~���O�Ŕ������ꂽ�R�}���h�̊i�[�ꏊ

    private bool isStandbyCutRoll = false;     // �ؒf����ҋ@
    private bool isCutRoll = false;     // �ؒf����ҋ@
    [SerializeField] private int  cutRollResult = 0;          // �ؒf����̃��U���g
    private int cutSite = 0;                 // �ؒf���ꂻ���ȕ���
    [SerializeField] private Text rollResultText;
    [SerializeField] private Button diceRollButton; // �_�C�X���[�����s���{�^��
    [SerializeField] private Image diceRollButtonImg;       // �_�C�X���o�ȂǂɎg���{�^���̉摜
    [SerializeField] private Animator diceRollAnim;         // �_�C�X���[���̃A�j��

    private List<Doll_blu_Nor> damageCharas = new List<Doll_blu_Nor>();

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
    /// ���������}�j���[�o�[����������̂��̊m�F������
    /// </summary>
    public void ExeManeuver(CharaManeuver maneuver, Doll_blu_Nor dmgExeChara)
    {
        // �_���[�W�𑝉�����}�j���[�o�[�̏���
        if(maneuver.EffectNum.ContainsKey(EffNum.Damage))
        {
            DamageUPProcess(maneuver,dmgExeChara);
        }
        // �h��l�𑝉�����}�j���[�o�[�̏���
        else if(dollManeuver.EffectNum.ContainsKey(EffNum.Guard))   
        {
            GuardProcess(maneuver, dmgExeChara);
        }
        else   // ��̓�ɊY�����Ȃ��ꍇ�A�ŗL�̌��ʂƎg�p����
        {

        }

        // ���J�E���g�œ����L�������_���[�W�^�C�~���O�̃}�j���[�o�[�𔭓������ꍇ�A���J�E���g�ɍs�����ł��Ȃ��Ȃ�̂ō��̕\������ѓ�����\��̃L�����̃��X�g����폜����
        if(selectedAllyChara.NowCount==ManagerAccessor.Instance.battleSystem.NowCount)
        {
            ManagerAccessor.Instance.battleSystem.DeleteMoveChara(selectedAllyChara.Name);
        }

        confirmatButton.SetActive(false);
        ZoomOutObj();
    }

    private void DamageUPProcess(CharaManeuver maneuver, Doll_blu_Nor dmgExeChara)
    {
        // �^����_���[�W���オ��n�̏���
        // �˒������g�݂̂̏ꍇ�A�_���[�W��^����L�����ƃ_���[�W�^�C�~���O�œ����L�������������ǂ������ׂ�
        if (dollManeuver.MinRange == 10)
        {
            if(actingChara == dmgExeChara)
            {
                addDamage += maneuver.EffectNum[EffNum.Damage];
                // �vif�������B����ȃR�X�g�ǂ������f����
                dmgExeChara.NowCount -= maneuver.Cost;
            }
        }
        // �G�L�����̃G���A�ƑI�����ꂽ�}�j���[�o�̎˒����Βl�Ŕ�ׂāA�˒����ł���΍U�����邩�I������R�}���h��\������
        // �G�L�����̃G���A�̐�Βl���U���̍ő�˒��ȉ����A
        // �G�L�����̃G���A�̐�Βl���U���̍ŏ��˒��ȏ�Ȃ甭������
        else if ((Mathf.Abs(actingChara.area) <= Mathf.Abs(maneuver.MaxRange + dmgExeChara.area)  &&
                  Mathf.Abs(actingChara.area) >= Mathf.Abs(maneuver.MinRange + dmgExeChara.area)) &&
                (!maneuver.isUse && !maneuver.isDmage))
        {
            addDamage += maneuver.EffectNum[EffNum.Damage];
            // �vif�������B����ȃR�X�g�ǂ������f����
            dmgExeChara.NowCount -= maneuver.Cost;
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
                // �vif�������B����ȃR�X�g�ǂ������f����
                dmgExeChara.NowCount -= maneuver.Cost;
            }
        }
        else if ((Mathf.Abs(damageChara.area) <= Mathf.Abs(maneuver.MaxRange + dmgExeChara.area) &&
                  Mathf.Abs(damageChara.area) >= Mathf.Abs(maneuver.MinRange + dmgExeChara.area)) &&
                (!maneuver.isUse && !maneuver.isDmage))
        {
            if(maneuver.EffectNum.ContainsKey(EffNum.Guard))
            {
                dmgGuard += maneuver.EffectNum[EffNum.Guard];
            }
            // �vif�������B����ȃR�X�g�ǂ������f����
            selectedAllyChara.NowCount -= maneuver.Cost;
        }
        else
        {
            // �˒�������܂��񌩂����ȕ\�L����
        }
    }

    private void EXProcess()
    {
        // ���΂��̏���
        if (dollManeuver.EffectNum.ContainsKey(EffNum.Protect))
        {
            // �������΂��̏������邩��
            // �vif�������B����ȃR�X�g�ǂ������f����
            selectedAllyChara.NowCount -= dollManeuver.Cost;
        }
    }

    public void OnClickDiceRoll()
    {
        diceRollAnim.gameObject.SetActive(true);
        rollResultText.text = "";   // �������ז��Ȃ̂ň�U��f�[�^����
        randoms = new Unity.Mathematics.Random((uint)Random.Range(0, 468446876));

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
        if (rollResult > 10)
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
            // ���ʑI�����Ȃ���΂��̂܂ܒǉ����ʔ���ֈڍs������
            isAddEffectStep = true;
        }
    }



    /// <summary>
    /// �ǂ��̕��ʂɃ_���[�W�����邩
    /// </summary>
    /// <param name="site"></param>
    void SortDamageParts(int site)
    {
        if(actManeuver.Atk.isAllAttack)
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
                damageChara.HeadParts = GiveDamageParts(damageChara.HeadParts);
            }
            else if (site == ARM)
            {
                damageChara.ArmParts = GiveDamageParts(damageChara.ArmParts);
            }
            else if (site == BODY)
            {
                damageChara.BodyParts = GiveDamageParts(damageChara.BodyParts);
            }
            else if (site == LEG)
            {
                damageChara.LegParts = GiveDamageParts(damageChara.LegParts);
            }

            if (exprosionSite != 0)
            {
                int buf = exprosionSite;
                exprosionSite = 0;
                SortDamageParts(buf);
            }
            else
            {
                StartCoroutine(ManeuverAnimation(actManeuver, callBack => EndFlowProcess()));
            }
        }
    }

    void SortDamageParts(int site, ref List<Doll_blu_Nor> listChara)
    {
        if (site == HEAD)
        {
            for(int i=0;i<listChara.Count;i++)
            {
                listChara[i].HeadParts = GiveDamageParts(listChara[i].HeadParts);
            }
        }
        else if (site == ARM)
        {
            for (int i = 0; i < listChara.Count; i++)
            {
                listChara[i].ArmParts = GiveDamageParts(listChara[i].ArmParts);
            }
        }
        else if (site == BODY)
        {
            for (int i = 0; i < listChara.Count; i++)
            {
                listChara[i].BodyParts = GiveDamageParts(listChara[i].BodyParts);
            }
        }
        else if (site == LEG)
        {
            for (int i = 0; i < listChara.Count; i++)
            {
                listChara[i].LegParts = GiveDamageParts(listChara[i].LegParts);
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
        if (actManeuver.Atk.isExplosion)
        {
            StartCoroutine(SelectExplosionSite(callBack =>
            {
                isAddEffectStep = false;
                SortDamageParts(rollResult);
            }));
        }
        else if (actManeuver.Atk.isCutting)
        {
            isStandbyCutRoll = true;
            diceRollButton.gameObject.SetActive(true);
        }
        else
        {
            SortDamageParts(rollResult);
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

    public IEnumerator SelectDamageSite(System.Action<bool> callBack)
    {
        while(!siteSelect)
        {
            yield return null;
        }

        siteSelect = false;
        callBack(true);
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

    public void SiteSelectButtonsActive(bool isActive)
    {
        siteSelectHead.gameObject.SetActive(isActive);
        siteSelectArm.gameObject.SetActive(isActive);
        siteSelectBody.gameObject.SetActive(isActive);
        siteSelectLeg.gameObject.SetActive(isActive);
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
            ManagerAccessor.Instance.battleSystem.DeleteMoveChara();
            ManagerAccessor.Instance.battleSystem.BattleExe = true;
            nextButton.gameObject.SetActive(false);
            
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

}
