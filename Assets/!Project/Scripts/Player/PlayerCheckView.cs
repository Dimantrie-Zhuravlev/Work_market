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

    private IInteractable _currentTargetInteract;
    private IInteractableMouse _currentTargetInteractMouse;
    private IInteractableRightMouse _currentTargetInteractRightMouse;

    public bool IsCarryingBox { get; private set; } = false;
    public void SetCarryingBox(bool value) => IsCarryingBox = value;
    public static PlayerCheckView Instance { get; private set;  }
    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        _mainCamera = CameraManager.Instance.GetComponent<Camera>().gameObject;
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

                if (hit.collider.gameObject.TryGetComponent<IInteractable>(out var targetInteract)) //все события с кликом на E
                {
                    _currentTargetInteract = targetInteract;
                    needSetEquipCursor = true;
                } else
                {
                    _currentTargetInteract = null;
                }

                if (hit.collider.gameObject.TryGetComponent<IInteractableMouse>(out var targetMouseInteract)) //все события с кликом на мышью
                {
                    _currentTargetInteractMouse = targetMouseInteract;
                    needSetEquipCursor = true;
                }
                else
                {
                    _currentTargetInteractMouse = null;
                }

                if (hit.collider.gameObject.TryGetComponent<IInteractableRightMouse>(out var targetMouseRightInteract)) //все события с кликом на мышью
                {
                    _currentTargetInteractRightMouse = targetMouseRightInteract;
                    needSetEquipCursor = true;
                }
                else
                {
                    _currentTargetInteractRightMouse = null;
                }

                if (hit.collider.gameObject.TryGetComponent<EnvironmentsPersonMessage>(out var environmentWithMessage)) //чтение сообщений от предметов если есть
                {
                    needSetEquipCursor = true;
                    PersonMessageInfo.Instance.SetPersonMessage(environmentWithMessage.PersonMessage);
                } else
                {
                    ClearMessageAndVieBox();
                }
            }
            else
            {
                _currentTargetInteract = null;
                _currentTargetInteractMouse = null;
                needSetEquipCursor = false;
                ClearMessageAndVieBox();
            }
            _lastCheckTime = Time.time;
            CrosshairController.Instance.SetEquipCursor(needSetEquipCursor);
        }

    }

    private void ClearMessageAndVieBox()
    {
        PersonMessageInfo.Instance.ClearPersonMessage();
    }
    public void OnPerformInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _currentTargetInteract?.Interact();
        }
    }
    public void LeftMouseCLickEventKeyboard(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _currentTargetInteractMouse?.InteractMouse();
        }
    }
    public void RightMouseCLickEventKeyboard(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _currentTargetInteractRightMouse?.InteractRightMouse();
        }
    }

}
