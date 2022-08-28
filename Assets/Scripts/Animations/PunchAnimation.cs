using System;
using DG.Tweening;
using UnityEngine;

public class PunchAnimation : AnimationCore
{
    [Header("References")]
    [SerializeField] protected Transform _transform;

    [Header("Preferences")]
    [SerializeField] protected Vector3 _direction;
    [SerializeField] protected float _force;
    [SerializeField] protected float _duration;
    [SerializeField] protected AnimationCurve _animationCurve;
    [SerializeField] protected int vibrato;
    [SerializeField] protected float _elasticity;
    [SerializeField] protected int _loops;
    [SerializeField] protected LoopType _loopType;

    protected Tween _punchTween;

    #region MonoBehaviour

    protected void OnValidate()
    {
        _transform = GetComponent<Transform>();
    }

    protected void OnDestroy()
    {
        Kill();
    }

    #endregion

    public override void Animate(bool state)
    {
        throw new NotImplementedException();
    }
    public override void Pause()
    {
        _punchTween.Pause();
    }
    public override void Play()
    {
        _punchTween.Play();
    }
    public override void Kill()
    {
        _punchTween.Kill();
    }
}
