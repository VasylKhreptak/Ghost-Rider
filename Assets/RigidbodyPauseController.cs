using System.Collections;
using UnityEngine;
using Zenject;

public class RigidbodyPauseController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody _rigidbody;

    private PauseEventsHolder _pauseEventsHolder;

    private Vector3 _previousVelocity;
    private Vector3 _previousAngularVelocity;

    [Inject]
    private void Construct(PauseEventsHolder pauseEventsHolder)
    {
        _pauseEventsHolder = pauseEventsHolder;
    }

    #region MonoBehaviour

    private void OnValidate()
    {
        _rigidbody ??= GetComponent<Rigidbody>();
    }

    private void Awake()
    {
        if (_rigidbody == null)
        {
            _rigidbody = GetComponent<Rigidbody>();
        }
    }

    private void OnEnable()
    {
        StartCoroutine(AddListenersRoutine());
    }

    private void OnDisable()
    {
        _pauseEventsHolder.onPause -= OnPause;
        _pauseEventsHolder.onResume -= OnResume;
    }

    #endregion

    private IEnumerator AddListenersRoutine()
    {
        yield return null;

        _pauseEventsHolder.onPause += OnPause;
        _pauseEventsHolder.onResume += OnResume;
    }

    private void OnPause()
    {
        _previousVelocity = _rigidbody.velocity;
        _previousAngularVelocity = _rigidbody.angularVelocity;
        _rigidbody.isKinematic = true;
    }

    private void OnResume()
    {
        _rigidbody.isKinematic = false;
        _rigidbody.velocity = _previousVelocity;
        _rigidbody.angularVelocity = _previousAngularVelocity;
    }
}
