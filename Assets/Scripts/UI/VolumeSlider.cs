using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Slider _slider;

    [Header("Preferences")]
    [SerializeField] private AudioMixerGroup _mixerGroup;
    [SerializeField] private AnimationCurve _valueCurve;
    [SerializeField] private float _mixerMin = -80;
    [SerializeField] private float _mixerMax = 0;

    #region MonoBehaviour

    private void OnValidate()
    {
        _slider ??= GetComponent<Slider>();
    }

    private void Awake()
    {
        _mixerGroup.audioMixer.GetFloat(_mixerGroup.name, out float mixerVolume);
        _slider.value = mixerVolume.Remap(_mixerMin, _mixerMax, _slider.minValue, _slider.maxValue);
    }

    private void OnEnable()
    {
        _slider.onValueChanged.AddListener(OnValueChanged);
    }

    private void OnDisable()
    {
        _slider.onValueChanged.RemoveListener(OnValueChanged);
    }

    #endregion

    private void OnValueChanged(float value)
    {
        float newValue = (_valueCurve.Evaluate(_slider.value / _slider.maxValue) * _slider.maxValue)
            .Remap(_slider.minValue, _slider.maxValue, _mixerMin, _mixerMax);
        _mixerGroup.audioMixer.SetFloat(_mixerGroup.name, newValue);
    }
}
