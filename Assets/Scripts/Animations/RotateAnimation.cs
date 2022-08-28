using DG.Tweening;
using UnityEngine;

public class RotateAnimation : AnimationCore
{
    [Header("References")]
    [SerializeField] private Transform _transform;

    [Header("Preferences")]
    [SerializeField] private Vector3 _startRotation;
    [SerializeField] private Vector3 _targetRotation;
    [SerializeField] private float _duration;
    [SerializeField] private AnimationCurve _animationCurve;
    [SerializeField] private int _loops;
    [SerializeField] private LoopType _loopType;
    
    private Tween _rotateTween;

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

        _transform.localRotation = Quaternion.Euler(state ? _startRotation : _targetRotation);
        
        _rotateTween = _transform.DOLocalRotate(_targetRotation, _duration).SetEase(_animationCurve).
            SetInverted(!state).SetLoops(_loops, _loopType);
    }
    public override void Pause()
    {
        _rotateTween.Pause();
    }
    public override void Play()
    {
        _rotateTween.Play();
    }
    public override void Kill()
    {
        _rotateTween.Kill();
    }
}
