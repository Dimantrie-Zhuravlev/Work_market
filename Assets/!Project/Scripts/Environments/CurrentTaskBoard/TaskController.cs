using TMPro;
using UnityEngine;

namespace TaskBoards.Current
{
    public class TaskController : MonoBehaviour
    {
        [SerializeField] TMP_Text levelText;
        [SerializeField] TMP_Text rewardText;
        [SerializeField] TMP_Text molokoText;
        [SerializeField] TMP_Text goroxText;
        public void SetTaskQuest(SctructureTasksSettingsServer dataTask)
        {
            levelText.text = $"Сложность: {dataTask.TaskLevel}";
            rewardText.text = $"Награда: {dataTask.Reward}";
            molokoText.text = dataTask.Objects.Makaron > 0 ? $"Нужно {dataTask.Objects.Makaron} макарон" : "";
            goroxText.text = dataTask.Objects.Gorox > 0 ? $"Нужно: {dataTask.Objects.Gorox} гороха" : "";
        }
    }
}
