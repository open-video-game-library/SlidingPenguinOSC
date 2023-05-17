using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using penguin;

public class OSCControlToggleController : MonoBehaviour
{
    private Toggle toggle;

    // Start is called before the first frame update
    void Start()
    {
        toggle = GetComponent<Toggle>();
        PenguinBehavior.isReceiveOSCInput = toggle.isOn;
    }

    public void SetOSCControlSetting()
    {
        PenguinBehavior.isReceiveOSCInput = toggle.isOn;
    }
}
