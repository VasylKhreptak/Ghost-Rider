using Zenject;

public class OnNetworkDisconnectedEvent : MonoEvent
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
		_networkConnectionEvents.onDisconnected += Invoke;
	}

	private void OnDisable()
	{
		_networkConnectionEvents.onDisconnected -= Invoke;
	}

	#endregion
}
