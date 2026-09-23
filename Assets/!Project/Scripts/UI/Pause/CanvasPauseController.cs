using UnityEngine;

public class CanvasPauseController : MonoBehaviour
{
    public static CanvasPauseController Instance;
    [SerializeField] UIPauseSaveData pauseButton;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void PauseButtonChangeInteractable(bool interactable)
    {
        pauseButton.ChangeInteractable(interactable);
    }
}

