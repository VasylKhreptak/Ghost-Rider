using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PlatformEffector3D : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private OnTriggerEnterMonoEvent _triggetEnterEvent;
    [SerializeField] private OnTriggerExitMonoEvent _triggerExitMonoEvent;
    [SerializeField] private Collider _mainCollider;

    [Header("Preferences")]
    [SerializeField] private LayerMask _layerMask;

    #region MonoBehaviour

    private void OnValidate()
    {
        _mainCollider ??= GetComponent<Collider>();
    }

    private void OnEnable()
    {
        _triggetEnterEvent.onMonoCall += DisableCollider;
        _triggerExitMonoEvent.onMonoCall += EnableCollider;
    }

    private void OnDisable()
    {
        _triggetEnterEvent.onMonoCall -= DisableCollider;
        _triggerExitMonoEvent.onMonoCall -= EnableCollider;
    }

    private void OnCollisionExit(Collision other)
    {
        if (_layerMask.ContainsLayer(other.gameObject.layer))
        {
            EnableCollider();
        }
    }

    #endregion
    
    private void EnableCollider() => SetColliderState(true);

    private void DisableCollider() => SetColliderState(false);
    
    private void SetColliderState(bool state) => _mainCollider.isTrigger = !state;
}
