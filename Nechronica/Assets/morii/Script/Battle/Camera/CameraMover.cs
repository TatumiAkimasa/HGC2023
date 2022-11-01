using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;


// Nekomaru�l���炨�؂肵�܂����B
// https://qiita.com/Nekomasu/items/f195db36a2516e0dd460

public class CameraMover : MonoBehaviour
{
    // SD�F���E�̈ړ�
    // WS�F�㏸�E�~��
    // �E�h���b�O�F�J�����̉�]
    // ���h���b�O�F�O�㍶�E�̈ړ�
    // �X�y�[�X�F�J��������̗L���E�����̐؂�ւ�
    // P�F��]�����s���̏�Ԃɏ���������

    //�J�����̈ړ���
    [SerializeField, Range(0.1f, 10.0f)]
    private float _positionStep = 2.0f;

    //�}�E�X���x
    [SerializeField, Range(30.0f, 150.0f)]
    private float _mouseSensitive = 90.0f;

    //�J��������̗L������
    private bool _cameraMoveActive = true;
    //�J������transform  
    private Transform _camTransform;
    //�}�E�X�̎n�_ 
    private Vector3 _startMousePos;
    //�J������]�̎n�_���
    private Vector3 _presentCamRotation;
    private Vector3 _presentCamPos;
    //������� Rotation
    private Quaternion _initialCamRotation;
    //UI���b�Z�[�W�̕\��
    private bool _uiMessageActiv;

    void Start()
    {
        _camTransform = this.gameObject.transform;

        //������]�̕ۑ�
        _initialCamRotation = this.gameObject.transform.rotation;
    }

    void Update()
    {

        CamControlIsActive(); //�J��������̗L������

        if (_cameraMoveActive)
        {
            ResetCameraRotation(); //��]�p�x�̂݃��Z�b�g
            CameraPositionKeyControl(); //�J�����̃��[�J���ړ� �L�[
            CameraRotationMouseControl();  //�J�����̉�] �}�E�X
        }
    }

    //�J�����̉�] �}�E�X
    private void CameraRotationMouseControl()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _startMousePos = Input.mousePosition;
            _presentCamRotation.x = _camTransform.transform.eulerAngles.x;
            _presentCamRotation.y = _camTransform.transform.eulerAngles.y;
        }

        if (Input.GetMouseButton(0))
        {
            //(�ړ��J�n���W - �}�E�X�̌��ݍ��W) / �𑜓x �Ő��K��
            float x = (_startMousePos.x - Input.mousePosition.x) / Screen.width;
            float y = (_startMousePos.y - Input.mousePosition.y) / Screen.height;

            //��]�J�n�p�x �{ �}�E�X�̕ω��� * �}�E�X���x
            float eulerX = _presentCamRotation.x + y * _mouseSensitive;
            float eulerY = _presentCamRotation.y + x * _mouseSensitive;

            _camTransform.rotation = Quaternion.Euler(eulerX, 0, 0);
        }
    }

    //�J��������̗L������
    public void CamControlIsActive()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _cameraMoveActive = !_cameraMoveActive;

            if (_uiMessageActiv == false)
            {
                StartCoroutine(DisplayUiMessage());
            }
            Debug.Log("CamControl : " + _cameraMoveActive);
        }
    }

    //��]��������Ԃɂ���
    private void ResetCameraRotation()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            this.gameObject.transform.rotation = _initialCamRotation;
            Debug.Log("Cam Rotate : " + _initialCamRotation.ToString());
        }
    }


    //�J�����̃��[�J���ړ� �L�[
    private void CameraPositionKeyControl()
    {
        Vector3 campos = _camTransform.position;
        //if (Input.GetKey(KeyCode.W)) { campos += _camTransform.up * Time.deltaTime * _positionStep; }
        //if (Input.GetKey(KeyCode.S)) { campos -= _camTransform.up * Time.deltaTime * _positionStep; }
        //if (Input.GetKey(KeyCode.A)) { campos -= _camTransform.right * Time.deltaTime * _positionStep; }
        //if (Input.GetKey(KeyCode.D)) { campos += _camTransform.right * Time.deltaTime * _positionStep; }

        if (Input.GetKey(KeyCode.W)) { campos.y += _positionStep; }
        if (Input.GetKey(KeyCode.S)) { campos.y -= _positionStep; }
        if (Input.GetKey(KeyCode.A)) { campos.x -= _positionStep; }
        if (Input.GetKey(KeyCode.D)) { campos.x += _positionStep; }

        //if (Input.GetKey(KeyCode.A)) { campos.z -= _positionStep; }
        //if (Input.GetKey(KeyCode.D)) { campos.z += _positionStep; }

        // �J����������艺�ɂ����Ȃ��悤�ɂ���
        if (campos.y<=4.3f)
        {
            campos.y = 4.3f;
        }

        _camTransform.position = campos;
    }

    //UI���b�Z�[�W�̕\��
    private IEnumerator DisplayUiMessage()
    {
        _uiMessageActiv = true;
        float time = 0;
        while (time < 2)
        {
            time = time + Time.deltaTime;
            yield return null;
        }
        _uiMessageActiv = false;
    }

    void OnGUI()
    {
        if (_uiMessageActiv == false) { return; }
        GUI.color = Color.black;
        if (_cameraMoveActive == true)
        {
            GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height - 30, 100, 20), "�J�������� �L��");
        }

        if (_cameraMoveActive == false)
        {
            GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height - 30, 100, 20), "�J�������� ����");
        }
    }

}
