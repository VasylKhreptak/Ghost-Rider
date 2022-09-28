using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MouseSensitivitySlider : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private Slider _slider;
	[SerializeField] private RCC_Camera _camera;

	private SettingsProvider _settingsProvider;

	[Inject]
	private void Construct(SettingsProvider settingsProvider)
	{
		_settingsProvider = settingsProvider;
	}

	#region MonoBehaviour

	private void OnValidate()
	{
		_slider ??= GetComponent<Slider>();
	}

	private void Start()
	{
		_slider.value = _settingsProvider.settings.mouseSensitivity;
	}

	private void OnEnable()
	{
		_slider.onValueChanged.AddListener(SetSensitivity);
	}

	private void OnDisable()
	{
		_slider.onValueChanged.RemoveListener(SetSensitivity);
	}

	#endregion

	private void SetSensitivity(float value)
	{
		_camera.sensitivity = value;
		_settingsProvider.settings.mouseSensitivity = value;
	}
}
