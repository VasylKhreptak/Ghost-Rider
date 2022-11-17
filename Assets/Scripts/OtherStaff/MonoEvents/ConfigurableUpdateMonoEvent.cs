using System;
using UnityEngine;

public class ConfigurableUpdateMonoEvent : MonoEvent
{
	[Header("Preferences")]
	[SerializeField] private float _delay;

	private ConfigurableUpdate _configurableUpdate = new ConfigurableUpdate();
	
	#region MonoBehaviour

	private void Awake()
	{
		_configurableUpdate.Init(this, _delay, Invoke);
	}

	private void OnEnable()
	{
		_configurableUpdate.StartUpdating();
	}

	private void OnDisable()
	{
		_configurableUpdate.StopUpdating();
	}

	#endregion
}
