using TMPro;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class SaveFileData : MonoBehaviour
{
    private int SaveFileIndex;
    [SerializeField] private TMP_Text fileName;
    [SerializeField] private TMP_Text buttonLoadName;
    [SerializeField] private GameObject buttonDelete;

    private string filejsonUrl;
    public void LoadSystem(int index)
    {
        SaveFileIndex = index;
        filejsonUrl = $"{Application.persistentDataPath}/FilesSettings/Market_settings_{index}.json";
        fileName.text = $"Сохранение {index}";
        bool hasSaveFile = File.Exists(filejsonUrl);
        buttonLoadName.text = hasSaveFile ? "Загрузить" : "Новая игра";
        if (!hasSaveFile) { buttonDelete.SetActive(false); }
    }

    public void LoadFile()
    {
        LoadGameData.Instance.LoadSettings(SaveFileIndex);
    }

    public void DeleteFile()
    {
        File.Delete(filejsonUrl);
        LoadSystem(SaveFileIndex);
    }
}
