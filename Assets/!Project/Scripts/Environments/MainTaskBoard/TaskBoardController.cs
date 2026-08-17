using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TaskBoards.Main
{
    public class DataContainer
    {
        public SctructureTasksSettingsServer[] TasksArray;
    }
    public class TaskBoardController : MonoBehaviour
    {
        private List<GameObject> _tasksList = new List<GameObject>();
        private readonly int _maxTasksCount = 8;

        private const string _tasksSettingsServerName = "Market_tasks_settings.json";
        private DataContainer _tasksSettings;

        public static TaskBoards.Main.TaskBoardController Instance;

        public bool CanAddNewTask => currentCountsTasks < _maxTasksCount;

        private int currentCountsTasks = 0;

        async void Start()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            await LoadTasksSettingsAsync();


            Transform tasksContainer = transform.GetChild(1);
            for (int i = 0; i < tasksContainer.childCount; i++)
            {
                GameObject currentItem = tasksContainer.GetChild(i).gameObject;
                _tasksList.Add(currentItem);
                currentItem.SetActive(false);
            }
        }

        public void AddNewTask()
        {
            if (CanAddNewTask)
            {
                GameObject currentTask = _tasksList.Find(elem => !elem.activeInHierarchy);
                currentTask.GetComponent<TaskBoards.Main.TaskController>().SetTaskQuest(_tasksSettings.TasksArray[Random.Range(0, 5)]);
                currentTask.SetActive(true);
                currentCountsTasks = Mathf.Clamp(currentCountsTasks + 1, 0, _maxTasksCount);
            }
            else
            {
                PersonMessageLifeCycle.Instance.SendLifeCycleMessage("Лимит заданий");
            }

        }

        public void DeleteSelectedTask(GameObject selectedTask)
        {
            int index = _tasksList.IndexOf(selectedTask);
            _tasksList[index].SetActive(false);
            currentCountsTasks = _tasksList.Count(obj => obj.activeInHierarchy);
        }


        private async Task LoadTasksSettingsAsync()
        {
            if (File.Exists(_tasksSettingsServerName))
            {
                try
                {
                    using (var reader = new StreamReader(_tasksSettingsServerName, Encoding.UTF8))
                    {
                        string json = await reader.ReadToEndAsync();
                        _tasksSettings = JsonUtility.FromJson<DataContainer>(json);
                    }
                }
                catch (IOException e)
                {
                    Debug.LogError($"Ошибка чтения файла: {e.Message}");
                    CreateDefaultSave(); // Если файл битый — создаем новый
                }

            }
            else
            {
                SaveDataTasks();
            }

            //SaveDataTasks();
        }

        private async void SaveDataTasks()
        {
            SctructureTasksSettingsServer[] data = new SctructureTasksSettingsServer[6];
            data[0] = new SctructureTasksSettingsServer(0, 1, 1, 7);
            data[1] = new SctructureTasksSettingsServer(0, 1, 2, 11);
            data[2] = new SctructureTasksSettingsServer(0, 2, 1, 10);
            data[3] = new SctructureTasksSettingsServer(0, 2, 2, 14);
            data[4] = new SctructureTasksSettingsServer(0, 3, 2, 17);
            data[5] = new SctructureTasksSettingsServer(0, 2, 3, 18);
            var wrapper = new DataContainer { TasksArray = data };
            try
            {
                await File.WriteAllTextAsync(_tasksSettingsServerName, JsonUtility.ToJson(wrapper, true));
            }
            catch (IOException e)
            {
                Debug.LogError($"Ошибка записи файла: {e.Message}");
            }
        }

        private void CreateDefaultSave()
        {
            _tasksSettings = new DataContainer();
            SaveDataTasks();
        }
    }
}
