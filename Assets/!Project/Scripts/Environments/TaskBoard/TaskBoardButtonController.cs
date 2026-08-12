using UnityEngine;

public class TaskBoardButtonController : MonoBehaviour
{
    public void AddTaskOnBoard()
    {
        if (TaskBoardTasksController.Instance.CanAddNewTask)
        {
            TaskBoardTasksController.Instance.AddNewTask();
        }
    }
}
