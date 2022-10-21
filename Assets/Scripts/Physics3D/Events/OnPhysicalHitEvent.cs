using UnityEngine;

public class OnPhysicalHitEvent : MonoEvent
{
	[Header("References")]
	[SerializeField] private OnCollisionEnterEvent _collisionEnterEvent;

	[Header("Preferences")]
	[SerializeField] private float _minImpulse;
	
	#region MonoBehaviour

	private void OnValidate()
	{
		_collisionEnterEvent ??= GetComponent<OnCollisionEnterEvent>();
	}

	private void OnEnable()
	{
		_collisionEnterEvent.onEnter += OnEnter;
	}

	private void OnDisable()
	{
		_collisionEnterEvent.onEnter -= OnEnter;
	}

	#endregion

	private void OnEnter(Collision collision)
	{
		Debug.Log("Enter");

		if (collision.impulse.magnitude > _minImpulse)
		{
			Invoke();
		}
	}
}
