using DG.Tweening;
using UnityEngine;

public class DelayedMonoEvent : MonoEvent
{
    [Header("References")]
    [SerializeField] private MonoEvent _monoEvent;

    [Header("Preferences")]
    [SerializeField] private float _delay;

    private Tween _waitTween;

    #region MonoBehaviour

    private void OnValidate()
    {
        _monoEvent ??= GetComponent<MonoEvent>();
    }

    private void Awake()
    {
        _monoEvent.onMonoCall += DelayMonoEvent;
    }

    private void OnDestroy()
    {
        _monoEvent.onMonoCall -= DelayMonoEvent;

        _waitTween.Kill();
    }

    #endregion

    private void DelayMonoEvent()
    {
        _waitTween.Kill();
        _waitTween = this.DOWait(_delay).OnComplete(() => { onMonoCall?.Invoke(); });
    }
}
