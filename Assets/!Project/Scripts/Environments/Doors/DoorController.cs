using UnityEngine;

public class DoorController : MonoBehaviour, IInteractable
{
    private Animator _animator;
    private EnvironmentsPersonMessage _message;

    private bool doorOpened = false;

    private int _stateHash;
    private bool isAnimating = false;

    [SerializeField] int needLvlToOpen = 0;


    private void Awake()
    {
        _stateHash = Animator.StringToHash("IsOpen");
        _message = GetComponent<EnvironmentsPersonMessage>();
        _animator = GetComponent<Animator>();
    }
    //
    public void Interact()
    {
        if (ExperienceSystem.Instance.CurrentLevel >= needLvlToOpen)
        {
            if (isAnimating) return;
            bool targetState = !_animator.GetBool("IsOpen");

            isAnimating = true;
            _animator.SetBool("IsOpen", targetState);


            doorOpened = !doorOpened;
            _animator.SetBool(_stateHash, doorOpened);

            float animationTime = 1f;
            Invoke(nameof(UnlockInteraction), animationTime);
        }
        else
        {
            PersonMessageLifeCycle.Instance.SendLifeCycleMessage($"Нужен {needLvlToOpen} уровень");
        }

    }

    private void UnlockInteraction()
    {
        isAnimating = false;
    }

}
