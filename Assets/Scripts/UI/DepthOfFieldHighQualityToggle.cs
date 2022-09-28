using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using Zenject;

public class DepthOfFieldHighQualityToggle : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private Toggle _toggle;
	[SerializeField] private Volume _postProcessingVolume;

	private SettingsProvider _settingsProvider;
	
	private DepthOfField _depthOfField;

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
		_postProcessingVolume.profile.TryGet(out _depthOfField);
		_toggle.isOn = _settingsProvider.settings.dofHighQualityEnabled;
		_toggle.interactable = _settingsProvider.settings.postProcessingEnabled;
		_depthOfField.highQualitySampling.value = _settingsProvider.settings.dofHighQualityEnabled;
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
		_depthOfField.highQualitySampling.value = enabled;

		_settingsProvider.settings.dofHighQualityEnabled = enabled;
	}
}
