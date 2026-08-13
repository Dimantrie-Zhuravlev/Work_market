using UnityEngine;

namespace TaskBoards.Current
{
    public class ButtonController : MonoBehaviour, IInteractable
    {
        public void Interact()
        {
            TaskBoards.Current.TaskBoardController.Instance.DeleteActiveTask();
        }
    }

}
