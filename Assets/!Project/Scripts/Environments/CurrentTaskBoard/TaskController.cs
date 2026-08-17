using TMPro;
using UnityEngine;

namespace TaskBoards.Current
{
    public class TaskController : MonoBehaviour, IInteractable
    {
        [SerializeField] TMP_Text levelText;
        [SerializeField] TMP_Text rewardText;
        [SerializeField] TMP_Text molokoText;
        [SerializeField] TMP_Text goroxText;

        private SctructureTasksSettingsServer currentQuest;

        public void SetTaskQuest(SctructureTasksSettingsServer dataTask)
        {
            currentQuest = dataTask;
            levelText.text = $"Сложность: {dataTask.TaskLevel}";
            rewardText.text = $"Награда: {dataTask.Reward}";
            molokoText.text = $"Нужно {dataTask.Makaron} макарон";
            goroxText.text = $"Нужно: {dataTask.Gorox} гороха";
        }


        public void Interact()
        {
            PersonMessageLifeCycle.Instance.SendLifeCycleMessage($"На баланс добавлено {currentQuest.Reward}");
            TaskBoards.Current.TaskBoardController.Instance.DeleteActiveTask();
            PlayerWallet.Instance.IncreaseBalance(currentQuest.Reward);
        }
    }

}
