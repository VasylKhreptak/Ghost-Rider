using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;
using Zenject;

public class BloomToggle : UIUpdatableItem
{
	[Header("References")]
	[SerializeField] private Toggle _toggle;
	[SerializeField] private Volume _postProcessingVolume;

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
		_postProcessingVolume.profile.TryGet(out _bloom);
		
		UpdateValue();
	}

	private void OnEnable()
	{
		_toggle.onValueChanged.AddListener(SetBloomState);
	}

	private void OnDisable()
	{
		_toggle.onValueChanged.RemoveListener(SetBloomState);
	}

	#endregion

	public override void UpdateValue()
	{
		_toggle.isOn = _settingsProvider.settings.bloomEnabled;
		_bloom.active = _settingsProvider.settings.bloomEnabled;
		_toggle.interactable = _settingsProvider.settings.postProcessingEnabled;
		_toggle.onValueChanged?.Invoke(_toggle.isOn);
	}

	private void SetBloomState(bool enabled)
	{
		_bloom.active = enabled;

		_settingsProvider.settings.bloomEnabled = enabled;
	}
}
