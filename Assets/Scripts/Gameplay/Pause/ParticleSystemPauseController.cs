using System.Collections;
using UnityEngine;
using Zenject;

public class ParticleSystemPauseController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ParticleSystem _particleSystem;

    private PauseManager _pauseManager;

    private bool _wasPlaying;
    
    [Inject]
    private void Construct(PauseManager pauseManager)
    {
        this._pauseManager = pauseManager;
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
        _pauseManager.onPause -= OnPause;
        _pauseManager.onResume -= OnResume;
    }

    #endregion

    private IEnumerator AddListenersRoutine()
    {
        yield return null;

        _pauseManager.onPause += OnPause;
        _pauseManager.onResume += OnResume;
    }
    
    private void OnPause()
    {
        _wasPlaying = _particleSystem.isPlaying;
        
        _particleSystem.Pause();
    }

    private void OnResume()
    {
        if (_wasPlaying == false) return;
        
        _particleSystem.Play();
    }
}
