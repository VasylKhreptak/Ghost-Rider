using DG.Tweening;
using UnityEngine;
using Zenject;

public class AudioPoolItemPauseController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private AudioPoolItem _audioPoolItem;

    private PauseManager _pauseManager;

    [Inject]
    private void Construct(PauseManager pauseManager)
    {
        this._pauseManager = pauseManager;
    }

    #region MonoBehaviour

    private void OnValidate()
    {
        _audioPoolItem ??= GetComponent<AudioPoolItem>();
    }

    private void OnEnable()
    {
        _pauseManager.onPause += OnPause;
        _pauseManager.onResume += OnResume;
    }

    private void OnDisable()
    {
        _pauseManager.onPause -= OnPause;
        _pauseManager.onResume -= OnResume;
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
