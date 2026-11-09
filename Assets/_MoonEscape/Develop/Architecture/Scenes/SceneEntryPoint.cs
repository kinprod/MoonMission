using Cysharp.Threading.Tasks;
using GameInputActions;
using Scenes;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;

public abstract class SceneEntryPoint : MonoBehaviour
{
    private SceneDoors _sceneDoors;

    [Inject]
    private void Init(SceneDoors sceneDoors)
    {
        _sceneDoors = sceneDoors;
    }

    virtual public void Run() { }

    virtual public async void LoadNextScene(string sceneName)
    {
        _sceneDoors.CloseDoors();

        await UniTask.Delay(500);
        await LoadScene(Scenes.Scenes.EMPTY);
        await LoadScene(sceneName);

        _sceneDoors.OpenDoors();

        var sceneEntryPoint = Object.FindAnyObjectByType<SceneEntryPoint>();
        sceneEntryPoint?.Run();
    }

    protected async UniTask LoadScene(string sceneName)
    {
        await SceneManager.LoadSceneAsync(sceneName);
    }
}
