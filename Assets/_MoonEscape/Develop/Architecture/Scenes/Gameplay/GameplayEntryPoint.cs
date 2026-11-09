using GameInputActions;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Scenes
{
    public class GameplayEntryPoint : SceneEntryPoint
    {

        override public void Run()
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

            LoadScene(gameSettings.SavedScene);
        }
    }
}