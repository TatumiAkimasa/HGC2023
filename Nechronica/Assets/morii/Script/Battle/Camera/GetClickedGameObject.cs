using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GetClickedGameObject : MonoBehaviour
{
    [SerializeField]
    private GameObject clickedGameObject;

    private CinemachineVirtualCamera CharaCamera;   //

    [SerializeField]
    private new Camera camera;                      // ���C���J����

    [SerializeField]
    private CinemachineVirtualCamera cinemaCamera;  // �N���[���������̃V�l�}�J����

    [SerializeField]
    private CinemachineVirtualCamera MainCamera;    // �S�̂��f���V�l�}�J����


    void Update()
    {
        //���N���b�N��
        if (Input.GetMouseButtonDown(0) && CharaCamera==null)
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
            }

            if(clickedGameObject.CompareTag("PlayableChara"))
            {
                // �N���b�N�����I�u�W�F�N�g�̍��W�����擾
                Vector3 clickedObjPos = clickedGameObject.transform.position;
                // �擾�������W��񂩂班�����ꂽ�ʒu�ɍ��W��ύX
                clickedObjPos.z -= 10.0f;
                clickedObjPos.x -= 2.5f;

                // �V�l�}�J�����̃v���n�u�𐶐����N���b�N�����I�u�W�F�N�g��e�I�u�W�F�N�g�ɂ���
                CharaCamera = Instantiate(cinemaCamera, clickedObjPos, Quaternion.identity);
                CharaCamera.transform.parent = clickedGameObject.transform;

                // ���������v���n�u�J�����̃v���C�I���e�B�����C���J�����ɐݒ�
                CharaCamera.Priority = MainCamera.Priority;
            }
        }
        // �E�N���b�N��
        else if(Input.GetMouseButtonDown(1) && CharaCamera != null)
        {
            // 
            cinemaCamera.Priority = MainCamera.Priority;

            StartCoroutine(DstroyCamera());
        }

        IEnumerator DstroyCamera()
        {
            for(int i=0;i<2;i++)
            {
                if(i==0)
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
    }
}
