using System.Collections;
using UnityEngine;
using Zenject;

public class ParticleSystemPauseController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ParticleSystem _particleSystem;

    private PauseEventsHolder _pauseEventsHolder;
    
    [Inject]
    private void Construct(PauseEventsHolder pauseEventsHolder)
    {
        _pauseEventsHolder = pauseEventsHolder;
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
        _particleSystem.Pause();
    }

    private void OnResume()
    {
        _particleSystem.Play();
    }
}
