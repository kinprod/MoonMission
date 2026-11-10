using UnityEngine;
using VContainer;
using VContainer.Unity;

public class LevelContainer : SceneContainer
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<LevelEntryPoint>(Lifetime.Scoped);

    }
}
