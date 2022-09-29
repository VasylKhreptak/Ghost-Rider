using UnityEngine;

public class SettingsProvider : DataLoader
{
    public Settings settings;
    
    protected override void Save()
    {
        GameDataProvider.Save(_playerPrefsKey, settings);
    }

    protected override void Load()
    {
        settings = GameDataProvider.Load(_playerPrefsKey, new Settings());
    }
}
