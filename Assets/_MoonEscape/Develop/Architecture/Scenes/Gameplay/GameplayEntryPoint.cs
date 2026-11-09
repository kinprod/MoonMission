using GameInputActions;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Scenes
{
    public class GameplayEntryPoint : SceneEntryPoint
    {
        [SerializeField]
        private LifetimeScope _gameplayContainer;


        override public void Run()
        {
        }
    }
}