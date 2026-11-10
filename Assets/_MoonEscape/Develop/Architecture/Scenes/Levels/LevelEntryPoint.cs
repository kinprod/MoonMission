using Cysharp.Threading.Tasks;
using Player;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;

public class LevelEntryPoint : SceneEntryPoint
{
    [SerializeField]
    private Player.Player _player;

    private void OnEnable()
    {
        _player.PlayerDeath += ReloadScene;
    }

    private void OnDisable()
    {
        _player.PlayerDeath -= ReloadScene;
    }

    private async void ReloadScene()
    {
        await UniTask.Delay(1000);
        LoadNextScene(SceneManager.GetActiveScene().name);
    }

}
