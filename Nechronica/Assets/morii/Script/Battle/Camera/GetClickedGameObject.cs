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
    public const int RAPID  = 1;
    public const int JUDGE  = 2;               // �q�I�u�W�F�N�g�擾�̂��߂̒萔
    public const int DAMAGE = 3;               // �q�I�u�W�F�N�g�擾�̂��߂̒萔
    public const int STATUS = 0;               // �G�̃X�e�[�^�X���擾���邽�߂̒萔
    public const int BUTTONS = 1;              // �G�̃{�^�����擾���邽�߂̒萔
    public const int ATKBUTTONS = 1;           // �A�^�b�N�{�^���Ƃ��̎q�I�u�W�F�N�g���擾���邽�߂̒萔
    public const int DICEROLL = 1;             // �_�C�X���[���{�^�����擾���邽�߂̒萔
    //----------------------------------------------------------

    protected CinemachineVirtualCamera CharaCamera;   // �L�����Ɏ�������v���n�u�̃N���[���̃J����

    protected CinemachineVirtualCamera saveCharaCamera;   // Chara�J�����̓��e�ۑ��p�ϐ�

    [SerializeField] protected new Camera camera;                      // ���C���J����
    [SerializeField] protected CinemachineVirtualCamera cinemaCamera;  // �N���[���������̃V�l�}�J����
    [SerializeField] private CinemachineVirtualCamera MainCamera;    // �S�̂��f���V�l�}�J����
    [SerializeField] protected BattleSystem battleSystem;              // �o�g���V�X�e���Ƃ̕ϐ��󂯓n���p
    [SerializeField] protected Transform childCommand;                 // �v���C�A�u���L�����̃R�}���h�I�u�W�F�N�g

    [SerializeField] protected Text timingText;                 // �v���C�A�u���L�����̃R�}���h�I�u�W�F�N�g
    public void SetTimingText(string set) { timingText.text = set; }
    
    // �ق��X�N���v�g������l��ύX����ϐ�
    protected bool isStandbyCharaSelect = false;

    public bool StandbyCharaSelect
    {
        get { return isStandbyCharaSelect; }
        set { isStandbyCharaSelect = value; }
    }

    protected bool isStandbyEnemySelect = false;
    public bool StandbyEnemySelect
    {
        get { return isStandbyEnemySelect; }
        set { isStandbyEnemySelect = value; }
    }

    protected bool skillSelected = false;

    public bool SkillSelected
    {
        get { return skillSelected; }
        set { skillSelected = value; }
    }
    [SerializeField]
    protected Doll_blu_Nor actingChara;                                   // �U���Ȃǂ̍s�������悤�Ƃ��Ă���L����
    public void SetActChara(Doll_blu_Nor doll) => actingChara = doll;
    public Doll_blu_Nor ActingChara
    {
        set { actingChara = value; }
        get { return actingChara; }
    }


    protected CharaManeuver dollManeuver;         // �I�����ꂽ�h�[���̃}�j���[�o�i�[�p�ϐ�
    public void SetManeuver(CharaManeuver set) { dollManeuver = set; }

    protected int movingArea = 0;                   // �I�����ꂽ�h�[���̏����G���A�i�[�p�ϐ�
    public void SetArea(int set) { movingArea = set; }
    public int GetArea() => movingArea;

    protected bool isSelectedChara = false;

    //------------------------------------


    private void Awake()
    {
        ManagerAccessor.Instance.getClickedObj = this;
    }

    private void Update()
    {
        // �o�g�������X�e�b�v
        if(isSelectedChara)
        {
            SkillSelectStandby();
        }
        else if (isStandbyEnemySelect)
        {
            EnemySelectStandby();
        }
        else if(isStandbyCharaSelect)
        {
            CharaSelectStandby();
        }
    }

    protected virtual void CharaSelectStandby()
    {
        //���N���b�N��
        if (Input.GetMouseButtonDown(0) && CharaCamera == null && !isSelectedChara)
        {
            GameObject clickedObj = ShotRay();

            //�N���b�N�����Q�[���I�u�W�F�N�g�������L�����Ȃ�
            if (clickedObj.CompareTag("AllyChara"))
            {
                ZoomUpObj(clickedObj);
                isSelectedChara = true;
                isStandbyCharaSelect = false;
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
            isSelectedChara = false;
            isStandbyCharaSelect = true;
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
                if(CharaCamera!=null)
                {
                    Destroy(CharaCamera.gameObject);
                }
                //priority�����̐��l�ɂ���
                cinemaCamera.Priority = 10;
                isSelectedChara = false;
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
    protected virtual void ZoomUpObj(GameObject clicked)
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

    /// <summary>
    /// �߂����������J����������Ă�������
    /// </summary>
    protected virtual void ZoomOutObj()
    {
        // �S�̂�\��������J������D��ɂ���B
        CharaCamera.Priority = 0;
        // �R�}���h������
        //childCommand.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        if(childCommand!=null)
        {
            childCommand.gameObject.SetActive(false);
        }
        // ���������v���n�u�J�����������B
        StartCoroutine(DstroyCamera());
    }

    /// <summary>
    /// �}�j���[�o���g�p�����L�����N�^�[�̎˒����ɑI�����ꂽ�L���������邩�ǂ���
    /// </summary>
    /// <param name="targetChara">�}�j���[�o�̃^�[�Q�b�g�ɑI�����ꂽ�L����</param>
    /// <param name="maneuver">�}�j���[�o�̏��</param>
    /// <param name="exeChara">�}�j���[�o���g�p�����L����</param>
    /// <returns>�˒����ł����true�ŕԂ�</returns>
    public bool RangeCheck(Doll_blu_Nor targetChara, CharaManeuver maneuver, Doll_blu_Nor exeChara)
    {
        if(maneuver.Timing==CharaBase.ACTION)
        {
            if ((Mathf.Abs(targetChara.area) <= Mathf.Abs(maneuver.MaxRange + exeChara.area) &&
                 Mathf.Abs(targetChara.area) >= Mathf.Abs(maneuver.MinRange + exeChara.area)) &&
               !maneuver.isDmage)
            {
                return true;
            }
        }
        else
        {
            if ((Mathf.Abs(targetChara.area) <= Mathf.Abs(maneuver.MaxRange + exeChara.area) &&
                 Mathf.Abs(targetChara.area) >= Mathf.Abs(maneuver.MinRange + exeChara.area)) &&
               (!maneuver.isUse && !maneuver.isDmage))
            {
                return true;
            }
        }

        return false;
    }
}
