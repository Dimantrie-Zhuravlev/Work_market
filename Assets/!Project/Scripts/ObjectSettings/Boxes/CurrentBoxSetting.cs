using System;
using System.Collections.Generic;
using UnityEngine;

public class CurrentBoxSetting : MonoBehaviour, IInteractableMouse, IDropableObject
{
    [HideInInspector]
    public BoxSettings _currentBoxSetting;
    private AbstractPoolBoxes _abstractPoolBox;
    private GameObject _abstractPoolBoxGameObject;
    public AbstractPoolBoxes AbstractPoolBox => _abstractPoolBox;

    private List<AbstractSupplyPark> listSupply = new List<AbstractSupplyPark>();
    [SerializeField] Transform SypplusContainer;

    private int currentCountObjectsInBox;
    [HideInInspector]
    public string _boxName;

    private string nameId;
    public string NameId => nameId;
    public int CurrentCountObjectsInBox => currentCountObjectsInBox;

    private List<GameObject> _objectsInBoxes = new List<GameObject>();

    public void Awake()
    {
        InitNameId();
    }
    public void OnEnable()
    {
        if (!isLoadSave)
        {
            currentCountObjectsInBox = _currentBoxSetting.MaxObjectsInBox;
            InitializeAwake();
            RestartObjectInBox();
        }
    }

    public void InitNameId()
    {
        nameId = gameObject.name;
    }
    private void InitializeAwake(bool isRestore = false)
    {
        Transform childTransform = transform.GetChild(0);
        _boxName = childTransform.name == "Objects" ? childTransform.GetChild(0).name : EnumBoxesName.EmptyProduct;
        _abstractPoolBox = ConnectNamesProducts.Instance.DataProducts(_boxName)._ProductsBoxPool;
        _abstractPoolBoxGameObject = ConnectNamesProducts.Instance.DataProducts(_boxName)._BoxPoolGameObject;
        if (SypplusContainer == null)
        {
            SypplusContainer = GameObject.FindWithTag("SupplyParksContainer").transform;
        }
        for (int i = 0; i < SypplusContainer.childCount; i++)
        {
            AbstractSupplyPark currentItem = SypplusContainer.GetChild(i).gameObject.GetComponent<AbstractSupplyPark>();
            listSupply.Add(currentItem);
        }
    }
    public void InteractMouse()
    {
        if (HandObjectsController.Instance.CurrentObjectInHand == null)
        {
            if (gameObject.transform.parent.gameObject.name == "BoxesSupplyPark") //Если коробка поднята со стеллажа, то у стеллажа надо ее убрать
            {
                gameObject.transform.parent.gameObject.transform.parent.gameObject.GetComponent<AbstractSupplyPark>().PullBoxFromPark(gameObject);
            }

            HandObjectsController.Instance.PickUpObjectFromGround(gameObject, "box");
        }
    }
    public void RestartObjectInBox()
    {
        if (_boxName != EnumBoxesName.EmptyProduct)
        {
            int counts1 = 0;
            Transform objectsContainer = transform.GetChild(0);
            int indexInActive = isLoadSave ? currentCountObjectsInBox : _currentBoxSetting.MaxObjectsInBox;
            for (int i = 0; i < _currentBoxSetting.MaxObjectsInBox; i++)
            {
                GameObject currentItem = objectsContainer.GetChild(i).gameObject;
                _objectsInBoxes.Add(currentItem);
                counts1 = i < indexInActive ? counts1 + 1 : counts1;
                currentItem.SetActive(i < indexInActive);
            }
            SetNewMessageForCount();
        }
    }

    public void SetNewMessageForCount()
    {
        EnvironmentsPersonMessage message = GetComponent<EnvironmentsPersonMessage>();
        message.AddCurrentMessage($"({currentCountObjectsInBox})");
    }

    public void DecrementOneObjectInBox()
    {
        currentCountObjectsInBox = Math.Clamp(currentCountObjectsInBox - 1, 0, _currentBoxSetting.MaxObjectsInBox);
        _objectsInBoxes[currentCountObjectsInBox].SetActive(false);
        SetNewMessageForCount();
        if (currentCountObjectsInBox == 0)
        {
            ChangeBoxTypeOnEmpty();
        }
    }

    public void ChangeBoxTypeOnEmpty()
    {
        AbstractPoolBoxes currentAbstractClass = _abstractPoolBox;
        EnableColliderOnDropObject(HandObjectsController.Instance.CurrentObjectInHand); //включает коллайдер коробке в руках

        RestartObjectInBox();//заполняем коробку товарами
        currentAbstractClass.Release(HandObjectsController.Instance.CurrentObjectInHand); //релизим

        HandObjectsController inst1 = HandObjectsController.Instance;
        inst1.SetCurrentObject(PoolEmptyBoxes.Instance.Get(inst1.CurrentObjectInHand.transform.position, inst1.CurrentObjectInHand.transform.rotation, inst1.transform));
        HandObjectsController.Instance.CurrentObjectInHand.GetComponent<Rigidbody>().isKinematic = true;
        HandObjectsController.Instance.CurrentObjectInHand.GetComponent<BoxCollider>().enabled = false;
    }

    public void DropObject(GameObject currentObject)
    {
        EnableColliderOnDropObject(currentObject);
        AbstractPoolBoxes currentBoxPool = currentObject.GetComponent<CurrentBoxSetting>().AbstractPoolBox;
        currentObject.transform.SetParent(currentBoxPool.gameObject.transform);
    }

    public void EnableColliderOnDropObject(GameObject currentObject)
    {
        currentObject.GetComponent<BoxCollider>().enabled = true;
    }
    private bool isLoadSave = false;
    public void RestoreState(StructureBoxSave state)
    {
        currentCountObjectsInBox = state.CountObjectsInBox;
        isLoadSave = true;
        RestartObjectInBox();
        gameObject.SetActive(state.IsActive);
        InitializeAwake();
        SetNewMessageForCount();
        switch (state.ParentName)
        {
            case "SupplyMakaronsPark":
                listSupply[0].AddBoxOnSupplyPark(gameObject);
                break;
            case "SupplyGoroxPark":
                listSupply[1].AddBoxOnSupplyPark(gameObject);
                break;
            case "Hands":
                HandObjectsController.Instance.PickUpObjectFromGround(gameObject, "box");
                break;
            default:
                gameObject.transform.SetParent(_abstractPoolBoxGameObject.transform);
                transform.localPosition = state.Position;
                break;
        }
    }
    public StructureBoxSave GetStructureData()
    {
        string newParenStringt = transform.parent.name == "BoxesSupplyPark" ? transform.parent.transform.parent.name : transform.parent.name;
        return new StructureBoxSave(NameId, transform.position, gameObject.activeInHierarchy, newParenStringt, currentCountObjectsInBox);
    }
}
