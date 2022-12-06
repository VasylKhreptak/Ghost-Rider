using Zenject;

public class OnNetworkConnectedEvent : MonoEvent
{
	private NetworkConnectionEvents _networkConnectionEvents;
	
	[Inject]
	private void Construct(NetworkConnectionEvents networkConnectionEvents)
	{
		_networkConnectionEvents = networkConnectionEvents;
	}
	
	#region MonoBehaviour

	private void OnEnable()
	{
		_networkConnectionEvents.onConnected += Invoke;
	}

	private void OnDisable()
	{
		_networkConnectionEvents.onConnected -= Invoke;
	}

	#endregion
}
