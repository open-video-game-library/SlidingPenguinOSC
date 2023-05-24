using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using TMPro;

public class InputFieldManager : MonoBehaviour
{
    // �p�����[�^�𒲐�����InputField�B�����\�p�����[�^�𑝂₷�ꍇInputField���ǉ����A�Q�Ƃ���B
    [SerializeField] private TMP_InputField sensitivity;
    [SerializeField] private TMP_InputField limitedTime;
    [SerializeField] private TMP_InputField maximumSpeed;
    [SerializeField] private TMP_InputField acceleration;
    [SerializeField] private TMP_InputField friction;

    // Start is called before the first frame update
    void Start()
    {
        sensitivity.text = ParameterManager.sensitivity.ToString("f2");
        limitedTime.text = ParameterManager.limitedTime.ToString();
        maximumSpeed.text = ParameterManager.maximumSpeed.ToString("f2");
        acceleration.text = ParameterManager.acceleration.ToString("f2");
        friction.text = ParameterManager.friction.ToString("f2");
    }

    public void SetSensitivityValue()
    {
        if (!float.TryParse(sensitivity.text, out float floatValue)
            || floatValue < 0.0f)
        {
            sensitivity.text = ParameterManager.sensitivity.ToString("f2");
            return;
        }
        ParameterManager.sensitivity = floatValue;
    }

    public void SetLimitedTimeValue()
    {
        if (!int.TryParse(limitedTime.text, out int intValue)
            || intValue < 0)
        {
            limitedTime.text = ParameterManager.limitedTime.ToString("f2");
            return;
        }
        ParameterManager.limitedTime = intValue;
    }

    public void SetMaximumSpeedValue()
    {
        if (!float.TryParse(maximumSpeed.text, out float floatValue)
            || floatValue < 0.0f
            || floatValue < ParameterManager.acceleration)
        {
            maximumSpeed.text = ParameterManager.maximumSpeed.ToString("f2");
            return;
        }
        ParameterManager.maximumSpeed = floatValue;
    }

    public void SetAccelerationValue()
    {
        if (!float.TryParse(acceleration.text, out float floatValue)
            || floatValue < 0.0f
            || floatValue > ParameterManager.maximumSpeed)
        {
            acceleration.text = ParameterManager.acceleration.ToString("f2");
            return;
        }
        ParameterManager.acceleration = floatValue;
    }

    public void SetFrictionValue()
    {
        if (!float.TryParse(friction.text, out float floatValue)
            || floatValue < 0.0f
            || floatValue > 1.0f)
        {
            friction.text = ParameterManager.friction.ToString("f2");
            return;
        }
        ParameterManager.friction = floatValue;
    }
}
