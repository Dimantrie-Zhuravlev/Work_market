using UnityEngine;
using UnityEngine.InputSystem;

public class HandObjectsController : MonoBehaviour
{
    private GameObject currentObjectInHand = null;

    public GameObject CurrentObjectInHand => currentObjectInHand;

    [SerializeField] private Animator _animator;

    private int _hashIsHolding;

    public static HandObjectsController Instance { get; private set; }
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        _hashIsHolding = Animator.StringToHash("IsHolding");
    }

    private StructureObjectPosition trayPosition = new StructureObjectPosition(new Vector3(0, 0.1f, 0.3f), Quaternion.Euler(0, -90f, 0)); //Относительные координаты для подноса
    private StructureObjectPosition boxPosition = new StructureObjectPosition(new Vector3(0, 0, 0), Quaternion.Euler(180f, 0, 0)); //Относительные координаты для коробки

    public void PickUpObjectFromGround(GameObject newObject, string objectName)
    {
        _animator.SetBool(_hashIsHolding, true);
        newObject.transform.SetParent(transform, true);
        StructureObjectPosition objectPosition;
        switch (objectName)
        {
            case "tray":
                objectPosition = trayPosition;
                break;
            case "box":
                objectPosition = boxPosition;
                break;
            default:
                objectPosition = trayPosition;
                break;
        }
        newObject.transform.SetLocalPositionAndRotation(objectPosition.ObjectPosition, objectPosition.ObjectRotation);
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
