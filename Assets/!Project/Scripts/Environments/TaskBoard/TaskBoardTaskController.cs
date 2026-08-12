using UnityEngine;

public class TaskBoardTaskController : MonoBehaviour
{
    public void AddTaskOnActiveTaskCapBoard(GameObject selectedTask)
    {
        if (!TaskActiveBoardController.Instance.IsActiveTaskActive)
        {
            TaskActiveBoardController.Instance.AddActiveTask();
            TaskBoardTasksController.Instance.DeleteSelectedTask(selectedTask);
        }
        else
        {
            PersonMessageLifeCycle.Instance.SendLifeCycleMessage("Активное задание уже выбрано");
        }
    }
}
