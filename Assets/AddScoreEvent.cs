using Zenject;

public class AddScoreEvent : MonoEvent
{
	[Inject(Id = MonoEvents.AddScore)] private MonoEvent _event;

	#region MonoBehaviour

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
