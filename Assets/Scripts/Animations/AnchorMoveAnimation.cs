using DG.Tweening;
using UnityEngine;

public class AnchorMoveAnimation : AnimationCore
{
    [Header("References")]
    [SerializeField] private RectTransform _rectTransform;

    [Header("Preferences")]
    [SerializeField] private bool _resetPositionOnStart = true;

    [Header("Animation preferences")]
    [SerializeField] private Vector2 _startAnchorPosition;
    [SerializeField] private Vector2 _targetAnchorPosition;
    [SerializeField] private float _duration;
    [SerializeField] private AnimationCurve _animationCurve;
    [SerializeField] private int _loops;
    [SerializeField] private LoopType _loopType;

    private Tween _moveTween;

    #region MonoBehaviour

    private void OnValidate()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    private void OnDestroy()
    {
        Kill();
    }

    #endregion
    public override void Animate(bool state)
    {
        Kill();

        if (_resetPositionOnStart)
        {
            _rectTransform.anchoredPosition = state ? _startAnchorPosition : _targetAnchorPosition;
        }

        _moveTween = _rectTransform.DOAnchorPos(state ? _targetAnchorPosition : _startAnchorPosition, _duration)
            .SetEase(_animationCurve).SetLoops(_loops, _loopType);

    }
    public override void Pause()
    {
        _moveTween.Pause();
    }
    public override void Play()
    {
        _moveTween.Play();
    }
    public override void Kill()
    {
        _moveTween.Kill();
    }
}
