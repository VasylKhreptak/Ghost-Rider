using DG.Tweening;
using UnityEngine;

public class DelayedManualMonoEvent : MonoEvent
{
    [Header("References")]
    [SerializeField] private MonoEvent _monoEvent;
    
    [Header("Preferences")]
    [SerializeField] private bool _resetTimerOnStart = true;
    
    [Header("Timer preferences")]
    [SerializeField] private float _delay;

    private Tween _waitTween;

    #region MonoBehaviour

    private void Awake()
    {
        _monoEvent.onMonoCall += StartTimer;
    }

    private void OnDestroy()
    {
        _monoEvent.onMonoCall -= StartTimer;
    }

    #endregion
    
    public void StartTimer()
    {
        if (_resetTimerOnStart)
        {
            StopTimer();
        }

        _waitTween = this.DOWait(_delay).OnComplete(() => onMonoCall?.Invoke());
    }

    public void StopTimer()
    {
        _waitTween.Kill();
    }
}
