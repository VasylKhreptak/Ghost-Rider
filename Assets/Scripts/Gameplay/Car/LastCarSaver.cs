using UnityEngine;

public class LastCarSaver : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private MainCarSpawner _mainCarSpawner;
	[SerializeField] private PlayerDataProvider _playerDataProvider;
	
	#region MonoBehaviour

	private void OnValidate()
	{
		_mainCarSpawner ??= GetComponent<MainCarSpawner>();
		_playerDataProvider ??= FindObjectOfType<PlayerDataProvider>();
	}

	private void OnEnable()
	{
		_mainCarSpawner.onSpawn += SaveLastCar;
	}

	private void OnDisable()
	{
		_mainCarSpawner.onSpawn -= SaveLastCar;
	}

	#endregion

	private void SaveLastCar(MainCar lastCar)
	{
		_playerDataProvider.playerData.lastCar = lastCar.Pool;
	}
}
