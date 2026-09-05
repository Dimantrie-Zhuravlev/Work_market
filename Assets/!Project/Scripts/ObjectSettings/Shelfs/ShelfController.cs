using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShelfController : MonoBehaviour, IInteractableMouse, IInteractableRightMouse
{
    [HideInInspector]
    public string _shelfProductName;

    private List<GameObject> _ObjectsOnShelf = new List<GameObject>();
    public List<GameObject> ObjectsShelf => _ObjectsOnShelf; //используется исключительно для сохранения содержимого 

    private AbstractPoolProducts _currentPoolProduct;
    public int ShelfsData()
    {
        return _ObjectsOnShelf.Count(item => item.activeInHierarchy);
    }
    private void Start()
    {
        _shelfProductName = transform.childCount > 0 ? transform.GetChild(0).name : EnumBoxesName.EmptyProduct; //если ошибка проверить имя первого элемента дочернего, кроме empty
        _currentPoolProduct = ConnectNamesProducts.Instance.DataProducts(_shelfProductName)._ProductPool;
    }
    public void InteractMouse()
    {
        GameObject currentObjectInHand = HandObjectsController.Instance.CurrentObjectInHand;
        if (currentObjectInHand != null)
        {
            if (currentObjectInHand.name == "Tray")
            {
                TrayController tray = currentObjectInHand.GetComponent<TrayController>();
                if (_shelfProductName != EnumBoxesName.EmptyProduct && !tray.isTrayFull)
                {
                    tray.PickUpProductFromShelf(_currentPoolProduct);
                    TakeoverOneObject();
                }
            }
            else
            {
                CurrentBoxSetting currentBox = currentObjectInHand.GetComponent<CurrentBoxSetting>();
                if (currentBox.CurrentCountObjectsInBox > 0)
                {
                    if (currentBox._boxName == _shelfProductName) //проверка что товар в коробке и на полке совпадают
                    {
                        AddOneObjectFromBox(currentBox);
                    }
                    else
                    {
                        if (_shelfProductName == EnumBoxesName.EmptyProduct && currentBox._boxName != EnumBoxesName.EmptyProduct)
                        {
                            ShelfsPoolController.Instance.ChangeShelfTypeAndAddObject(PlayerCheckView.Instance.ViewWorkingObject);
                        }
                    }
                }
            }
        }
    }


    public void UploadSaveProducts(int countObjectsUploda)
    {
        for (int i = 0; i < countObjectsUploda; i++)
        {
            _ObjectsOnShelf[i].SetActive(true);
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
            ShelfsPoolController.Instance.ChangeShelfTypeOnEmpty(this, ConnectNamesProducts.Instance.DataProducts(_shelfProductName)._ProductShelfPool);
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
                case EnumBoxesName.MakaronsProduct:
                    if (tray.CurrentTrayProducts.Makarons > 0 && Get()) //проверяем есть ли на подносе макароны
                    {
                        tray.PutProductFromTray(EnumBoxesName.MakaronsProduct); // убираем макароны с подноса если есть место на сл
                    }
                    break;

                case EnumBoxesName.GoroxProduct:
                    if (tray.CurrentTrayProducts.Gorox > 0 && Get()) //проверяем есть ли на подносе макароны
                    {
                        tray.PutProductFromTray(EnumBoxesName.GoroxProduct); // убираем макароны с подноса если есть место на сл
                    }
                    break;

                case EnumBoxesName.EmptyProduct:
                    print("Пока ничего не делаем");
                    break;

                default:
                    Debug.LogWarning($"Неизвестный тип стеллажа");
                    break;
            }
        }
    }
}
