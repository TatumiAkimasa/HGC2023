using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class JdgTimingProcess : GetClickedGameObject
{
    private bool isDiceRoll;
    private bool isStandbyDiceRoll;

    private int  giveDamage;
    public int GiveDamage
    {
        get { return giveDamage; }
        set { giveDamage = value; }
    }

    public bool IsStandbyDiceRoll
    {
        get { return isStandbyDiceRoll; }
        set { isStandbyDiceRoll = value; }
    }

    [SerializeField] private Text rollResultText;

    [SerializeField] private Button nextButton;     // �W���b�W�^�C�~���O���I��点��{�^��
    [SerializeField] private Button diceRollButton; // �_�C�X���[�����s���{�^��

    [SerializeField] private GameObject buttons;    // �Ō�ɔ������邩�ǂ����̃{�^��
    public GameObject JudgeButtons
    {
        get { return buttons; }
    }

    public Button DiceRollButton
    {
        get { return diceRollButton; }
    }

    public Text RollResultText
    {
        get { return rollResultText; }
    }

    private Unity.Mathematics.Random randoms;       // �Č��\�ȗ����̓�����Ԃ�ێ�����C���X�^���X
    private int rollResult = 0;                     // �_�C�X���[���̌��ʂ��i�[����ϐ�
    private int movingCharaArea;                    // �U���r���̓G�A�����I�u�W�F�N�g�̃G���A
    public int MovingCharaArea
    {
        get { return movingCharaArea; }
        set { movingCharaArea = value; }
    }

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
                standbyCharaSelect = true;
                //passButton.gameObject.SetActive(true);
            }
        }
        else if(standbyCharaSelect)
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
                    standbyCharaSelect = false;
                    // �I�������L�����̃R�}���h�̃I�u�W�F�N�g���擾
                    childCommand = move.transform.GetChild(CANVAS).transform.GetChild(JUDGE);
                    // �Z�R�}���h��������\��
                    childCommand.gameObject.SetActive(true);
                }
            }
        }
    }

    /// <summary>
    /// �W���b�W�^�C�~���O���I��点�鏈��
    /// </summary>
    public void OnClickPass()
    {
        // �_���[�W�^�C�~���O�Ɉڍs
    }

    /// <summary>
    /// �_�C�X���[������{�^��
    /// </summary>
    public void OnClickDiceRoll()
    {
        randoms = new Unity.Mathematics.Random((uint)Random.Range(0, 468446876));
        rollResult = randoms.NextInt(1, 11);
        rollResultText.text = rollResult.ToString();
        isDiceRoll = true;
    }

    public void OnClickExeManeuver()
    {
        // �G�L�����̃G���A�ƑI�����ꂽ�}�j���[�o�̎˒����Βl�Ŕ�ׂāA�˒����ł���΍U�����邩�I������R�}���h��\������
        // �G�L�����̃G���A�̐�Βl���U���̍ő�˒��ȉ����A
        // �G�L�����̃G���A�̐�Βl���U���̍ŏ��˒��ȏ�Ȃ甭������
        if (dollManeuver.MinRange != 10 &&
            (Mathf.Abs(movingCharaArea) <= Mathf.Abs(dollManeuver.MaxRange + targetArea) &&
             Mathf.Abs(movingCharaArea) >= Mathf.Abs(dollManeuver.MinRange + targetArea))&&
             (!dollManeuver.isUse && !dollManeuver.isDmage))
        {
            rollResult += dollManeuver.EffectNum[EffNum.Judge];
            rollResultText.text = rollResult.ToString();
            dollManeuver.isUse = true; 
            // �X�L���𔭓�������R�}���h�{�^�����\���ɂ����A�L�����I��ҋ@��Ԃɂ��ǂ�
            JudgeButtons.SetActive(false);
            skillSelected = false;
            ZoomOutObj();
            standbyCharaSelect = true;
        }
        else
        {
            // �˒�������܂���݂����ȕ\�L������
        }
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

            ProcessAccessor.Instance.dmgTiming.GiveDamage = giveDamage + addDmg;
        }
        else if(rollResult==1)
        {
            // �厸�s�̏���
        }
        else
        {
            // ���̃J�E���g�ɍs������
        }

    }

}

