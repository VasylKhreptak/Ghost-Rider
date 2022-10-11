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

    protected override void OnApplicationPause(bool pauseStatus)
    {
#if !UNITY_EDITOR
        base.OnApplicationPause(pauseStatus);
#endif
    }

    protected override void OnApplicationQuit()
    {
#if !UNITY_EDITOR
        base.OnApplicationQuit();
#endif
    }


    protected override void Save()
    {
        EncryptedFileDataProvider.Save(_path, settings);
    }

    protected override void Load()
    {
        settings = EncryptedFileDataProvider.Load(_path, new Settings());
    }
}
