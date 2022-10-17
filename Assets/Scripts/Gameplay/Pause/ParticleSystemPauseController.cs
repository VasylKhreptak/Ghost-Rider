using System.Collections;
using UnityEngine;
using Zenject;

public class ParticleSystemPauseController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ParticleSystem _particleSystem;

    private PauseEvents _pauseEvents;
    
    [Inject]
    private void Construct(PauseEvents _pauseEvents)
    {
        this._pauseEvents = _pauseEvents;
    }

    #region MonoBehaviour

    private void OnValidate()
    {
        _particleSystem ??= GetComponent<ParticleSystem>();
    }

    private void Awake()
    {
        if (_particleSystem == null)
        {
            _particleSystem = GetComponent<ParticleSystem>();
        }
    }

    private void OnEnable()
    {
        StartCoroutine(AddListenersRoutine());
    }

    private void OnDisable()
    {
        _pauseEvents.onPause -= OnPause;
        _pauseEvents.onResume -= OnResume;
    }

    #endregion

    private IEnumerator AddListenersRoutine()
    {
        yield return null;

        _pauseEvents.onPause += OnPause;
        _pauseEvents.onResume += OnResume;
    }
    
    private void OnPause()
    {
        _particleSystem.Pause();
    }

    private void OnResume()
    {
        _particleSystem.Play();
    }
}
