using System;
using UnityEngine;

public class LoadGameData : MonoBehaviour
{
    [SerializeField] SceneSwitcher _sceneSwitcher;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void LoadSettings(int indexFile)
    {
        _sceneSwitcher.LoadGameScene();
    }
}
