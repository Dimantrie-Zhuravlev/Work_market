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
    private AbstractPoolShelf _SetPoolShelf(string shelfName)
    {
        AbstractPoolShelf shelfValue = PoolMakaronShelf.Instance;
        switch (shelfName)
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

    public void ChangeShelfTypeOnEmpty(ShelfController currentShelf, AbstractPoolShelf shelfPool)
    {
        PoolEmptyShelf.Instance.Get(currentShelf.transform.position, currentShelf.transform.rotation, currentShelf.transform.parent);
        shelfPool.Release(currentShelf.gameObject);
    }

    public ShelfController ChangeShelfTypeFromSave(GameObject currentShelf, ShelfProductsData newData) //Этот метод только при чтении данных с файла загрузки
    {
        ShelfController newShelf = _SetPoolShelf(newData.ShelfName).Get(currentShelf.transform.position, currentShelf.transform.rotation, currentShelf.transform.parent).GetComponent<ShelfController>();
        PoolEmptyShelf.Instance.Release(currentShelf); 
        return newShelf;
    }

    public void ChangeShelfTypeAndAddObject(GameObject currentShelf)
    {
        ShelfController newShelf = _SetPoolShelf().Get(currentShelf.transform.position, currentShelf.transform.rotation, currentShelf.transform.parent).GetComponent<ShelfController>();
        newShelf.AddOneObjectFromBox(HandObjectsController.Instance.CurrentObjectInHand.GetComponent<CurrentBoxSetting>());

        PoolEmptyShelf.Instance.Release(currentShelf);
    }
}
