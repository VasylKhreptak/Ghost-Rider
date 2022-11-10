using Zenject;

public class OnGamePlayEvent : MonoEvent
{
	[Inject(Id = MonoEvents.OnGamePlay)] private MonoEvent _event;

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
