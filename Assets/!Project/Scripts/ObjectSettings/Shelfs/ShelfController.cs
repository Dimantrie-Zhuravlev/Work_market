using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShelfController : MonoBehaviour, IInteractableMouse
{
    [SerializeField] public string _shelfName;

    private List<GameObject> _ObjectsOnShelf = new List<GameObject>();

    private AbstractPoolShelf _currentPoolShelf;
    public AbstractPoolShelf CurrentPoolShelf => _currentPoolShelf;

    private void Start()
    {
        switch (_shelfName)  //Это нужно чтобы дочерние элементы определили свой пул, из-за связи префаба-элемента
        {
            case EnumBoxesName.EmptyBox:
                _currentPoolShelf = PoolEmptyShelf.Instance;
                break;
            case EnumBoxesName.MakaronsBox:
                _currentPoolShelf = PoolMakaronShelf.Instance;
                break;

            case EnumBoxesName.GoroxBox:
                _currentPoolShelf = PoolGoroxShelf.Instance;
                break;

            default:
                Debug.LogWarning($"Неизвестный тип коробки");
                break;
        }
    }

    public void InteractMouse()
    {
        CurrentBoxSetting currentBox = HandObjectsController.Instance.CurrentBoxHasCountObjects();
        if (currentBox != null)
        {
            GameObject viewWorkingObject = PlayerCheckView.Instance.ViewWorkingObject;
            ShelfController shelfController = viewWorkingObject.GetComponent<ShelfController>();
            if (currentBox._currentBoxSetting.typeBox == shelfController._shelfName) //проверка что товар в коробке и на полке совпадают
            {
                if (currentBox.CurrentCountObjectsInBox > 0)
                {
                    shelfController.AddOneObject(currentBox);
                }
            }
            else
            {
                if (shelfController._shelfName == "EMPTY" && currentBox._currentBoxSetting.typeBox != "EMPTY")
                {
                    if (currentBox.CurrentCountObjectsInBox > 0)
                    {
                        ShelfsPoolController.Instance.ChangeShelfTypeAndAddObject(viewWorkingObject);
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

    public void AddOneObject(CurrentBoxSetting currentBox)
    {
        if (Get())
        {
            currentBox.DecrementOneObjectInBox();
        }
    }


}
