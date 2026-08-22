using DiasGames.Abilities;
using UnityEngine;

public class CarryBoxAbility : AbstractAbility
{
    [SerializeField] private float carrySpeed = 3f;
    [SerializeField] private CharacterController controller;
    [SerializeField] private Transform mainCamera;

    private Vector3 _velocity;
    private bool isWalking = false;

    public override bool ReadyToRun()
    {
        return HandObjectsController.Instance.CurrentObjectInHand != null;
    }

    public override void OnStartAbility()
    {
        _velocity = Vector3.zero;
        isWalking = false;
        SetAnimationState("CarryBox");

        GameObject handsObject = HandObjectsController.Instance.CurrentObjectInHand;
        if (handsObject != null)
        {
            var boxSettings = handsObject.GetComponent<CurrentBoxSetting>();
        }
    }

    public override void UpdateAbility()
    {
        // ВАЖНО: Если коробка исчезла из рук - останавливаем способность
        if (HandObjectsController.Instance.CurrentObjectInHand == null)
        {
            StopAbility(); // Это вызовет OnStopAbility() и переключит приоритет
            return;
        }

        if (controller == null || _action == null || mainCamera == null) return;

        // Гравитация
        if (controller.isGrounded && _velocity.y < 0)
            _velocity.y = -2f;

        _velocity.y += Physics.gravity.y * Time.deltaTime;

        // Движение с коробкой (медленнее)
        if (_action.move != Vector2.zero)
        {
            if (!isWalking)
            {
                isWalking = true;
                _animator.SetBool(_hashIsWalking, true);
            }

            Vector3 cameraForward = mainCamera.forward;
            Vector3 cameraRight = mainCamera.right;
            cameraForward.y = 0;
            cameraRight.y = 0;
            cameraForward.Normalize();
            cameraRight.Normalize();

            Vector3 desiredMove = cameraForward * _action.move.y + cameraRight * _action.move.x;

            Vector3 move = desiredMove * carrySpeed;
            move.y = _velocity.y;

            controller.Move(move * Time.deltaTime);
        }
        else
        {
            if (isWalking)
            {
                isWalking = false;
                _animator.SetBool(_hashIsWalking, false);
            }

            Vector3 move = new Vector3(0, _velocity.y, 0);
            controller.Move(move * Time.deltaTime);
        }
    }

    public override void OnStopAbility()
    {
        _velocity = Vector3.zero;
        isWalking = false;
        _animator.SetBool(_hashIsWalking, false);
        SetAnimationState("Idle");
    }
}