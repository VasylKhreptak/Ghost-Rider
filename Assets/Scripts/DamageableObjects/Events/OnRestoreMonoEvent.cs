using UnityEngine;

public class OnRestoreMonoEvent : MonoEvent
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
		_damageableObject.onRestore += Invoke;
	}


	private void OnDisable()
	{
		_damageableObject.onRestore -= Invoke;
	}

	#endregion
}
