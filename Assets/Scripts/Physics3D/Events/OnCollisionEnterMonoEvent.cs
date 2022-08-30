using UnityEngine;

public class OnCollisionEnterMonoEvent : MonoEvent
{
    [Header("References")]
    [SerializeField] private OnCollisionEnterEvent _collisionEnterEvent;

    #region Monobehaviour

    private void OnValidate()
    {
        _collisionEnterEvent ??= GetComponent<OnCollisionEnterEvent>();
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

    private void CollisionEnter(Collision collision) => CollisionEnter();

    private void CollisionEnter()
    {
        onMonoCall?.Invoke();
    }
}
