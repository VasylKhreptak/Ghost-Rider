using DG.Tweening;
using UnityEngine;
using Zenject;

public class AudioPoolItemPauseController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private AudioPoolItem _audioPoolItem;

    private PauseEvents _pauseEvents;

    [Inject]
    private void Construct(PauseEvents _pauseEvents)
    {
        this._pauseEvents = _pauseEvents;
    }

    #region MonoBehaviour

    private void OnValidate()
    {
        _audioPoolItem ??= GetComponent<AudioPoolItem>();
    }

    private void OnEnable()
    {
        _pauseEvents.onPause += OnPause;
        _pauseEvents.onResume += OnResume;
    }

    private void OnDisable()
    {
        _pauseEvents.onPause -= OnPause;
        _pauseEvents.onResume -= OnResume;
    }

    #endregion

    private void OnPause()
    {
        _audioPoolItem.audioSource.Pause();
        _audioPoolItem.waitTween.Pause();
    }

    private void OnResume()
    {
        _audioPoolItem.audioSource.Play();
        _audioPoolItem.waitTween.Play();
    }
}
