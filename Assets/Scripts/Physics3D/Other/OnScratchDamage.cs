using UnityEngine;

public class OnScratchDamage : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private Rigidbody _rigidbody;
	[SerializeField] private DamageableObject _damageableObject;
	[SerializeField] private ScratchEvents _scratchEvents;

	[Header("Preferences")]
	[SerializeField] private float _maxVelocity;
	[SerializeField] private float _damageDelay;
	[SerializeField] private float _minDamage;
	[SerializeField] private float _maxDamage;
	[SerializeField] private AnimationCurve _damageCurve;
	
	private ConfigurableUpdate _configurableUpdate = new ConfigurableUpdate();
	
	#region MonoBehaviour

	private void OnValidate()
	{
		_rigidbody ??= GetComponent<Rigidbody>(); 
		_damageableObject ??= GetComponent<DamageableObject>();
		_scratchEvents ??= GetComponent<ScratchEvents>();
		
		if (_scratchEvents != null && _maxVelocity < _scratchEvents.MinVelocity)
		{
			_maxVelocity = _scratchEvents.MinVelocity;
		}
	}

	private void Awake()
	{
		_configurableUpdate.Init(this, _damageDelay, DamageRoutine);
	}

	private void OnEnable()
	{
		_scratchEvents.onStartScratching += StartDamaging;
		_scratchEvents.onStopScratching += StopDamaging;
	}

	private void OnDisable()
	{
		_scratchEvents.onStartScratching -= StartDamaging;
		_scratchEvents.onStopScratching -= StopDamaging;
		
		StopDamaging();
	}

	#endregion

	private void StartDamaging(Vector3 position) => StartDamaging();

	private void StartDamaging()
	{
		_configurableUpdate.StartUpdating();
	}
	
	private void StopDamaging()
	{
		_configurableUpdate.StopUpdating();
	}

	private void DamageRoutine()
	{
		_damageableObject.TakeDamage(GetDamage());
	}

	private float GetDamage()
	{
		return _damageCurve.Evaluate(_scratchEvents.MinVelocity, _maxVelocity,
			_rigidbody.velocity.magnitude, _minDamage, _maxDamage);
	}
}
