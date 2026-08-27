using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    private enum ScenesName
    {
        MainMenuScene_01,
        GameScene_02
    }
    public void LoanMainMenu()
    {
        SceneManager.LoadScene(ScenesName.MainMenuScene_01.ToString(), LoadSceneMode.Single);
    }
    public void LoadGameScene()
    {
        SceneManager.LoadScene(ScenesName.GameScene_02.ToString(), LoadSceneMode.Single);
    }
}
