using GameInputActions;
using UnityEngine;
using VContainer;
using VContainer.Unity;


namespace DI
{
    public class GameScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponentOnNewGameObject<InputManager>(
                Lifetime.Singleton,
                "InputManager"
                );
            Debug.Log("GameScope configure");
        }
    }
}
