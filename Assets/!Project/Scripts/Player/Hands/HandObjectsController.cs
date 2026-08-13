using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class HandObjectsController : MonoBehaviour
{
    private GameObject currentObjectInHand = null;

    public GameObject CurrentObjectInHand => currentObjectInHand;
    public string CurrentObjectInHandName => currentObjectInHand.GetComponent<CurrentBoxSetting>()._currentBoxSetting.typeBox; //только для коробок

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
    }

    public void PickUpObjectFromGround(GameObject newObject, StructureObjectPosition localPoition)
    {
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
            currentObjectInHand.AddComponent<Rigidbody>();
            drop.DropObject(currentObjectInHand);
            SetCurrentObject(null);
        }
    }

}
