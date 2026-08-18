using System;
using System.Collections.Generic;
using UnityEngine;

public class CurrentBoxSetting : MonoBehaviour, IInteractable, IDropableObject
{
    [SerializeField] public BoxSettings _currentBoxSetting;
    private AbstractPoolBoxes _abstractPoolBox;
    public AbstractPoolBoxes AbstractPoolBox => _abstractPoolBox;

    private int currentCountObjectsInBox;
    public string _boxName;
    public int CurrentCountObjectsInBox => currentCountObjectsInBox;

    private List<GameObject> _objectsInBoxes = new List<GameObject>();

    private void Start()
    {
        Transform childTransform = transform.GetChild(0);
        _boxName = childTransform.name == "Objects" ? childTransform.GetChild(0).name : EnumBoxesName.EmptyProduct;

        switch (_boxName)  //Это нужно чтобы дочерние элементы определили свой пул, из-за связи префаба-элемента
        {
            case EnumBoxesName.EmptyProduct:
                _abstractPoolBox = PoolEmptyBoxes.Instance;
                break;
            case EnumBoxesName.MakaronsProduct:
                _abstractPoolBox = PoolMakaronsBoxes.Instance;
                break;

            case EnumBoxesName.GoroxProduct:
                _abstractPoolBox = PoolGoroxBoxes.Instance;
                break;

            default:
                Debug.LogWarning($"Неизвестный тип коробки");
                break;
        }
        currentCountObjectsInBox = _currentBoxSetting.MaxObjectsInBox;
        RestartObjectInBox();
    }

    public void Interact()
    {
        if (HandObjectsController.Instance.CurrentObjectInHand == null)
        {
            if (gameObject.transform.parent.gameObject.name == "BoxesSupplyPark") //Если коробка поднята со стеллажа, то у стеллажа надо ее убрать
            {
                gameObject.transform.parent.gameObject.transform.parent.gameObject.GetComponent<AbstractSupplyPark>().PullBoxFromPark(gameObject);
            }

            HandObjectsController.Instance.PickUpObjectFromGround(gameObject, new StructureObjectPosition(new Vector3(0, 0, 0), Quaternion.Euler(180f, 0, 0)));
        }
    }

    public void RestartObjectInBox()
    {
        if (_boxName != EnumBoxesName.EmptyProduct)
        {
            Transform objectsContainer = transform.GetChild(0);
            for (int i = 0; i < objectsContainer.childCount; i++)
            {
                GameObject currentItem = objectsContainer.GetChild(i).gameObject;
                _objectsInBoxes.Add(currentItem);
                currentItem.SetActive(true);
                if (i >= _currentBoxSetting.MaxObjectsInBox)
                {
                    currentItem.SetActive(false);
                }
            }
            currentCountObjectsInBox = _currentBoxSetting.MaxObjectsInBox;
            SetNewMessageForCount();
        }
    }

    public void SetNewMessageForCount()
    {
        EnvironmentsPersonMessage message = this.GetComponent<EnvironmentsPersonMessage>();
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

}
