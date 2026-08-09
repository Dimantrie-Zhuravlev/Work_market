using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputs : MonoBehaviour
{
    //[SerializeField] private GunsController _gunsController;
    //[SerializeField] private ZoneSpidersCreateCollider _zoneSpiderButton;
    [Header("Abilities")]
    [SerializeField] private PlayerMovement _abilityMove;
    [SerializeField] private PlayerCheckView _abilityWithBox;
    //[SerializeField] private PlayerAbilityJump _abilityJump;
    [SerializeField] private PlayerRotation _CameraRotation;
    //[SerializeField] private PlayerAbilityCrouch _abilityCrouch;


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

    public void OnBoxPickUp(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _abilityWithBox.PickUpBoxOnEventKeyboard(context);
        }
    }

    public void OnTrashEmptyBoxes(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _abilityWithBox.TrashEmptyBoxesOnEventKeyboard(context);
        }
    }
    public void OnBoxDropDown(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _abilityWithBox.DropBoxOnEventKeyboard();
        }
    }


    public void OnLook(InputAction.CallbackContext context)
    {
        _CameraRotation.AbilityActivatePerformed(context);
    }
}
