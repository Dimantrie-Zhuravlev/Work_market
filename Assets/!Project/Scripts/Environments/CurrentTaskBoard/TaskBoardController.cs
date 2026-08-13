using UnityEngine;

namespace TaskBoards.Current
{
    public class TaskBoardController : MonoBehaviour
    {
        private GameObject activeTask;

        public static TaskBoards.Current.TaskBoardController Instance;

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
            if (IsActiveTaskActive)
            {
                activeTask.SetActive(false);
            }
            else
            {
                PersonMessageLifeCycle.Instance.SendLifeCycleMessage("Сначала выберите задание");
            }

        }
    }
}
