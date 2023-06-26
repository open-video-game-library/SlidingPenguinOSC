using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System.IO;

public class Build
{
    [PostProcessBuildAttribute(1)]
    public static void OnPostprocessBuild(BuildTarget target, string path)
    {
        string outFolderPath = Path.GetDirectoryName(path) + "/SlidingPenguinOSC_Data/Parameter";

        if (Directory.Exists(outFolderPath))
        {
            Directory.Delete(outFolderPath, true);
        }
        FileUtil.CopyFileOrDirectory(Application.dataPath + "/Parameter", outFolderPath);
    }
}