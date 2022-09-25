using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class DepthOfFieldAnimation : AnimationCore
{
    [Header("References")]
    [SerializeField] private Volume _postProcessVolume;

    [Header("Preferences")]
    [SerializeField] private float _startAperture;
    [SerializeField] private float _targetAperture;
    [SerializeField] private float _duration;
    [SerializeField] private AnimationCurve _animationCurve;

    private DepthOfField _depthOfField;

    private Tween _tween;

    #region MonoBehaviour

    private void OnValidate()
    {
        _postProcessVolume ??= GetComponent<Volume>();
    }

    private void Awake()
    {
        _postProcessVolume.profile.TryGet(out _depthOfField);
    }

    private void OnDestroy()
    {
        Kill();
    }

    #endregion

    public override void Animate(bool state)
    {
        Kill();

        _depthOfField.aperture.value = state ? _startAperture : _targetAperture;

        _tween = DOTween
            .To(() => _depthOfField.aperture.value, x => _depthOfField.aperture.value = x,
                state ? _targetAperture : _startAperture, _duration)
            .SetEase(_animationCurve);
    }
    public override void Pause()
    {
        _tween.onPause();
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
