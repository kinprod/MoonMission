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
        private LifetimeScope _childContainer;
        private static EntryPoint _instance;



        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void AutostartGame()
        {
            _instance = new EntryPoint();
            _instance.RunGame();
        }

        private EntryPoint()
        {

        }

        private async UniTask RunGame()
        {
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
            await LoadScene(Scenes.LOADING);
            await LoadScene(Scenes.GAMEPLAY);

            _childContainer.Dispose();
            LifetimeScope childContainer = _childContainer = _rootContainer.CreateChild();

            var sceneEntryPoint = Object.FindAnyObjectByType<GameplayEntryPoint>();
            sceneEntryPoint.Run(childContainer);
        }


        private async UniTask LoadAndStartMainMenu()
        {
            await LoadScene(Scenes.BOOTSTRAP);
            await LoadScene(Scenes.LOADING);
            await LoadScene(Scenes.MAIN_MENU);
        }


        private async UniTask LoadScene(string sceneName)
        {
            using (LifetimeScope.EnqueueParent(_rootContainer))
            {
                await SceneManager.LoadSceneAsync(sceneName);
            }
        }
    }
}