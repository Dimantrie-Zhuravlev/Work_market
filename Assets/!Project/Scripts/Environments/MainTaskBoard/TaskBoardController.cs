using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace TaskBoards.Main
{
    public class TaskBoardController : MonoBehaviour
    {
        private List<GameObject> _tasksList = new List<GameObject>();
        private readonly int _maxTasksCount = 8;

        public static TaskBoards.Main.TaskBoardController Instance;

        public bool CanAddNewTask => currentCountsTasks < _maxTasksCount;

        private int currentCountsTasks = 0;

        void Start()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;

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
                _tasksList.Find(elem => !elem.activeInHierarchy).SetActive(true);
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
    }
}
