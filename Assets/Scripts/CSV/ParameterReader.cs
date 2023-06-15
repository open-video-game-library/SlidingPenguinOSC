using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class ParameterReader : MonoBehaviour
{
    private TextAsset parameterCsvFile; // CSVファイル
    private List<string[]> parameterDatas = new List<string[]>(); // CSVの中身を入れるリスト;

    [SerializeField] private TMP_InputField sensitivity;
    [SerializeField] private TMP_InputField limitedTime;
    [SerializeField] private TMP_InputField maximumSpeed;
    [SerializeField] private TMP_InputField acceleration;
    [SerializeField] private TMP_InputField friction;

    void Start()
    {
        parameterCsvFile = Resources.Load("parameter") as TextAsset; // Resouces下のCSV読み込み
        StringReader reader = new StringReader(parameterCsvFile.text);

        // , で分割しつつ一行ずつ読み込み
        // リストに追加していく
        while (reader.Peek() != -1) // reader.Peaekが-1になるまで
        {
            string line = reader.ReadLine(); // 一行ずつ読み込み
            parameterDatas.Add(line.Split(',')); // , 区切りでリストに追加
        }
    }

    public void SetParameters()
    {
        // ParameterManager で定義されているパラメータに、CSVファイルで設定したパラメータを上書きする
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
