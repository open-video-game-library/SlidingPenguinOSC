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

        // �V����csv�t�@�C�����쐬���āA{}�̒��̗v�f��csv�ɒǋL������
        sw = new StreamWriter(@"SaveData/game_data.csv", false, Encoding.GetEncoding("Shift_JIS"));

        // CSV1�s�ڂ̃J�����ŁAStreamWriter �I�u�W�F�N�g�֏�������
        string[] labels = { "Success", "FishNumber", "ClearTime", "Distance", "Sensitivity", "LimitedTime", "MaximumSpeed", "Acceleration", "Friction"};

         // ������z��̂��ׂĂ̗v�f���u,�v�ŘA������
        string label = string.Join(",", labels);

        // �u,�v�ŘA�������������csv�t�@�C���֏�������
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
