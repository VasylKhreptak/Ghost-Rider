using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ChromaticAberrationHighSpeedEffect : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private Volume _volume;
	[SerializeField] private MainCarSpawner _mainCarSpawner;
	[SerializeField] private PauseManager _pauseManager;

	[Header("Preferences")]
	[SerializeField] private bool _syncMaxCarSpeed;
	[SerializeField, Range(0f, 1f)] float _minIntensity;
	[SerializeField, Range(0f, 1f)] float _maxIntensity;
	[SerializeField] private float _minCarSpeed;
	[HideIf(nameof(_syncMaxCarSpeed)), SerializeField] private float _maxCarSpeed;
	[SerializeField] private AnimationCurve _intensityCurve;

	[Header("Events")]
	[SerializeField] private MonoEvent _updateEvent;
	[SerializeField] private MonoEvent _restoreEvent;

	private ChromaticAberration _chromaticAberration;

	private float _previousIntensity;
	
	#region MonoBehaviour

	private void OnValidate()
	{
		_mainCarSpawner ??= FindObjectOfType<MainCarSpawner>();
		_volume ??= GetComponent<Volume>();
		_pauseManager ??= FindObjectOfType<PauseManager>();
	}

	private void Awake()
	{
		_volume.profile.TryGet(out _chromaticAberration);

		_previousIntensity = _chromaticAberration.intensity.value;
	}

	private void OnEnable()
	{
		_mainCarSpawner.onSpawn += TryUpdateMaxSpeed;
		_updateEvent.onMonoCall += TryUpdateAberration;
		_restoreEvent.onMonoCall += Restore;
	}

	private void OnDisable()
	{
		_mainCarSpawner.onSpawn -= TryUpdateMaxSpeed;
		_updateEvent.onMonoCall -= TryUpdateAberration;
		_restoreEvent.onMonoCall -= Restore;
	}

	#endregion

	private void TryUpdateMaxSpeed(MainCar mainCar)
	{
		if (_syncMaxCarSpeed)
		{
			_maxCarSpeed = mainCar.CarController.maxspeed;
		}
	}
	
	private void TryUpdateAberration()
	{
		if (CanUpdateAberration())
		{
			UpdateAberrationStrength();
		}
	}
	
	private bool CanUpdateAberration()
	{
		return _mainCarSpawner.CurrentCar != null && _pauseManager.isPaused == false;
	}
	
	private void UpdateAberrationStrength()
	{
		_chromaticAberration.intensity.value = GetIntensity(_mainCarSpawner.CurrentCar.CarController.speed);
	}

	private float GetIntensity(float carSpeed)
	{
		return _intensityCurve.Evaluate(_minCarSpeed, _maxCarSpeed, carSpeed, _minIntensity, _maxIntensity);
	}

	private void Restore()
	{
		_chromaticAberration.intensity.value = _previousIntensity;
	}
}
