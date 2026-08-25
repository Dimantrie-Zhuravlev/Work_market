using UnityEngine;
using UnityEngine.InputSystem;

public class GameStart : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] GameObject mainCanvas;
    [SerializeField] GameObject ComputerCanvas;
    private void Awake()
    {
        _playerInput.SwitchCurrentActionMap("Player");
        Cursor.lockState = CursorLockMode.Locked;
        mainCanvas.SetActive(true);
        ComputerCanvas.SetActive(false);
    }
}
