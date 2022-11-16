using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class GetClickedGameObject : MonoBehaviour
{
    // �萔-----------------------------------------------------
    const int CANVAS = 0;

    const int CharaPriority = 20;       // �V�l�}�J�����̗D��x�p�萔�B�L�������Y�[������p�̃J�����̗D��x���ŗD��ɂ���
    const int ACTION = 0;               // �q�I�u�W�F�N�g�擾�̂��߂̒萔
    const int JUDGE  = 1;               // �q�I�u�W�F�N�g�擾�̂��߂̒萔
    const int DAMAGE = 2;               // �q�I�u�W�F�N�g�擾�̂��߂̒萔

    const int STATUS = 0;               // �G�̃X�e�[�^�X���擾���邽�߂̒萔
    const int BUTTONS = 1;              // �G�̃{�^�����擾���邽�߂̒萔

    const int ATKBUTTONS = 0;           // �A�^�b�N�{�^���Ƃ��̎q�I�u�W�F�N�g���擾���邽�߂̒萔
    const int DICEROLL = 1;             // �_�C�X���[���{�^�����擾���邽�߂̒萔
    //----------------------------------------------------------

    private CinemachineVirtualCamera CharaCamera;   // �L�����Ɏ�������v���n�u�̃N���[���̃J����

    private CinemachineVirtualCamera saveCharaCamera;   // Chara�J�����̓��e�ۑ��p�ϐ�

    [SerializeField]
    private new Camera camera;                      // ���C���J����

    [SerializeField]
    private CinemachineVirtualCamera cinemaCamera;  // �N���[���������̃V�l�}�J����

    [SerializeField]
    private CinemachineVirtualCamera MainCamera;    // �S�̂��f���V�l�}�J����

    [SerializeField]
    private BattleSystem battleSystem;              // �o�g���V�X�e���Ƃ̕ϐ��󂯓n���p

    [SerializeField]
    private Transform childCommand;                 // �v���C�A�u���L�����̃R�}���h�I�u�W�F�N�g

    private GameObject atkTargetEnemy;              // �U������G�I�u�W�F�N�g���i�[�ꏊ

    private Unity.Mathematics.Random randoms;       // �Č��\�ȗ����̓�����Ԃ�ێ�����C���X�^���X

    private int rollResult = 0;                     // �_�C�X���[���̌��ʂ��i�[����ϐ�

    [SerializeField]
    private Button atkButton;
    [SerializeField]
    private Button backButton;

    // �ق��X�N���v�g������l��ύX����ϐ�
    private bool standbyCharaSelect = false;

    public bool StandbyCharaSelect
    {
        get { return standbyCharaSelect; }
        set { standbyCharaSelect = value; }
    }

    private bool standbyEnemySelect = false;
    public bool StandbyEnemySelect
    {
        get { return standbyEnemySelect; }
        set { standbyEnemySelect = value; }
    }

    private bool skillSelected = false;

    public bool SkillSelected
    {
        get { return skillSelected; }
        set { skillSelected = value; }
    }

    private bool isDiceRoll;
    private bool isStandbyDiceRoll;
    private bool isJudgeTiming=false;


    private CharaManeuver dollManeuver;         // �I�����ꂽ�h�[���̃}�j���[�o�i�[�p�ϐ�
    public void SetManeuver(CharaManeuver set) { dollManeuver = set; }

    private int dollArea = 0;                   // �I�����ꂽ�h�[���̏����G���A�i�[�p�ϐ�
    public void SetArea(int set) { dollArea = set; }
    public int GetArea() => dollArea;

    [SerializeField]
    private Text text;

    //------------------------------------

    private bool selectedChara = false;

    private void Awake()
    {
        ManagerAccessor.Instance.getClickedObj = this;
        backButton.onClick.AddListener(OnClickBack);
    }

    private void Update()
    {
        if(selectedChara)
        {
            SkillSelectStandby();
        }
        else if (standbyEnemySelect)
        {
            EnemySelectStandby();
        }
        else if(standbyCharaSelect)
        {
            CharaSelectStandby();
        }
        else if(isStandbyDiceRoll)
        {
            if(isDiceRoll)
            {
                isStandbyDiceRoll = false;
                isDiceRoll = false;

                // ��������W���b�W�^�C�~���O
                standbyCharaSelect = true;
                isJudgeTiming = true;
            }
        }
    }

    void CharaSelectStandby()
    {
        //���N���b�N��
        if (Input.GetMouseButtonDown(0) && CharaCamera == null && !selectedChara)
        {
            GameObject clickedObj = ShotRay();

            //�N���b�N�����Q�[���I�u�W�F�N�g�������L�����Ȃ�
            if (clickedObj.CompareTag("AllyChara"))
            {

                clickedObj.GetComponent<BattleCommand>().SetNowSelect(true);
                ZoomUpObj(clickedObj);
                selectedChara = true;
                // �����ŃR�}���h�\��
                StartCoroutine(MoveStandby(clickedObj));
            }
        }
    }

    public void EnemySelectStandby()
    {
        //���N���b�N��
        if (Input.GetMouseButtonDown(0))
        {
            // �v���C�A�u���L�����Ɍ����Ă����J��������ۑ�
            saveCharaCamera = CharaCamera;

            GameObject clickedObj = ShotRay();

            //�N���b�N�����Q�[���I�u�W�F�N�g���G�L�����Ȃ�
            if (clickedObj.CompareTag("EnemyChara"))
            {
                ZoomUpObj(clickedObj);
                // �����ŃR�}���h�\��
                StartCoroutine(MoveStandby(clickedObj));
            }
        }
    }

    public void SkillSelectStandby()
    {
        // �E�N���b�N��
        if ((Input.GetMouseButtonDown(1) || skillSelected) && CharaCamera != null)
        {
            // �}�j���[�o��I�ԃR�}���h�܂ŕ\������Ă����炻�̃R�}���h��������

            ZoomOutObj();
            // �L�����I��ҋ@��Ԃɂ���
            selectedChara = false;
        }
    }



    // �J���������S�ɗ���Ă���������߂̃R���[�`��
    IEnumerator DstroyCamera()
    {
        for (int i = 0; i < 2; i++)
        {
            if (i == 0)
            {
                yield return new WaitForSeconds(0.75f);
            }
            else
            {
                Destroy(CharaCamera.gameObject);
                //priority�����̐��l�ɂ���
                cinemaCamera.Priority = 10;
                selectedChara = false;
            }
        }
    }


    /// <summary>
    /// �J�������߂Â��Ă���R�}���h��\�����郁�\�b�h
    /// </summary>
    /// <param name="charaCommand">�N���b�N���ꂽ�L�����̎q�I�u�W�F�N�g�i�R�}���h�j</param>
    /// <returns></returns>
    public IEnumerator MoveStandby(GameObject move)
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
                if(move.CompareTag("AllyChara"))
                {
                    if(isJudgeTiming)
                    {
                        // �I�������L�����̃R�}���h�̃I�u�W�F�N�g���擾
                        childCommand = move.transform.GetChild(CANVAS).transform.GetChild(JUDGE);
                        // �Z�R�}���h��������\��
                        childCommand.gameObject.SetActive(true);
                    }
                    else
                    {
                        // �I�������L�����̃R�}���h�̃I�u�W�F�N�g���擾
                        childCommand = move.transform.GetChild(CANVAS).transform.GetChild(ACTION);
                        // �Z�R�}���h��������\��
                        childCommand.gameObject.SetActive(true);
                    }
                }
                else if(move.CompareTag("EnemyChara"))
                {
                    // �X�e�[�^�X���擾���A�\���B���ZoomOutObj�Ŏg���̂Ń����o�ϐ��Ɋi�[
                    childCommand = move.transform.GetChild(CANVAS);
                    childCommand.gameObject.SetActive(true);

                    // �G�L�����̃G���A�ƑI�����ꂽ�}�j���[�o�̎˒����Βl�Ŕ�ׂāA�˒����ł���΍U�����邩�I������R�}���h��\������
                    // �G�L�����̃G���A�̐�Βl���U���̍ő�˒��ȉ����A
                    // �G�L�����̃G���A�̐�Βl���U���̍ŏ��˒��ȏ�Ȃ�U������
                    if (Mathf.Abs(move.GetComponent<Doll_blu_Nor>().potition) <= Mathf.Abs(dollManeuver.MaxRange + dollArea) &&
                        Mathf.Abs(move.GetComponent<Doll_blu_Nor>().potition) >= Mathf.Abs(dollManeuver.MinRange + dollArea))
                    {
                        atkTargetEnemy = move;
                        atkTargetEnemy.transform.GetChild(CANVAS).gameObject.SetActive(true);

                        atkButton.onClick.AddListener(() => OnClickAtk(move.GetComponent<Doll_blu_Nor>()));

                        this.transform.GetChild(CANVAS).transform.GetChild(ATKBUTTONS).gameObject.SetActive(true);
                    }
                    
                    // �U������`��I�񂾂�AselectedChara�AstandbyCharaSelect��false�ɂ��A�_�C�X���[����
                    // �_�C�X���[����A�}�j���[�o�A�GObj(move)�A�_�C�X�̒l�������Ƃ��A�_���[�W�^�C�~���O��
                }
            }
        }
    }

    /// <summary>
    /// �N���b�N����Obj���擾���A�Ԃ�
    /// </summary>
    /// <returns></returns>
    GameObject ShotRay()
    {
        //Click�����ӏ��Ƀ��C���΂��B
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();

        //�q�b�g�����I�u�W�F�N�g���擾
        if (Physics.Raycast(ray, out hit))
        {
            return hit.collider.gameObject;
        }

        return null;
    }

    /// <summary>
    /// �L�������I�����ꂽ��A�J�������I�����ꂽ�L�����ɋ߂Â����\�b�h
    /// </summary>
    /// <param name="clicked"></param>
    void ZoomUpObj(GameObject clicked)
    {
        // �N���b�N�����I�u�W�F�N�g�̍��W�����擾
        Vector3 clickedObjPos = clicked.transform.position;
        // �擾�������W��񂩂班�����ꂽ�ʒu�ɍ��W�𒲐�
        clickedObjPos.z -= 10.0f;
        clickedObjPos.x -= 2.5f;

        // �V�l�}�J�����̃v���n�u�𐶐����N���b�N�����I�u�W�F�N�g��e�I�u�W�F�N�g�ɂ���
        CharaCamera = Instantiate(cinemaCamera, clickedObjPos, Quaternion.identity, clicked.transform);

        // ���������v���n�u�̃o�[�`�����J���������C���J�����ɂȂ�悤�v���C�I���e�B��ݒ�
        CharaCamera.Priority = CharaPriority;
    }

    void ZoomOutObj()
    {
        // �S�̂�\��������J������D��ɂ���B
        CharaCamera.Priority = 0;
        // �R�}���h������
        childCommand.gameObject.SetActive(false);
        // ���������v���n�u�J�����������B
        StartCoroutine(DstroyCamera());
    }

    void OnClickBack()
    {
        // childCommand�̒��g������Ƃ������Ƃ̓R�}���h���\������Ă����ԂȂ̂ŁA������\���ɂ��AchildCommand�̒��g���Ȃ���
        if (childCommand != null)
        {
            childCommand.gameObject.SetActive(false);
            childCommand = null;
        }
        else
        {
            // �J���������̈ʒu�ɖ߂��AUI������
            ZoomOutObj();
            this.transform.GetChild(CANVAS).gameObject.SetActive(false);
        }
       
    }

    void OnClickAtk(Doll_blu_Nor enemy)
    {
        // �J���������̈ʒu�ɖ߂��AUI������
        ZoomOutObj();
        enemy.transform.GetChild(CANVAS).gameObject.SetActive(false);
        this.transform.GetChild(CANVAS).transform.GetChild(ATKBUTTONS).gameObject.SetActive(false);
        this.transform.GetChild(CANVAS).transform.GetChild(DICEROLL).gameObject.SetActive(true);

        selectedChara = false;
        standbyEnemySelect = false;
        standbyCharaSelect = false;

        // �����W���b�W�������
        isStandbyDiceRoll = true;

        battleSystem.DamageTiming(dollManeuver, enemy);
    }

    public void OnClickDiceRoll()
    {
        randoms = new Unity.Mathematics.Random((uint)Random.Range(0, 468446876));
        rollResult = randoms.NextInt(1, 10);
        text.text = rollResult.ToString();
        isDiceRoll = true;
    }

    public int JudgeTiming()
    {
        // �G�̉���i�H�j

        // �����I��

        // �����̃W���b�W����

        // �_�C�X���ʂɒl�����Z

        // ������x����������

        // pass����������I��


        return 0;
    }
}
