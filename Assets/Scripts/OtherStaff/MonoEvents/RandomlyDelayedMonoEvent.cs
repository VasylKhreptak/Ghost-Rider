using DG.Tweening;
using UnityEngine;

public class RandomlyDelayedMonoEvent : MonoEvent
{
    [Header("References")]
    [SerializeField] private MonoEvent _monoEvent;

    [Header("Preferences")]
    [SerializeField] private float _minDelay;
    [SerializeField] private float _maxDelay;

    private float Delay => Random.Range(_minDelay, _maxDelay);

    private Tween _waitTween;

    #region MonoBehaviour

    private void OnValidate()
    {
        _monoEvent ??= GetComponent<MonoEvent>();
    }

    private void OnEnable()
    {
        TryAddListener();
    }

    private void OnDisable()
    {
        _monoEvent.onMonoCall -= DelayInvoke;

        _waitTween.Kill();
    }

    #endregion

    public void TryAddListener()
    {
        if (_monoEvent != null)
        {
            _monoEvent.onMonoCall += DelayInvoke;
        }
    }

    public void SetEvent(MonoEvent monoEvent)
    {
        _monoEvent = monoEvent;
    }

    private void DelayInvoke()
    {
        _waitTween.Kill();
        _waitTween = this.DOWait(Delay).OnComplete(() => { onMonoCall?.Invoke(); });
    }

    public void KillDelay()
    {
        _waitTween.Kill();
    }
}
