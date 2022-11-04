using UnityEngine;

public class GoToMenuEvent : MonoEvent
{
	[Header("References")]
	[SerializeField] private MonoEvent _event;

	#region MonoBehaviour

	private void OnValidate()
	{
		_event ??= GetComponent<MonoEvent>();
	}

	private void OnEnable()
	{
		_event.onMonoCall += Invoke;
	}

	private void OnDisable()
	{
		_event.onMonoCall -= Invoke;
	}

	#endregion
}
