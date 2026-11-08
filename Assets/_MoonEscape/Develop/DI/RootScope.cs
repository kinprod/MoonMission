using GameInputActions;
using UnityEngine;
using VContainer;
using VContainer.Unity;


namespace DI
{
    public class RootScope : LifetimeScope
    {
        public bool IsDebug = true;

        protected override void Configure(IContainerBuilder builder)
        {
            var inputManagerObject = new GameObject("INPUT_MANAGER");
            DontDestroyOnLoad(inputManagerObject);

            var inputManager = inputManagerObject.AddComponent<InputManager>();
            
            builder.RegisterComponent<InputManager>(inputManager);
        }
    }
}
