using UnityEngine;

public class OnCollisionStay2DMonoEvent : MonoEvent
{
    [Header("References")]
    [SerializeField] private OnCollisionStay2DEvent _collisionStayEvent;

    #region Monobehaviour

    private void OnValidate()
    {
        _collisionStayEvent ??= GetComponent<OnCollisionStay2DEvent>();
    }

    private void Awake()
    { 
        _collisionStayEvent.onStay += CollisionStay;
    }
    
    private void OnDestroy()
    {
        _collisionStayEvent.onStay -= CollisionStay;
    }

    #endregion

    private void CollisionStay(Collision2D collision) => CollisionStay();

    private void CollisionStay()
    {
        onMonoCall?.Invoke();
    }
}
