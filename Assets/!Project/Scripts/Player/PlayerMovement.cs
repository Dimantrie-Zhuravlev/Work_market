using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Параметры")]
    [SerializeField, Range(6, 12f)] private float _baseSpeed = 6f;
    [SerializeField, Range(8f, 18f)] private float _shiftSpeed = 12f;
    [SerializeField, Range(1f, 2f)] private float _gravityScale = 1.5f;


    [Header("Ссылки")]
    [SerializeField] private Transform _mainCamera;
    [SerializeField] private CharacterController controller;

    private Animator _animator;
    private Vector3 playerVelocity;

    private int _hashIsWalking;

    private bool isPlayerOnVerticalStair = false;

    public void ChangeIsPlayerVerticalStair(bool value)
    {
        isPlayerOnVerticalStair = value;
    }

    public Vector3 PlayerVelocity
    {
        get { return playerVelocity; }
        set { playerVelocity = value; }
    }

    private Vector2 _inputDirection;
    private float currentSpeed;

    public float CurrentSpeed => currentSpeed;

    public void Awake()
    {
        _animator = this.GetComponent<Animator>();
    }
    private void Start()
    {
        SetBaseSpeed();
        _hashIsWalking = Animator.StringToHash("IsWalking");
    }
    private void FixedUpdate()
    {
        if (!isPlayerOnVerticalStair)
        {  //логика для обычного движения
            if (controller.isGrounded && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }
            // 2. Обрабатываем горизонтальное движение
            HandleHorizontalMovement();
            // 3. Применяем гравитацию
            playerVelocity.y += Physics.gravity.y * Time.deltaTime * _gravityScale;
            controller.Move(playerVelocity * Time.deltaTime);
        }
        else
        {//логика для движения по вертикальной лестнице
            // 2. Обрабатываем горизонтальное движение
            HandleVerticalStairMovement();
            // 3. Применяем гравитацию
            controller.Move(playerVelocity * Time.deltaTime);
        }

    }

    public void SetBaseSpeed() => currentSpeed = _baseSpeed;
    public void SetShiftSpeed() => currentSpeed = _shiftSpeed;

    private void HandleHorizontalMovement()
    {
        if (_inputDirection != Vector2.zero)
        {
            Vector3 cameraForward = _mainCamera.forward;
            Vector3 cameraRight = _mainCamera.right;

            cameraForward.y = 0;
            cameraRight.y = 0;

            cameraForward.Normalize();
            cameraRight.Normalize();


            Vector3 desiredMoveDirection = cameraForward * _inputDirection.y + cameraRight * _inputDirection.x;
            // Устанавливаем горизонтальную скорость. Вертикальная (y) остается прежней.
            playerVelocity.x = desiredMoveDirection.x * currentSpeed;
            playerVelocity.z = desiredMoveDirection.z * currentSpeed;
        }
        else
        {
            playerVelocity.x = 0f;
            playerVelocity.z = 0f;
        }
    }

    private void HandleVerticalStairMovement()
    {
        if (_inputDirection != Vector2.zero)
        {
            Vector3 cameraForward = _mainCamera.forward;
            Vector3 cameraRight = _mainCamera.right;

            cameraForward.Normalize();
            cameraRight.Normalize();

            Vector3 desiredMoveDirection = cameraForward * _inputDirection.y + cameraRight * _inputDirection.x;
            // Устанавливаем скорость. В отличие от обычного y тоже меняется
            playerVelocity = desiredMoveDirection * currentSpeed;
        }
        else
        {
            playerVelocity.x = 0f;
            playerVelocity.y = 0f;
            playerVelocity.z = 0f;
        }
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _inputDirection = context.ReadValue<Vector2>();
            _animator.SetBool(_hashIsWalking, true);      
        }
        else if (context.canceled)
        {
            _animator.SetBool(_hashIsWalking, false);
            _inputDirection = Vector2.zero;
        }
    }
}
