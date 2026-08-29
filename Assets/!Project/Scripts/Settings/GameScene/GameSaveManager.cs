using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class GameSaveManager : MonoBehaviour
{
    private StructureSaveFile _saveData;

    private List<CurrentBoxSetting> _globalEntitiesCache;
    public void SaveDataFile()
    {
        print("saveFile");
        _globalEntitiesCache = Resources.FindObjectsOfTypeAll<CurrentBoxSetting>().ToList();

        var sceneData = new List<StructureBoxSave>();

        LoadGameData.Instance.SaveFileSetting(new StructureSaveFile(
        PlayerWallet.Instance.CurrentBalance, //Текущий баланс
        ExperienceSystem.Instance.Experience, //Текущий опыт
        EngineController.Instance.EngineData,
        _globalEntitiesCache.Where(item => item.gameObject.activeInHierarchy).Select(item => item.GetStructureData()).ToList()

    ));
    }
    public void InstantiateDataGame()
    {
        _saveData = LoadGameData.Instance.FileData;
        PlayerWallet.Instance.LoadInitialWallet(_saveData.CurrentBalance);
        ExperienceSystem.Instance.InitialExperience(_saveData.Experience);
        EngineController.Instance.InitializeStartCapacity(_saveData.EngineData);

        //коробки
        var sceneData = _saveData.BoxesData;
        // Сначала восстанавливаем ВСЕХ, кто уже лежит на сцене
        if (_saveData.BoxesData.Count > 0)
        {
            var entitiesOnScene = Resources.FindObjectsOfTypeAll<CurrentBoxSetting>().ToList();

            foreach (var fsdfbox in entitiesOnScene)
            {
                fsdfbox.InitNameId();
            }

            foreach (var box in sceneData)
            {
                var element = entitiesOnScene.Find(item => item.NameId == box.NameId);
                if (element.NameId != "") element.RestoreState(box);
            }
        }
    }
}
