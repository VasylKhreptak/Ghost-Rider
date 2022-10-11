using UnityEngine;

public class PlayerDataProvider : DataProvider
{
    public PlayerData playerData = new PlayerData();

    private string _path;

    #region MonoBehaviuor

    protected override void Awake()
    {
        _path = Application.dataPath + "/" + "playerData";

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
        PlayerPrefsSafeDataProvider.Save(_path, playerData);
    }

    protected override void Load()
    {
        playerData = PlayerPrefsSafeDataProvider.Load(_path, new PlayerData());
    }
}
