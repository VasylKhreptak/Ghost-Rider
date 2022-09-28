using UnityEngine;
using Zenject;

public class MusicVolumeSlider : VolumeSlider
{
	private SettingsProvider _settingsProvider;

	[Inject]
	private void Construct(SettingsProvider settingsProvider)
	{
		_settingsProvider = settingsProvider;
	}
	
	#region MonoBehaviour
	
	private void Start()
	{
		float mixerVolume = _settingsProvider.settings.musicMixerVolume;

		_slider.value = Mathf.Pow(2, mixerVolume / _mixerVolumeAmplifier) * _slider.maxValue;
	}
	protected override void SetVolume(float value)
	{
		float newVolume = Mathf.Log(value / _slider.maxValue, _logBase) * _mixerVolumeAmplifier;
        
		_mixerGroup.audioMixer.SetFloat(_mixerGroup.name, newVolume);

		_settingsProvider.settings.musicMixerVolume = newVolume;
	}

	#endregion
}
