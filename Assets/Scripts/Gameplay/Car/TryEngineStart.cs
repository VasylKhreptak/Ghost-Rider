using System;
using UnityEngine;

public class TryEngineStart : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RCC_CarControllerV3 _carController;
    [SerializeField] private DamageableObject _damageableObject;

    [Header("Preferences")]
    [SerializeField, Range(0f, 1f)] private float _minEngineStartProbability;
    [SerializeField, Range(0f, 1f)] private float _maxEngineStartProbability;
    [SerializeField] private float _minHealth;
    [SerializeField] private float _maxHealth;
    [SerializeField] private AnimationCurve _startEngineCurve;

    [Header("Events")]
    [SerializeField] private MonoEvent _gasEvent;

    public ManualMonoEvent onStartFailed;
    public ManualMonoEvent onStartEngine;
    
    #region MonoBehaviour

    private void OnValidate()
    {
        _damageableObject ??= GetComponent<DamageableObject>();
        _carController ??= GetComponent<RCC_CarControllerV3>();
    }

    private void OnEnable()
    {
        _gasEvent.onMonoCall += TryStartEngine;
    }

    private void OnDisable()
    {
        _gasEvent.onMonoCall -= TryStartEngine;
    }

    #endregion

    private void TryStartEngine()
    {
        float health = _damageableObject.GetHealth();
        
        if (health > _maxHealth) return;
        
        if (CanStartEngine(health))
        {
            StartEngine();
        }
        else if (_carController.engineRunning == false)
        {
            onStartFailed?.Invoke();
        }
    }

    private bool CanStartEngine(float health)
    {
        return _carController.engineRunning == false && health < _maxHealth && Extensions.Mathf.Probability(GetEngineStartProbability(health));
    }

    private void StartEngine()
    {
        _carController.StartEngine();

        onStartEngine?.Invoke();
    }

    private float GetEngineStartProbability(float health)
    {
        return _startEngineCurve.Evaluate(_minHealth, _maxHealth, health, 
            _minEngineStartProbability, _maxEngineStartProbability);
    }
}
