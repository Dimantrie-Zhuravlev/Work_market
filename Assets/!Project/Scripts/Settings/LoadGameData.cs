using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataContainer
{
    public StructureSaveFile SaveFile;
    public int Version;
}

public class LoadGameData : MonoBehaviour
{
    private int currentIndexGame;
    private int _version;

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
        { 
            Destroy(gameObject); 
            return;
        }
        _version = 0;
    }
    public void LoadSettings(int indexFile)
    {
        currentIndexGame = indexFile;
        string basePath = Application.persistentDataPath;
        currentFilePath = $"{basePath}/FilesSettings/Market_settings_{currentIndexGame}.json";
        if (File.Exists(currentFilePath))
        {
            string json = File.ReadAllText(currentFilePath);
            DataContainer result = JsonUtility.FromJson<DataContainer>(json);
            if (result.Version != _version)
            {
                changeVersion(result.Version);
            }
            else
            {
                fileData = result.SaveFile;
            }
        }
        else
        {
            fileData = CreateDefaultData();
            SaveFileSetting(fileData);
        }
        SceneSwitcher.LoadGameScene();
    }

    private void changeVersion(int fileVersion)
    {
        switch (fileVersion)
        {
            case 0:
                print("1");
                break;
        }
    }


    private StructureSaveFile CreateDefaultData()
    {
        return new StructureSaveFile(new Money(10, 0), new StructureExperience(0, new Money(0, 0)), new StructureEngineData(-1000, 1, 0), new List<StructureBoxSave>());
    }
    public void SaveFileSetting(StructureSaveFile data)
    {
        string fullPath = Path.Combine(Application.persistentDataPath, "FilesSettings", currentFilePath);
        // 2. Создаем директорию, если её нет // Directory.CreateDirectory делает всё: если папка есть - ничего не будет, 
        // если её нет (включая вложенные подпапки) - создаст.
        string directoryPath = Path.GetDirectoryName(fullPath);
        if (!string.IsNullOrEmpty(directoryPath)) { Directory.CreateDirectory(directoryPath); }

        var wrapper = new DataContainer { SaveFile = data, Version = _version };
        File.WriteAllText(fullPath, JsonUtility.ToJson(wrapper, true));
    }
}
