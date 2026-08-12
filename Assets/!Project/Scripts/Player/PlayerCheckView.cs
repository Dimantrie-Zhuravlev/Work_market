using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCheckView : MonoBehaviour
{
    private float _lastCheckTime = 0f; //динамическая
    private float _checkInterval = 0.1f;

    private GameObject _mainCamera;
    [SerializeField] private LayerMask _layerMask;

    private GameObject viewWorkingObject;
    public GameObject ViewWorkingObject =>  viewWorkingObject;

    [SerializeField] private PlayerInput playerInput;
    private IInteractable _currentTargetInteract;

    private InputAction _putObjectOnShelfAction;
    private InputAction _clickTaskBoardButton;
    private InputAction _clickTaskBoardTask;
    private InputAction _buttonSelectedTaskCompleteAction;

    private void Start()
    {
        _mainCamera = CameraManager.Instance.GetComponent<Camera>().gameObject;
    }

    private void Awake()
    {
        _putObjectOnShelfAction = playerInput.actions["PutObjectOnShelf"];
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

                if (hit.collider.gameObject.TryGetComponent<IInteractable>(out var targetInteract)) //Подъем коробки 
                {
                    _currentTargetInteract = targetInteract;
                    needSetEquipCursor = true;
                } else
                {
                    _currentTargetInteract = null;
                }


                if (hit.collider.gameObject.TryGetComponent<EnvironmentsPersonMessage>(out var environmentWithMessage)) //чтение сообщений от предметов если есть
                {
                    needSetEquipCursor = true;
                    sendPersonMessage(environmentWithMessage.PersonMessage);
                } else
                {
                    ClearMessageAndVieBox();
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
        _putObjectOnShelfAction.Disable();
        _clickTaskBoardButton.Disable();
        _clickTaskBoardTask.Disable();
    }

    public void OnPerformInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _currentTargetInteract?.Interact();
        }
    }

    public void TaskBoardButtonEventKeyboard(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            viewWorkingObject.GetComponent<TaskBoardButtonController>().AddTaskOnBoard();
        }
    }

    public void ClickTaskBoardTaskEventKeyboard(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            viewWorkingObject.GetComponent<TaskBoardTaskController>().AddTaskOnActiveTaskCapBoard(viewWorkingObject); //что за чушь
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
