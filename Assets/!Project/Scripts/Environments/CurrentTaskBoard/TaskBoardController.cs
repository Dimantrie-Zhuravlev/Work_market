using UnityEngine;

namespace TaskBoards.Current
{
    public class TaskBoardController : MonoBehaviour
    {
        private GameObject activeTask;

        public static TaskBoards.Current.TaskBoardController Instance;

        public bool IsActiveTaskActive => activeTask.activeInHierarchy;

        private void Awake()
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

        public SctructureTasksSettingsServer currendData;
        public SctructureTasksSettingsServer CurrentData => currendData;

        public void AddActiveTask(SctructureTasksSettingsServer dataTask)
        {
            currendData = dataTask;
            activeTask.GetComponent<TaskBoards.Current.TaskController>().SetTaskQuest(dataTask); //вывешивание самой бумажки с заданием
            activeTask.SetActive(true);
            QuestProductsController.Instance.AddQuestGhostsProducts(new StructureTrayObjects(dataTask.Objects.Makaron, dataTask.Objects.Gorox, dataTask.Objects.Makaron + dataTask.Objects.Gorox));
        }

        public void DeleteActiveTask()
        {
            if (IsActiveTaskActive)
            {
                activeTask.SetActive(false);
                currendData = new SctructureTasksSettingsServer();
            }
            else
            {
                PersonMessageLifeCycle.Instance.SendLifeCycleMessage("Сначала выберите задание");
            }

        }
    }
}
