using System.IO;
using System.Text;
using UnityEngine;

public class GameDataExport : MonoBehaviour
{
    // System.IO
    private StreamWriter sw;

    // Start is called before the first frame update
    void Start()
    {

        // 新しくcsvファイルを作成して、{}の中の要素分csvに追記をする
        sw = new StreamWriter(@"SaveData/game_data.csv", false, Encoding.GetEncoding("Shift_JIS"));

        // CSV1行目のカラムで、StreamWriter オブジェクトへ書き込む
        string[] labels = { "Success", "FishNumber", "ClearTime", "Distance", "Sensitivity", "LimitedTime", "MaximumSpeed", "Acceleration", "Friction"};

         // 文字列配列のすべての要素を「,」で連結する
        string label = string.Join(",", labels);

        // 「,」で連結した文字列をcsvファイルへ書き込む
        sw.WriteLine(label);
    }

    public void ExportGameData(bool success, int fishNumber, string clearTime, float distance, 
        float sensitivity, int limitedTime, float maximumSpeed, float acceleration, float friction)
    {
        string[] data = { success.ToString(), fishNumber.ToString(), clearTime, distance.ToString(), sensitivity.ToString(), limitedTime.ToString(), maximumSpeed.ToString(), acceleration.ToString(), friction.ToString() };
        string saveData = string.Join(",", data);

        sw.WriteLine(saveData);
    }

    public void SaveCSV()
    {
        sw.Close();
    }
}
