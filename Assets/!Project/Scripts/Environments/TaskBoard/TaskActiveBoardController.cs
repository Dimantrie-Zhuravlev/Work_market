using System.Collections.Generic;
using UnityEngine;

public class TaskActiveBoardController : MonoBehaviour
{
    public GameObject activeTask;

    public static TaskActiveBoardController Instance;

    public bool IsActiveTaskActive => activeTask.activeInHierarchy;

    void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        activeTask = transform.GetChild(1).transform.GetChild(0).gameObject;
        activeTask.SetActive(false);
    }

    public void AddActiveTask()
    {
        activeTask.SetActive(true);
    }

    public void DeleteActiveTask()
    {
        activeTask.SetActive(false);
    }
}
