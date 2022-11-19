using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class DistortionHighSpeedEffect : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Volume _volume;
    [SerializeField] private MainCarSpawner _mainCarSpawner;
    [SerializeField] private PauseManager _pauseManager;

    [Header("Preferences")]
    [SerializeField] private bool _syncMaxCarSpeed;
    [SerializeField, Range(-1f, 1f)] private float _minIntensity;
    [SerializeField, Range(-1f, 1f)] private float _maxIntensity;
    [SerializeField] private float _minCarSpeed;
    [HideIf(nameof(_syncMaxCarSpeed)), SerializeField] private float _maxCarSpeed;
    [SerializeField] private AnimationCurve _intensityCurve;

    [Header("Events")]
    [SerializeField] private MonoEvent _updateEvent;
    [SerializeField] private MonoEvent _restoreEvent;
    
    private LensDistortion _lensDistortion;

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
        _volume.profile.TryGet(out _lensDistortion);

        _previousIntensity = _lensDistortion.intensity.value;
    }

    private void OnEnable()
    {
        _mainCarSpawner.onSpawn += TryUpdateMaxSpeed;
        _updateEvent.onMonoCall += TryUpdateDistortion;
        _restoreEvent.onMonoCall += Restore;
    }

    private void OnDisable()
    {
        _mainCarSpawner.onSpawn -= TryUpdateMaxSpeed;
        _updateEvent.onMonoCall -= TryUpdateDistortion;
        _restoreEvent.onMonoCall -= Restore;
    }
    
    #endregion

    private void TryUpdateDistortion()
    {
        if (CanUpdateDistortion())
        {
            UpdateDistortionStrength();
        }
    }
    
    private void TryUpdateMaxSpeed(MainCar mainCar)
    {
        if (_syncMaxCarSpeed)
        {
            _maxCarSpeed = mainCar.CarController.maxspeed;
        }
    }

    private bool CanUpdateDistortion()
    {
        return _mainCarSpawner.CurrentCar != null && _pauseManager.isPaused == false;
    }

    private void UpdateDistortionStrength()
    {
        _lensDistortion.intensity.value = GetIntensity(_mainCarSpawner.CurrentCar.CarController.speed);
    }

    private float GetIntensity(float carSpeed)
    {
        return _intensityCurve.Evaluate(_minCarSpeed, _maxCarSpeed, carSpeed, _minIntensity, _maxIntensity);
    }

    private void Restore()
    {
        _lensDistortion.intensity.value = _previousIntensity;
    }
}
