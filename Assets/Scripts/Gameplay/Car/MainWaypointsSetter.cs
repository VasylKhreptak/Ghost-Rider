using UnityEngine;
using Zenject;

public class MainWaypointsSetter : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private RCC_AICarController _aiCarController;

	private RCC_AIWaypointsContainer _waypointsContainer;
	
	[Inject]
	private void Construct(RCC_AIWaypointsContainer waypointsContainer)
	{
		_waypointsContainer = waypointsContainer;
	}
	
	#region MonoBehaviour

	private void OnValidate()
	{
		_aiCarController ??= GetComponent<RCC_AICarController>();
	}

	private void Awake()
	{
		_aiCarController.waypointsContainer = _waypointsContainer;
	}

	#endregion
}
