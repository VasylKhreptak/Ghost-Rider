using System;
using UnityEngine;

public class HealthValueEvent : MonoEvent
{
    [Header("References")]
    [SerializeField] private DamageableObject _damageableObject;

    [Header("Reset event")]
    [SerializeField] protected float _targetHealth;
    [SerializeField] private MonoEvent _resetEvent;

    private bool _wasInvoked;
    
    #region MonoBehaviour

    private void OnValidate()
    {
        _damageableObject ??= GetComponent<DamageableObject>();
        _resetEvent ??= GetComponent<MonoEvent>();
    }
	
    private void Awake()
    {
        _resetEvent.onMonoCall += ResetCount;
        _damageableObject.onSetHealth += TryInvoke;
    }

    private void OnDestroy()
    {
        _resetEvent.onMonoCall -= ResetCount;
        _damageableObject.onSetHealth -= TryInvoke;
    }
	
    #endregion
    
    private void TryInvoke(float health)
    {
        if (CanInvoke(health))
        {
            Invoke();
        }
    }

    private bool CanInvoke(float health)
    {
        return _wasInvoked == false && HasAppropriateHealth(health);
    }

    protected virtual bool HasAppropriateHealth(float health)
    {
        throw new NotImplementedException();
    }
    
    public override void Invoke()
    {
        base.Invoke();

        _wasInvoked = true;
    }

    private void ResetCount()
    {
        _wasInvoked = false;
    }
}
