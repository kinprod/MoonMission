using System;
using UnityEngine;

public class WinUI : MonoBehaviour
{
    public event Action NextSceneLoad;

    [SerializeField]
    private LevelEntryPoint _entryPoint;

    public void NextScene()
    {
        NextSceneLoad?.Invoke();
    }

    public void MainMenu()
    {
        _entryPoint.LoadNextScene(Scenes.Scenes.MAIN_MENU);
    }
}
