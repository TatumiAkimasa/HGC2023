using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class JdgTimingProcess : GetClickedGameObject
{
    //-------------------------------
    // �ق�����񃁃�
    // �U������enemy
    // �^�[�Q�b�g�ɂȂ��Ă閡���L����
    // �_�C�X���[���̒l
    //-------------------------------

    private bool isDiceRoll;
    private bool isStandbyDiceRoll;
    private int continuousAtk;

    private GameObject atkTargetEnemy;      // �U������G�I�u�W�F�N�g���i�[�ꏊ

    Doll_blu_Nor selectedAllyChara;
    public GameObject AtkTargetEnemy
    {
        set { atkTargetEnemy = value; }
        get { return atkTargetEnemy; }
    }

    private CharaManeuver actManeuver;     // �A�N�V�����^�C�~���O�Ŕ������ꂽ�R�}���h�̊i�[�ꏊ
    [SerializeField]private Animator diceRollAnim;

    public CharaManeuver ActMneuver
    {
        get { return actManeuver; }
        set { actManeuver = value; }
    }

    public bool IsStandbyDiceRoll
    {
        get { return isStandbyDiceRoll; }
        set { isStandbyDiceRoll = value; }
    }

    [SerializeField] private Text plusNumText;      // �p�b�V�u�Ȃǂɂ��_�C�X���[���ɍs������Z
    [SerializeField] private Button nextButton;     // �W���b�W�^�C�~���O���I��点��{�^��
    [SerializeField] private Text rollResultText;
    [SerializeField] private Button diceRollButton; // �_�C�X���[�����s���{�^��
    [SerializeField] private Image diceRollButtonImg;       // �_�C�X���o�ȂǂɎg���{�^���̉摜
    [SerializeField] private GameObject confirmatButton;    // �Ō�ɔ������邩�m�F����{�^��
    [SerializeField] private GameObject judgeButtons;       // �W���b�W�^�C�~���O�ň����{�^���̐e�I�u�W�F�N�g

    public Button GetDiceRollButton() => diceRollButton;
    public GameObject GetConfirmatButton() => confirmatButton;
    public GameObject GetJudgeButton() => judgeButtons;
    public GameObject GetPlusNum() => plusNumText.gameObject;
    public int SetContinuousAtk(int num) => continuousAtk = num;


    private Unity.Mathematics.Random randoms;       // �Č��\�ȗ����̓�����Ԃ�ێ�����C���X�^���X
    private int rollResult = 0;                     // �_�C�X���[���̌��ʂ��i�[����ϐ�


    void Awake()
    {
        ProcessAccessor.Instance.jdgTiming = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (isStandbyDiceRoll)
        {
            if (isDiceRoll)
            {
                isStandbyDiceRoll = false;
                isDiceRoll = false;

                // ��������W���b�W�^�C�~���O
                isStandbyCharaSelect = true;
                nextButton.gameObject.SetActive(true);
            }
        }
        else if(isStandbyCharaSelect)
        {
            CharaSelectStandby();
        }
    }

    //override protected void CharaSelectStandby()
    //{
    //    //���N���b�N��
    //    if (Input.GetMouseButtonDown(0) && CharaCamera == null && !selectedChara)
    //    {
    //        GameObject clickedObj = ShotRay();

    //        //�N���b�N�����Q�[���I�u�W�F�N�g�������L�����Ȃ�
    //        if (clickedObj.CompareTag("AllyChara"))
    //        {
    //            clickedObj.GetComponent<BattleCommand>().SetNowSelect(true);
    //            ZoomUpObj(clickedObj);
    //            selectedChara = true;
    //            // �����ŃR�}���h�\��
    //            StartCoroutine(MoveStandby(clickedObj));
    //        }
    //    }
    //}

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
                    childCommand = move.transform.GetChild(CANVAS).transform.GetChild(JUDGE);
                    // �Z�R�}���h��������\��
                    childCommand.gameObject.SetActive(true);
                }
            }
        }
    }

    public void OnClickBack()
    {
        ZoomOutObj();
        confirmatButton.SetActive(false);
        isStandbyCharaSelect = true;
    }

    /// <summary>
    /// �_�C�X���[������{�^��
    /// </summary>
    public void OnClickDiceRoll()
    {
        diceRollAnim.gameObject.SetActive(true);
        rollResultText.text = "";   // �������ז��Ȃ̂ň�U��f�[�^����
        randoms = new Unity.Mathematics.Random((uint)Random.Range(0, 468446876));
        
        // ���̌�̑�����ז����Ȃ��悤��false�ɂ��Ă���
        diceRollButtonImg.raycastTarget = false;
        diceRollButton.enabled = false;
        StartCoroutine(RollAnimStandby(diceRollAnim,callBack => 
        {
            rollResult = randoms.NextInt(1, 11);
            rollResult = 11;
            rollResultText.text = rollResult.ToString();
            isDiceRoll = true;
        }));
        
    }

    /// <summary>
    /// �_�C�X�̉�]���I���̂�҂��\�b�h
    /// </summary>
    /// <returns></returns>
    private IEnumerator RollAnimStandby(Animator anim, System.Action<bool> callBack)
    {
        while(!anim.GetCurrentAnimatorStateInfo(0).IsName("End"))
        {
            yield return null;
        }

        callBack(true);
    }

    public void OnClickManeuver()
    {
        if(dollManeuver.MinRange==10 && actingChara == selectedAllyChara)
        {
            ExeJudgManeuver(dollManeuver, selectedAllyChara);
        }
        // �G�L�����̃G���A�ƑI�����ꂽ�}�j���[�o�̎˒����Βl�Ŕ�ׂāA�˒����ł���΍U�����邩�I������R�}���h��\������
        // �G�L�����̃G���A�̐�Βl���U���̍ő�˒��ȉ����A
        // �G�L�����̃G���A�̐�Βl���U���̍ŏ��˒��ȏ�Ȃ甭������
        if (dollManeuver.MinRange != 10 && RangeCheck(actingChara, dollManeuver, selectedAllyChara))
        {
            ExeJudgManeuver(dollManeuver, selectedAllyChara);
        }
        else
        {
            // �˒�������܂���݂����ȕ\�L������
        }
    }

    /// <summary>
    /// ���ۂɃW���b�W�^�C�~���O�̌��ʂ𔭓����鏈��
    /// </summary>
    /// <param name="maneuver"></param>
    /// <param name="judgExeChara"></param>
    public void ExeJudgManeuver(CharaManeuver maneuver, Doll_blu_Nor judgExeChara)
    {
        // �_�C�X���[���̌��ʂɎx���A�W�Q�̒l�𔽉f
        rollResult += dollManeuver.EffectNum[EffNum.Judge];
        rollResultText.text = rollResult.ToString();
        maneuver.isUse = true;
        // �X�L���𔭓�������R�}���h�{�^�����\���ɂ��A�L�����I��ҋ@��Ԃɂ��ǂ�
        confirmatButton.SetActive(false);
        skillSelected = false;
        if(judgExeChara.gameObject.CompareTag("AllyChara"))
        {
            ZoomOutObj();
        }
        isStandbyCharaSelect = true;

        // �s���l���R�X�g������������
        judgExeChara.NowCount -= dollManeuver.Cost;
        if (judgExeChara.NowCount == ManagerAccessor.Instance.battleSystem.NowCount && judgExeChara != actingChara)
        {
            ManagerAccessor.Instance.battleSystem.DeleteMoveChara(judgExeChara.Name);
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
        nextButton.gameObject.SetActive(true);
    }

    /// <summary>
    /// ���̃��\�b�h����������_���[�W�^�C�~���O�Ɉڍs����
    /// </summary>
    public void OnClickNext()
    {
        if(rollResult>=6)
        {
            int addDmg = 0;

            if (rollResult > 10)
            {
                addDmg = rollResult - 10;
            }

            ProcessAccessor.Instance.dmgTiming.SetActChara(actingChara);
            ProcessAccessor.Instance.dmgTiming.ActMneuver = actManeuver;
            ProcessAccessor.Instance.dmgTiming.StandbyCharaSelect = true;
            ProcessAccessor.Instance.dmgTiming.SetDamageChara(atkTargetEnemy.GetComponent<Doll_blu_Nor>());
            ProcessAccessor.Instance.dmgTiming.SetRollResult(rollResult);
            ProcessAccessor.Instance.dmgTiming.GetDamageButtons().SetActive(true);
            diceRollButton.gameObject.SetActive(false);
            judgeButtons.SetActive(false);
            
        }
        else if(rollResult==1)
        {
            // �厸�s�̏���
        }
        else
        {
            // �A�N�V�����^�C�~���O�ōs�������L������\���������
            ManagerAccessor.Instance.battleSystem.DeleteMoveChara();
            ManagerAccessor.Instance.battleSystem.BattleExe = true;
            nextButton.gameObject.SetActive(false);
            diceRollButton.gameObject.SetActive(false);
            judgeButtons.SetActive(false);
        }

        // ���̃W���b�W�^�C�~���O�Ŏg����悤��true�ɂ���
        diceRollButtonImg.raycastTarget = true;
        diceRollButton.enabled  = true;
        isStandbyCharaSelect = false;
        rollResultText.text = "�_�C�X���[��";
        diceRollAnim.gameObject.SetActive(false);

        nextButton.gameObject.SetActive(false);
    }

}

