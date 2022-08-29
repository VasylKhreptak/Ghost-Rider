using UnityEngine;

public class OnCollisionStayMonoEvent : MonoEvent
{
    [Header("References")]
    [SerializeField] private OnCollisionStayEvent _collisionStayEvent;

    #region Monobehaviour

    private void OnValidate()
    {
        _collisionStayEvent ??= GetComponent<OnCollisionStayEvent>();
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

    private void CollisionStay(Collision collision) => CollisionStay();

    private void CollisionStay()
    {
        onMonoCall?.Invoke();
    }
}
