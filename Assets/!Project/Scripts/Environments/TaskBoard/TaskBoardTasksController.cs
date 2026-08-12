using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TaskBoardTasksController : MonoBehaviour
{
    private List<GameObject> _tasksList = new List<GameObject>();
    private readonly int _maxTasksCount = 8;

    public static TaskBoardTasksController Instance;

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
        _tasksList.Find(elem => !elem.activeInHierarchy).SetActive(true);
        currentCountsTasks = Mathf.Clamp(currentCountsTasks + 1, 0, _maxTasksCount);
    }

    public void DeleteSelectedTask(GameObject selectedTask)
    {
        currentCountsTasks = _tasksList.Count(obj => obj.activeInHierarchy);
    }

}
