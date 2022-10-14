using UnityEngine;
using Random = UnityEngine.Random;
using System;

public class RoadCarRandomSpeedSetter : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private RCC_AICarController _aiCarController;
	[SerializeField] private RCC_Waypoint _waypoint;
	
	[Header("Preferences")]
	[SerializeField] private float _minSpeed;
	[SerializeField] private float _maxSpeed;
	
	private float _speed => Random.Range(_minSpeed, _maxSpeed);

	public Action<float> onSet;

	#region MonoBehaviour

	protected virtual void OnValidate()
	{
		_aiCarController ??= transform.GetComponentInChildren<RCC_AICarController>();
		_waypoint ??= transform.GetComponentInChildren<RCC_Waypoint>();
	}

	#endregion
	
	public void SetRandomSpeed()
	{
		float speed = _speed;
		
		_waypoint.targetSpeed = speed;
		_aiCarController.maximumSpeed = speed;
		
		onSet?.Invoke(speed);
	}
}
