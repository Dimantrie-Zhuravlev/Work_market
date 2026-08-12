using UnityEngine;

public class TrashCanDataAbility : MonoBehaviour, IInteractable
{

    //public void Interact(GameObject boxInScene)
    //{

    //}

    public void Interact()
    {
        GameObject boxInHands = HandsPollBoxes.Instance.CurrentObjectInHand;
        if (boxInHands != null && HandsPollBoxes.Instance.CurrentObjectInHandName == EnumBoxesName.EmptyBox)
        {
            PoolEmptyBoxes.Instance.Release(boxInHands);
        }
        else
        {
            PersonMessageLifeCycle.Instance.SendLifeCycleMessage("Выкидывать можно только пустые коробки");
        }
    }
}
