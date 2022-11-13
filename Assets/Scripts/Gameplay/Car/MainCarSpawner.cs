using System;
using UnityEngine;
using Zenject;

public class MainCarSpawner : MonoBehaviour
{
	[Header("Preferences")]
	[SerializeField] private Transform _spawnTransform;

	public Action<MainCar> onSpawn;
	public Action<MainCar> onDespawn;
	private ObjectPooler _objectPooler;

	private MainCar _currentCar;

	public MainCar CurrentCar => _currentCar;

	[Inject]
	private void Construct(ObjectPooler objectPooler)
	{
		_objectPooler = objectPooler;
	}

	public MainCar Spawn(Pools carPool)
	{
		if (_currentCar != null && _currentCar.IsActive())
		{
			_currentCar.Disable();
			
			onDespawn?.Invoke(_currentCar);
		}

		GameObject carObj = _objectPooler.Spawn(carPool, _spawnTransform.position, _spawnTransform.rotation);

		MainCar car = carObj.GetComponent<MainCar>();
		
		_currentCar = car;

		onSpawn?.Invoke(car);
		
		return car;
	}
}
