using UnityEngine;

public class PlatformEffector3D : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _transform;
    [SerializeField] private Collider _collider;
    [SerializeField] private OnTriggerEnterEvent _triggerEnterEvent;
    [SerializeField] private OnTriggerExitEvent _triggerExitEvent;

    #region MonoBehaviour

    private void OnValidate()
    {
        _transform ??= GetComponent<Transform>();
        _collider ??= GetComponent<Collider>();
        _triggerEnterEvent ??= GetComponent<OnTriggerEnterEvent>();
        _triggerExitEvent ??= GetComponent<OnTriggerExitEvent>();
    }

    private void OnEnable()
    {
        _triggerEnterEvent.onEnter += OnEnter;
        _triggerExitEvent.onExit += OnExit;
    }

    private void OnDisable()
    {
        _triggerEnterEvent.onEnter -= OnEnter;
        _triggerExitEvent.onExit -= OnExit;
    }

    #endregion

    private void OnEnter(Collider collider)
    {
        if (_transform.position.z - collider.transform.position.z < 0)
        {
            SetColliderState(true);
        }
    }

    private void OnExit(Collider collider)
    {
        SetColliderState(false);
    }

    private void SetColliderState(bool state)
    {
        _collider.isTrigger = !state;
    }
}
