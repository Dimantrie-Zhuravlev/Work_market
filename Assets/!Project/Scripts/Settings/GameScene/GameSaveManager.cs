using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class GameSaveManager : MonoBehaviour
{
    private StructureSaveFile _saveData;

    private List<CurrentBoxSetting> _globalEntitiesCache;

    [SerializeField] GameObject _player;

    private TrayController saveTray; //чтобы не искать несколько раз закэширую

    private void Awake()
    {
        saveTray = UnityEngine.Object.FindFirstObjectByType<TrayController>();
    }
    public void SaveDataFile()
    {
        _globalEntitiesCache = Resources.FindObjectsOfTypeAll<CurrentBoxSetting>().ToList();


        var sceneData = new List<StructureBoxSave>();
        Quaternion playerRotation = _player.GetComponent<PlayerRotation>().CameraRotation();

        LoadGameData.Instance.SaveFileSetting(new StructureSaveFile(
        true,
        PlayerWallet.Instance.CurrentBalance, //Текущий баланс
        ExperienceSystem.Instance.Experience, //Текущий опыт
        EngineController.Instance.EngineData, //Двигатель
        _globalEntitiesCache.Where(item => item.gameObject.activeInHierarchy).Select(item => item.GetStructureData()).ToList(), //Коробки
        new StructurePositionData(_player.transform.position, playerRotation), //Игрок
        new BoardTasks(TaskBoards.Main.TaskBoardController.Instance.GetTasksList(), TaskBoards.Current.TaskBoardController.Instance.CurrentData, QuestProductsController.Instance.QuestData), //Задания
        saveTray.SaveTrayData()
        ));
    }
    public void InstantiateDataGame()
    {
        _saveData = LoadGameData.Instance.FileData;
        PlayerWallet.Instance.LoadInitialWallet(_saveData.CurrentBalance);
        ExperienceSystem.Instance.InitialExperience(_saveData.Experience);
        EngineController.Instance.InitializeStartCapacity(_saveData.EngineData, _saveData.HasSavedGame);

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
        if (_saveData.HasSavedGame) //+ то что не иницилизируется при старте как значения
        {
            _player.transform.position = _saveData.Player.Position;
            _player.GetComponent<PlayerRotation>().SetCameraRotation(_saveData.Player.Rotation); //Это игрок
            _player.SetActive(true);

            TaskBoards.Main.TaskBoardController.Instance.LoadDataList(_saveData.Tasks.ListMainTasks);
            if (_saveData.Tasks.CurrentTask.Reward != new Money(0,0))
            {
                TaskBoards.Current.TaskBoardController.Instance.AddActiveTask(_saveData.Tasks.CurrentTask);
                QuestProductsController.Instance.LoadData(_saveData.Tasks.GhostsElements);
            }
            saveTray.LoadData(_saveData.Tray);
        } else
        {
            _player.SetActive(true);
        }

    }
}
