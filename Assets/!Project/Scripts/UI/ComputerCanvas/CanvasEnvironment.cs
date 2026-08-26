using UnityEngine;
using UnityEngine.InputSystem;

public class CanvasEnvironment : MonoBehaviour
{
    [Header("Это два основных канваса")]
    [SerializeField] GameObject mainCanvas;
    [Header("Панели магазина и улучшений")]
    [SerializeField] GameObject panelMenu;
    [SerializeField] GameObject panelShop;
    [SerializeField] GameObject panelSystem;
    [SerializeField] GameObject panelUpgrades;


    [SerializeField] private PlayerInput _playerInput;
    private void OnEnable()
    {
        panelMenu.SetActive(true);
        panelShop.SetActive(false);
        panelUpgrades.SetActive(false);
        panelSystem.SetActive(false);
        currentPanelActive = 0;
    }

    private int currentPanelActive = 0;

    public void InteractPanelComputer(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            switch (currentPanelActive)
            {
                case 0:
                    CloseComputer();
                    break;
                case 1:
                    ClosePanelShop();
                    break;
                case 2:
                    ClosePanelUpgrades();
                    break;
                case 3:
                    ClosePanelSystem();
                    break;
            }

        }
    }
    public void ClosePanelShop()
    {
        currentPanelActive = 0;
        panelMenu.SetActive(true);
        panelShop.SetActive(false);
    }
    public void ClosePanelUpgrades()
    {
        currentPanelActive = 0;
        panelMenu.SetActive(true);
        panelUpgrades.SetActive(false);
    }

    public void ClosePanelSystem()
    {
        currentPanelActive = 0;
        panelMenu.SetActive(true);
        panelSystem.SetActive(false);
    }

    public void OpenPanelShop()
    {
        panelMenu.SetActive(false);
        panelShop.SetActive(true);
        currentPanelActive = 1;
    }

    public void OpenPanelUpgrades()
    {
        panelMenu.SetActive(false);
        panelUpgrades.SetActive(true);
        currentPanelActive = 2;
    }

    public void OpenPanelSystem()
    {
        panelMenu.SetActive(false);
        panelSystem.SetActive(true);
        currentPanelActive = 3;
    }

    public void CloseComputer()
    {
        mainCanvas.SetActive(true);
        _playerInput.SwitchCurrentActionMap("Player");
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        Cursor.visible = false;
        gameObject.SetActive(false);
    }
}
