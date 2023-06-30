using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using penguin;

public class RespawnManager : MonoBehaviour
{
    // ���݂̃X�e�[�^�X���Ǘ�����N���X
    [SerializeField] private InGameStatusManager statusManager;
    // �y���M���̃��f��
    [SerializeField] private GameObject penguinModel;
    // �y���M���̋����𐧌䂷��N���X
    [SerializeField] private PenguinBehavior _penguinBehavior;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Respawn()
    {
        // �y���M�����~�����A�����off�ɂ���
        StartCoroutine(_penguinBehavior.Stop(0.5f));
        penguinModel.SetActive(false);
        statusManager.CurrentStatus = InGameStatus.CourseOut;

        // ��ʂ��t�F�[�h�A�E�g�����鏈��
        // �y���M�����X�^�[�g�n�_�ɖ߂�����
    }
}
