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

        private void Start()
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
            //инициализация
            Transform tasksContainer = transform.GetChild(1);
            for (int i = 0; i < tasksContainer.childCount; i++)
            {
                GameObject currentItem = tasksContainer.GetChild(i).gameObject;
                _tasksList.Add(currentItem);
                currentItem.SetActive(false);
            }

            TimeGameManager.OnThirtyMinutesPassed += AddNewTask; //подписка на прошедшие полчаса игрового времени
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

                Money makarons1 = ProductsGlobalData.Instance.ProductsGlobal[0].PriceProduct * makarons;
                Money gorox1 = ProductsGlobalData.Instance.ProductsGlobal[1].PriceProduct * gorox;

                SctructureTasksSettingsServer data = new SctructureTasksSettingsServer(0, makarons, gorox, makarons1 + gorox1);

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
