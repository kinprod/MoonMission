using GameInputActions;
using Unity.VisualScripting;
using UnityEngine;
using VContainer;
using VContainer.Unity;


namespace DI
{
    public class RootScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            var inputManagerObject = new GameObject("INPUT_MANAGER");
            DontDestroyOnLoad(inputManagerObject);

            var inputManager = inputManagerObject.AddComponent<InputManager>();

            builder.RegisterComponent<InputManager>(inputManager);
            
            var sceneDoorsObject = Instantiate(Resources.Load("UI/SceneDoors"));
            sceneDoorsObject.name = "SCENE_DOORS";
            DontDestroyOnLoad(sceneDoorsObject);

            builder.RegisterComponent<SceneDoors>(sceneDoorsObject.GetComponent<SceneDoors>());
            
        }
    }
}
