using Unity.VisualScripting;
using UnityEngine;

public class ShelfsPoolController : MonoBehaviour
{
    public static ShelfsPoolController Instance { get; private set; }

    private AbstractPoolShelf _SetPoolShelf() {
        AbstractPoolShelf shelfValue = PoolMakaronShelf.Instance;
        switch (HandObjectsController.Instance.CurrentObjectInHand.GetComponent<CurrentBoxSetting>()._boxName)
        {
            case EnumBoxesName.MakaronsProduct:
                shelfValue = PoolMakaronShelf.Instance;
                break;

            case EnumBoxesName.GoroxProduct:
                shelfValue = PoolGoroxShelf.Instance;
                break;
            default:
                Debug.LogWarning($"Неизвестный тип коробки");
                break;
        }
        return shelfValue;
     }

    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

    }

    public void ChangeShelfTypeOnEmpty(ShelfController currentShelf)
    {
        PoolEmptyShelf.Instance.Get(currentShelf.transform.position, currentShelf.transform.rotation, currentShelf.transform.parent);
        currentShelf.CurrentPoolShelf.Release(currentShelf.gameObject);
    }


    public void ChangeShelfTypeAndAddObject(GameObject currentShelf)
    {
        ShelfController newShelf = _SetPoolShelf().Get(currentShelf.transform.position, currentShelf.transform.rotation, currentShelf.transform.parent).GetComponent<ShelfController>();
        newShelf.AddOneObjectFromBox(HandObjectsController.Instance.CurrentObjectInHand.GetComponent<CurrentBoxSetting>());


        PoolEmptyShelf.Instance.Release(currentShelf);
    }
}
