using System;
using UnityEngine;

public class WinUI : MonoBehaviour
{
    public event Action NextSceneLoad;

    [SerializeField]
    private LevelEntryPoint _entryPoint;

    private void OnEnable()
    {
        Time.timeScale = 0f;
    }

    public void NextScene()
    {
        Time.timeScale = 1f;
        NextSceneLoad?.Invoke();
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        _entryPoint.LoadNextScene(Scenes.Scenes.MAIN_MENU);
    }
}
