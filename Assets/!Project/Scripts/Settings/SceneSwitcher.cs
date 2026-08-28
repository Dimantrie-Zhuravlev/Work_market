using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneSwitcher
{
    private enum ScenesName
    {
        MainMenuScene_01,
        GameScene_02
    }
    public static void LoanMainMenu()
    {
        SceneManager.LoadScene(ScenesName.MainMenuScene_01.ToString(), LoadSceneMode.Single);
    }
    public static void LoadGameScene()
    {
        SceneManager.LoadScene(ScenesName.GameScene_02.ToString(), LoadSceneMode.Single);
    }
}
