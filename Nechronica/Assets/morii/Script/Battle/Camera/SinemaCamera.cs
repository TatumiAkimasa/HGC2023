using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SinemaCamera : CinemachineExtension
{
    // �eStage�̃J�������[�N������ɌĂ΂��R�[���o�b�N
    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage,
        ref CameraState state,
        float deltaTime
    )
    {
        // ���O��stage�̒l���o��
        Debug.Log($"stage = {stage}");
    }
}
