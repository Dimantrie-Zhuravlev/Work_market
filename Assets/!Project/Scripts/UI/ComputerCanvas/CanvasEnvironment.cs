using UnityEngine;
using UnityEngine.InputSystem;

namespace Canvas.Computer
{
    public class CanvasEnvironment : MonoBehaviour
    {
        [Header("Это два основных канваса")]
        [SerializeField] GameObject mainCanvas;
        [Header("Панели магазина и улучшений")]
        [SerializeField] GameObject panelMenu;
        [SerializeField] GameObject panelShop;
        [SerializeField] GameObject panelSystem;
        [SerializeField] GameObject panelUpgrades;

        [SerializeField] private InputActionAsset _playerInput;
        private void OnEnable()
        {

            _playerInput.FindActionMap("ComputerUI").Enable();
            _playerInput.FindActionMap("PauseUI").Disable();
            _playerInput.FindActionMap("Player").Disable();

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
                        CloseCanvas();
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

        public void CloseCanvas()
        {
            mainCanvas.SetActive(true);
            _playerInput.FindActionMap("ComputerUI").Disable();
            _playerInput.FindActionMap("PauseUI").Disable();
            _playerInput.FindActionMap("Player").Enable();
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
            Cursor.visible = false;
            gameObject.SetActive(false);
        }
    }

}
