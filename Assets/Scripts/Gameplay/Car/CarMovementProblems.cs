using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using VLB;
using Zenject;

public class CarMovementProblems : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RCC_CarControllerV3 _carController;
    [SerializeField] private DamageableObject _damageableObject;

    [Header("General Preferences")]
    [SerializeField] private float _minHealth;
    [SerializeField] private float _maxHealth;

    [Header("Problem delay")]
    [SerializeField] private float _minProblemDelay;
    [SerializeField] private float _maxProblemDelay;
    [SerializeField] private AnimationCurve _problemDelayCurve;

    [Header("Problem probability")]
    [SerializeField, Range(0f, 1f)] private float _minProblemProbability;
    [SerializeField, Range(0f, 1f)] private float _maxProblemProbability;
    [SerializeField] private AnimationCurve _problemProbabilityCurve;

    [Header("Gear shifting probability")]
    [SerializeField, Range(0f, 1f)] private float _minGearShiftProbability;
    [SerializeField, Range(0f, 1f)] private float _maxGearShiftProbability;
    [SerializeField] private AnimationCurve _gearShiftProbabilityCurve;

    [Header("Gear shifting delay")]
    [SerializeField] private float _minGearShiftingDelay;
    [SerializeField] private float _maxGearShiftingDelay;
    [SerializeField] private AnimationCurve _gearShiftDelayCurve;

    [Header("Maximum speed")]
    [SerializeField] private float _minSpeed;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private AnimationCurve _speedCurve;

    [Header("Engine Torque")]
    [SerializeField] private float _minEngineTorque;
    [SerializeField] private float _maxEngineTorque;
    [SerializeField] private AnimationCurve _engineTorqueCurve;

    [Header("Engine Torque At RPM")]
    [SerializeField] private float _minEngineRPMTorque;
    [SerializeField] private float _maxEngineRPMTorque;
    [SerializeField] private AnimationCurve _engineRPMTorqueCurve;

    [Header("Max RPM")]
    [SerializeField] private float _minRPM;
    [SerializeField] private float _maxRPM;
    [SerializeField] private AnimationCurve _rpmCurve;

    [Header("Brake Torque")]
    [SerializeField] private float _minBrakeTorque;
    [SerializeField] private float _maxBrakeTorque;
    [SerializeField] private AnimationCurve _brakeTorqueCurve;

    private Coroutine _problemCoroutine;

    private int _rawGearID;

    private PauseManager _pauseManager;

    [Inject]
    private void Construct(PauseManager pauseManager)
    {
        _pauseManager = pauseManager;
    }
    
    #region MonoBehaviour

    private void OnValidate()
    {
        _carController ??= transform.parent.GetComponent<RCC_CarControllerV3>();
        _damageableObject ??= transform.parent.GetComponent<DamageableObject>();

        if (_carController == null) return;

        SyncVariables();
    }

    private void Awake()
    {
        _rawGearID = _carController.currentGear;

        SyncVariables();
    }

    private void SyncVariables()
    {
        _minGearShiftingDelay = _carController.gearShiftingDelay;
        _maxSpeed = _carController.maxspeed;
        _maxEngineTorque = _carController.maxEngineTorque;
        _maxEngineRPMTorque = _carController.maxEngineTorqueAtRPM;
        _maxRPM = _carController.maxEngineRPM;
        _maxBrakeTorque = _carController.brakeTorque;
    }

    private void OnEnable()
    {
        _damageableObject.onSetHealth += OnHealthUpdated;
    }

    private void OnDisable()
    {
        _damageableObject.onSetHealth -= OnHealthUpdated;
    }

    #endregion

    private void OnHealthUpdated(float health)
    {
        if (_pauseManager.isPaused) return;

        if (HasAppropriateHealth(ref health))
        {
            TryStartProblems(ref health);
        }
        else
        {
            TryStopProblems();
        }

        UpdateCarPreferences(ref health);
    }

    private bool HasAppropriateHealth(ref float health)
    {
        return health < _maxHealth && health.Approximately(0) == false;
    }

    private void TryStartProblems(ref float health)
    {
        if (_problemCoroutine == null)
        {
            _problemCoroutine = StartCoroutine(ProblemsRoutine(health));
        }
    }

    public void TryStopProblems()
    {
        if (_problemCoroutine != null)
        {
            StopCoroutine(_problemCoroutine);

            _problemCoroutine = null;
        }
    }

    private IEnumerator ProblemsRoutine(float health)
    {
        while (true)
        {
            if (_pauseManager.isPaused == false)
            {
                TryDoProblem(ref health);
            }

            yield return new WaitForSeconds(GetProblemOccurDelay(ref health));
        }
    }

    private float GetProblemOccurDelay(ref float health)
    {
        return _problemDelayCurve.Evaluate(_minHealth, _maxHealth, health,
            _minProblemDelay, _maxProblemDelay);
    }

    private void TryDoProblem(ref float health)
    {
        if (Extensions.Mathf.Probability(GetProblemProbability(ref health)))
        {
            DoProblem(ref health);
        }
    }

    private float GetProblemProbability(ref float health)
    {
        return _problemProbabilityCurve.Evaluate(_minHealth, _maxHealth, health,
            _maxProblemProbability, _minProblemProbability);
    }

    private void DoProblem(ref float health)
    {
        TryUpdateGear(ref health);
    }

    private void UpdateCarPreferences(ref float health)
    {
        UpdateGearShiftDelay(ref health);

        UpdateCarSpeed(ref health);

        UpdateMaxTorque(ref health);

        UpdateMaxTorueAtRPM(ref health);

        UpdateMaxRPM(ref health);

        UpdateBrakeTorque(ref health);
    }

    private void TryUpdateGear(ref float health)
    {
        if (Extensions.Mathf.Probability(GetGearShiftProbability(ref health)))
        {
            UpdateCarGear();
        }
    }
    private float GetGearShiftProbability(ref float health)
    {
        return _gearShiftProbabilityCurve.Evaluate(_minHealth, _maxHealth, health,
            _maxGearShiftProbability, _minGearShiftProbability);
    }

    private void UpdateCarGear()
    {
        if (Extensions.Random.Boolean())
        {
            _carController.GearShiftUp();
        }
        else
        {
            _carController.GearShiftDown();
        }
    }

    private void UpdateGearShiftDelay(ref float health)
    {
        _carController.gearShiftingDelay = GetGearShiftingDelay(ref health);
    }

    private float GetGearShiftingDelay(ref float health)
    {
        return _gearShiftDelayCurve.Evaluate(_minHealth, _maxHealth, health,
            _maxGearShiftingDelay, _minGearShiftingDelay);
    }

    public void CancelProblems()
    {
        _carController.currentGear = _rawGearID;
        _carController.gearShiftingDelay = _minGearShiftingDelay;
        _carController.maxspeed = _maxSpeed;
        _carController.maxEngineTorque = _maxEngineTorque;
        _carController.maxEngineTorqueAtRPM = _maxEngineRPMTorque;
        _carController.maxEngineRPM = _maxRPM;
        _carController.brakeTorque = _maxBrakeTorque;
    }

    private void UpdateCarSpeed(ref float health)
    {
        _carController.maxspeed = GetCarSpeed(ref health);
    }

    private float GetCarSpeed(ref float health)
    {
        return _speedCurve.Evaluate(_minHealth, _maxHealth, health, _minSpeed, _maxSpeed);
    }

    private void UpdateMaxTorque(ref float health)
    {
        _carController.maxEngineTorque = GetMaxTorque(ref health);
    }

    private float GetMaxTorque(ref float health)
    {
        return _engineTorqueCurve.Evaluate(_minHealth, _maxHealth, health, _minEngineTorque, _maxEngineTorque);
    }

    private void UpdateMaxTorueAtRPM(ref float health)
    {
        _carController.maxEngineTorqueAtRPM = GetMaxTorqueAtRPM(ref health);
    }

    private float GetMaxTorqueAtRPM(ref float health)
    {
        return _engineRPMTorqueCurve.Evaluate(_minHealth, _maxHealth, health, _minEngineRPMTorque, _maxEngineRPMTorque);
    }

    private void UpdateMaxRPM(ref float health)
    {
        _carController.maxEngineRPM = GetMaxRPM(ref health);
    }

    private float GetMaxRPM(ref float health)
    {
        return _rpmCurve.Evaluate(_minHealth, _maxHealth, health, _minRPM, _maxRPM);
    }

    private void UpdateBrakeTorque(ref float health)
    {
        _carController.brakeTorque = GetBrakeTorque(ref health);
    }

    private float GetBrakeTorque(ref float health)
    {
        return _brakeTorqueCurve.Evaluate(_minHealth, _maxHealth, health, _minBrakeTorque, _maxBrakeTorque);
    }
}
