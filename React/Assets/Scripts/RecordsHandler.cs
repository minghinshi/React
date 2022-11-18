using Newtonsoft.Json;
using System.IO;
using UnityEngine;

public class RecordsHandler : MonoBehaviour
{
    public static Records records;

    private void Start()
    {
        LoadRecords();
    }

    private void OnApplicationQuit()
    {
        SaveRecords();
    }

    private void SaveRecords()
    {
        string json = JsonConvert.SerializeObject(records);
        File.WriteAllText(GetSaveFilePath(), json);
    }

    private void LoadRecords()
    {
        bool hasSaveFile = File.Exists(GetSaveFilePath());
        records = hasSaveFile ? GetRecords() : new();
        GameInterface.instance.UpdateMenuScoreDisplay();
    }

    private string GetDataPath()
    {
        return Application.isEditor ? Application.dataPath + "/Saves" : Application.persistentDataPath;
    }

    private string GetSaveFilePath()
    {
        return GetDataPath() + "/save.json";
    }

    private Records GetRecords()
    {
        string json = File.ReadAllText(GetSaveFilePath());
        return JsonConvert.DeserializeObject<Records>(json);
    }
}
