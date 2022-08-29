using UnityEngine;

public class OnTriggerExitMonoEvent : MonoEvent
{
    [Header("References")]
    [SerializeField] private OnTriggerExitEvent _triggerExitEvent;

    #region Monobehaviour

    private void OnValidate()
    {
        _triggerExitEvent ??= GetComponent<OnTriggerExitEvent>();
    }

    private void Awake()
    { 
        _triggerExitEvent.onExit += TriggerExit;
    }
    
    private void OnDestroy()
    {
        _triggerExitEvent.onExit -= TriggerExit;
    }

    #endregion

    private void TriggerExit(Collider collider) => TriggerExit();

    private void TriggerExit()
    {
        onMonoCall?.Invoke();
    }
}
