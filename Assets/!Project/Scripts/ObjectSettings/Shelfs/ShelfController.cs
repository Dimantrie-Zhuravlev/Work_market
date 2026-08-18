using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShelfController : MonoBehaviour, IInteractableMouse, IInteractableRightMouse
{
    [SerializeField] public string _shelfProductName;

    private List<GameObject> _ObjectsOnShelf = new List<GameObject>();

    private AbstractPoolShelf _currentPoolShelf;
    private AbstractPoolProducts _currentPoolProduct;
    public AbstractPoolShelf CurrentPoolShelf => _currentPoolShelf;

    private void Start()
    {
        switch (_shelfProductName)  //Это нужно чтобы полки определили свой пул, из-за связи префаба-элемента
        {
            case EnumBoxesName.EmptyBox:
                _currentPoolShelf = PoolEmptyShelf.Instance;
                break;
            case EnumBoxesName.MakaronsBox:
                _currentPoolShelf = PoolMakaronShelf.Instance;
                _currentPoolProduct = PoolProductMakaron.Instance;
                break;

            case EnumBoxesName.GoroxBox:
                _currentPoolShelf = PoolGoroxShelf.Instance;
                _currentPoolProduct = PoolProductGorox.Instance;
                break;

            default:
                Debug.LogWarning($"Неизвестный тип коробки");
                break;
        }
    }

    public void InteractMouse()
    {
        GameObject currentObjectInHand = HandObjectsController.Instance.CurrentObjectInHand;
        if (currentObjectInHand != null)
        {
            CurrentBoxSetting currentBox = currentObjectInHand.GetComponent<CurrentBoxSetting>();
            GameObject viewWorkingObject = PlayerCheckView.Instance.ViewWorkingObject;
            if (currentObjectInHand.name == "Tray")
            {
                TrayController tray = currentObjectInHand.GetComponent<TrayController>();
                if (_shelfProductName != "EMPTY" && !tray.isTrayFull)
                {
                    tray.PickUpProductFromShelf(_currentPoolProduct);
                    TakeoverOneObject();
                }
            }
            else
            {
                if (currentBox.CurrentCountObjectsInBox > 0)
                {
                    if (currentBox._currentBoxSetting.typeBox == _shelfProductName) //проверка что товар в коробке и на полке совпадают
                    {
                        AddOneObjectFromBox(currentBox);
                    }
                    else
                    {
                        if (_shelfProductName == "EMPTY" && currentBox._currentBoxSetting.typeBox != "EMPTY")
                        {
                            ShelfsPoolController.Instance.ChangeShelfTypeAndAddObject(viewWorkingObject);
                        }
                    }
                }
            }
        }

    }

    public void OnEnable()
    {
        for (int i = 0; i < transform.childCount; i++) //Предзаполнение массива дочерними элементами, в данном случае это пачки макарон, банки гороха и тд
        {
            _ObjectsOnShelf.Add(transform.GetChild(i).gameObject);
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    public bool Get()
    {
        var obj = _ObjectsOnShelf?.FirstOrDefault(x => !x.activeSelf);
        if (obj != null)
        {
            obj.SetActive(true);
        }
        return obj != null;
    }

    public void Release(GameObject obj)
    {
        obj.SetActive(false);
    }

    public void AddOneObjectFromBox(CurrentBoxSetting currentBox)
    {
        if (Get())
        {
            currentBox.DecrementOneObjectInBox();
        }
    }

    public void TakeoverOneObject()
    {
        int indexActiveElement = _ObjectsOnShelf.FindLastIndex(x => x.activeSelf);
        if (indexActiveElement == 0)
        {
            ShelfsPoolController.Instance.ChangeShelfTypeOnEmpty(this);
        }
        _ObjectsOnShelf[indexActiveElement].SetActive(false);
    }

    public void InteractRightMouse()
    {
        GameObject currentObjectInHand = HandObjectsController.Instance.CurrentObjectInHand;
        if (currentObjectInHand != null && currentObjectInHand.name == "Tray")
        {
            TrayController tray = currentObjectInHand.GetComponent<TrayController>();
            switch (_shelfProductName)
            {
                case EnumBoxesName.MakaronsBox:
                    if (tray.CurrentTrayProducts.Makarons > 0 && Get()) //проверяем есть ли на подносе макароны
                    {
                        tray.PutProductFromShelf(_currentPoolProduct, "Makarons"); // убираем макароны с подноса если есть место на сл
                    }
                    break;

                case EnumBoxesName.GoroxBox:
                    if (tray.CurrentTrayProducts.Goroxs > 0 && Get()) //проверяем есть ли на подносе макароны
                    {
                        tray.PutProductFromShelf(_currentPoolProduct, "Gorox"); // убираем макароны с подноса если есть место на сл
                    }
                    break;

                case EnumBoxesName.EmptyBox:
                    print("Пока ничего не делаем");
                    break;

                default:
                    Debug.LogWarning($"Неизвестный тип стеллажа");
                    break;
            }
        }
    }
}
