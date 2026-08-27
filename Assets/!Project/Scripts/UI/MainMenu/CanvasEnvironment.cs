using UnityEngine;
using UnityEngine.InputSystem;

namespace Canvas.MainMenu
{
    public class CanvasEnvironment : MonoBehaviour
    {
        private int currentPanelActive;
        [SerializeField] GameObject MainPanel;
        [SerializeField] GameObject GamePanel;
        [SerializeField] GameObject SettingsPanel;
        [SerializeField] GameObject InformationPanel;

        private void Start()
        {
            currentPanelActive = 0;
        }
        public void OpenGamePanel()
        {
            currentPanelActive = 1;
            GamePanel.SetActive(true);
            MainPanel.SetActive(false);
        }
        public void CloseGamePanel()
        {
            currentPanelActive = 0;
            GamePanel.SetActive(false);
            MainPanel.SetActive(true);
        }

        public void OpenSettings()
        {
            currentPanelActive = 2;
            SettingsPanel.SetActive(true);
            MainPanel.SetActive(false);
        }

        public void CloseSettings()
        {
            currentPanelActive = 0;
            SettingsPanel.SetActive(false);
            MainPanel.SetActive(true);
        }

        public void OpenInformation()
        {
            currentPanelActive = 3;
            InformationPanel.SetActive(true);
            MainPanel.SetActive(false);
        }
        public void CloseInformation()
        {
            currentPanelActive = 0;
            InformationPanel.SetActive(false);
            MainPanel.SetActive(true);
        }

        public void CloseGame()
        {
            Application.Quit();
        }


        public void MainMenuEsc(InputAction.CallbackContext context)
        {
            if (context.performed && currentPanelActive>0)
            {
                switch (currentPanelActive)
                {
                    case 1:
                        CloseGamePanel();
                        break;
                    case 2:
                        CloseSettings();
                        break;
                    case 3:
                        CloseInformation();
                        break;
                        //case 3:
                        //    ClosePanelSystem();
                        //    break;
                }
            }
        }
    }
}

