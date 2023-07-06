using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectToggleController : MonoBehaviour
{
    private Toggle toggle;

    // Start is called before the first frame update
    void Start()
    {
        toggle = GetComponent<Toggle>();
        toggle.isOn = ParameterManager.gameEffect;
    }

    public void SetSetting()
    {
        ParameterManager.gameEffect = toggle.isOn;
    }
}
