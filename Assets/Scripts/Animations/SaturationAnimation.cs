using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class SaturationAnimation : AnimationCore
{
    [Header("References")]
    [SerializeField] private Volume _volume;

    [Header("Preferences")]
    [SerializeField] private float _startSuturation;
    [SerializeField] private float _targetSaturation;
    [SerializeField] private float _duration;
    [SerializeField] private AnimationCurve _animationCurve;

    private ColorAdjustments _colorAdjustments;

    private float Saturation { get => _colorAdjustments.saturation.value; set => _colorAdjustments.saturation.value = value; }

    private Tween _tween;

    #region MonoBehaviour

    private void OnValidate()
    {
        _volume ??= GetComponent<Volume>();
    }

    private void Awake()
    {
        _volume.profile.TryGet(out _colorAdjustments);
    }

    private void OnDestroy()
    {
        Kill();
    }

    #endregion

    public override void Animate(bool state)
    {
        Kill();

        if (state)
        {
            _colorAdjustments.active = true;
        }

        Saturation = state ? _startSuturation : _targetSaturation;
        _tween = DOTween
            .To(() => Saturation, x => Saturation = x, state ? _targetSaturation : _startSuturation, _duration)
            .SetEase(_animationCurve)
            .OnComplete(() =>
            {
                if (state == false)
                {
                    _colorAdjustments.active = false;
                }
            });
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
