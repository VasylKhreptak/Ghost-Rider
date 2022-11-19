using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using Zenject;

public class HighSpeedEffectToggle : UIUpdatableItem
{
	[Header("References")]
	[SerializeField] private Volume _volume;
	[SerializeField] private Toggle _toggle;
	[SerializeField] private ChromaticAberrationHighSpeedEffect _chromaticAberrationEffect;
	[SerializeField] private DistortionHighSpeedEffect _distortionEffect;

	private SettingsProvider _settingsProvider;

	private LensDistortion _lensDistortion;
	private ChromaticAberration _chromaticAberration;
	
	[Inject]
	private void Construct(SettingsProvider settingsProvider)
	{
		_settingsProvider = settingsProvider;
	}
	
	#region MonoBehaviour

	private void OnValidate()
	{
		_toggle ??= GetComponent<Toggle>();
		
		if(_volume != null)
		{
			_chromaticAberrationEffect ??= _volume.GetComponent<ChromaticAberrationHighSpeedEffect>();
			_distortionEffect ??= _volume.GetComponent<DistortionHighSpeedEffect>();
		}
	}

	private void Start()
	{
		_volume.profile.TryGet(out _lensDistortion);
		_volume.profile.TryGet(out _chromaticAberration);
	}

	private void OnEnable()
	{
		_toggle.onValueChanged.AddListener(SetHighSpeedEffect);
	}

	private void OnDisable()
	{
		_toggle.onValueChanged.RemoveListener(SetHighSpeedEffect);
	}

	#endregion

	public override void UpdateValue()
	{
		_toggle.isOn = _settingsProvider.settings.highSpeedEffectEnabled;
		_toggle.interactable = _settingsProvider.settings.postProcessingEnabled;
		_chromaticAberration.active = _settingsProvider.settings.highSpeedEffectEnabled;
		_lensDistortion.active = _settingsProvider.settings.highSpeedEffectEnabled;
	}

	private void SetHighSpeedEffect(bool enabled)
	{
		_chromaticAberration.active = enabled;
		_lensDistortion.active = enabled;

		_chromaticAberrationEffect.enabled = enabled;
		_distortionEffect.enabled = enabled;
		
		_settingsProvider.settings.highSpeedEffectEnabled = enabled;
	}
}
