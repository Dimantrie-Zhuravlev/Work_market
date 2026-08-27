using UnityEngine;
using UnityEngine.InputSystem;

public class MenuSceneStart : MonoBehaviour
{
    [SerializeField] private InputActionAsset _playerInput;

    [SerializeField] GameObject MainPanel;
    [SerializeField] GameObject GamePanel;
    [SerializeField] GameObject SettingsPanel;
    [SerializeField] GameObject InformationPanel;

    private void Start()
    {
        _playerInput.FindActionMap("ComputerUI").Enable();
        _playerInput.FindActionMap("PauseUI").Disable();
        _playerInput.FindActionMap("Player").Disable();
        _playerInput.FindActionMap("MainMenu").Enable();

        MainPanel.SetActive(true);
        SettingsPanel.SetActive(false);
        InformationPanel.SetActive(false);
        GamePanel.SetActive(false);
    }
}
