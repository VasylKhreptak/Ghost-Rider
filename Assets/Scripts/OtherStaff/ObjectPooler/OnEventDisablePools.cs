using UnityEngine;
using Zenject;

public class OnEventDisablePools : MonoBehaviour
{
	[Header("Preferences")]
	[SerializeField] private Pools[] _pools;

	[Header("MonoEvent")]
	[SerializeField] private MonoEvent _event;

	private ObjectPooler _objectPooler;

	[Inject]
	private void Construct(ObjectPooler objectPooler)
	{
		_objectPooler = objectPooler;
	}
	
	#region MonoBehaviour

	private void OnValidate()
	{
		_event ??= GetComponent<MonoEvent>();
	}

	private void OnEnable()
	{
		_event.onMonoCall += DisablePools;
	}

	private void OnDisable()
	{
		_event.onMonoCall -= DisablePools;
	}

	#endregion

	private void DisablePools()
	{
		foreach (var pool in _pools)
		{
			_objectPooler.DisablePool(pool);
		}
	}
}
