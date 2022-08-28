using DG.Tweening;
using UnityEngine;

public class ColorAnimation : AnimationCore
{
    [Header("References")]
    [SerializeField] private ColorAdapter _colorAdapter;

    [Header("Preferences")]
    [SerializeField] private float _duration;
    [SerializeField] private Color _startColor;
    [SerializeField] private Color _targetColor;
    [SerializeField] private AnimationCurve _animationCurve;
    [SerializeField] private int _loops;
    [SerializeField] private LoopType _loopType;

    private Tween _colorTween;

    #region MonoBaheviour

    private void OnValidate()
    {
        _colorAdapter ??= GetComponent<ColorAdapter>();
    }

    private void OnDestroy()
    {
        Kill();
    }

    #endregion

    public override void Animate(bool state)
    {
        _colorAdapter.color = state ? _startColor : _targetColor;

        _colorTween = DOTween
            .To(() => _colorAdapter.color, x => _colorAdapter.color = x, state ? _targetColor : _startColor, _duration)
            .SetEase(_animationCurve).SetLoops(_loops, _loopType);
    }
    public override void Pause()
    {
        _colorTween.Pause();
    }
    public override void Play()
    {
        _colorTween.Play();
    }

    public override void Kill()
    {
        _colorTween.Kill();
    }
}
