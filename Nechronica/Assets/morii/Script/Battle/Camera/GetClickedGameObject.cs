using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class GetClickedGameObject : MonoBehaviour
{
    // �萔-----------------------------------------------------
    public const int CANVAS = 0;
    public const int CharaPriority = 20;       // �V�l�}�J�����̗D��x�p�萔�B�L�������Y�[������p�̃J�����̗D��x���ŗD��ɂ���
    public const int ACTION = 0;               // �q�I�u�W�F�N�g�擾�̂��߂̒萔
    public const int JUDGE  = 1;               // �q�I�u�W�F�N�g�擾�̂��߂̒萔
    public const int DAMAGE = 2;               // �q�I�u�W�F�N�g�擾�̂��߂̒萔
    public const int STATUS = 0;               // �G�̃X�e�[�^�X���擾���邽�߂̒萔
    public const int BUTTONS = 1;              // �G�̃{�^�����擾���邽�߂̒萔
    public const int ATKBUTTONS = 0;           // �A�^�b�N�{�^���Ƃ��̎q�I�u�W�F�N�g���擾���邽�߂̒萔
    public const int DICEROLL = 1;             // �_�C�X���[���{�^�����擾���邽�߂̒萔
    //----------------------------------------------------------

    protected CinemachineVirtualCamera CharaCamera;   // �L�����Ɏ�������v���n�u�̃N���[���̃J����

    protected CinemachineVirtualCamera saveCharaCamera;   // Chara�J�����̓��e�ۑ��p�ϐ�

    [SerializeField]
    protected new Camera camera;                      // ���C���J����

    [SerializeField]
    protected CinemachineVirtualCamera cinemaCamera;  // �N���[���������̃V�l�}�J����

    [SerializeField]
    private CinemachineVirtualCamera MainCamera;    // �S�̂��f���V�l�}�J����

    [SerializeField]
    protected BattleSystem battleSystem;              // �o�g���V�X�e���Ƃ̕ϐ��󂯓n���p

    [SerializeField]
    protected Transform childCommand;                 // �v���C�A�u���L�����̃R�}���h�I�u�W�F�N�g

    [SerializeField] protected Button exeButton;
    public Button ExeButton
    {
        get { return exeButton; }
    }

    // �ق��X�N���v�g������l��ύX����ϐ�
    protected bool standbyCharaSelect = false;

    public bool StandbyCharaSelect
    {
        get { return standbyCharaSelect; }
        set { standbyCharaSelect = value; }
    }

    protected bool standbyEnemySelect = false;
    public bool StandbyEnemySelect
    {
        get { return standbyEnemySelect; }
        set { standbyEnemySelect = value; }
    }

    protected bool skillSelected = false;

    public bool SkillSelected
    {
        get { return skillSelected; }
        set { skillSelected = value; }
    }



    protected CharaManeuver dollManeuver;         // �I�����ꂽ�h�[���̃}�j���[�o�i�[�p�ϐ�
    public void SetManeuver(CharaManeuver set) { dollManeuver = set; }

    protected int targetArea = 0;                   // �I�����ꂽ�h�[���̏����G���A�i�[�p�ϐ�
    public void SetArea(int set) { targetArea = set; }
    public int GetArea() => targetArea;

    protected bool selectedChara = false;

    //------------------------------------


    private void Awake()
    {
        ManagerAccessor.Instance.getClickedObj = this;
    }

    private void Update()
    {
        // �o�g�������X�e�b�v
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
    }

    protected virtual void CharaSelectStandby()
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
                standbyCharaSelect = false;
                // �����ŃR�}���h�\��
                StartCoroutine(MoveStandby(clickedObj));
            }
        }
    }

    protected virtual void EnemySelectStandby()
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

    protected virtual void SkillSelectStandby()
    {
        // �E�N���b�N��
        if ((Input.GetMouseButtonDown(1) || skillSelected) && CharaCamera != null)
        {
            // �}�j���[�o��I�ԃR�}���h�܂ŕ\������Ă����炻�̃R�}���h��������
            ZoomOutObj();
            // �L�����I��ҋ@��Ԃɂ���
            selectedChara = false;
            standbyCharaSelect = true;
        }
    }



    // �J���������S�ɗ���Ă���������߂̃R���[�`��
    protected IEnumerator DstroyCamera()
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
    protected virtual IEnumerator MoveStandby(GameObject move)
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
                
            }
        }
    }

    /// <summary>
    /// �N���b�N����Obj���擾���A�Ԃ�
    /// </summary>
    /// <returns></returns>
    protected GameObject ShotRay()
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
    protected void ZoomUpObj(GameObject clicked)
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

    protected void ZoomOutObj()
    {
        // �S�̂�\��������J������D��ɂ���B
        CharaCamera.Priority = 0;
        // �R�}���h������
        childCommand.gameObject.SetActive(false);
        // ���������v���n�u�J�����������B
        StartCoroutine(DstroyCamera());
    }

    protected void OnClickBack()
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
