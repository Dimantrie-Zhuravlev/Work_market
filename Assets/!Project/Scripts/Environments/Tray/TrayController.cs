using UnityEngine;

public class TrayController : MonoBehaviour, IInteractable, IDropableObject
{
    public void EnableColliderOnDropObject(GameObject currentObject)
    {
        currentObject.GetComponent<BoxCollider>().enabled = true;
    }

    public void DropObject(GameObject currentObject)
    {
        EnableColliderOnDropObject(gameObject);
        currentObject.transform.SetParent(null);
    }

    public void Interact()
    {
        if (HandObjectsController.Instance.CurrentObjectInHand == null)
        {
            if (gameObject.transform.parent?.gameObject.name == "table1_3m_08m_1m_with_tray") //Если коробка поднята со стола
            {
                TrayGhostTableController.Instance.gameObject.SetActive(true);
            }
            HandObjectsController.Instance.PickUpObjectFromGround(gameObject, new StructureObjectPosition(new Vector3(0, 0.1f, 0.3f), Quaternion.Euler(0, 90f, 0)));
        }
    }
}
