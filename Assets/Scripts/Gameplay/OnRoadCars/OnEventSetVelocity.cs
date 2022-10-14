using UnityEngine;

public class OnEventSetVelocity : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private Rigidbody _rigidbody;

	[Header("MonoEvent")]
	[SerializeField] private MonoEvent _event;
	
	[Header("Preferences")]
	[SerializeField] private Vector3 _targetVelocity;

	#region MonoBehaviour

	private void OnValidate()
	{
		_rigidbody ??= GetComponent<Rigidbody>();
		_event ??= GetComponent<MonoEvent>();
	}

	private void OnEnable()
	{
		_event.onMonoCall += SetVelocity;
	}

	private void OnDisable()
	{
		_event.onMonoCall -= SetVelocity;
	}

	#endregion

	private void SetVelocity()
	{
		_rigidbody.velocity = _targetVelocity;
	}
}
