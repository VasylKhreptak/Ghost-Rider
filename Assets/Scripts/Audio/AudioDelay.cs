using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class AudioDelay : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private MonoEvent _monoEvent;
    [FormerlySerializedAs("_audioClipHolder")]
    [SerializeField] private SoundHolder _soundHolder;

    [Header("Preferences")]
    [SerializeField] private bool _resetOnStart = true;

    [Header("Timer preferences")]
    [SerializeField] private float _delay;

    private Tween _waitTween;

    #region MyRegion

    private void OnValidate()
    {
        _monoEvent ??= GetComponent<MonoEvent>();
        _soundHolder ??= GetComponent<SoundHolder>();
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
        _soundHolder.Play();
    }

    private void KillTween()
    {
        _waitTween.Kill();
    }
}
