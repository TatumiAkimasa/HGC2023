using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetClickedGameObject : MonoBehaviour
{
    [SerializeField]
    private GameObject clickedGameObject;

    [SerializeField]
    private new Camera camera;


    private void Start()
    {
        
    }

    void Update()
    {
        //���N���b�N��
        if (Input.GetMouseButtonDown(0))
        {
            //���L�ϐ���������
            clickedGameObject = null;

            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();

            if (Physics.Raycast(ray, out hit))
            {
                clickedGameObject = hit.collider.gameObject;
            }

            Vector3 pos = camera.transform.position;

            pos.x = clickedGameObject.transform.position.x;
            pos.z = clickedGameObject.transform.position.z - 5.0f;

            camera.transform.position = pos;

            Debug.Log(clickedGameObject);
        }
    }
}
