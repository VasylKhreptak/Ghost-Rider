using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SmoothSlider : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Slider _slider;

    [Header("Preferences")]
    [SerializeField] private float _duration;
    [SerializeField] private AnimationCurve _curve;

    private Tween _tween;

    public float value
    {
        get => _slider.value;
        set => SetValue(value);
    }

    #region MonoBehaviour

    private void OnValidate()
    {
        _slider ??= GetComponent<Slider>();
    }

    #endregion
    
    private void OnDisable()
    {
        _tween.Kill();
    }

    private void SetValue(float value)
    {
        _tween.Kill();
        
        _tween = DOTween
            .To(() => _slider.value, x => _slider.value = x, value, _duration)
            .SetEase(_curve);
    }
}
