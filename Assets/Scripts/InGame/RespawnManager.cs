using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using penguin;

public class RespawnManager : MonoBehaviour
{
    // ���݂̃X�e�[�^�X���Ǘ�����N���X
    [SerializeField] private InGameStatusManager statusManager;
    // �y���M����GameObject
    [SerializeField] private GameObject penguin;
    // �y���M���̃��f��
    [SerializeField] private GameObject penguinModel;
    // �y���M���̋����𐧌䂷��N���X
    [SerializeField] private PenguinBehavior penguinBehavior;
    // InGame�V�[����SE�Đ��E��~�N���X
    [SerializeField] private InGameAudio audio;

    // �����ł���ӏ����Ǘ�����N���X
    [SerializeField] private CheckPointsManager checkPoints;

    [SerializeField] private RespawnCamera respawnCamera;

    public IEnumerator Respawn()
    {
        if (statusManager.CurrentStatus == InGameStatus.InGameNormal || statusManager.CurrentStatus == InGameStatus.HurryUp)
        {
            InGameStatus originalState = statusManager.CurrentStatus;
            Vector3 originalPenguinPosition = penguin.transform.position;

            // �y���M�����~�����A�����off�ɂ���
            StartCoroutine(penguinBehavior.Stop(0.5f));
            penguinModel.SetActive(false);
            audio.drop.Play();
            statusManager.CurrentStatus = InGameStatus.CourseOut;

            // �y���M�����X�^�[�g�n�_�ɖ߂�����
            yield return new WaitForSeconds(1.50f);

            respawnCamera.Teleport();
            penguin.transform.position = checkPoints.DecideRespawnPosition(originalPenguinPosition);
            penguin.transform.eulerAngles = Vector3.zero;
            penguin.GetComponent<PenguinBehavior>().enabled = true;
            penguinModel.SetActive(true);
            statusManager.CurrentStatus = originalState;
        }
    }
}
