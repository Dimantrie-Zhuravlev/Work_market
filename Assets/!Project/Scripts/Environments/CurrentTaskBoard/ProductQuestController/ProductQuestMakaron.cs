using UnityEngine;

public class ProductQuestMakaron : AbstractProductQuest
{
    public override void InteractMouse()
    {
        GameObject objectHand = HandObjectsController.Instance.CurrentObjectInHand;
        if (objectHand != null && objectHand.TryGetComponent<TrayController>(out var tray))
        {
            if (tray.CurrentTrayProducts.Makarons > 0)
            {
                StructureTrayObjects currentQuest = QuestProductsController.Instance.CurrentQuestAddObject(EnumBoxesName.MakaronsProduct);
                tray.PutProductFromTray(EnumBoxesName.MakaronsProduct);
                if (currentQuest.Makarons == 0)
                {
                    base.InteractMouse();
                }
                else
                {
                    gameObject.GetComponent<EnvironmentsPersonMessage>().SetCurrentMessage($"Нужно еще {currentQuest.Makarons} макарон");
                }
            }
        }

    }
}
