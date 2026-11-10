using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class FinishDoor : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private int _nextLevel = 0;
    [SerializeField]
    private LevelEntryPoint _levelEntryPoint;
    [SerializeField]
    private WinUI _winUI;
    [SerializeField]
    private GameObject _winUIObject;

    

    private Dictionary<int, string> _levels = new()
    {
        { 1, Scenes.Scenes.LEVEL_1},
        { 2, Scenes.Scenes.LEVEL_2},
        { 3, Scenes.Scenes.LEVEL_3},
        { 4, Scenes.Scenes.LEVEL_4},
        { 5, Scenes.Scenes.LEVEL_5},
        { 6, Scenes.Scenes.LEVEL_6},
        { 7, Scenes.Scenes.LEVEL_7},
        { 8, Scenes.Scenes.LEVEL_8},
        { 9, Scenes.Scenes.LEVEL_9},
        { 10, Scenes.Scenes.LEVEL_10},
        { 11, Scenes.Scenes.MAIN_MENU}
    };

    private void OnEnable()
    {
        _winUI.NextSceneLoad += LoadNext;
    }

    private void OnDisable()
    {
        _winUI.NextSceneLoad -= LoadNext;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            _winUIObject.SetActive(true);
        }
    }

    private void LoadNext()
    {
        _animator.SetBool("Open", true);
        _levelEntryPoint.LoadNextScene(_levels.GetValueOrDefault(_nextLevel));

        GameSettings gameSettings;

        if (!PlayerPrefs.HasKey("Settings"))
        {
            gameSettings = new GameSettings();
            gameSettings.SavedScene = _levels.GetValueOrDefault(_nextLevel == 11 ? 1 : _nextLevel);
        }
        else
        {
            string json = PlayerPrefs.GetString("Settings");
            gameSettings = JsonUtility.FromJson<GameSettings>(json);
            gameSettings.SavedScene = _levels.GetValueOrDefault(_nextLevel == 11 ? 1 : _nextLevel);
        }

        var settingsJson = JsonUtility.ToJson(gameSettings);
        PlayerPrefs.SetString("Settings", settingsJson);
        PlayerPrefs.Save();
    }
}
