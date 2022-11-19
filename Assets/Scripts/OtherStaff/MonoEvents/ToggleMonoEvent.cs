using System;
using UnityEngine;

public class ToggleMonoEvent : MonoBehaviour
{
	[Header("Events")]
	[SerializeField] private MonoEvent _triggerEvent;

	[Header("Preferences")]
	[SerializeField] private bool _startState;
	
	public Action<bool> onToggle;
	public Action onEnable;
	public Action onDisable;

	private bool _state;
	
	#region MonoBehaviour

	private void OnValidate()
	{
		_triggerEvent ??= GetComponent<MonoEvent>();
	}

	private void Awake()
	{
		_state = _startState;
	}

	private void OnEnable()
	{
		_triggerEvent.onMonoCall += TriggerEvent;
	}

	private void OnDisable()
	{
		_triggerEvent.onMonoCall -= TriggerEvent;
	}

	#endregion

	private void TriggerEvent()
	{
		_state = !_state;
		
		onToggle?.Invoke(_state);
		
		(_state ? onEnable : onDisable)?.Invoke();
	}
}
