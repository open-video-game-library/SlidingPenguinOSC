using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using penguin;

public class RespawnManager : MonoBehaviour
{
    // 現在のステータスを管理するクラス
    [SerializeField] private InGameStatusManager statusManager;
    // ペンギンのGameObject
    [SerializeField] private GameObject penguin;
    // ペンギンのモデル
    [SerializeField] private GameObject penguinModel;
    // ペンギンの挙動を制御するクラス
    [SerializeField] private PenguinBehavior penguinBehavior;
    // InGameシーンのSE再生・停止クラス
    [SerializeField] private InGameAudio audio;

    [SerializeField] private RespawnCamera respawnCamera;

    public IEnumerator Respawn()
    {
        InGameStatus originalState = statusManager.CurrentStatus;

        // ペンギンを停止させ、操作をoffにする
        StartCoroutine(penguinBehavior.Stop(0.5f));
        penguinModel.SetActive(false);
        audio.drop.Play();
        statusManager.CurrentStatus = InGameStatus.CourseOut;
        
        // ペンギンをスタート地点に戻す処理
        yield return new WaitForSeconds(1.50f);
        respawnCamera.Teleport();
        penguin.transform.position = Vector3.zero;
        penguin.transform.eulerAngles = Vector3.zero;
        penguin.GetComponent<PenguinBehavior>().enabled = true;
        penguinModel.SetActive(true);
        statusManager.CurrentStatus = originalState;
    }
}
