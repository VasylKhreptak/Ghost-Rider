using UnityEngine;

public class OnEventLogMessage : MonoBehaviour
{
	[Header("Events")]
	[SerializeField] private MonoEvent _monoEvent;

	[Header("Preferences")]
	[SerializeField] private string _message;

	#region MonoBehaviour

	private void OnValidate()
	{
		_monoEvent ??= GetComponent<MonoEvent>();
	}

	private void OnEnable()
	{
		_monoEvent.onMonoCall += LogMessage;
	}

	private void OnDisable()
	{
		_monoEvent.onMonoCall -= LogMessage;
	}

	#endregion

	private void LogMessage()
	{
		Debug.Log(_message);
	}
}
