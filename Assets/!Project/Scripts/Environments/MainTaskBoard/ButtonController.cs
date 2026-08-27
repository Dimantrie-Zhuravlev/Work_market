using UnityEngine;

namespace TaskBoards.Main
{
    public class ButtonController : MonoBehaviour, IInteractableMouse
    {
        public void InteractMouse()
        {
            TaskBoards.Main.TaskBoardController.Instance.AddNewTask();
        }
    }

}
