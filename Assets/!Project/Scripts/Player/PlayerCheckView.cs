using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCheckView : MonoBehaviour
{
    private float _lastCheckTime = 0f; //динамическая
    private float _checkInterval = 0.2f;

    private GameObject _mainCamera;
    [SerializeField] private LayerMask _layerMask;

    private string viewBoxName;  //эта и ниже связано только с коробками
    private GameObject viewBoxObject;

    private TrashCanDataAbility viewTrashObject; //текущая мусорка

    private ShelfController viewShelfController; //текущая полка товаров на стеллаже
    private GameObject viewShelfObject; //текущая полка товаров на стеллаже

    [SerializeField] private PlayerInput playerInput;
    private InputAction _pickUpBoxAction;//Дизейбл эвента по коробкам
    private InputAction _utilizeCanAction;
    private InputAction _putObjectOnShelfAction;

    private void Start()
    {
        _mainCamera = CameraManager.Instance.GetComponent<Camera>().gameObject;
    }

    private void Awake()
    {
        _pickUpBoxAction = playerInput.actions["PickUpBox"];
        _utilizeCanAction = playerInput.actions["UtilizeEmptyBoxes"];
        _putObjectOnShelfAction = playerInput.actions["PutObjectOnShelf"];

        _pickUpBoxAction.Disable();
        _utilizeCanAction.Disable();
        _putObjectOnShelfAction.Disable();
    }
    private bool needSetEquipCursor = false;

    void Update()
    {
        if (Time.time - _lastCheckTime > _checkInterval)
        {
            RaycastHit hit;
            bool hasHit = Physics.Raycast(_mainCamera.transform.position, _mainCamera.transform.forward, out hit, 5f, _layerMask, QueryTriggerInteraction.Ignore);
            if (hasHit && hit.collider)
            {
                CurrentBoxSetting currentBox = HandsPollBoxes.Instance.CurrentBoxHasCountObjects();
                bool hasMessage = false;
                needSetEquipCursor = false;
                if (hit.collider.gameObject.TryGetComponent<CurrentBoxSetting>(out var viewBox)) //Подъем коробки 
                {
                    hasMessage = true;
                    viewBoxName = viewBox._currentBoxSetting.typeBox;
                    _pickUpBoxAction.Enable();
                    viewBoxObject = hit.collider.gameObject;
                    needSetEquipCursor = true;
                }
                else
                {
                    _pickUpBoxAction.Disable();
                }

                if (hit.collider.gameObject.TryGetComponent<EnvironmentsPersonMessage>(out var environmentWithMessage)) //чтение сообщений от предметов если есть
                {
                    hasMessage = true;
                    needSetEquipCursor = true;
                    sendPersonMessage(environmentWithMessage.PersonMessage);
                }

                if (currentBox != null && hit.collider.gameObject.TryGetComponent<TrashCanDataAbility>(out var trashCan)) //мусорка пустых коробок
                {
                    _utilizeCanAction.Enable();
                    needSetEquipCursor = true;
                    viewTrashObject = trashCan;
                }
                else
                {
                    _utilizeCanAction.Disable();
                    trashCan = null;
                }
                if (currentBox != null && hit.collider.gameObject.TryGetComponent<ShelfController>(out var shelf)) //продуктовая полка стеллажа
                {
                    viewShelfController = shelf;
                    needSetEquipCursor = true;
                    viewShelfObject = hit.collider.gameObject;
                    _putObjectOnShelfAction.Enable();
                }
                else
                {
                    viewShelfObject = null;
                    _putObjectOnShelfAction.Disable();
                    shelf = null;
                }

                CrosshairController.Instance.SetEquipCursor(needSetEquipCursor);

                if (!hasMessage)
                {
                    ClearMessageAndVieBox();
                }


            }
            else
            {
                CrosshairController.Instance.SetEquipCursor(false);
                ClearMessageAndVieBox();
            }
            _lastCheckTime = Time.time;
        }

    }
    public void PickUpBoxOnEventKeyboard(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            HandsPollBoxes.Instance.PickUpHandBox(viewBoxObject, viewBoxName);
        }
    }
    public void UtilizeEmptyBoxesOnEventKeyboard(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            HandsPollBoxes.Instance.UtilizeHandBox();
        }
    }
    public void DropBoxOnEventKeyboard(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            HandsPollBoxes.Instance.DropHandBox();
        }
    }

    public void PutObjectOnShelfOnEventKeyboard(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            CurrentBoxSetting currentBox = HandsPollBoxes.Instance.CurrentBoxHasCountObjects();
            if (HandsPollBoxes.Instance.CurrentBoxNameInHands == viewShelfController._shelfName) //проверка что товар в коробке и на полке совпадают
            {
                if (currentBox.CurrentCountObjectsInBox > 0)
                {
                    viewShelfController.AddOneObject(currentBox);
                }
            }
            else
            {
                if (viewShelfController._shelfName == "EMPTY" && HandsPollBoxes.Instance.CurrentBoxNameInHands != "EMPTY")
                {
                    if (currentBox.CurrentCountObjectsInBox > 0)
                    {
                        ShelfsPoolController.Instance.ChangeShelfTypeAndAddObject(viewShelfObject);
                    }
                }
            }
        }
    }

    private void ClearMessageAndVieBox()
    {
        viewBoxObject = null;
        PersonMessageInfo.Instance.ClearPersonMessage();
    }
    public void sendPersonMessage(string message)
    {
        PersonMessageInfo.Instance.SetPersonMessage(message);
    }
}
