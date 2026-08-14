using UnityEngine;

public class TrashCanDataAbility : MonoBehaviour, IInteractableMouse
{
    public void InteractMouse()
    {
        GameObject boxInHands = HandObjectsController.Instance.CurrentObjectInHand;
        if (boxInHands != null && boxInHands.TryGetComponent<CurrentBoxSetting>(out var box) && box._currentBoxSetting.typeBox == EnumBoxesName.EmptyBox)
        {
            PoolEmptyBoxes.Instance.Release(boxInHands);
            HandObjectsController.Instance.SetCurrentObject(null);
        }
        else
        {
            PersonMessageLifeCycle.Instance.SendLifeCycleMessage("Выкидывать можно только пустые коробки");
        }
    }
}
