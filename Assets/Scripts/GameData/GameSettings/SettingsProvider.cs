using System;
using UnityEngine;

public class SettingsProvider : MonoBehaviour
{
    [Header("Preferences")]
    [SerializeField] private string _playerPrefsKey;

    public Settings settings;

    #region MonoBehaviour

    private void Awake()
    {
        Load();
    }

    private void OnApplicationQuit()
    {
        Save();
    }


    private void OnApplicationPause(bool pauseStatus)
    {
        Save();
    }

    #endregion

    private void Save()
    {
        GameDataProvider.Save(_playerPrefsKey, settings);
    }

    private void Load()
    {
        settings = GameDataProvider.Load(_playerPrefsKey, new Settings());
    }
}
