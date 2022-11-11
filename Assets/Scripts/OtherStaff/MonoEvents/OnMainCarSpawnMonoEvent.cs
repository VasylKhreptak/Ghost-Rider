using UnityEngine;

public class OnMainCarSpawnMonoEvent : MonoEvent
{
	[Header("References")]
	[SerializeField] private MainCarSpawner _mainCarSpawner;

	#region MonoBehaviour

	private void OnEnable()
	{
		_mainCarSpawner.onSpawn += Invoke;
	}

	private void OnDisable()
	{
		_mainCarSpawner.onSpawn -= Invoke;
	}

	#endregion

	private void Invoke(MainCar mainCar) => Invoke();
}
