using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWarp : CheckUp_Base
{
    public Transform WarpZone;
    void Update()
    {
        if (PL != null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {//�@CharacterController�R���|�[�l���g����U����������
                PL.GetComponent<CharacterController>().enabled = false;
                //�@�L�����N�^�[�̈ʒu��ύX����
                PL.transform.position = WarpZone.position;
                //�@CharacterController�R���|�[�l���g��L��������
                PL.GetComponent<CharacterController>().enabled = true;
                PL = null;
            }
        }
    }
}
