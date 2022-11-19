using System;
using UnityEngine;

public class OnToggleEnableEvent : MonoEvent
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
		_toggleMonoEvent.onEnable += Invoke;
	}

	private void OnDisable()
	{
		_toggleMonoEvent.onEnable -= Invoke;
	}

	#endregion
}
