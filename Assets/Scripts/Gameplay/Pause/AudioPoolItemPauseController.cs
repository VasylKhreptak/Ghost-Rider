using DG.Tweening;
using UnityEngine;
using Zenject;

public class AudioPoolItemPauseController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private AudioPoolItem _audioPoolItem;

    private PauseEventsHolder _pauseEventsHolder;

    [Inject]
    private void Construct(PauseEventsHolder pauseEventsHolder)
    {
        _pauseEventsHolder = pauseEventsHolder;
    }

    #region MonoBehaviour

    private void OnValidate()
    {
        _audioPoolItem ??= GetComponent<AudioPoolItem>();
    }

    private void OnEnable()
    {
        _pauseEventsHolder.onPause += OnPause;
        _pauseEventsHolder.onResume += OnResume;
    }

    private void OnDisable()
    {
        _pauseEventsHolder.onPause -= OnPause;
        _pauseEventsHolder.onResume -= OnResume;
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
