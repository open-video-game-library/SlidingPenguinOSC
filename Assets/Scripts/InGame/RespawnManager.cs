using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using penguin;

public class RespawnManager : MonoBehaviour
{
    // 現在のステータスを管理するクラス
    [SerializeField] private InGameStatusManager statusManager;
    // ペンギンのモデル
    [SerializeField] private GameObject penguinModel;
    // ペンギンの挙動を制御するクラス
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
        // ペンギンを停止させ、操作をoffにする
        StartCoroutine(_penguinBehavior.Stop(0.5f));
        penguinModel.SetActive(false);
        statusManager.CurrentStatus = InGameStatus.CourseOut;

        // 画面をフェードアウトさせる処理
        // ペンギンをスタート地点に戻す処理
    }
}
