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

    protected override void Save()
    {
#if UNITY_EDITOR
        PlayerPrefsSafeDataProvider.Save("PlayerData", playerData);
#else
        EncryptedFileDataProvider.Save(_path, playerData);
#endif
    }

    protected override void Load()
    {
#if UNITY_EDITOR
        playerData = PlayerPrefsSafeDataProvider.Load("PlayerData", new PlayerData());
#else
        playerData = EncryptedFileDataProvider.Load(_path, new PlayerData());
#endif
    }
}
