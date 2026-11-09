using Cysharp.Threading.Tasks;
using Scenes;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class SceneEntryPoint : MonoBehaviour
{
    abstract public void Run();

    virtual public async void LoadNextScene(string sceneName)
    {
        await LoadScene(Scenes.Scenes.EMPTY);
        await LoadScene(Scenes.Scenes.LOADING);
        await LoadScene(sceneName);

        var sceneEntryPoint = Object.FindAnyObjectByType<SceneEntryPoint>();
        sceneEntryPoint?.Run();
    }

    private async UniTask LoadScene(string sceneName)
    {
        await SceneManager.LoadSceneAsync(sceneName);
    }
}
