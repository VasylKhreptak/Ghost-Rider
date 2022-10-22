using UnityEngine;

public class TryKillEngineOnCollision : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private RCC_CarControllerV3 _carController;
	[SerializeField] private DamageableObject _damageableObject;
	[SerializeField] private OnCollisionEnterEvent _collisionEnterEvent;

	[Header("OnCollision Preferences")]
	[SerializeField, Range(0f, 1f)] private float _minEngineKillProbability;
	[SerializeField, Range(0f, 1f)] private float _maxEngineKillProbability;
	[SerializeField] private AnimationCurve _engineKillProbabilityCurve;
	[SerializeField, Min(0f)] private float _minHealth;
	[SerializeField] private float _maxHealth;
	
	public ManualMonoEvent onEngineKill;
	
	#region MonoBehaviour

	private void OnValidate()
	{
		_collisionEnterEvent ??= GetComponent<OnCollisionEnterEvent>();
		_carController ??= GetComponent<RCC_CarControllerV3>();
		_damageableObject ??= GetComponent<DamageableObject>();
		
		if (_damageableObject != null && _maxHealth > _damageableObject.GetMaxHealth())
		{
			_maxHealth = _damageableObject.GetMaxHealth();
		}
	}

	private void OnEnable()
	{
		_collisionEnterEvent.onEnter += TryKillEngine;
	}

	private void OnDisable()
	{
		_collisionEnterEvent.onEnter -= TryKillEngine;
	}

	#endregion

	private void TryKillEngine(Collision collision) => TryKillEngine();
	
	private void TryKillEngine()
	{
		if (CanKillEngine(_damageableObject.GetHealth()))
		{
			KillEngine();
		}
	}

	private bool CanKillEngine(float health)
	{
		return _carController.engineRunning && health < _maxHealth 
			&& Extensions.Mathf.Probability(GetEngineKillProbability(health));
	}

	private void KillEngine()
	{
		_carController.KillEngine();

		onEngineKill?.Invoke();
	}

	private float GetEngineKillProbability(float health)
	{
		Debug.Log("Probability: " + (_engineKillProbabilityCurve.Evaluate(_minHealth, _maxHealth, health, 
			_maxEngineKillProbability, _minEngineKillProbability)));
		
		return _engineKillProbabilityCurve.Evaluate(_minHealth, _maxHealth, health, 
			_maxEngineKillProbability, _minEngineKillProbability);
	}
}
