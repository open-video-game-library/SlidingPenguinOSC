using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayConsecutivelyToggleController : MonoBehaviour
{
    private Toggle toggle;

    // Start is called before the first frame update
    void Start()
    {
        toggle = GetComponent<Toggle>();
        toggle.isOn = ParameterManager.playConsecutively;
    }

    public void SetSetting()
    {
        ParameterManager.playConsecutively = toggle.isOn;
    }
}
