using UnityEngine;

public class ProductQuestGorox : AbstractProductQuest
{
    public override void InteractMouse()
    {
        GameObject objectHand = HandObjectsController.Instance.CurrentObjectInHand;

        if (objectHand != null && objectHand.TryGetComponent<TrayController>(out var tray)) {
            if (tray.CurrentTrayProducts.Gorox > 0)
            {
                StructureTrayObjects currentQuest = QuestProductsController.Instance.CurrentQuestAddObject(EnumBoxesName.GoroxProduct);
                print(currentQuest.Gorox);
                tray.PutProductFromTray(EnumBoxesName.GoroxProduct);
                print(currentQuest.Gorox);
                if (currentQuest.Gorox == 0)
                {
                    base.InteractMouse();
                }
                else
                {
                    gameObject.GetComponent<EnvironmentsPersonMessage>().SetCurrentMessage($"Нужно еще {currentQuest.Gorox} гороха");
                }
            }
        }
    }
}
