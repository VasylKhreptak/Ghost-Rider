using DG.Tweening;

public class ScalePunchAnimation : PunchAnimation
{
    public override void Animate(bool state)
    {
        Kill();

        _punchTween = _transform.DOPunchScale(Extensions.Mathf.Sign(state) * _direction * _force, _duration, vibrato, _elasticity)
            .SetEase(_animationCurve).SetLoops(_loops, _loopType);
    }
}
