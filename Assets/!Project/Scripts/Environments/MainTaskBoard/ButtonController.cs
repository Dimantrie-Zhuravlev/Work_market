using UnityEngine;

namespace TaskBoards.Main
{
    public class ButtonController : MonoBehaviour, IInteractable
    {
        public void Interact()
        {
            TaskBoards.Main.TaskBoardController.Instance.AddNewTask();
        }
    }

}
