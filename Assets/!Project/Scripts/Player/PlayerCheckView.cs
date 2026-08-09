using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCheckView : MonoBehaviour
{
    private float _lastCheckTime = 0f; //динамическая
    private float _checkInterval = 0.05f;

    private GameObject _mainCamera;
    [SerializeField] private LayerMask _layerMask;

    private string viewBoxName;  //эта и ниже связано только с коробками
    private GameObject viewBoxObject;

    private TrashCanDataAbility viewTrashObject; //текущая мусорка

    private ShelfController viewShelfObject; //текущая полка товаров на стеллаже

    [SerializeField] private PlayerInput playerInput;
    private InputAction _pickUpBoxAction;//Дизейбл эвента по коробкам
    private InputAction _trashCanAction;
    private InputAction _putObjectOnShelfAction;

    private void Start()
    {
        _mainCamera = CameraManager.Instance.GetComponent<Camera>().gameObject;
    }

    private void Awake()
    {
        _pickUpBoxAction = playerInput.actions["PickUpBox"];
        _trashCanAction = playerInput.actions["TrashEmptyBoxes"];
        _putObjectOnShelfAction = playerInput.actions["PutObjectOnShelf"];
    }

    void Update()
    {
        if (Time.time - _lastCheckTime > _checkInterval)
        {
            RaycastHit hit;
            bool hasHit = Physics.Raycast(_mainCamera.transform.position, _mainCamera.transform.forward, out hit, 5f, _layerMask, QueryTriggerInteraction.Ignore);
            if (hasHit && hit.collider)
            {
                bool hasMessage = false;
                if (hit.collider.gameObject.TryGetComponent<CurrentBoxSetting>(out var viewBox)) //Подъем коробки 
                {
                    hasMessage = true;
                    viewBoxName = viewBox._currentBoxSetting.typeBox;
                    _pickUpBoxAction.Enable();
                    viewBoxObject = hit.collider.gameObject;
                    sendPersonMessage(viewBox._currentBoxSetting.playerMessageViewBox);
                }
                else
                {
                    _pickUpBoxAction.Disable();
                }

                if (hit.collider.gameObject.TryGetComponent<EnvironmentsPersonMessage>(out var environmentWithMessage)) //чтение сообщений от предметов если есть
                {
                    hasMessage = true;
                    sendPersonMessage(environmentWithMessage.personMessage);
                }

                if (hit.collider.gameObject.TryGetComponent<TrashCanDataAbility>(out var trashCan)) //мусорка пустых коробок
                {
                    _trashCanAction.Enable();
                    viewTrashObject = trashCan;
                }
                else
                {
                    _trashCanAction.Disable();
                    trashCan = null;
                }

                if (hit.collider.gameObject.TryGetComponent<ShelfController>(out var shelf)) //продуктовая полка стеллажа
                {
                    viewShelfObject = shelf;
                    _putObjectOnShelfAction.Enable();
                }
                else
                {
                    _putObjectOnShelfAction.Disable();
                    shelf = null;
                }

                if (!hasMessage)
                {
                    ClearMessageAndVieBox();
                }
            }
            else
            {
                ClearMessageAndVieBox();
            }
            _lastCheckTime = Time.time;
        }

    }
    public void PickUpBoxOnEventKeyboard(InputAction.CallbackContext obj) //функционал от ввода, его не убрать
    {
        HandsPollBoxes.Instance.ActivateHandBox(viewBoxObject, viewBoxName);
    }
    public void TrashEmptyBoxesOnEventKeyboard(InputAction.CallbackContext obj) //функционал от ввода, его не убрать
    {
        HandsPollBoxes.Instance.UtilizeHandBox();
    }
    public void DropBoxOnEventKeyboard()
    {
        HandsPollBoxes.Instance.DropHandBox();
    }

    public void PutObjectOnShelfOnEventKeyboard()
    {
        if (HandsPollBoxes.Instance.CurrentBoxNameInHands == viewShelfObject._shelfName) //проверка что товар в коробке и на полке совпадают
        {
            CurrentBoxSetting currentBox = HandsPollBoxes.Instance.CurrentBoxHasCountObjects();
            //print(currentBox.CurrentCountObjectsInBox);
            if (currentBox.CurrentCountObjectsInBox > 0)
            {
                viewShelfObject.AddOneObject(currentBox);
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
