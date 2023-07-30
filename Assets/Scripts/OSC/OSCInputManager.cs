using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using OscCore;
using penguin;

public class OSCInputManager : MonoBehaviour
{
    [System.NonSerialized]
    public Vector2 speed;

    [System.NonSerialized]
    public int acceleration;

    private OscClient client = new OscClient("127.0.0.1", 9001);
    private int oscStatus;

    // 現在のステータスを管理するクラス
    [SerializeField] private InGameStatusManager statusManager;

    private void Update()
    {
        if (!PenguinBehavior.isReceiveOSCInput) { return; }

        bool isInGame = (statusManager.CurrentStatus == InGameStatus.InGameNormal ||
                             statusManager.CurrentStatus == InGameStatus.HurryUp);

        if (isInGame) { oscStatus = 1; }
        else if (statusManager.CurrentStatus == InGameStatus.CourseOut && ParameterManager.respawn) { oscStatus = 2; }
        else { oscStatus = 0; }

        ExportStatus();
        ExportHapticStatus();
    }

    public void InputMovingSpeed(Vector3 inputSpeed)
    {
        speed.x = inputSpeed.x;
        speed.y = inputSpeed.y;
    }

    public void InputAcceleration(int inputAcceleration)
    {
        acceleration = inputAcceleration;
    }

    public void ExportStatus()
    {
        if (PenguinBehavior.isReceiveOSCInput)
        {
            int status = oscStatus;
            client.Send("/status", status);
        }
    }

    public void ExportHapticStatus()
    {
        int status = Convert.ToInt32(ParameterManager.shareHaptic);
        client.Send("/hapticStatus", status);
        Debug.Log(status);
    }
}
