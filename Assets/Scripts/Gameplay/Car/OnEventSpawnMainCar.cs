using UnityEngine;

public class OnEventSpawnMainCar : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private MainCarSpawner _mainCarSpawner;
	
	[Header("Preferences")]
	[SerializeField] private Pools _car;
	
	[Header("Events")]
	[SerializeField] private MonoEvent _monoEvent;

	#region MonoBehaviour

	private void Awake()
	{
		_monoEvent.onMonoCall += SpawnMainCar;
	}

	private void OnDestroy()
	{
		_monoEvent.onMonoCall -= SpawnMainCar;
	}

	#endregion

	private void SpawnMainCar()
	{
		_mainCarSpawner.Spawn(_car);
	}
}
