using UnityEngine;
public class OnTriggerStay2DMonoEvent : MonoEvent
{
    [Header("References")]
    [SerializeField] private OnTriggerStay2DEvent _triggerStayEvent;

    #region Monobehaviour

    private void OnValidate()
    {
        _triggerStayEvent ??= GetComponent<OnTriggerStay2DEvent>();
    }

    private void Awake()
    { 
        _triggerStayEvent.onStay += TriggerStay;
    }
    
    private void OnDestroy()
    {
        _triggerStayEvent.onStay -= TriggerStay;
    }

    #endregion

    private void TriggerStay(Collider2D collider) => TriggerStay();

    private void TriggerStay()
    {
        onMonoCall?.Invoke();
    }
}
