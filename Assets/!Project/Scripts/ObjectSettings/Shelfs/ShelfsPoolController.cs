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
        switch (HandsPollBoxes.Instance.CurrentBoxNameInHands)
        {
            case EnumBoxesName.MakaronsBox:
                PoolEmptyShelf.Instance.Release(currentShelf);
                newShelf = PoolMakaronShelf.Instance.Get(currentShelf.transform.position, currentShelf.transform.rotation).GetComponent<ShelfController>();
                newShelf.AddOneObject(HandsPollBoxes.Instance.CurrentBoxHasCountObjects());
                break;

            case EnumBoxesName.GoroxBox:
                PoolEmptyShelf.Instance.Release(currentShelf);
                newShelf = PoolGoroxShelf.Instance.Get(currentShelf.transform.position, currentShelf.transform.rotation).GetComponent<ShelfController>();
                newShelf.AddOneObject(HandsPollBoxes.Instance.CurrentBoxHasCountObjects());
                break;
            default:
                Debug.LogWarning($"Неизвестный тип коробки");
                break;
        }
    }
}
