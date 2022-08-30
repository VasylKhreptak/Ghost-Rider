using UnityEngine;

public class OnCollisionEnter2DMonoEvent : MonoEvent
{
    [Header("References")]
    [SerializeField] private OnCollisionEnter2DEvent _collisionEnterEvent;

    #region Monobehaviour

    private void OnValidate()
    {
        _collisionEnterEvent ??= GetComponent<OnCollisionEnter2DEvent>();
    }

    private void Awake()
    { 
        _collisionEnterEvent.onEnter += CollisionEnter;
    }
    
    private void OnDestroy()
    {
        _collisionEnterEvent.onEnter -= CollisionEnter;
    }

    #endregion

    private void CollisionEnter(Collision2D collision) => CollisionEnter();

    private void CollisionEnter()
    {
        onMonoCall?.Invoke();
    }
}
