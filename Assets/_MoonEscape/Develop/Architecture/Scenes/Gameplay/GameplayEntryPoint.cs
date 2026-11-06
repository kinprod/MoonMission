using GameInputActions;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Scenes
{
    public class GameplayEntryPoint : MonoBehaviour
    {
        private LifetimeScope _gameplayContainer;

        public void Run(LifetimeScope lifetimeScope)
        {
            _gameplayContainer = lifetimeScope;
            _gameplayContainer.Container.Resolve<InputManager>().ShotPerformedAction += () =>
            {
                Debug.Log("Shot in container");
            };

            Debug.Log("Run entry point");
        }
    }
}