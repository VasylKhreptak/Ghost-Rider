using UnityEngine;
public class OnTriggerEnterMonoEvent : MonoEvent
{
    [Header("References")]
    [SerializeField] private OnTriggerEnterEvent _triggerEnterEvent;

    #region Monobehaviour

    private void OnValidate()
    {
        _triggerEnterEvent ??= GetComponent<OnTriggerEnterEvent>();
    }

    private void Awake()
    { 
        _triggerEnterEvent.onEnter += TriggerEnter;
    }
    
    private void OnDestroy()
    {
        _triggerEnterEvent.onEnter -= TriggerEnter;
    }

    #endregion

    private void TriggerEnter(Collider collider) => TriggerEnter();

    private void TriggerEnter()
    {
        onMonoCall?.Invoke();
    }
}
