using UnityEngine;
using Random = UnityEngine.Random;

public class RoadCarRandomSpeedSetter : RoadCarSpeedSetter
{
	[Header("Preferences")]
	[SerializeField] private float _minSpeed;
	[SerializeField] private float _maxSpeed;

	private float _speed => Random.Range(_minSpeed, _maxSpeed);

	public void SetRandomSpeed()
	{
		SetSpeed(_speed);
	}
}
