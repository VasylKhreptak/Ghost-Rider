using UnityEngine;

public class SettingsProvider : DataProvider
{
    public Settings settings = new Settings();

    private string _path;

    #region MonoBehaviuor

    protected override void Awake()
    {
        _path = Application.dataPath + "/" + "settings";

        base.Awake();
    }

    #endregion

    protected override void Save()
    {
#if UNITY_EDITOR
        PlayerPrefsSafeDataProvider.Save("Settings", settings);
#else
        EncryptedFileDataProvider.Save(_path, settings);
#endif
    }

    protected override void Load()
    {
#if UNITY_EDITOR
        settings = PlayerPrefsSafeDataProvider.Load("Settings", new Settings());
#else
        settings = EncryptedFileDataProvider.Load(_path, new Settings());
#endif
    }
}
