using UnityEngine;
using VContainer;

public class MainMenuUI : MonoBehaviour
{
    private MainMenuEntryPoint _entryPoint;

    [SerializeField]
    private GameObject _settingsUI;


    [Inject]
    private void Init(MainMenuEntryPoint mainMenuEntryPoint)
    {
        _entryPoint = mainMenuEntryPoint;
    }

    public void StartGame()
    {
        _entryPoint.LoadNextScene(Scenes.Scenes.GAMEPLAY);
    }

    public void Settings()
    {
        _settingsUI.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
