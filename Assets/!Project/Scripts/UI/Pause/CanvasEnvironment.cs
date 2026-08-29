using UnityEngine;
using UnityEngine.InputSystem;

namespace Canvas.Pause
{
    public class CanvasEnvironment : MonoBehaviour
    {
        private int currentPanelActive = 0;

        [SerializeField] private InputActionAsset _playerInput;
        public void ActivePause(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _playerInput.FindActionMap("ComputerUI").Disable();
                _playerInput.FindActionMap("PauseUI").Enable();
                _playerInput.FindActionMap("Player").Disable();

                Time.timeScale = 0;
                gameObject.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
        public void ClosePause(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                switch (currentPanelActive)
                {
                    case 0:
                        CloseCanvas();
                        break;
                    //case 1:
                    //    ClosePanelShop();
                    //    break;
                    //case 2:
                    //    ClosePanelUpgrades();
                    //    break;
                    //case 3:
                    //    ClosePanelSystem();
                    //    break;
                }

            }
        }

        public void CloseCanvas()
        {
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


