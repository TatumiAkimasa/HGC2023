using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GetClickedGameObject : MonoBehaviour
{
    // �萔-----------------------------------------------------
    const int CharaPriority = 20;       // �V�l�}�J�����̗D��x�p�萔�B�L�������Y�[������p�̃J�����̗D��x���ŗD��ɂ���
    const int ACTION = 0;               // �q�I�u�W�F�N�g�擾�̂��߂̒萔
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

    private CharaManeuver dollManeuver;         // �I�����ꂽ�h�[���̃}�j���[�o�i�[�p�ϐ�
    public void SetManeuver(CharaManeuver set) { dollManeuver = set; }

    private int dollArea = 0;                   // �I�����ꂽ�h�[���̏����G���A�i�[�p�ϐ�
    public void SetArea(int set) { dollArea = set; }

    //------------------------------------

    private bool selectedChara = false;

    private void Update()
    {
        if(selectedChara)
        {
            SkillSelectStandby();
        }
        else if(standbyCharaSelect)
        {
            CharaSelectStandby();
        }

        if(standbyEnemySelect)
        {
            EnemySelectStandby();
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
                // �G�L�����̃G���A�ƑI�����ꂽ�}�j���[�o�̎˒����Βl�Ŕ�ׂāA
                if (Mathf.Abs(dollArea - clickedObj.GetComponent<Doll_blu_Nor>().potition) <= Mathf.Abs(dollManeuver.MaxRange-dollManeuver.MinRange))
                {
                    ZoomUpObj(clickedObj);
                    // �����ŃR�}���h�\��
                    StartCoroutine(MoveStandby(clickedObj));
                }
            }
        }
    }

    public void SkillSelectStandby()
    {
        // �E�N���b�N��
        if ((Input.GetMouseButtonDown(1) || skillSelected) && CharaCamera != null)
        {
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
                    // �I�������L�����̃R�}���h�̃I�u�W�F�N�g���擾
                    childCommand = move.transform.GetChild(ACTION).transform.GetChild(ACTION);
                    // �Z�R�}���h��������\��
                    childCommand.gameObject.SetActive(true);
                }
                else if(move.CompareTag("EnemyChara"))
                {
                    // ������ �U������`�݂����ȃR�}���h��\��
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
}
