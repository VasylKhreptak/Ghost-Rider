using System;
using UnityEngine;

public class ScratchEvents : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private OnCollisionEnterEvent _collisionEnterEvent;
    [SerializeField] private OnCollisionStayEvent _collisionStayEvent;
    [SerializeField] private OnCollisionExitEvent _collisionExitEvent;

    [Header("Preferences")]
    [SerializeField] private float _minVelocity;

    public float MinVelocity => _minVelocity;
    
    public Action<Vector3> onStartScratching;
    public Action<Vector3> onScratchUpdate;
    public Action onStopScratching;

    private bool _hasAppropriateVelocity;
    private bool _isScratching;

    #region MonoBehaviour

    private void OnValidate()
    {
        _rigidbody ??= GetComponent<Rigidbody>();
        _collisionEnterEvent ??= GetComponent<OnCollisionEnterEvent>();
        _collisionStayEvent ??= GetComponent<OnCollisionStayEvent>();
        _collisionExitEvent ??= GetComponent<OnCollisionExitEvent>();
    }

    private void OnEnable()
    {
        _collisionEnterEvent.onEnter += OnEnter;
        _collisionStayEvent.onStay += OnStay;
        _collisionExitEvent.onExit += OnExit;
    }

    private void OnDisable()
    {
        _collisionEnterEvent.onEnter -= OnEnter;
        _collisionStayEvent.onStay -= OnStay;
        _collisionExitEvent.onExit -= OnExit;
    }

    #endregion

    private void OnEnter(Collision collision)
    {
        CheckVelocity(collision);

        TryStartScratching(collision);
    }

    private void OnStay(Collision collision)
    {
        CheckVelocity(collision);

        TryStartScratching(collision);

        TryStopScratchingOnStay(collision);
        
        TryUpdateScratch(collision);
    }

    private void OnExit(Collision collision)
    {
        TryStopScratchingOnExit();
    }

    private void CheckVelocity(Collision collision)
    {
        Rigidbody attachedRigidbody = collision.rigidbody;

        if (attachedRigidbody == null)
        {
            _hasAppropriateVelocity = _rigidbody.velocity.magnitude > _minVelocity;

            return;
        }

        float relativeVelocity = Mathf.Abs(_rigidbody.velocity.magnitude - attachedRigidbody.velocity.magnitude);

        _hasAppropriateVelocity = relativeVelocity > _minVelocity;
    }

    private void TryStartScratching(Collision collision)
    {
        if (CanStartScratching())
        {
            StartScratching(collision);
        }
    }

    private bool CanStartScratching()
    {
        return _isScratching == false && _hasAppropriateVelocity;
    }

    private void StartScratching(Collision collision)
    {
        onStartScratching?.Invoke(collision.GetContact(0).point);

        _isScratching = true;
    }

    private void TryStopScratchingOnStay(Collision collision)
    {
        if (CanStopScratchingOnStay())
        {
            StopScratching();
        }
    }

    private bool CanStopScratchingOnStay()
    {
        return _hasAppropriateVelocity == false && _isScratching;
    }

    public void TryStopScratchingOnExit()
    {
        if (CanStopScratchingOnExit())
        {
            StopScratching();
        }
    }

    private bool CanStopScratchingOnExit()
    {
        return _isScratching;
    }
    
    public void StopScratching()
    {
        onStopScratching?.Invoke();

        _isScratching = false;
    }

    private void TryUpdateScratch(Collision collision)
    {
        if (CanUpdateScratch())
        {
            UpdateScratch(collision);
        }
    }

    private bool CanUpdateScratch()
    {
        return _isScratching && _hasAppropriateVelocity;
    }

    private void UpdateScratch(Collision collision)
    {
        onScratchUpdate?.Invoke(collision.GetContact(0).point);
    }
}
