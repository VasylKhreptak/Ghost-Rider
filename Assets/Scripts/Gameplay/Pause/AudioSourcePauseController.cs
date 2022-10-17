using System.Collections;
using System.ComponentModel;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(AudioSource))]
public class AudioSourcePauseController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private AudioSource _audioSource;

    private PauseEvents _pauseEvents;
    
    [Inject]
    private void Construct(PauseEvents _pauseEvents)
    {
        this._pauseEvents = _pauseEvents;
    }

    #region MonoBehaviour

    private void OnValidate()
    {
        _audioSource ??= GetComponent<AudioSource>();
    }

    private void Awake()
    {
        if (_audioSource == null)
        {
            _audioSource = GetComponent<AudioSource>();
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
        _audioSource.Pause();
    }

    private void OnResume()
    {
        _audioSource.Play();
    }
}
