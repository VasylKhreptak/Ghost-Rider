public class OnUpdateEvent : MonoEvent
{
	#region MonoBehaviour

	private void Update()
	{
		Invoke();
	}

	#endregion
}
