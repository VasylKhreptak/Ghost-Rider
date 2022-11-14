using UnityEngine;

public class OnMainCarSpawnedMonoEvent : MonoEvent
{
	[Header("References")]
	[SerializeField] private MainCarSpawner _carSpawner;

	#region MonoBehaviour

	private void OnValidate()
	{
		_carSpawner ??= GetComponent<MainCarSpawner>();
	}

	private void OnEnable()
	{
		_carSpawner.onSpawn += Invoke;
	}

	private void OnDisable()
	{
		_carSpawner.onSpawn -= Invoke;
	}

	#endregion

	private void Invoke(MainCar mainCar) => Invoke();
}
