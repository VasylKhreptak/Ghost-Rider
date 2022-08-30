using UnityEngine;
public class OnTriggerEnter2DMonoEvent : MonoEvent
{
    [Header("References")]
    [SerializeField] private OnTriggerEnter2DEvent _triggerEnterEvent;

    #region Monobehaviour

    private void OnValidate()
    {
        _triggerEnterEvent ??= GetComponent<OnTriggerEnter2DEvent>();
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

    private void TriggerEnter(Collider2D collider) => TriggerEnter();

    private void TriggerEnter()
    {
        onMonoCall?.Invoke();
    }
}
