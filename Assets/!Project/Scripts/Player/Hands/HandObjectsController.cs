using UnityEngine;
using UnityEngine.InputSystem;

public class HandObjectsController : MonoBehaviour
{
    private GameObject currentObjectInHand = null;

    public GameObject CurrentObjectInHand => currentObjectInHand;

    [SerializeField] private Animator _animator;

    private int _hashIsHolding;

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
        newObject.GetComponent<Rigidbody>().isKinematic = true;
        newObject.GetComponent<BoxCollider>().enabled = false;
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
            currentObjectInHand.GetComponent<Rigidbody>().isKinematic = false;
            drop.DropObject(currentObjectInHand);
            SetCurrentObject(null);
        }
    }

}
