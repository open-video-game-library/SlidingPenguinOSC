using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using TMPro;

public class ParameterReader : MonoBehaviour
{
    // private TextAsset parameterCsvFile; // CSVファイル
    private List<string[]> parameterDatas = new List<string[]>(); // CSVの中身を入れるリスト

    [SerializeField] private TMP_InputField sensitivity;
    [SerializeField] private TMP_InputField limitedTime;
    [SerializeField] private TMP_InputField maximumSpeed;
    [SerializeField] private TMP_InputField acceleration;
    [SerializeField] private TMP_InputField friction;

    void Start()
    {
        StreamReader reader = new StreamReader(Application.dataPath + "/Parameter/parameter.csv", Encoding.GetEncoding("UTF-8"));
        string line;

        while ((line = reader.ReadLine()) != null)
        {
            string[] splitLine = line.Split(",");
            parameterDatas.Add(splitLine);
        }
    }

    public void SetParameters()
    {
        // ParameterManager で定義されているパラメータに、CSVファイルで設定したパラメータを上書きする
        SetSensitivityValue(parameterDatas[1][0]);
        SetLimitedTimeValue(parameterDatas[1][1]);
        SetMaximumSpeedValue(parameterDatas[1][2]);
        SetAccelerationValue(parameterDatas[1][3]);
        SetFrictionValue(parameterDatas[1][4]);
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
