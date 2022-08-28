using DG.Tweening;

public class PositionPunchAnimation : PunchAnimation
{
    public override void Animate(bool state)
    {
        Kill();

        _punchTween = _transform.DOPunchPosition(Extensions.Mathf.Sign(state) * _direction * _force, _duration, vibrato, _elasticity)
            .SetEase(_animationCurve).SetLoops(_loops, _loopType);
    }
}
