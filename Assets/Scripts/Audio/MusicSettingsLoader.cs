using System;
using UnityEngine;
using UnityEngine.Audio;
using Zenject;

public class MusicSettingsLoader : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private AudioMixerGroup _musicOutput;

	private SettingsProvider _settingsProvider;

	[Inject]
	private void Construct(SettingsProvider settingsProvider)
	{
		_settingsProvider = settingsProvider;
	}

	#region MonoBehaviour

	private void Start()
	{
		_musicOutput.audioMixer.SetFloat(_musicOutput.name, _settingsProvider.settings.musicMixerVolume);
	}

	#endregion
}
