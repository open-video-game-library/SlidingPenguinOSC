using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System;

public enum ExportData
{
    ScoreData,
    TrailData
}

public class GameDataExport : MonoBehaviour
{
    // System.IO
    private static StreamWriter scoreSW;
    private static StreamWriter trailSW;

    public static void CreateScoreCSV()
    {
        DateTime currentTime = DateTime.Now;
        string year = currentTime.Year.ToString();
        string month = currentTime.Month.ToString();
        string day = currentTime.Day.ToString();
        string hour = currentTime.Hour.ToString();
        string minute = currentTime.Minute.ToString();
        string second = currentTime.Second.ToString();

        // SaveDataフォルダが存在しない場合は、新しく作る
        if (!Directory.Exists(Application.dataPath + "/ScoreData")) { Directory.CreateDirectory(Application.dataPath + "/ScoreData"); }
        scoreSW = new StreamWriter(@Application.dataPath + "/ScoreData/" + year + "-" + month + "-" + day + "-" + hour + "-" + minute + "-" + second + "_result.csv", 
            false, Encoding.GetEncoding("UTF-8"));

        // ラベルを書き込む
        string[] labels = { "Success", "FishNumber", "ClearTime", "Distance", "Sensitivity", "LimitedTime", "MaximumSpeed", "Acceleration", "Friction" };

        // 文字列配列のすべての要素を「,」で連結する
        string label = string.Join(",", labels);

        // 「,」で連結した文字列をcsvファイルへ書き込む
        scoreSW.WriteLine(label);
    }

    public static void CreateTrailCSV()
    {
        DateTime currentTime = DateTime.Now;
        string year = currentTime.Year.ToString();
        string month = currentTime.Month.ToString();
        string day = currentTime.Day.ToString();
        string hour = currentTime.Hour.ToString();
        string minute = currentTime.Minute.ToString();
        string second = currentTime.Second.ToString();

        // TrailDataフォルダが存在しない場合は、新しく作る
        if (!Directory.Exists(Application.dataPath + "/TrailData")) { Directory.CreateDirectory(Application.dataPath + "/TrailData"); }
        trailSW = new StreamWriter(@Application.dataPath + "/TrailData/" + year + "-" + month + "-" + day + "-" + hour + "-" + minute + "-" + second + "-trial" + ExperimentManager.trialCount + "_trail.csv", 
            false, Encoding.GetEncoding("UTF-8"));

        // ラベルを書き込む
        string[] labels = { "time stamp", "penguin x", "penguin y" };
        // 文字列配列のすべての要素を「,」で連結する
        string label = string.Join(",", labels);

        // 「,」で連結した文字列をcsvファイルへ書き込む
        trailSW.WriteLine(label);
    }

    public static void ExportGameData(bool success, int fishNumber, string clearTime, float distance, List<Trail> trail,
        float sensitivity, int limitedTime, float maximumSpeed, float acceleration, float friction)
    {
        string[] score = { success.ToString(), fishNumber.ToString(), clearTime, distance.ToString(), sensitivity.ToString(), limitedTime.ToString(), maximumSpeed.ToString(), acceleration.ToString(), friction.ToString() };
        string scoreData = string.Join(",", score);
        scoreSW.WriteLine(scoreData);

        for (int i = 0; i < trail.Count; i++)
        {
            string trailData = string.Join(",", trail[i].timeStamp, trail[i].x, trail[i].y);
            trailSW.WriteLine(trailData);
        }
        SaveCSV(ExportData.TrailData);
    }

    public static void SaveCSV(ExportData exportData)
    {
        switch (exportData)
        {
            case ExportData.ScoreData:
                scoreSW.Close();
                break;
            case ExportData.TrailData:
                trailSW.Close();
                break;
        }
    }
}
