using System;
using Unity.VisualScripting;
using UnityEngine;

public class RepeatingMonoEvent : MonoEvent
{
	[Header("Events")]
	[SerializeField] private MonoEvent _monoEvent;

	[Header("Preferences")]
	[SerializeField] private float _delay;
	[SerializeField] private int _repeatCount;

	private ConfigurableUpdate _configurableUpdate = new ConfigurableUpdate();

	private int _invocationCount;
	
	#region MonoBehaviour

	private void OnValidate()
	{
		_monoEvent ??= GetComponent<MonoEvent>();
	}

	private void Awake()
	{
		_configurableUpdate.Init(this, _delay, Invoke);
	}

	private void OnEnable()
	{
		_monoEvent.onMonoCall += StartRepeating;
	}

	private void OnDisable()
	{
		_monoEvent.onMonoCall -= StartRepeating;
		
		StopRepeating();
	}

	#endregion

	private void StartRepeating()
	{
		_invocationCount = 0;
		
		_configurableUpdate.StartUpdating();
	}

	public void StopRepeating()
	{
		_invocationCount = 0;

		_configurableUpdate.StopUpdating();
	}

	public override void Invoke()
	{
		if (_invocationCount == _repeatCount - 1)
		{
			StopRepeating();
		}
		
		base.Invoke();

		_invocationCount++;
	}
}
