using DG.Tweening;
using UnityEngine;

public class MoveAnimation : AnimationCore
{
    [Header("References")]
    [SerializeField] private Transform _transform;

    [Header("Preferences")]
    [SerializeField] private bool _startAtBeginning;
    [SerializeField] private Vector3 _startPosition;
    [SerializeField] private Vector3 _targetPosition;
    [SerializeField] private float _duration;
    [SerializeField] private AnimationCurve _animationCurve;
    [SerializeField] private int _loops;
    [SerializeField] private LoopType _loopType;

    private Tween _moveTween;
    
    #region MonoBehaviour

    private void OnValidate()
    {
        _transform = GetComponent<Transform>();
    }

    private void OnDestroy()
    {
        Kill();
    }

    #endregion
    public override void Animate(bool state)
    {
        Kill();

        if (_startAtBeginning)
        {
            _transform.localPosition = state ? _startPosition : _targetPosition;
        }
        
        _moveTween = _transform.DOLocalMove(state ? _targetPosition : _startPosition, _duration)
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
