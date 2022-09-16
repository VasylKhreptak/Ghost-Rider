using UnityEngine;
using UnityEngine.Events;

public class UnityMonoEvent : MonoEvent
{
    [Header("References")]
    [SerializeField] private MonoEvent _eventTrigger;

    [Header("Event")]
    [SerializeField] private UnityEvent _unityEvent;

    #region Monobehaviour

    private void OnValidate()
    {
        _eventTrigger ??= GetComponent<MonoEvent>();
    }

    private void Awake()
    {
        _eventTrigger.onMonoCall += Invoke;
    }

    private void OnDestroy()
    {
        _eventTrigger.onMonoCall -= Invoke;
    }

    #endregion

    private void Invoke()
    {
        onMonoCall?.Invoke();

        _unityEvent.Invoke();
    }
}
