using UnityEditor.Overlays;
using UnityEngine;
using UnityEngine.Playables;

public class GameSaveManager : MonoBehaviour
{
    private StructureSaveFile _saveData;
    private void Start()
    {
        TimeGameManager.OnHourPassed += SaveDataFile;
    }

    private void SaveDataFile()
    {
        LoadGameData.Instance.SaveFileSetting(new StructureSaveFile(PlayerWallet.Instance.CurrentBalance));
    }

    public void InstantiateDataGame() {
        _saveData = LoadGameData.Instance.FileData;
        PlayerWallet.Instance.LoadInitialWallet(_saveData.CurrentBalance);
    }

    private void OnDestroy()
    {
        TimeGameManager.OnFiveMinutesPassed -= SaveDataFile;
    }
}
