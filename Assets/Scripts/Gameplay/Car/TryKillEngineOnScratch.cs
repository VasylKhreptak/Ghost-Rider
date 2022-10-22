using UnityEngine;

public class TryKillEngineOnScratch : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private RCC_CarControllerV3 _carController;
	[SerializeField] private DamageableObject _damageableObject;
	[SerializeField] private ScratchEvents _scratchEvents;

	[Header("Preferences")]
	[SerializeField, Range(0f, 1f)] private float _minEngineKillProbability;
	[SerializeField, Range(0f, 1f)] private float _maxEnginKillProbability;
	[SerializeField] private AnimationCurve _engineKillProbabilityCurve;
	[SerializeField, Min(0f)] private float _minHealth;
	[SerializeField] private float _maxHealth;
	
	public ManualMonoEvent onEngineKill;

	#region MonoBehaviour

	private void OnValidate()
	{
		_scratchEvents ??= GetComponent<ScratchEvents>();
		_carController ??= GetComponent<RCC_CarControllerV3>();
		_damageableObject ??= GetComponent<DamageableObject>();
		
		if (_damageableObject != null && _maxHealth > _damageableObject.GetMaxHealth())
		{
			_maxHealth = _damageableObject.GetMaxHealth();
		}
	}

	private void OnEnable()
	{
		_scratchEvents.onScratchUpdate += TryKillEngine;
	}

	private void OnDisable()
	{
		_scratchEvents.onScratchUpdate -= TryKillEngine;
	}

	#endregion

	private void TryKillEngine(Vector3 position) => TryKillEngine();
	
	private void TryKillEngine()
	{
		if (CanKillEngine(_damageableObject.GetHealth()))
		{
			KillEngine();
		}
	}

	private void KillEngine()
	{
		_carController.KillEngine();

		onEngineKill?.Invoke();
	}
	
	private bool CanKillEngine(float health)
	{
		return _carController.engineRunning && health < _maxHealth 
			&& Extensions.Mathf.Probability(GetEngineKillProbability(health));
	}
	
	private float GetEngineKillProbability(float health)
	{
		Debug.Log("Probability: " + (_engineKillProbabilityCurve.Evaluate(_minHealth, _maxHealth, health, 
			_maxEnginKillProbability, _minEngineKillProbability)));
		
		return _engineKillProbabilityCurve.Evaluate(_minHealth, _maxHealth, health, 
			_maxEnginKillProbability, _minEngineKillProbability);
	}
}
