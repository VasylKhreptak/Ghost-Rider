public class PlayerDataProvider : DataLoader
{
	public PlayerData playerData;

	protected override void Save()
	{
		GameDataProvider.Save(_playerPrefsKey, playerData);
	}

	protected override void Load()
	{
		playerData = GameDataProvider.Load(_playerPrefsKey, new PlayerData());
	}
}
