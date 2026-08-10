using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputs : MonoBehaviour
{
    [Header("Abilities")]
    [SerializeField] private PlayerMovement _abilityMove;
    [SerializeField] private PlayerRotation _CameraRotation;
    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _abilityMove.AbilityActivatePerformed(context);
        }
        else if (context.canceled)
        {
            _abilityMove.AbilityActivateCanceled(context);
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        _CameraRotation.AbilityActivatePerformed(context);
    }
}
