using System.IO;
using System.Text;
using UnityEngine;
using System;

public class GameDataExport : MonoBehaviour
{
    // System.IO
    private static StreamWriter sw;

    public static void CreateCSV()
    {
        DateTime currentTime = DateTime.Now;
        string year = currentTime.Year.ToString();
        string month = currentTime.Month.ToString();
        string day = currentTime.Day.ToString();
        string hour = currentTime.Hour.ToString();
        string minute = currentTime.Minute.ToString();
        string second = currentTime.Second.ToString();

        // 新しくcsvファイルを作成
        sw = new StreamWriter(@"SaveData/game_data_" + year + month + day + hour + minute + second + ".csv", false, Encoding.GetEncoding("Shift_JIS"));

        // ラベルを書き込む
        string[] labels = { "Success", "FishNumber", "ClearTime", "Distance", "Sensitivity", "LimitedTime", "MaximumSpeed", "Acceleration", "Friction" };

        // 文字列配列のすべての要素を「,」で連結する
        string label = string.Join(",", labels);

        // 「,」で連結した文字列をcsvファイルへ書き込む
        sw.WriteLine(label);
    }

    public static void ExportGameData(bool success, int fishNumber, string clearTime, float distance, 
        float sensitivity, int limitedTime, float maximumSpeed, float acceleration, float friction)
    {
        string[] data = { success.ToString(), fishNumber.ToString(), clearTime, distance.ToString(), sensitivity.ToString(), limitedTime.ToString(), maximumSpeed.ToString(), acceleration.ToString(), friction.ToString() };
        string saveData = string.Join(",", data);

        sw.WriteLine(saveData);
    }

    public static void SaveCSV()
    {
        sw.Close();
    }
}
