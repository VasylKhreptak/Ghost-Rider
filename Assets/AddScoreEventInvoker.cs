using System;
using UnityEngine;
using Zenject;

public class AddScoreEventInvoker : MonoBehaviour
{
	[Header("Events")]
	[SerializeField] private MonoEvent _monoEvent;
	
	[Inject(Id = MonoEvents.AddScore)] private MonoEvent _addScoreEvent;

	#region MonoBehaviour

	private void OnValidate()
	{
		_monoEvent ??= GetComponent<MonoEvent>();
	}

	private void OnEnable()
	{
		_monoEvent.onMonoCall += _addScoreEvent.Invoke;
	}

	private void OnDisable()
	{
		_monoEvent.onMonoCall -= _addScoreEvent.Invoke;
	}

	#endregion
}
