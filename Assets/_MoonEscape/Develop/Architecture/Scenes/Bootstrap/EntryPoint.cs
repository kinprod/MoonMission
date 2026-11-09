using Cysharp.Threading.Tasks;
using DI;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer.Unity;


namespace Scenes
{
    public class EntryPoint
    {
        [SerializeField]
        private LifetimeScope _rootContainer;
        private static EntryPoint _instance;



        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void AutostartGame()
        {
            _instance = new EntryPoint();
            _instance.RunGame().Forget();
        }

        private EntryPoint()
        {

        }

        private async UniTask RunGame()
        {
            ApplySettings();

#if UNITY_EDITOR
            var sceneName = SceneManager.GetActiveScene().name;

            if (sceneName == Scenes.GAMEPLAY)
            {
                await LoadAndStartGameplay();
                return;
            }

            if (sceneName == Scenes.MAIN_MENU)
            {
                await LoadAndStartMainMenu();
                return;
            }

            if (sceneName != Scenes.BOOTSTRAP)
            {
                return;
            }
#endif

            await LoadAndStartMainMenu();
        }

        private async UniTask LoadAndStartGameplay()
        {
            await LoadScene(Scenes.BOOTSTRAP);
            await LoadScene(Scenes.EMPTY);
            await LoadScene(Scenes.GAMEPLAY);


            var sceneEntryPoint = Object.FindAnyObjectByType<SceneEntryPoint>();
            sceneEntryPoint?.Run();
        }


        private async UniTask LoadAndStartMainMenu()
        {
            await LoadScene(Scenes.BOOTSTRAP);
            await LoadScene(Scenes.EMPTY);
            await LoadScene(Scenes.MAIN_MENU);

            var sceneEntryPoint = Object.FindAnyObjectByType<SceneEntryPoint>();
            sceneEntryPoint?.Run();
        }


        private async UniTask LoadScene(string sceneName)
        {
            await SceneManager.LoadSceneAsync(sceneName);
        }

        private void ApplySettings()
        {
            GameSettings gameSettings;

            if (!PlayerPrefs.HasKey("Settings"))
            {
                gameSettings = new GameSettings();
                gameSettings.MusicValue = 1f;

            }
            else
            {
                string json = PlayerPrefs.GetString("Settings");
                gameSettings = JsonUtility.FromJson<GameSettings>(json);
            }

            AudioListener.volume = gameSettings.MusicValue;
        }
    }
}