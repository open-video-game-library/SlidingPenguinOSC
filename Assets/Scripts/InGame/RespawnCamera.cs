using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using penguin;

public class RespawnCamera : MonoBehaviour
{
    private CinemachineVirtualCamera virtualCamera;
    private CinemachineFramingTransposer transposer;

    private Vector3 defaultPosition;

    void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        transposer = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();

        defaultPosition = transform.position;
    }

    public void Teleport()
    {
        transposer.m_XDamping = 0.0f;
        transposer.m_YDamping = 0.0f;
        transposer.m_ZDamping = 0.0f;
        transform.position = defaultPosition;

        // ���̃t���[���Ō��ɖ߂�
        StartCoroutine(ResetBrainUpdateMethod());
    }

    private IEnumerator ResetBrainUpdateMethod()
    {
        // 1�t���[���҂�
        for (int i = 0; i < 10; i++) { yield return null; }

        // ���ɖ߂�
        transposer.m_XDamping = 5.0f;
        transposer.m_YDamping = 5.0f;
        transposer.m_ZDamping = 5.0f;
    }
}
