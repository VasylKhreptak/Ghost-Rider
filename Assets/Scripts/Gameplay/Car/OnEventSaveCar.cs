using UnityEngine;

public class OnEventSaveCar : MonoBehaviour
{
	[Header("References")]
	[SerializeField] protected MainCarSpawner _mainCarSpawner;
	[SerializeField] protected PlayerDataProvider _playerDataProvider;

	[Header("Events")]
	[SerializeField] private MonoEvent _saveCarEvent;
	
	#region MonoBehaviour

	private void OnValidate()
	{
		_mainCarSpawner ??= FindObjectOfType<MainCarSpawner>();
		_playerDataProvider ??= FindObjectOfType<PlayerDataProvider>();
	}

	private void OnEnable()
	{
		_saveCarEvent.onMonoCall += SaveCar;
	}

	private void OnDisable()
	{
		_saveCarEvent.onMonoCall -= SaveCar;
	}

	#endregion

	protected virtual void SaveCar()
	{
		_playerDataProvider.playerData.lastCar = _mainCarSpawner.CurrentCar.Pool;
	}
}
