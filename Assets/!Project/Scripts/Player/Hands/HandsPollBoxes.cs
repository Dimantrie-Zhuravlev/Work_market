using UnityEngine;



public class HandsPollBoxes : MonoBehaviour
{
    private readonly Vector3 BoxColliderCenter = new Vector3(0f, -0.14f, 0f);
    private readonly Vector3 BoxColliderSize = new Vector3(0.81f, 0.3f, 0.4f);

    private GameObject _boxesInHand;
    [SerializeField] private GameObject _handsPosition;

    public static HandsPollBoxes Instance { get; private set; }

    private string currentBoxNameInHands;
    private GameObject currentObjectInHand = null;

    public CurrentBoxSetting CurrentBoxHasCountObjects()
    {
        return currentObjectInHand == null ? null : currentObjectInHand.GetComponent<CurrentBoxSetting>();
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
            if (boxInScene.transform.parent.gameObject.name == "BoxesSupplyPark") //Если коробка поднята со стеллажа, то у стеллажа надо ее убрать
            {
                boxInScene.transform.parent.gameObject.transform.parent.gameObject.GetComponent<AbstractSupplyPark>().PullBoxFromPark(boxInScene);
            }

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
        AbstractPoolBoxes currentBoxPool = currentObjectInHand.GetComponent<CurrentBoxSetting>().AbstractPoolBox;
        currentObjectInHand.transform.parent = currentBoxPool.gameObject.transform;
        if (needRelease)
        {
            currentBoxPool.Release(currentObjectInHand);
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
        AbstractPoolBoxes currentAbstractClass = currentObjectInHand.GetComponent<CurrentBoxSetting>().AbstractPoolBox;
        currentAbstractClass.Release(currentObjectInHand);
        currentObjectInHand.transform.parent = currentAbstractClass.gameObject.transform;

        currentObjectInHand = PoolEmptyBoxes.Instance.Get(currentObjectInHand.transform.position, currentObjectInHand.transform.rotation);
        currentObjectInHand.transform.parent = this.transform; //кладется в руки пустая коробка
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
