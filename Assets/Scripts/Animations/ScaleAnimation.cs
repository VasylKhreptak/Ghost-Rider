using DG.Tweening;
using UnityEngine;

public class ScaleAnimation : AnimationCore
{
    [Header("References")]
    [SerializeField] private Transform _transform;

    [Header("Preferences")]
    [SerializeField] private bool _fromStart = true;

    [Header("Animate Preferences")]
    [SerializeField] private Vector3 _startScale;
    [SerializeField] private Vector3 _targetScale;
    [SerializeField] private float _duration;
    [SerializeField] private AnimationCurve _animationCurve;
    [SerializeField] private int _loops;
    [SerializeField] private LoopType _loopType;

    private Tween _scaleTween;

    #region MonoBehaviour

    private void OnValidate()
    {
        _transform ??= GetComponent<Transform>();
    }

    private void OnDestroy()
    {
        Kill();
    }

    #endregion

    public override void Animate(bool state)
    {
        Kill();

        if (_fromStart)
        {
            _transform.localScale = state ? _startScale : _targetScale;
        }

        _scaleTween = _transform.DOScale(state ? _targetScale : _startScale, _duration)
            .SetEase(_animationCurve).SetLoops(_loops, _loopType);
    }
    public override void Pause()
    {
        _scaleTween.Pause();
    }
    public override void Play()
    {
        _scaleTween.Play();
    }
    public override void Kill()
    {
        _scaleTween.Kill();
    }
}
