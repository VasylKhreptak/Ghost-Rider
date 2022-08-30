using UnityEngine;

public class OnTriggerExit2DMonoEvent : MonoEvent
{
    [Header("References")]
    [SerializeField] private OnTriggerExit2DEvent _triggerExitEvent;

    #region Monobehaviour

    private void OnValidate()
    {
        _triggerExitEvent ??= GetComponent<OnTriggerExit2DEvent>();
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

    private void TriggerExit(Collider2D collider) => TriggerExit();

    private void TriggerExit()
    {
        onMonoCall?.Invoke();
    }
}
