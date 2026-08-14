using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class DoorController : MonoBehaviour, IInteractable
{
    private Animator _animator;
    private EnvironmentsPersonMessage _message;

    private bool doorOpened = false;

    private int _stateHash;
    private bool isAnimating = false; 

    private void Awake()
    {
        _stateHash = Animator.StringToHash("IsOpen");
        _message = this.GetComponent<EnvironmentsPersonMessage>();
        _animator = this.GetComponent<Animator>();
    }
    //
    public void Interact()
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

    private void UnlockInteraction()
    {
        isAnimating = false;
    }

}
