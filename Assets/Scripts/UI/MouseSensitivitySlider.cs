using System;
using UnityEngine;
using UnityEngine.UI;

public class MouseSensitivitySlider : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private Slider _slider;
	[SerializeField] private RCC_Camera _camera;

	#region MonoBehaviour

	private void OnValidate()
	{
		_slider ??= GetComponent<Slider>();
	}

	private void Awake()
	{
		_slider.value = _camera.sensitivity;
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
	}
}
