using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Cinemachine;

public class GetClickedGameObject : MonoBehaviour
{
    // �萔-----------------------------------------------------
    const int CharaPriority = 20;       // �V�l�}�J�����̗D��x�p�萔�B�L�������Y�[������p�̃J�����̗D��x���ŗD��ɂ���
    const int ACTION = 0;               // �q�I�u�W�F�N�g�擾�̂��߂̒萔
    //----------------------------------------------------------

    [SerializeField]
    private GameObject clickedGameObject;           // �N���b�N�����Q�[���I�u�W�F�N�g

    private CinemachineVirtualCamera CharaCamera;   // �L�����Ɏ�������v���n�u�̃N���[���̃J����


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

    private bool selectedChara = false;

    private void Update()
    {
        //���N���b�N��
        if (Input.GetMouseButtonDown(0) && CharaCamera == null && !selectedChara)
        {
            //Click�����ӏ��Ƀ��C���΂��B
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();

            //�q�b�g�����I�u�W�F�N�g���擾
            if (Physics.Raycast(ray, out hit))
            {
                clickedGameObject = hit.collider.gameObject;
                Debug.Log(clickedGameObject.name);
            }

            //�N���b�N�����Q�[���I�u�W�F�N�g�������L�����Ȃ�
            if (clickedGameObject.CompareTag("AllyChara"))
            {
                CharaSelect();
                // �����ŃR�}���h�\��
                StartCoroutine(MoveStandby(clickedGameObject));
            }

            
        }
        else if(selectedChara)
        {
            SkillSelectStandby();

        }
    }

    /// <summary>
    /// �L�������I�����ꂽ��ɋ߂Â����\�b�h
    /// </summary>
    /// <returns></returns>
    public void CharaSelect()
    {
        
        Debug.Log("�ǂ��ł��傤��");
        // �N���b�N�����I�u�W�F�N�g�̍��W�����擾
        Vector3 clickedObjPos = clickedGameObject.transform.position;
        // �擾�������W��񂩂班�����ꂽ�ʒu�ɍ��W�𒲐�
        clickedObjPos.z -= 10.0f;
        clickedObjPos.x -= 2.5f;

        // �V�l�}�J�����̃v���n�u�𐶐����N���b�N�����I�u�W�F�N�g��e�I�u�W�F�N�g�ɂ���
        CharaCamera = Instantiate(cinemaCamera, clickedObjPos, Quaternion.identity);
        CharaCamera.transform.parent = clickedGameObject.transform;

        // ���������v���n�u�̃o�[�`�����J���������C���J�����ɂȂ�悤�v���C�I���e�B��ݒ�
        CharaCamera.Priority = CharaPriority;

        selectedChara = true;
        return;
    }

    public void SkillSelectStandby()
    {
        //�Z�𔭓��������_�ł��̃��\�b�h�𔲂�������������B
        //�I�������Z�R�}���h���擾����K�v���肩�c

        // �E�N���b�N��
        if (Input.GetMouseButtonDown(1) && CharaCamera != null)
        {
            // �S�̂�\��������J������D��ɂ���B
            CharaCamera.Priority = 0;
            // �R�}���h������
            childCommand.gameObject.SetActive(false);
            // ���������v���n�u�J�����������B
            StartCoroutine(DstroyCamera());
        }
        // else if(�Z�R�}���h�I���Łc
        //{
        //    DestroyCamera();
        //    battleSystem.BattleProcess(�I�񂾋Z�R�}���h);
        //    battleSystem.rayGuard.SetActive(true);
        //    selsectedChara=false;
        //}

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
                // �K�v�ȏ��͎擾������Ȃ̂ŏ��������Ă���
                clickedGameObject = null;
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
                // �I�������L�����̃R�}���h�̃I�u�W�F�N�g���擾
                childCommand = move.transform.GetChild(ACTION);
                //�Z�R�}���h��������\��
                childCommand.gameObject.SetActive(true);
            }
        }
    }
}
