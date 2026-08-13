using UnityEngine;

namespace TaskBoards.Current
{
    public class TaskController : MonoBehaviour, IInteractable
    {
        public void Interact()
        {
            PersonMessageLifeCycle.Instance.SendLifeCycleMessage("Еще не реализовано");
        }
    }

}
