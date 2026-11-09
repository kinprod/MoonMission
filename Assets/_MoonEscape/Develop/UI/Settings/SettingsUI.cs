using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class SettingsUI : MonoBehaviour
{
    private GameSettings _gameSettings;

    [SerializeField]
    private UnityEngine.UI.Slider _musicVolume;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("Settings"))
        {
            _gameSettings = new GameSettings();
            _gameSettings.MusicValue = 1f;

        }
        else
        {
            string json = PlayerPrefs.GetString("Settings");
            _gameSettings = JsonUtility.FromJson<GameSettings>(json);
        }
        _musicVolume.value = _gameSettings.MusicValue;
    }


    private void OnEnable()
    {
        _musicVolume.onValueChanged.AddListener(MusicVolumeUpdate);
    }

    private void OnDisable()
    {
        _musicVolume.onValueChanged.RemoveListener(MusicVolumeUpdate);
    }

    private void MusicVolumeUpdate(float value)
    {
        AudioListener.volume = _musicVolume.value;
        Debug.Log(value);
    }

    public void ApplySettings()
    {
        _gameSettings.MusicValue = _musicVolume.value;
        SaveSettings();
        gameObject.SetActive(false);
    }

    private void SaveSettings()
    {
        var settingsJson = JsonUtility.ToJson(_gameSettings);
        Debug.Log(settingsJson);
        PlayerPrefs.SetString("Settings", settingsJson);
        PlayerPrefs.Save();
    }
}
