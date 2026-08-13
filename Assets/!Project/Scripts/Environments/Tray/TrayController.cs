using UnityEngine;

public class TrayController : MonoBehaviour, IInteractable, IDropableObject
{
    private StructureBoxCollider boxCollider = new StructureBoxCollider(new Vector3(0f, -0.05f, 0f), new Vector3(0.7f, 0.15f, 1.37f)); //точка создания коллайдера, для теста сделано через структуру
    public void AddBoxColliderOnDropObject(GameObject currentObject)
    {
        var newCol = gameObject.AddComponent<BoxCollider>();
        newCol.center = boxCollider.BoxColliderCenter;
        newCol.size = boxCollider.BoxColliderSize;
    }

    public void DropObject(GameObject currentObject)
    {
        AddBoxColliderOnDropObject(gameObject);
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
