using DiasGames.Abilities;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : AbstractAbility
{
    [Header("Скорости")]
    [SerializeField, Range(6, 12f)] private float _baseSpeed = 6f;
    [SerializeField, Range(8f, 18f)] private float _shiftSpeed = 12f;

    [Header("Ссылки")]
    [SerializeField] private Transform _mainCamera;
    [SerializeField] private CharacterController controller;

    private Vector3 playerVelocity;
    private bool isWalking = false;
    //[Header("Параметры")]


    //[SerializeField, Range(1f, 2f)] private float _gravityScale = 1.5f;

    //private bool isPlayerOnVerticalStair = false; //если будет когда-нибудь вертикальная лестница раскоммитить логику

    public override bool ReadyToRun()
    {
        return true;
    }

    public override void OnStartAbility()
    {
        // Ничего особенного
    }


    //public void ChangeIsPlayerVerticalStair(bool value)
    //{
    //    isPlayerOnVerticalStair = value;
    //}

    public Vector3 PlayerVelocity
    {
        get { return playerVelocity; }
        set { playerVelocity = value; }
    }

    private Vector2 _inputDirection;

    //private void FixedUpdate()
    //{
    //if (!isPlayerOnVerticalStair)
    //{  //логика для обычного движения
    //    if (controller.isGrounded && playerVelocity.y < 0)
    //    {
    //        playerVelocity.y = 0f;
    //    }
    //    // 2. Обрабатываем горизонтальное движение
    //    HandleHorizontalMovement();
    //    // 3. Применяем гравитацию
    //    playerVelocity.y += Physics.gravity.y * Time.deltaTime * 1;
    //    controller.Move(playerVelocity * Time.deltaTime);
    //}
    //else
    //{//логика для движения по вертикальной лестнице
    // 2. Обрабатываем горизонтальное движение
    //HandleVerticalStairMovement();
    //// 3. Применяем гравитацию
    //controller.Move(playerVelocity * Time.deltaTime);
    //}

    //}

    public override void UpdateAbility()
    {
        if (controller == null || _action == null) return;

        // Гравитация
        if (controller.isGrounded && playerVelocity.y < 0)
            playerVelocity.y = -2f;

        playerVelocity.y += Physics.gravity.y * Time.deltaTime;

        // Движение
        if (_action.move != Vector2.zero)
        {
            isWalking = true;
            _animator.SetBool(_hashIsWalking, true);

            Vector3 cameraForward = _mainCamera.forward;
            Vector3 cameraRight = _mainCamera.right;
            cameraForward.y = 0;
            cameraRight.y = 0;
            cameraForward.Normalize();
            cameraRight.Normalize();

            Vector3 desiredMove = cameraForward * _action.move.y + cameraRight * _action.move.x;

            // ИЗМЕНЕНИЕ: инвертируем логику walk
            // Если walk == true (нажат Shift) → runSpeed
            // Если walk == false (Shift не нажат) → baseSpeed
            float speed = _action.walk ? _shiftSpeed : _baseSpeed;

            Vector3 move = desiredMove * speed;
            move.y = playerVelocity.y;

            controller.Move(move * Time.deltaTime);
        }
        else
        {
            if (isWalking)
            {
                _animator.SetBool(_hashIsWalking, false);
                isWalking = false;
            }
            // Двигаем только по вертикали (гравитация)
            Vector3 move = new Vector3(0, playerVelocity.y, 0);
            controller.Move(move * Time.deltaTime);
        }
    }


    //private void HandleHorizontalMovement()
    //{
    //    if (_inputDirection != Vector2.zero)
    //    {
    //        Vector3 cameraForward = _mainCamera.forward;
    //        Vector3 cameraRight = _mainCamera.right;

    //        cameraForward.y = 0;
    //        cameraRight.y = 0;

    //        cameraForward.Normalize();
    //        cameraRight.Normalize();


    //        Vector3 desiredMoveDirection = cameraForward * _inputDirection.y + cameraRight * _inputDirection.x;
    //        // Устанавливаем горизонтальную скорость. Вертикальная (y) остается прежней.
    //        playerVelocity.x = desiredMoveDirection.x * playerVelocity.x;
    //        playerVelocity.z = desiredMoveDirection.z * playerVelocity.y;
    //    }
    //    else
    //    {
    //        playerVelocity.x = 0f;
    //        playerVelocity.z = 0f;
    //    }
    //}

    //private void HandleVerticalStairMovement()
    //{
    //    if (_inputDirection != Vector2.zero)
    //    {
    //        Vector3 cameraForward = _mainCamera.forward;
    //        Vector3 cameraRight = _mainCamera.right;

    //        cameraForward.Normalize();
    //        cameraRight.Normalize();

    //        Vector3 desiredMoveDirection = cameraForward * _inputDirection.y + cameraRight * _inputDirection.x;
    //        // Устанавливаем скорость. В отличие от обычного y тоже меняется
    //        playerVelocity = desiredMoveDirection * currentSpeed;
    //    }
    //    else
    //    {
    //        playerVelocity.x = 0f;
    //        playerVelocity.y = 0f;
    //        playerVelocity.z = 0f;
    //    }
    //}
}
