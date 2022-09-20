using System.Collections;
using System.ComponentModel;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(AudioSource))]
public class AudioSourcePauseController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private AudioSource _audioSource;

    private PauseEventsHolder _pauseEventsHolder;
    
    [Inject]
    private void Construct(PauseEventsHolder pauseEventsHolder)
    {
        _pauseEventsHolder = pauseEventsHolder;
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
        _audioSource.Pause();
    }

    private void OnResume()
    {
        _audioSource.Play();
    }
}
