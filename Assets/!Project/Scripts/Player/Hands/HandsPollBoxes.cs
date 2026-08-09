using UnityEngine;



public class HandsPollBoxes : MonoBehaviour
{
    private readonly Vector3 BoxColliderCenter = new Vector3(0f, -0.14f, 0f);
    private readonly Vector3 BoxColliderSize = new Vector3(0.81f, 0.3f, 0.4f);

    private GameObject _boxesInHand;
    [SerializeField] private GameObject _handsPosition;

    [Header("Пулы коробок, куда возвращаются коробки")]
    [SerializeField] private GameObject _poolEmptyPosition;
    [SerializeField] private GameObject _poolMakaronsPosition;
    [SerializeField] private GameObject _poolGoroxPosition;

    public static HandsPollBoxes Instance { get; private set; }

    private string currentBoxNameInHands;
    private GameObject currentObjectInHand = null;

    public CurrentBoxSetting CurrentBoxHasCountObjects()
    {
        return currentObjectInHand.GetComponent<CurrentBoxSetting>();
    }
    public string CurrentBoxNameInHands => currentBoxNameInHands;
    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        _boxesInHand = transform.GetChild(0).gameObject;
    }

    public void PickUpHandBox(GameObject boxInScene, string boxName)
    {
        if (currentObjectInHand == null)
        {
            boxInScene.transform.SetPositionAndRotation(_boxesInHand.transform.position, _boxesInHand.transform.rotation);

            boxInScene.transform.parent = _handsPosition.transform;
            Destroy(boxInScene.GetComponent<Rigidbody>());
            Destroy(boxInScene.GetComponent<BoxCollider>());

            currentObjectInHand = boxInScene;
            currentBoxNameInHands = boxName;
        }
    }

    private void SendBoxInPool(bool needRelease)
    {
        switch (currentObjectInHand.GetComponent<CurrentBoxSetting>()._currentBoxSetting.typeBox)
        {
            case EnumBoxesName.EmptyBox:
                currentObjectInHand.transform.parent = _poolEmptyPosition.transform;
                if (needRelease)
                {
                    PoolEmptyBoxes.Instance.Release(currentObjectInHand);
                }
                break;

            case EnumBoxesName.MakaronsBox:
                currentObjectInHand.transform.parent = _poolMakaronsPosition.transform;
                if (needRelease)
                {
                    PoolMakaronsBoxes.Instance.Release(currentObjectInHand);
                }
                break;

            case EnumBoxesName.GoroxBox:
                currentObjectInHand.transform.parent = _poolGoroxPosition.transform;
                if (needRelease)
                {
                    PoolGoroxBoxes.Instance.Release(currentObjectInHand);
                }
                break;

            default:
                Debug.LogWarning($"Неизвестный тип коробки");
                break;
        }
        currentObjectInHand = null;
        currentBoxNameInHands = "";
    }

    public void UtilizeHandBox()
    {
        if (currentObjectInHand.GetComponent<CurrentBoxSetting>()._currentBoxSetting.typeBox == "EMPTY")
        {
            SendBoxInPool(true);
        }
        else
        {
            PersonMessageLifeCycle.Instance.SendLifeCycleMessage("Выкидывать можно только пустые коробки");
        }
    }

    public void ChangeBoxTypeOnEmpty()
    {
        switch (currentObjectInHand.GetComponent<CurrentBoxSetting>()._currentBoxSetting.typeBox)
        {
            case EnumBoxesName.MakaronsBox:
                currentObjectInHand.transform.parent = _poolMakaronsPosition.transform;
                PoolMakaronsBoxes.Instance.Release(currentObjectInHand);
                break;

            case EnumBoxesName.GoroxBox:
                currentObjectInHand.transform.parent = _poolGoroxPosition.transform;
                PoolGoroxBoxes.Instance.Release(currentObjectInHand);
                break;

            default:
                Debug.LogWarning($"Неизвестный тип коробки");
                break;
        }

        currentObjectInHand = PoolEmptyBoxes.Instance.Get(currentObjectInHand.transform.position, currentObjectInHand.transform.rotation);
        currentObjectInHand.transform.parent = this.transform;
        Destroy(currentObjectInHand.GetComponent<Rigidbody>());
        Destroy(currentObjectInHand.GetComponent<BoxCollider>());
        currentBoxNameInHands = "EMPTY";
    }



    public void DropHandBox()
    {
        if (currentObjectInHand != null)
        {
            currentObjectInHand.AddComponent<Rigidbody>();

            var newCol = currentObjectInHand.AddComponent<BoxCollider>();
            newCol.center = BoxColliderCenter;
            newCol.size = BoxColliderSize;
            SendBoxInPool(false);
        }
    }

}
