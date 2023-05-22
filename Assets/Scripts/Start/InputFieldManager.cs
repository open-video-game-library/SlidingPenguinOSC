using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using TMPro;

public class InputFieldManager : MonoBehaviour
{
    // パラメータを調整するスライダー。調整可能パラメータを増やす場合sliderも追加し、参照する。
    [SerializeField] private TMP_InputField sensitivity;
    [SerializeField] private TMP_InputField limitedTime;

    // Start is called before the first frame update
    void Start()
    {
        sensitivity.text = ParameterManager.sensitivity.ToString("f2");
        limitedTime.text = ParameterManager.limitedTime.ToString("f2");
    }

    public void SetSensitivityValue()
    {
        if (!float.TryParse(sensitivity.text, out float floatValue))
        {
            sensitivity.text = ParameterManager.sensitivity.ToString("f2");
            return;
        }
        ParameterManager.sensitivity = floatValue;
    }

    public void SetLimitedTimeValue()
    {
        if (!int.TryParse(limitedTime.text, out int intValue))
        {
            limitedTime.text = ParameterManager.limitedTime.ToString("f2");
            return;
        }
        ParameterManager.limitedTime = intValue;
    }
}
