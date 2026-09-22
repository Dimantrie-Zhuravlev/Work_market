using FMODUnity;
using System.Collections;
using System.Collections.Generic;
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

        private int NeedTotalObject()
        {
            return currendData.Objects.Makaron + currendData.Objects.Gorox;
        }

        private IEnumerator SeeObjects()
        {
            bool findElementForSearch = false;
            yield return new WaitForSeconds(10);
            if (currendData.Objects.Makaron > 0)
            {
                findElementForSearch = true;
                ProductsTasksGarbage.Instance.CheckProduct(EnumBoxesName.MakaronsProduct, currendData.Objects.Makaron);
            }
            if (!findElementForSearch && currendData.Objects.Gorox > 0)
            {
                findElementForSearch = true;
                ProductsTasksGarbage.Instance.CheckProduct(EnumBoxesName.GoroxProduct, currendData.Objects.Gorox);
            }
        }

        public void CompletePartOfTask(string objectName, int objectCount) //это уже результат сбор со стеллажа
        {
            switch (objectName)
            {
                case EnumBoxesName.MakaronsProduct:
                    currendData.Objects.Makaron = objectCount == 1 ? --currendData.Objects.Makaron : 0;
                    if (objectCount == 1)
                    {
                        currentQuest.Reward += ProductsGlobalData.Instance.ProductsGlobal[0].PriceProduct; //Надо поменять структура наград за ресурс, индекс - хуйня
                    }
                    break;
                case EnumBoxesName.GoroxProduct:
                    currendData.Objects.Gorox = objectCount == 1 ? --currendData.Objects.Gorox : 0;
                    if (objectCount == 1)
                    {
                        currentQuest.Reward += ProductsGlobalData.Instance.ProductsGlobal[1].PriceProduct; //Надо поменять структура наград за ресурс, индекс - хуйня
                    }
                    break;

                default:
                    Debug.LogWarning($"Неизвестный тип стеллажа");
                    break;
            }
            if (NeedTotalObject() == 0)
            {
                PersonMessageLifeCycle.Instance.SendLifeCycleMessage("Квест завершен");
                TaskCoroutine = null;
                StopAllCoroutines();
                CompleteQuestAndTakeRewards();
            }
            else
            {
                StartCoroutine(SeeObjects());
            }
        }
        private SctructureTasksSettingsServer currentQuest;
        //[SerializeField] private string pickupEvent = "event:/SFX/Play_Box_Pickup";
        private void CompleteQuestAndTakeRewards()
        {
            //RuntimeManager.PlayOneShot(pickupEvent, transform.position); //Это звуковое сопровождение
            PersonMessageLifeCycle.Instance.SendLifeCycleMessage($"На баланс добавлено {currentQuest.Reward}");
            DeleteActiveTask();
            PlayerWallet.Instance.IncreaseBalance(currentQuest.Reward);
            QuestProductsController.Instance.ClearCurrentQuest();
            ExperienceSystem.Instance.UpdateExperience(currentQuest.Reward);
        }

        private Coroutine TaskCoroutine = null;

        private SctructureTasksSettingsServer currendData;
        public SctructureTasksSettingsServer CurrentData => currendData;

        public void AddActiveTask(SctructureTasksSettingsServer dataTask)
        {
            currendData = dataTask;
            currentQuest = new SctructureTasksSettingsServer(0, new Money(0, 0), new StructureTaskObjects());
            activeTask.GetComponent<TaskBoards.Current.TaskController>().SetTaskQuest(dataTask); //вывешивание самой бумажки с заданием
            activeTask.SetActive(true);

            if (TaskCoroutine == null)
            {
                print("Я начал поиск предметов на сцене");
                TaskCoroutine = StartCoroutine(SeeObjects());
            }
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
