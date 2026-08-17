using TMPro;
using UnityEngine;

namespace TaskBoards.Main
{
    public class TaskController : MonoBehaviour, IInteractable
    {
        [SerializeField] TMP_Text levelText;
        [SerializeField] TMP_Text rewardText;
        [SerializeField] TMP_Text molokoText;
        [SerializeField] TMP_Text goroxText;

        private SctructureTasksSettingsServer currentQuest;

        public void Interact()
        {
            if (!TaskBoards.Current.TaskBoardController.Instance.IsActiveTaskActive)
            {
                TaskBoards.Current.TaskBoardController.Instance.AddActiveTask(currentQuest);
                TaskBoards.Main.TaskBoardController.Instance.DeleteSelectedTask(PlayerCheckView.Instance.ViewWorkingObject);
            }
            else
            {
                PersonMessageLifeCycle.Instance.SendLifeCycleMessage("Активное задание уже выбрано");
            }
        }

        public void SetTaskQuest(SctructureTasksSettingsServer dataTask)
        {
            currentQuest = dataTask;
            levelText.text = $"Сложность: {dataTask.TaskLevel}";
            rewardText.text = $"Награда: {dataTask.Reward}";
            molokoText.text = $"Нужно {dataTask.Makaron} макарон";
            goroxText.text = $"Нужно: {dataTask.Gorox} гороха" ;
        }
    }

}
