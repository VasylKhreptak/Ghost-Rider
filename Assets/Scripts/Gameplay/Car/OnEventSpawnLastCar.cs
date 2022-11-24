using UnityEngine;

public class OnEventSpawnLastCar : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private MainCarSpawner _mainCarSpawner;
	[SerializeField] private PlayerDataProvider _playerDataProvider;
	
	[Header("Events")]
	[SerializeField] private MonoEvent _event;

	#region MonoBehaviour

	private void OnValidate()
	{
		_mainCarSpawner ??= GetComponent<MainCarSpawner>();
		_playerDataProvider ??= FindObjectOfType<PlayerDataProvider>();
		_event ??= GetComponent<MonoEvent>();
	}

	private void OnEnable()
	{
		_event.onMonoCall += SpawnCar;
	}

	private void OnDisable()
	{
		_event.onMonoCall -= SpawnCar;
	}

	#endregion

	private void SpawnCar()
	{
		_mainCarSpawner.Spawn(_playerDataProvider.playerData.lastCar);
	}
}
