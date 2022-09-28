using System;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using Zenject;

public class TextureResolutionDropdown : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private TMP_Dropdown _dropdown;

	private SettingsProvider _settingsProvider;

	[Inject]
	private void Construct(SettingsProvider settingsProvider)
	{
		_settingsProvider = settingsProvider;
	}
	
	#region MonoBehaviour

	private void OnValidate()
	{
		_dropdown ??= GetComponent<TMP_Dropdown>();
	}

	private void Start()
	{
		_dropdown.value = _settingsProvider.settings.masterTextureLimit;
		QualitySettings.masterTextureLimit = _settingsProvider.settings.masterTextureLimit;
	}

	private void OnEnable()
	{
		_dropdown.onValueChanged.AddListener(SetResolution);
	}

	private void OnDisable()
	{
		_dropdown.onValueChanged.RemoveListener(SetResolution);
	}

	#endregion

	private void SetResolution(int index)
	{
		QualitySettings.masterTextureLimit = index;

		_settingsProvider.settings.masterTextureLimit = index;
	}
}
