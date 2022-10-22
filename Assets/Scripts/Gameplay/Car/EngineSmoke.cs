using UnityEngine;

public class EngineSmoke : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private DamageableObject _damageableObject;
    [SerializeField] private ParticleSystem _particle;

    [Header("Preferences")]
    [SerializeField] private float _minLifetime;
    [SerializeField] private float _maxLifetime;
    [SerializeField] private AnimationCurve _lifetimeCurve;
    [SerializeField] private float _minParticleSpeed;
    [SerializeField] private float _maxParticleSpeed;
    [SerializeField] private AnimationCurve _speedCurve;
    [SerializeField] private int _minRateOverTime;
    [SerializeField] private int _maxRateOverTime;
    [SerializeField] private AnimationCurve _rateOverTimeCurve;
    [SerializeField] private float _minStartSize;
    [SerializeField] private float _maxStartSize;
    [SerializeField] private AnimationCurve _sizeCurve;
    [SerializeField] private float _minHealth;
    [SerializeField] private float _maxHealth;

    #region MonoBehaviour

    private void OnValidate()
    {
        _particle ??= GetComponentInChildren<ParticleSystem>();
        _damageableObject ??= GetComponent<DamageableObject>();
    }

    private void OnEnable()
    {
        _damageableObject.onSetHealth += OnSetHealth;
    }

    private void OnDisable()
    {
        _damageableObject.onSetHealth -= OnSetHealth;
    }

    #endregion

    private void OnSetHealth(float health)
    {
        if (HasAppropriateHealth(health))
        {
            TryStartSmoking();

            UpdateParticle(health);
        }
        else
        {
            TryStopSmoking();
        }
    }

    private bool HasAppropriateHealth(float health)
    {
        return health < _maxHealth;
    }

    private bool IsSmoking() => _particle.isPlaying;

    private void TryStartSmoking()
    {
        if (IsSmoking() == false)
        {
            StartSmoking();
        }
    }

    private void StartSmoking()
    {
        _particle.Play();
    }

    private void TryStopSmoking()
    {
        if (IsSmoking())
        {
            StopSmoking();
        }
    }

    private void StopSmoking()
    {
        _particle.Stop();
    }

    private void UpdateParticle(float health)
    {
        UpdateParticleLifetime(health);
        
        UpdateParticleSpeed(health);
        
        UpdateParticleRateOverTime(health);
        
        UpdateParticleSize(health);
    }

    private void UpdateParticleLifetime(float health)
    {
        ParticleSystem.MainModule mainModule = _particle.main;
        mainModule.startLifetime = GetLifetime(health);
    }
    
    private void UpdateParticleSpeed(float health)
    {
        ParticleSystem.MainModule mainModule = _particle.main;
        mainModule.startSpeed = GetSmokeSpeed(health);
    }

    private void UpdateParticleRateOverTime(float health)
    {
        ParticleSystem.EmissionModule emissionModule = _particle.emission;
        emissionModule.rateOverTime = GetRateOverTime(health);
    }

    private void UpdateParticleSize(float health)
    {
        ParticleSystem.MainModule mainModule = _particle.main;
        mainModule.startSize = GetStartSize(health);
    }

    private float GetSmokeSpeed(float health)
    {
        return _speedCurve.Evaluate(_minHealth, _maxHealth, health, _maxParticleSpeed, _minParticleSpeed);
    }

    private float GetRateOverTime(float health)
    {
        return _rateOverTimeCurve.Evaluate(_minHealth, _maxHealth, health, _maxRateOverTime, _minRateOverTime);
    }

    private float GetStartSize(float health)
    {
        return _sizeCurve.Evaluate(_minHealth, _maxHealth, health, _maxStartSize, _minStartSize);
    }

    private float GetLifetime(float health)
    {
        return _lifetimeCurve.Evaluate(_minHealth, _maxHealth, health, _maxLifetime, _minLifetime);
    }
}
