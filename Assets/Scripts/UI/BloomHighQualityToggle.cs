using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using Zenject;

public class BloomHighQualityToggle : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private Volume _volume;
	[SerializeField] private Toggle _toggle;

	private SettingsProvider _settingsProvider;

	private Bloom _bloom;

	[Inject]
	private void Construct(SettingsProvider settingsProvider)
	{
		_settingsProvider = settingsProvider;
	}
	
	#region MonoBehaviour

	private void OnValidate()
	{
		_toggle ??= GetComponent<Toggle>();
	}

	private void Start()
	{
		_volume.profile.TryGet(out _bloom);
		_toggle.isOn = _settingsProvider.settings.bloomHighQualityEnabled;
		_toggle.interactable = _settingsProvider.settings.postProcessingEnabled;
		_bloom.highQualityFiltering.value = _settingsProvider.settings.bloomHighQualityEnabled;
	}

	private void OnEnable()
	{
		_toggle.onValueChanged.AddListener(SetHighQuality);
	}

	private void OnDisable()
	{
		_toggle.onValueChanged.RemoveListener(SetHighQuality);
	}

	#endregion

	private void SetHighQuality(bool enabled)
	{
		_bloom.highQualityFiltering.value = enabled;

		_settingsProvider.settings.bloomHighQualityEnabled = enabled; 
	}
}
