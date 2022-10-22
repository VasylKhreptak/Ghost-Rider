using UnityEngine;

public class OnHealthValueEvent : MonoEvent
{
	[Header("References")]
	[SerializeField] private DamageableObject _damageableObject;

	[Header("Reset event")]
	[SerializeField] private float _healthThreshold;
	[SerializeField] private MonoEvent _resetEvent;

	private bool _wasInvoked;
	
	#region MonoBehaviour

	private void OnValidate()
	{
		_damageableObject ??= GetComponent<DamageableObject>();
		_resetEvent ??= GetComponent<MonoEvent>();
	}
	
	private void Awake()
	{
		_resetEvent.onMonoCall += ResetCount;
		_damageableObject.onSetHealth += TryInvoke;
	}

	private void OnDestroy()
	{
		_resetEvent.onMonoCall -= ResetCount;
		_damageableObject.onSetHealth -= TryInvoke;
	}
	
	#endregion

	private void TryInvoke(float health)
	{
		if (CanInvoke(health))
		{
			Invoke();
		}
	}

	private bool CanInvoke(float health)
	{
		return _wasInvoked == false && health < _healthThreshold;
	}

	public override void Invoke()
	{
		base.Invoke();

		_wasInvoked = true;
	}

	private void ResetCount()
	{
		_wasInvoked = false;
	}
}
