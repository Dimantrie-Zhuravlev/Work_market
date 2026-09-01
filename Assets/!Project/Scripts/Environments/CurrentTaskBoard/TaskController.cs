using TMPro;
using UnityEngine;
using TaskBoards.Current;

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
            molokoText.text = dataTask.Objects.Makaron > 0 ? $"Нужно {dataTask.Objects.Makaron} макарон" : "";
            goroxText.text = dataTask.Objects.Gorox > 0 ? $"Нужно: {dataTask.Objects.Gorox} гороха" : "";
        }



        public void Interact()
        {
            StructureTrayObjects quest = QuestProductsController.Instance.QuestData;
            if (quest.TotalProductsFroQuest == 0)
            {
                PersonMessageLifeCycle.Instance.SendLifeCycleMessage($"На баланс добавлено {currentQuest.Reward}");
                TaskBoardController.Instance.DeleteActiveTask();
                PlayerWallet.Instance.IncreaseBalance(currentQuest.Reward);
                QuestProductsController.Instance.ClearCurrentQuest();
                ExperienceSystem.Instance.UpdateExperience(currentQuest.Reward);
            }
            else
            {
                PersonMessageLifeCycle.Instance.SendLifeCycleMessage("Не все предметы доставлены");
            }
        }
    }

}
