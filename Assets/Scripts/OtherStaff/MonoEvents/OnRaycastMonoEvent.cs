using UnityEngine;
public class OnRaycastMonoEvent : MonoEvent
{
    [Header("References")]
    [SerializeField] private OnRaycastEvent _raycastEvent;

    #region MonoBehaviour

    private void OnValidate()
    {
        _raycastEvent ??= GetComponent<OnRaycastEvent>();
    }
    
    private void Awake()
    {
        _raycastEvent.onRaycast += Invoke;
    }

    private void OnDestroy()
    {
        _raycastEvent.onRaycast -= Invoke;
    }

    #endregion

    private void Invoke(RaycastHit raycastHit) => Invoke();
    
    private void Invoke()
    {
        onMonoCall?.Invoke();
    }
}
