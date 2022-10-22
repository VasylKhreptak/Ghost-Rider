using UnityEngine;

public class OnDeathMonoEvent : MonoEvent
{
	[Header("References")]
	[SerializeField] private DamageableObject _damageableObject;

	#region MonoBehaviour

	private void OnValidate()
	{
		_damageableObject ??= GetComponent<DamageableObject>();
	}

	private void OnEnable()
	{
		_damageableObject.onDeath += Invoke;
	}

	private void OnDisable()
	{
		_damageableObject.onDeath -= Invoke;
	}

	#endregion
}
