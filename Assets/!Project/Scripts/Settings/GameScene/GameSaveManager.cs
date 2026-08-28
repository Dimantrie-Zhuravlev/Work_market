using UnityEngine;

public class GameSaveManager : MonoBehaviour
{
    private StructureSaveFile _saveData;
    private void Start()
    {
        TimeGameManager.OnHourPassed += SaveDataFile;
    }

    private void SaveDataFile()
    {
        LoadGameData.Instance.SaveFileSetting(new StructureSaveFile(
            PlayerWallet.Instance.CurrentBalance, //Текущий баланс
            ExperienceSystem.Instance.Experience //Текущий опыт      
            ));
    }

    public void InstantiateDataGame()
    {
        _saveData = LoadGameData.Instance.FileData;
        PlayerWallet.Instance.LoadInitialWallet(_saveData.CurrentBalance);
        ExperienceSystem.Instance.InitialExperience(_saveData.Experience);
    }

    private void OnDestroy()
    {
        TimeGameManager.OnFiveMinutesPassed -= SaveDataFile;
    }
}
