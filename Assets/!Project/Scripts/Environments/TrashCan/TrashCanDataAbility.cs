using UnityEngine;

public class TrashCanDataAbility : MonoBehaviour, IInteractableMouse
{
    [SerializeField] private Animator _animator;

    private int _hashIsHolding;

    private void Start()
    {
        _hashIsHolding = Animator.StringToHash("IsHolding");
    }
    public void InteractMouse()
    {
        GameObject boxInHands = HandObjectsController.Instance.CurrentObjectInHand;
        if (boxInHands != null && boxInHands.TryGetComponent<CurrentBoxSetting>(out var box) && box._boxName == EnumBoxesName.EmptyProduct)
        {
            PoolEmptyBoxes.Instance.Release(boxInHands);
            HandObjectsController.Instance.SetCurrentObject(null);
            _animator.SetBool(_hashIsHolding, false);
        }
        else
        {
            PersonMessageLifeCycle.Instance.SendLifeCycleMessage("Выкидывать можно только пустые коробки");
        }
    }
}
