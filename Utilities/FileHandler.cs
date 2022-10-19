using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class FileHandler
{
    public string ReadFromFileAppDataFolder(string fileName)
    {

        string filePath = Application.persistentDataPath + "/" + fileName;
        Debug.Log(filePath);

        string[] dataText = File.ReadAllLines(filePath);

        string output = "";
        foreach (string line in dataText)
        {
            Debug.Log(line);
            output += line;
        };

        return output;
    }

    public string ReadFromFileResourcesFolder(string fileName)
    {
        string filePath = "Assets/Resources/" + "/" + fileName;
        Debug.Log(filePath);

        string[] dataText = File.ReadAllLines(filePath);

        string output = "";
        foreach (string line in dataText)
        {
            Debug.Log(line);
            output += line;
        };

        return output;
    }

    public void WriteDataToFileAppDataFolder(string fileName, string jsonString)
    {

        string filePath = Application.persistentDataPath + "/" + fileName;

        Debug.Log("AssetPath:" + filePath);
        File.WriteAllText(filePath, jsonString);

        

#if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
#endif
    }

    public void WriteDataToFileResourcesFolder(string fileName, string jsonString)
    {

        string filePath = "Assets/Resources/" + fileName;

        Debug.Log("AssetPath:" + filePath);
        File.WriteAllText(filePath, jsonString);



#if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
#endif
    }

}
