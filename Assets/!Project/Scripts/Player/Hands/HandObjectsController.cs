using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class HandObjectsController : MonoBehaviour
{
    private GameObject currentObjectInHand = null;

    public GameObject CurrentObjectInHand => currentObjectInHand;
    public string CurrentObjectInHandName => currentObjectInHand.GetComponent<CurrentBoxSetting>()._currentBoxSetting.typeBox; //только для коробок

    [SerializeField] private Animator _animator;

    private int _hashIsHolding;

    public CurrentBoxSetting CurrentBoxHasCountObjects()
    {
        return currentObjectInHand == null ? null : currentObjectInHand.GetComponent<CurrentBoxSetting>();
    }


    public static HandObjectsController Instance { get; private set; }
    void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        _hashIsHolding = Animator.StringToHash("IsHolding");
    }

    public void PickUpObjectFromGround(GameObject newObject, StructureObjectPosition localPoition)
    {
        _animator.SetBool(_hashIsHolding, true);
        newObject.transform.SetParent(transform, true);
        newObject.transform.SetLocalPositionAndRotation(localPoition.ObjectPosition, localPoition.ObjectRotation);
        Destroy(newObject.GetComponent<Rigidbody>());
        Destroy(newObject.GetComponent<BoxCollider>());
        SetCurrentObject(newObject);
    }

    public void SetCurrentObject(GameObject newCurrentElement)
    {
        currentObjectInHand = newCurrentElement;
    }

    public void DropCurrentHandObject(InputAction.CallbackContext context)
    {
        if (context.performed && currentObjectInHand != null && currentObjectInHand.TryGetComponent<IDropableObject>(out var drop))
        {
            _animator.SetBool(_hashIsHolding, false);
            currentObjectInHand.AddComponent<Rigidbody>();
            drop.DropObject(currentObjectInHand);
            SetCurrentObject(null);
        }
    }

}
