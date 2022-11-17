using UnityEngine;

public class OnStopRenderingEvent : MonoEvent
{
	[Header("References")]
	[SerializeField] private DistanceRenderingEvents _distanceRenderingEvents;

	#region MonoBehaviour

	private void OnValidate()
	{
		_distanceRenderingEvents ??= GetComponent<DistanceRenderingEvents>();
	}

	private void Awake()
	{
		_distanceRenderingEvents.onStopRendering += Invoke;
	}

	private void OnDestroy()
	{
		_distanceRenderingEvents.onStopRendering -= Invoke;
	}

	#endregion
}
