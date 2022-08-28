using DG.Tweening;
using UnityEngine;

public class AnimationDelay : AnimationCore
{
    [Header("References")]
    [SerializeField] private AnimationCore _animation;
    [SerializeField] private MonoEvent _monoEvent;

    [Header("Preferences")]
    [SerializeField] private float _delay;
    [SerializeField] private bool _forward = true;

    private Tween _waitTween;

    #region MonoBehaviour

    private void OnValidate()
    {
        _animation ??= GetComponent<AnimationCore>();
        _monoEvent ??= GetComponent<MonoEvent>();
    }

    private void Awake()
    {
        _monoEvent.onMonoCall += Animate;
    }

    private void OnDestroy()
    {
        _monoEvent.onMonoCall -= Animate;
        
        Kill();
    }

    #endregion

    private void Animate() => Animate(_forward);

    public override void Animate(bool state)
    {
        Kill();
        _waitTween = this.DOWait(_delay).OnComplete(() => { _animation.Animate(state); });
    }
    public override void Pause()
    {
        _waitTween.Pause();
    }
    public override void Play()
    {
        _waitTween.Play();
    }
    public override void Kill()
    {
        _waitTween.Kill();
    }
}
