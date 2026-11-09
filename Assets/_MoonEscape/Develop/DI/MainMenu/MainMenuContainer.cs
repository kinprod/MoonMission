using UnityEngine;
using VContainer;
using VContainer.Unity;

public class MainMenuContainer : SceneContainer
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<MainMenuEntryPoint>(Lifetime.Scoped);
    }
}
