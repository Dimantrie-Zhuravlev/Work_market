using System;
using UnityEngine;
using System.IO;

public class DataContainer
{
    public StructureSaveFile SaveFile;
}

public class LoadGameData : MonoBehaviour
{
    [SerializeField] SceneSwitcher _sceneSwitcher;
    private int currentIndexGame;

    public static LoadGameData Instance;

    private StructureSaveFile fileData;
    public StructureSaveFile FileData => fileData;
    private string currentFilePath;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (Instance == null)
        {
            Instance = this;
        }
        else
        { Destroy(gameObject); return; }
    }
    public void LoadSettings(int indexFile)
    {
        currentIndexGame = indexFile; ;
        string basePath = Application.persistentDataPath;
        currentFilePath = $"{basePath}/FilesSettings/Market_settings_{currentIndexGame}.json";
        if (File.Exists(currentFilePath))
        {
            string json = File.ReadAllText(currentFilePath);
            fileData = JsonUtility.FromJson<DataContainer>(json).SaveFile;
        }
        else
        {
            fileData = CreateDefaultData();
            SaveFileSetting(fileData);
        }
        _sceneSwitcher.LoadGameScene();
    }

    private StructureSaveFile CreateDefaultData()
    {
        return new StructureSaveFile(new Money(10, 0));
    }
    public void SaveFileSetting(StructureSaveFile data)
    {
        string fullPath = Path.Combine(Application.persistentDataPath, "FilesSettings", currentFilePath);
        // 2. Создаем директорию, если её нет // Directory.CreateDirectory делает всё: если папка есть - ничего не будет, 
        // если её нет (включая вложенные подпапки) - создаст.
        string directoryPath = Path.GetDirectoryName(fullPath);
        if (!string.IsNullOrEmpty(directoryPath)) { Directory.CreateDirectory(directoryPath); }

        var wrapper = new DataContainer { SaveFile = data };
        File.WriteAllText(fullPath, JsonUtility.ToJson(wrapper, true));
    }
}
