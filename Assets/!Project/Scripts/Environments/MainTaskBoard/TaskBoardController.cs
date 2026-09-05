using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace TaskBoards.Main
{
    public class TaskBoardController : MonoBehaviour
    {
        private List<GameObject> _tasksList;
        private readonly int _maxTasksCount = 8;

        public static TaskBoardController Instance;

        public bool CanAddNewTask => currentCountsTasks < _maxTasksCount;

        private int currentCountsTasks;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            //инициализация
            _tasksList = new List<GameObject>();
            currentCountsTasks = 0;
            Transform tasksContainer = transform.GetChild(1);
            for (int i = 0; i < tasksContainer.childCount; i++)
            {
                GameObject currentItem = tasksContainer.GetChild(i).gameObject;
                _tasksList.Add(currentItem);
                currentItem.SetActive(false);
            }
        }

        private void Start()
        {
            TimeGameManager.OnThirtyMinutesPassed += AddNewTask; //подписка на прошедшие полчаса игрового времени
        }

        public List<SctructureTasksSettingsServer> GetTasksList()
        {
            return _tasksList.Where(item=>item.activeInHierarchy).Select(item => item.GetComponent<TaskBoards.Main.TaskController>().CurrentQuest).ToList();
        }

        public void LoadDataList(List<SctructureTasksSettingsServer> loadData)
        {
            for (int i = 0; i < loadData.Count; i++) {
                GameObject currentTask = _tasksList.Find(elem => !elem.activeInHierarchy);
                currentTask.gameObject.GetComponent<TaskBoards.Main.TaskController>().SetTaskQuest(loadData[i]);
                currentTask.SetActive(true);
                currentCountsTasks = Mathf.Clamp(currentCountsTasks + 1, 0, _maxTasksCount);
            }
        }


        public void AddNewTask()
        {
            if (CanAddNewTask)
            {
                int makarons = Random.Range(0, 5);
                int gorox = Random.Range(0, 5);
                if (makarons == 0 && gorox == 0)
                {
                    makarons = Random.Range(1, 5);
                }

                Money makaronsPrice = ProductsGlobalData.Instance.ProductsGlobal[0].PriceProduct * makarons;
                Money goroxPrice = ProductsGlobalData.Instance.ProductsGlobal[1].PriceProduct * gorox;

                SctructureTasksSettingsServer data = new SctructureTasksSettingsServer(0, makaronsPrice + goroxPrice, new StructureTaskObjects(makarons, gorox));

                GameObject currentTask = _tasksList.Find(elem => !elem.activeInHierarchy);
                currentTask.GetComponent<TaskBoards.Main.TaskController>().SetTaskQuest(data);
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

        private void OnDestroy()
        {
            TimeGameManager.OnThirtyMinutesPassed -= AddNewTask;
        }

    }
}
