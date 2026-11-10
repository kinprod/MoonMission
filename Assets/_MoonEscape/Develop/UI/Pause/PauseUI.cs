using Cysharp.Threading.Tasks;
using GameInputActions;
using Unity.VisualScripting;
using UnityEngine;
using VContainer;

public class PauseUI : MonoBehaviour
{
    [SerializeField]
    private LevelEntryPoint _sceneEntryPoint;
    private InputManager _inputManager;

    private Object _settings;

    [Inject]
    private void Init(InputManager inputManager)
    {
        _inputManager = inputManager;

        _inputManager.StopPerformedAction += Stop;
    }

    private void OnDestroy()
    {
        _inputManager.StopPerformedAction -= Stop;
    }

    private void Stop()
    {
        gameObject.SetActive(!gameObject.activeSelf);

        if (_settings != null)
        {
            Destroy(_settings);
            _settings = null;
        }
    }

    public async void Continue()
    {
        gameObject.SetActive(false);

        if (_settings != null)
        {
            Destroy(_settings);
            _settings = null;
        }
    }

    public void Settings()
    {
        if (_settings == null)
        {
            _settings = Instantiate(Resources.Load("UI/Settings/Settings"));
            var gameObject = (GameObject)_settings;
            gameObject.SetActive(true);
        }
        else if (!((GameObject)_settings).activeSelf)
        {
            ((GameObject)_settings).SetActive(true);
        }
    }

    public void Exit()
    {
        _sceneEntryPoint?.LoadNextScene(Scenes.Scenes.MAIN_MENU);
    }
}
