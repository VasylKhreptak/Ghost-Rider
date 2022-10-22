using UnityEngine;

public class OnPhysicalHitDamage : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private OnCollisionEnterEvent _collisionEnterEvent;
	[SerializeField] private DamageableObject _damageableObject;

	[Header("Preferences")]
	[SerializeField] private float _minDamage;
	[SerializeField] private float _maxDamage;
	[SerializeField] private float _minImpulse;
	[SerializeField] private float _maxImpulse;
	[SerializeField] private AnimationCurve _damageCurve;
	
	#region MonoBehaviour

	private void OnValidate()
	{
		_collisionEnterEvent ??= GetComponent<OnCollisionEnterEvent>();
		_damageableObject ??= GetComponent<DamageableObject>();
	}

	private void OnEnable()
	{
		_collisionEnterEvent.onEnter += TryTakeDamage;
	}

	private void OnDisable()
	{
		_collisionEnterEvent.onEnter -= TryTakeDamage;
	}
	
	#endregion

	private void TryTakeDamage(Collision collision)
	{
		float impulse = collision.impulse.magnitude;

		if (CanTakeDamage(impulse))
		{
			TakeDamage(GetDamageValue(impulse));
		}
	}

	private bool CanTakeDamage(float impulse)
	{
		return impulse > _minImpulse;
	}

	private void TakeDamage(float damage)
	{
		_damageableObject.TakeDamage(damage);
	}

	private float GetDamageValue(float impulse)
	{
		return _damageCurve.Evaluate(_minImpulse, _maxImpulse, impulse, _minDamage, _maxDamage);
	}
}
