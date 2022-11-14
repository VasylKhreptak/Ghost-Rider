using UnityEngine;

public class MonoEventCountTrigger : MonoEvent
{
	[Header("Events")]
	[SerializeField] private MonoEvent _event;

	[Header("Preferences")]
	[SerializeField] private int _eventCounts;

	[Header("Reset Event")]
	[SerializeField] private MonoEvent _resetCounterEvent;

	private int _counter;

	#region MonoBehaviour

	private void OnValidate()
	{
		_event ??= GetComponent<MonoEvent>();
	}

	private void OnEnable()
	{
		_event.onMonoCall += TryInvoke;
		_resetCounterEvent.onMonoCall += ResetCounter;
	}

	private void OnDisable()
	{
		_event.onMonoCall -= TryInvoke;
		_resetCounterEvent.onMonoCall -= ResetCounter;
	}

	#endregion

	private void TryInvoke()
	{
		if (++_counter == _eventCounts)
		{
			Invoke();
		}
	}

	private void ResetCounter()
	{
		_counter = 0;
	}
}
