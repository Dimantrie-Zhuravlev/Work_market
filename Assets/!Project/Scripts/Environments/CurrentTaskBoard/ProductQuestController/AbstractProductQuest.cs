using UnityEngine;

public abstract class AbstractProductQuest : MonoBehaviour, IInteractableMouse
{
    [SerializeField] GameObject productUsual;
    public virtual void InteractMouse()
    {
        productUsual.SetActive(true);
        gameObject.SetActive(false);
    }
}
