using UnityEngine;
public class OnCollisionExitMonoEvent : MonoEvent
{
    [Header("References")]
    [SerializeField] private OnCollisionExitEvent _collisionExitEvent;

    #region Monobehaviour

    private void OnValidate()
    {
        _collisionExitEvent ??= GetComponent<OnCollisionExitEvent>();
    }

    private void Awake()
    { 
        _collisionExitEvent.onExit += CollisionExit;
    }
    
    private void OnDestroy()
    {
        _collisionExitEvent.onExit -= CollisionExit;
    }

    #endregion

    private void CollisionExit(Collision collision) => CollisionExit();

    private void CollisionExit()
    {
        onMonoCall?.Invoke();
    }
}
