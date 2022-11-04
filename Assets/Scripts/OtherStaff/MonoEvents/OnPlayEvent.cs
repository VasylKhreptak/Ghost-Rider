using UnityEngine;

public class OnPlayEvent : MonoEvent
{
	[Header("References")]
	[SerializeField] private MonoEvent _playEvent;

	#region MonoBehaviour

	private void OnValidate()
	{
		_playEvent ??= GetComponent<MonoEvent>();
	}

	private void OnEnable()
	{
		_playEvent.onMonoCall += Invoke;
	}

	private void OnDisable()
	{
		_playEvent.onMonoCall -= Invoke;
	}

	#endregion
}
