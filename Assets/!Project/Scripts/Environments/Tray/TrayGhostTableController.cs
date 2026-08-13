using UnityEngine;

public class TrayGhostTableController : MonoBehaviour, IInteractableMouse
{
    public static TrayGhostTableController Instance;
    private Transform parentObject;

    public void InteractMouse()
    {
        if (HandObjectsController.Instance.CurrentObjectInHand?.name == "Tray")
        {
            gameObject.SetActive(false);
            GameObject tray = HandObjectsController.Instance.CurrentObjectInHand;
            tray.GetComponent<TrayController>().AddBoxColliderOnDropObject(tray);
            tray.transform.SetParent(parentObject);
            tray.transform.SetPositionAndRotation(transform.position, transform.rotation);
            HandObjectsController.Instance.SetCurrentObject(null);
        }
    }

    private void Awake()
    {
       gameObject.SetActive(false);
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        parentObject = transform.parent;
    }
}
