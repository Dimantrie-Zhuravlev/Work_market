using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCheckView : MonoBehaviour
{
    private float _lastCheckTime = 0f; //динамическая
    private float _checkInterval = 0.1f;

    private GameObject _mainCamera;
    [SerializeField] private LayerMask _layerMask;

    private GameObject viewWorkingObject;

    [SerializeField] private PlayerInput playerInput;

    private InputAction _pickUpBoxAction;//Дизейбл эвента по коробкам
    private InputAction _utilizeCanAction;
    private InputAction _putObjectOnShelfAction;
    private InputAction _clickUniversalButtonfAction;
    private InputAction _clickTaskBoardButton;
    private InputAction _clickTaskBoardTask;
    private InputAction _buttonSelectedTaskCompleteAction;

    private void Start()
    {
        _mainCamera = CameraManager.Instance.GetComponent<Camera>().gameObject;
    }

    private void Awake()
    {
        _pickUpBoxAction = playerInput.actions["PickUpBox"];
        _utilizeCanAction = playerInput.actions["UtilizeEmptyBoxes"];
        _putObjectOnShelfAction = playerInput.actions["PutObjectOnShelf"];
        _clickUniversalButtonfAction = playerInput.actions["ClickUniversalButton"];
        _clickTaskBoardButton = playerInput.actions["ClickTaskBoardButton"];
        _clickTaskBoardTask = playerInput.actions["ClickTaskBoardTask"];
        _buttonSelectedTaskCompleteAction = playerInput.actions["ButtonSelectedTaskComplete"];


        DisableCurrentInputs();
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
                viewWorkingObject = hit.collider.gameObject;
                needSetEquipCursor = false;
                if (hit.collider.gameObject.TryGetComponent<CurrentBoxSetting>(out var viewBox)) //Подъем коробки 
                {
                    _pickUpBoxAction.Enable();
                    needSetEquipCursor = true;
                }
                else
                {
                    _pickUpBoxAction.Disable();
                }
                if (hit.collider.gameObject.TryGetComponent<EnvironmentsPersonMessage>(out var environmentWithMessage)) //чтение сообщений от предметов если есть
                {
                    needSetEquipCursor = true;
                    sendPersonMessage(environmentWithMessage.PersonMessage);
                } else
                {
                    ClearMessageAndVieBox();
                }
                if ( hit.collider.gameObject.TryGetComponent<TrashCanDataAbility>(out var trashCan)) //мусорка пустых коробок
                {
                    _utilizeCanAction.Enable();
                    needSetEquipCursor = true;
                }
                else
                {
                    _utilizeCanAction.Disable();

                }

                if (hit.collider.gameObject.TryGetComponent<ShelfController>(out var shelf)) //продуктовая полка стеллажа
                {
                    needSetEquipCursor = true;
                    _putObjectOnShelfAction.Enable();
                }
                else
                {
                    _putObjectOnShelfAction.Disable();
                    shelf = null;
                }

                if (hit.collider.gameObject.TryGetComponent<UniversalButtonEvent>(out var universalButton)) //клик на универсальную кнопку
                {
                    _clickUniversalButtonfAction.Enable();
                    needSetEquipCursor = true;
                }
                else
                {
                    _clickUniversalButtonfAction.Disable();
                }

                if (hit.collider.gameObject.TryGetComponent<TaskBoardButtonController>(out var taskBoardButton)) //кнопка на доске заданий
                {
                    _clickTaskBoardButton.Enable();
                    needSetEquipCursor = true;
                }
                else
                {
                    _clickTaskBoardButton.Disable();
                }

                if (hit.collider.gameObject.TryGetComponent<TaskBoardTaskController>(out var boardTask)) //задание на доске заданий
                {
                    _clickTaskBoardTask.Enable();
                    needSetEquipCursor = true;
                }
                else
                {
                    _clickTaskBoardTask.Disable();
                }

                if (hit.collider.gameObject.TryGetComponent<TaskActiveButtonController>(out var completeSelectedTask)) //задание на доске заданий
                {
                    _buttonSelectedTaskCompleteAction.Enable();
                    needSetEquipCursor = true;
                }
                else
                {
                    _buttonSelectedTaskCompleteAction.Disable();
                }
            }
            else
            {
                needSetEquipCursor = false;
                DisableCurrentInputs();
                ClearMessageAndVieBox();
            }
            _lastCheckTime = Time.time;
            CrosshairController.Instance.SetEquipCursor(needSetEquipCursor);
        }

    }

    private void DisableCurrentInputs()
    {
        _pickUpBoxAction.Disable();
        _utilizeCanAction.Disable();
        _putObjectOnShelfAction.Disable();
        _clickUniversalButtonfAction.Disable();
        _clickTaskBoardButton.Disable();
        _clickTaskBoardTask.Disable();
    }
    public void TaskBoardButtonEventKeyboard(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            viewWorkingObject.GetComponent<TaskBoardButtonController>().AddTaskOnBoard();
        }
    }


    public void PickUpBoxOnEventKeyboard(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            HandsPollBoxes.Instance.PickUpHandBox(viewWorkingObject);
        }
    }
    public void ClickTaskBoardTaskEventKeyboard(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            viewWorkingObject.GetComponent<TaskBoardTaskController>().AddTaskOnActiveTaskCapBoard(viewWorkingObject); //что за чушь
        }
    }
    public void UtilizeEmptyBoxesOnEventKeyboard(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            HandsPollBoxes.Instance.UtilizeHandBox();
        }
    }
    public void ClickOnUniversalButtonEventKeyboard(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            viewWorkingObject.GetComponent<UniversalButtonEvent>().BuyBoxObjects();
        }
    }
    public void DropBoxOnEventKeyboard(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            HandsPollBoxes.Instance.DropHandBox();
        }
    }

    public void CompleteSelectedTaskOnEventKeyboard(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            viewWorkingObject.GetComponent<TaskActiveButtonController>().AddTaskOnActiveBoard();
        }
    }




    public void PutObjectOnShelfOnEventKeyboard(InputAction.CallbackContext context)
    {
        CurrentBoxSetting currentBox = HandsPollBoxes.Instance.CurrentBoxHasCountObjects();
        if (context.performed && currentBox != null)
        {
            ShelfController shelfController = viewWorkingObject.GetComponent<ShelfController>();
            if (HandsPollBoxes.Instance.CurrentObjectInHandName == shelfController._shelfName ) //проверка что товар в коробке и на полке совпадают
            {
                if (currentBox.CurrentCountObjectsInBox > 0)
                {
                    shelfController.AddOneObject(currentBox);
                }
            }
            else
            {
                if (shelfController._shelfName == "EMPTY" && HandsPollBoxes.Instance.CurrentObjectInHandName != "EMPTY")
                {
                    if (currentBox.CurrentCountObjectsInBox > 0)
                    {
                        ShelfsPoolController.Instance.ChangeShelfTypeAndAddObject(viewWorkingObject);
                    }
                }
            }
        }
    }

    private void ClearMessageAndVieBox()
    {
        PersonMessageInfo.Instance.ClearPersonMessage();
    }
    public void sendPersonMessage(string message)
    {
        PersonMessageInfo.Instance.SetPersonMessage(message);
    }
}
