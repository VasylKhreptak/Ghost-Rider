using UnityEngine;
public class OnCollisionExit2DMonoEvent : MonoEvent
{
    [Header("References")]
    [SerializeField] private OnCollisionExit2DEvent _collisionExitEvent;

    #region Monobehaviour

    private void OnValidate()
    {
        _collisionExitEvent ??= GetComponent<OnCollisionExit2DEvent>();
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

    private void CollisionExit(Collision2D collision) => CollisionExit();

    private void CollisionExit()
    {
        onMonoCall?.Invoke();
    }
}
