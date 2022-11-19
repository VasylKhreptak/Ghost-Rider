using UnityEngine;

public class OnToggleDisableEvent : MonoEvent
{
	[Header("References")]
	[SerializeField] private ToggleMonoEvent _toggleMonoEvent;

	#region MonoBehaviour

	private void OnValidate()
	{
		_toggleMonoEvent ??= GetComponent<ToggleMonoEvent>();
	}

	private void OnEnable()
	{
		_toggleMonoEvent.onDisable += Invoke;
	}

	private void OnDisable()
	{
		_toggleMonoEvent.onDisable -= Invoke;
	}

	#endregion
}
