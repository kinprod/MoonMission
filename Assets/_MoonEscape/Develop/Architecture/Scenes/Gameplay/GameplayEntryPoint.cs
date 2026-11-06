using GameInputActions;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Scenes
{
    public class GameplayEntryPoint : MonoBehaviour
    {
        [SerializeField]
        private LifetimeScope _gameplayContainer;


        public void Run()
        {
            _gameplayContainer.Container.Resolve<InputManager>().ShotPerformedAction += () => {
                Debug.Log("Init");
            };
        }
    }
}