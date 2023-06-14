using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class ParameterReader : MonoBehaviour
{
    private TextAsset parameterCsvFile; // CSV�t�@�C��
    private List<string[]> parameterDatas = new List<string[]>(); // CSV�̒��g�����郊�X�g;

    [SerializeField] private TMP_InputField sensitivity;
    [SerializeField] private TMP_InputField limitedTime;
    [SerializeField] private TMP_InputField maximumSpeed;
    [SerializeField] private TMP_InputField acceleration;
    [SerializeField] private TMP_InputField friction;

    void Start()
    {
        parameterCsvFile = Resources.Load("parameter") as TextAsset; // Resouces����CSV�ǂݍ���
        StringReader reader = new StringReader(parameterCsvFile.text);

        // , �ŕ�������s���ǂݍ���
        // ���X�g�ɒǉ����Ă���
        while (reader.Peek() != -1) // reader.Peaek��-1�ɂȂ�܂�
        {
            string line = reader.ReadLine(); // ��s���ǂݍ���
            parameterDatas.Add(line.Split(',')); // , ��؂�Ń��X�g�ɒǉ�
        }
    }

    public void SetParameters()
    {
        // ParameterManager �Œ�`����Ă���p�����[�^�ɁACSV�t�@�C���Őݒ肵���p�����[�^���㏑������
        if (parameterCsvFile)
        {
            SetSensitivityValue(parameterDatas[0][1]);
            SetLimitedTimeValue(parameterDatas[1][1]);
            SetMaximumSpeedValue(parameterDatas[2][1]);
            SetAccelerationValue(parameterDatas[3][1]);
            SetFrictionValue(parameterDatas[4][1]);
        }
    }

    private void SetSensitivityValue(string value)
    {
        if (!float.TryParse(value, out float floatValue) || floatValue < 0.0f) { return; }
        sensitivity.text = value;
        ParameterManager.sensitivity = floatValue;
    }

    private void SetLimitedTimeValue(string value)
    {
        if (!int.TryParse(value, out int intValue) || intValue < 0) { return; }
        limitedTime.text = value;
        ParameterManager.limitedTime = intValue;
    }

    private void SetMaximumSpeedValue(string value)
    {
        if (!float.TryParse(value, out float floatValue) || floatValue < 0.0f || floatValue < ParameterManager.acceleration) { return; }
        maximumSpeed.text = value;
        ParameterManager.maximumSpeed = floatValue;
    }

    private void SetAccelerationValue(string value)
    {
        if (!float.TryParse(value, out float floatValue) || floatValue < 0.0f || floatValue > ParameterManager.maximumSpeed) { return; }
        acceleration.text = value;
        ParameterManager.acceleration = floatValue;
    }

    private void SetFrictionValue(string value)
    {
        if (!float.TryParse(value, out float floatValue) || floatValue < 0.0f || floatValue > 1.0f) { return; }
        friction.text = value;
        ParameterManager.friction = floatValue;
    }
}
