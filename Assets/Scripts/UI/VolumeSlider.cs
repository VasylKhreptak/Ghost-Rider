using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected Slider _slider;

    [Header("Preferences")]
    [SerializeField] protected AudioMixerGroup _mixerGroup;
    [SerializeField] protected float _mixerVolumeAmplifier = 20f;
    [SerializeField] protected int _logBase = 2;
    
    #region MonoBehaviour

    private void OnValidate()
    {
        _slider ??= GetComponent<Slider>();

        if (_slider.minValue == 0)
        {
            Debug.LogError("Slider min value cannot be 0, set 0.0...1");
        }
    }

    private void OnEnable()
    {
        _slider.onValueChanged.AddListener(SetVolume);
    }

    private void OnDisable()
    {
        _slider.onValueChanged.RemoveListener(SetVolume);
    }

    #endregion

    protected virtual void SetVolume(float value)
    {
        float newVolume = Mathf.Log(value / _slider.maxValue, _logBase) * _mixerVolumeAmplifier;
        
        _mixerGroup.audioMixer.SetFloat(_mixerGroup.name, newVolume);
    }
}
