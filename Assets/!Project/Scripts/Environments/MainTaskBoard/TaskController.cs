using UnityEngine;

namespace TaskBoards.Main
{
    public class TaskController : MonoBehaviour, IInteractable
    {
        public void Interact()
        {
            if (!TaskBoards.Current.TaskBoardController.Instance.IsActiveTaskActive)
            {
                TaskBoards.Current.TaskBoardController.Instance.AddActiveTask();
                TaskBoards.Main.TaskBoardController.Instance.DeleteSelectedTask(PlayerCheckView.Instance.ViewWorkingObject);
            }
            else
            {
                PersonMessageLifeCycle.Instance.SendLifeCycleMessage("Активное задание уже выбрано");
            }
        }
    }

}
