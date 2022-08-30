using UnityEngine;
public class OnTriggerStayMonoEvent : MonoEvent
{
    [Header("References")]
    [SerializeField] private OnTriggerStayEvent _triggerStayEvent;

    #region Monobehaviour

    private void OnValidate()
    {
        _triggerStayEvent ??= GetComponent<OnTriggerStayEvent>();
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

    private void TriggerStay(Collider collider) => TriggerStay();

    private void TriggerStay()
    {
        onMonoCall?.Invoke();
    }
}
