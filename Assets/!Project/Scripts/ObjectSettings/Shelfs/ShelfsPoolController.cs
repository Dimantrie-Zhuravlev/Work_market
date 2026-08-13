using Unity.VisualScripting;
using UnityEngine;

public class ShelfsPoolController : MonoBehaviour
{
    public static ShelfsPoolController Instance { get; private set; }

    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }


    public void ChangeShelfTypeAndAddObject(GameObject currentShelf)
    {
        ShelfController newShelf;
        switch (HandObjectsController.Instance.CurrentObjectInHandName)
        {
            case EnumBoxesName.MakaronsBox:
                newShelf = PoolMakaronShelf.Instance.Get(currentShelf.transform.position, currentShelf.transform.rotation, currentShelf.transform.parent).GetComponent<ShelfController>();
                newShelf.AddOneObject(HandObjectsController.Instance.CurrentBoxHasCountObjects());
                break;

            case EnumBoxesName.GoroxBox:
                newShelf = PoolGoroxShelf.Instance.Get(currentShelf.transform.position, currentShelf.transform.rotation, currentShelf.transform.parent).GetComponent<ShelfController>();
                newShelf.AddOneObject(HandObjectsController.Instance.CurrentBoxHasCountObjects());
                break;
            default:
                Debug.LogWarning($"Неизвестный тип коробки");
                break;
        }

        PoolEmptyShelf.Instance.Release(currentShelf);
    }
}
