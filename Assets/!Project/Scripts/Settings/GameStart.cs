using UnityEngine;
using UnityEngine.InputSystem;

public class GameStart : MonoBehaviour
{
    [SerializeField] private InputActionAsset _playerInput;
    [SerializeField] GameObject mainCanvas;
    [SerializeField] GameObject ComputerCanvas;
    [SerializeField] GameObject PauseCanvas;
    private void Awake()
    {
        _playerInput.FindActionMap("ComputerUI").Disable();
        _playerInput.FindActionMap("PauseUI").Disable();
        _playerInput.FindActionMap("Player").Enable();

        Cursor.lockState = CursorLockMode.Locked;
        mainCanvas.SetActive(true);
        ComputerCanvas.SetActive(false);
        PauseCanvas.SetActive(false);
    }
}
