using System.Collections;
using System.ComponentModel;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(AudioSource))]
public class AudioSourcePauseController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private AudioSource _audioSource;

    private PauseManager _pauseManager;
    
    [Inject]
    private void Construct(PauseManager pauseManager)
    {
        this._pauseManager = pauseManager;
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
        _audioSource.Pause();
    }

    private void OnResume()
    {
        _audioSource.Play();
    }
}
