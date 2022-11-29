using NaughtyAttributes;
using UnityEngine;

public class SpeedLinesEffect : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private ParticleSystem _particleSystem;
	[SerializeField] private MainCarSpawner _mainCarSpawner;
	[SerializeField] private PauseManager _pauseManager;

	[Header("Preferences")]
	[SerializeField] private bool _syncMaxSpeed;
	[SerializeField] private float _minCarSpeed;
	[HideIf(nameof(_syncMaxSpeed)), SerializeField] private float _maxCarSpeed;
	[SerializeField] private float _minRateOverTime;
	[SerializeField] private float _maxRateOverTime;
	[SerializeField] private AnimationCurve _rateOverTimeCurve;
	[SerializeField] private float _minEmissionRadius;
	[SerializeField] private float _maxEmissionRadius;
	[SerializeField] private AnimationCurve _radiusCurve;
	
	[Header("Events")]
	[SerializeField] private MonoEvent _updateEvent;
	[SerializeField] private MonoEvent _restoreEvent;

	private ParticleSystem.EmissionModule _particleEmissionModule;
	private ParticleSystem.ShapeModule _particleShapeModule;

	private float _previousRateOverTime;
	private float _previousRadius;
	
	#region MonoBehaviour

	private void OnValidate()
	{
		_particleSystem ??= GetComponent<ParticleSystem>();
		_mainCarSpawner ??= FindObjectOfType<MainCarSpawner>();
		_pauseManager ??= FindObjectOfType<PauseManager>();
	}

	private void Awake()
	{
		_particleEmissionModule = _particleSystem.emission;
		_particleShapeModule = _particleSystem.shape;

		_previousRadius = _particleShapeModule.radius;
		_previousRateOverTime = _particleEmissionModule.rateOverTime.constant;
	}

	private void OnEnable()
	{
		_mainCarSpawner.onSpawn += TryUpdateMaxSpeed;
		_updateEvent.onMonoCall += OnUpdate;
		_restoreEvent.onMonoCall += Restore;
	}

	private void OnDisable()
	{
		_mainCarSpawner.onSpawn -= TryUpdateMaxSpeed;
		_updateEvent.onMonoCall -= OnUpdate;
		_restoreEvent.onMonoCall -= Restore;
	}
	
	#endregion

	private void TryUpdateMaxSpeed(MainCar mainCar)
	{
		if (_syncMaxSpeed)
		{
			_maxCarSpeed = mainCar.CarController.maxspeed;
		}
	}

	private void OnUpdate()
	{
		if (HasCar() == false || _pauseManager.isPaused) return;
		
		TryStartSpeedLines();
		
		TryUpdateSpeedLines();
		
		TryStopSpeedLines();
	}

	private bool HasCar()
	{
		return _mainCarSpawner.CurrentCar != null;
	}
	
	private void TryStartSpeedLines()
	{
		if (CanStartSpeedLines())
		{
			StartSpeedLines();
		}
	}

	private bool CanStartSpeedLines()
	{
		return HasAppropriateSpeed() && _particleSystem.isPlaying == false;
	}
	
	private void StartSpeedLines()
	{
		_particleSystem.Play();
	}
	
	private void TryUpdateSpeedLines()
	{
		if (HasAppropriateSpeed())
		{
			UpdateSpeedLines();
		}
	}

	private void UpdateSpeedLines()
	{
		UpdateRadius();
		
		UpdateRateOverTime();
	}
	
	private void TryStopSpeedLines()
	{
		if (CanStopSpeedLines())
		{
			StopSpeedLines();
		}
	}

	private bool CanStopSpeedLines()
	{
		return HasAppropriateSpeed() == false && _particleSystem.isPlaying;
	}
	
	private void StopSpeedLines()
	{
		_particleSystem.Stop();
	}

	private bool HasAppropriateSpeed()
	{
		return _mainCarSpawner.CurrentCar.CarController.speed > _minCarSpeed;
	}

	private void UpdateRadius()
	{
		_particleShapeModule.radius = GetRadius();
	}

	private void UpdateRateOverTime()
	{
		_particleEmissionModule.rateOverTime = GetRateOverTime();
	}

	private float GetRadius()
	{
		return _radiusCurve.Evaluate(_minCarSpeed, _maxCarSpeed, _mainCarSpawner.CurrentCar.CarController.speed,
			_maxEmissionRadius, _minEmissionRadius);
	}

	private float GetRateOverTime()
	{
		return _rateOverTimeCurve.Evaluate(_minCarSpeed, _maxCarSpeed, _mainCarSpawner.CurrentCar.CarController.speed,
			_minRateOverTime, _maxRateOverTime);
	}
	
	private void Restore()
	{
		_particleSystem.Stop();
		_particleEmissionModule.rateOverTime = _previousRateOverTime;
		_particleShapeModule.radius = _previousRadius;
	}
}
