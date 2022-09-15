using UnityEngine;

public class MonoEventCountLimiter : MonoEvent
{
    [Header("References")]
    [SerializeField] private MonoEvent _targetEvent;
    [SerializeField] private MonoEvent _resetCounterEvent;
    
    [Header("Preferences")]
    [SerializeField] private int _maxEventsCount;

    private int _count;
    
    #region MonoBehaviour

    private void OnValidate()
    {
        _targetEvent ??= GetComponent<MonoEvent>();
    }

    private void Awake()
    {
        _targetEvent.onMonoCall += TryInvoke;
        _resetCounterEvent.onMonoCall += ResetCount;
    }

    private void OnDestroy()
    {
        _targetEvent.onMonoCall -= TryInvoke;
        _resetCounterEvent.onMonoCall -= ResetCount;
    }

    #endregion

    private void TryInvoke()
    {
        if (CanInvoke())
        {
            Invoke();

            _count++;

            if (_count == _maxEventsCount)
            {
                _targetEvent.enabled = false;
            }
        }
    }

    private bool CanInvoke()
    {
        return _count < _maxEventsCount;
    }
    
    private void Invoke()
    {
        onMonoCall?.Invoke();
    }
    
    private void ResetCount()
    {
        _count = 0;

        _targetEvent.enabled = true;
    }
}
