using DG.Tweening;
using UnityEngine;

public class AudioDelay : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private MonoEvent _monoEvent;
    [SerializeField] private AudioClipHolder _audioClipHolder;

    [Header("Preferences")]
    [SerializeField] private bool _resetOnStart = true;

    [Header("Timer preferences")]
    [SerializeField] private float _delay;

    private Tween _waitTween;

    #region MyRegion

    private void OnValidate()
    {
        _monoEvent ??= GetComponent<MonoEvent>();
        _audioClipHolder ??= GetComponent<AudioClipHolder>();
    }

    private void Awake()
    {
        _monoEvent.onMonoCall += StartTimer;
    }

    private void OnDisable()
    {
        KillTween();
    }

    private void OnDestroy()
    {
        _monoEvent.onMonoCall -= StartTimer;
    }

    #endregion

    private void StartTimer()
    {
        if (_resetOnStart)
        {
            KillTween();
        }

        _waitTween = this.DOWait(_delay).OnComplete(PlayAudio);
    }

    public void StopTimer()
    {
        KillTween();
    }
    
    private void PlayAudio()
    {
        _audioClipHolder.Play();
    }

    private void KillTween()
    {
        _waitTween.Kill();
    }
}
