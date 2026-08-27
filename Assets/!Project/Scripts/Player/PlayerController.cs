using DiasGames;
using DiasGames.Abilities;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    [Header("Ссылки")]
    [SerializeField] private AbilityScheduler scheduler;
    [SerializeField] private PlayerCheckView checkView;
    [SerializeField] private PlayerRotation rotation;


    private CharacterActions _actions;

    private void Start()
    {
        _actions = new CharacterActions();
    }
    private void Update()
    {
        if (scheduler != null)
        {
            // ПРАВИЛЬНО: обновляем поля, а не перезаписываем объект
            scheduler.characterActions.move = _actions.move;
            scheduler.characterActions.jump = _actions.jump;
            scheduler.characterActions.walk = _actions.walk;
            // Если есть другие поля, добавьте их здесь
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _actions.move = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        _actions.jump = context.performed;
    }

    public void OnWalk(InputAction.CallbackContext context)
    {
        _actions.walk = context.performed;
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            checkView.OnPerformInteract(context);
            if (checkView.ViewWorkingObject != null && checkView.ViewWorkingObject.CompareTag("Box"))
            {
                checkView.SetCarryingBox(!checkView.IsCarryingBox);
            }
        }
    }
}
