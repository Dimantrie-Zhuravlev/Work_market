using UnityEngine;

public class TaskActiveButtonController : MonoBehaviour
{
    public void AddTaskOnActiveBoard()
    {
        if (TaskActiveBoardController.Instance.IsActiveTaskActive) //это кнопка на активной доске с одним заданием
        {
            TaskActiveBoardController.Instance.DeleteActiveTask();
        } 
    }
}
