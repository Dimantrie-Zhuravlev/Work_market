using TMPro;
using UnityEngine;
using System.IO;

public class SaveFileData : MonoBehaviour
{
    private int SaveFileIndex;
    [SerializeField] private TMP_Text fileName;
    [SerializeField] private TMP_Text buttonName;
    public void LoadSystem(int index)
    {
        SaveFileIndex = index;
        string filejsonUrl = $"{Application.persistentDataPath}/FilesSettings/Market_settings_{index}.json";
        fileName.text = $"Сохранение {index}";
        buttonName.text = File.Exists(filejsonUrl) ? "Загрузить" : "Новая игра";
    }

    public void LoadFile()
    {
        LoadGameData.Instance.LoadSettings(SaveFileIndex);
    }
}
