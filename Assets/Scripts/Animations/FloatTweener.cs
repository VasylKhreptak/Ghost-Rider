using DG.Tweening;
using UnityEngine;

public class FloatTweener : AnimationCore
{
    [Header("References")]
    [SerializeField] private FloatAdapter _floatAdapter;

    [Header("Preferences")]
    [SerializeField] private bool _fromStart = true;

    [Header("Tween Preferences")]
    [SerializeField] private float _startValue;
    [SerializeField] private float _targetValue;
    [SerializeField] private float _duration;
    [SerializeField] private AnimationCurve _animationCurve;
    [SerializeField] private int _loops;
    [SerializeField] private LoopType _loopType;

    private Tween _tween;
    
    #region MonoBehaviour

    private void OnValidate()
    {
        _floatAdapter ??= GetComponent<FloatAdapter>();
    }

    private void OnDestroy()
    {
        Kill();
    }

    #endregion

    public override void Animate(bool state)
    {
        if (_fromStart)
        {
            _floatAdapter.value = state ? _startValue : _targetValue;
        }

        _tween = DOTween
            .To(() => _floatAdapter.value, x => _floatAdapter.value = x, state ? _targetValue : _startValue, _duration)
            .SetEase(_animationCurve)
            .SetLoops(_loops, _loopType);
    }
    public override void Pause()
    {
        _tween.Pause();
    }
    public override void Play()
    {
        _tween.Play();
    }
    public override void Kill()
    {
        _tween.Kill();
    }
}
