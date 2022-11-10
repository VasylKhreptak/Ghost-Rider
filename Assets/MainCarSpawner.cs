using System;
using UnityEngine;
using Zenject;

public class MainCarSpawner : MonoBehaviour
{
	public MainCar currentCar;
	
	public Action<MainCar> onSpawn;
	public Action<MainCar> onDespawn;

	private ObjectPooler _objectPooler;

	[Inject]
	private void Construct(ObjectPooler objectPooler)
	{
		_objectPooler = objectPooler;
	}
}
