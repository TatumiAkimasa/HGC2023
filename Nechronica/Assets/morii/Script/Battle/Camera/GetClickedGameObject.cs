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

    /// <summary>
    /// �L�������I�������܂ōċN����֐�
    /// </summary>
    /// <returns></returns>
    public Task<int> CharaSelectStandby()
    {
        Debug.Log("�����ʂ��Ă�H");
        //���N���b�N��
        if (Input.GetMouseButtonDown(0) && CharaCamera == null)
        {
            //���L�ϐ���������
            clickedGameObject = null;

            //Click�����ӏ��Ƀ��C���΂��B
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();

            //�q�b�g�����I�u�W�F�N�g���擾
            if (Physics.Raycast(ray, out hit))
            {
                clickedGameObject = hit.collider.gameObject;
                Debug.Log("�L�����擾������");
            }

            //�N���b�N�����Q�[���I�u�W�F�N�g���v���C�A�u���L�����Ȃ�
            if (clickedGameObject.CompareTag("PlayableChara"))
            {
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

                StandbyChara(clickedGameObject);
                return Task.FromResult(0);
            }
        }
        
        return Task.Run(()=> CharaSelectStandby());
    }

    public void SkillSelectStandby()
    {
        //�Z�𔭓��������_�ł��̃��\�b�h�𔲂�������������B
        //�I�������Z�R�}���h���擾����K�v���肩�c

        // �����ŃR�}���h�o��
        // StandbyChara(clickedGameObject)

        // �E�N���b�N��
        if (Input.GetMouseButtonDown(1) && CharaCamera != null)
        {
            // �S�̂�\��������J������D��ɂ���B
            CharaCamera.Priority = 0;

            // ���������v���n�u�J�����������B
            StartCoroutine(DstroyCamera());

            // �L�����Z���N�g��C�ɖ߂�
            CharaSelectStandby();
        }
        // else if(�Z�R�}���h�I���Łc
        //{
        //    BattleSystem.BattleProcess(�I�񂾋Z�R�}���h);
        //}
        else
        {
            SkillSelectStandby();
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
            }
        }
    }


    /// <summary>
    /// �J�������߂Â��Ă���R�}���h��\�����郁�\�b�h
    /// </summary>
    /// <param name="charaCommand">�N���b�N���ꂽ�L�����̎q�I�u�W�F�N�g�i�R�}���h�j</param>
    /// <returns></returns>
    public IEnumerator MoveStandby(Transform charaCommand)
    {
        while (true)
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
                    //�Z�R�}���h��������\��
                    charaCommand.gameObject.SetActive(true);
                }
            }
        }
    }


    /// <summary>
    /// ClickedGameObject���\�b�h�ŌĂяo�����B�N���b�N���ꂽ�L�����̃R�}���h��\�����邽�߂̃��\�b�h
    /// </summary>
    /// <param name="move">�N���b�N���ꂽ�L����</param>
    void StandbyChara(GameObject move)
    {
        Transform childCommand;
        childCommand = move.transform.GetChild(ACTION);
        StartCoroutine(MoveStandby(childCommand));
    }

    void Update()
    {
        
    }
}
