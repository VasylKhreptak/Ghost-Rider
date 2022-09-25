using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class DepthOfFieldHighQualityToggle : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private Toggle _toggle;
	[SerializeField] private Volume _postProcessingVolume;

	private DepthOfField _depthOfField;

	#region MonoBehaviour

	private void OnValidate()
	{
		_toggle ??= GetComponent<Toggle>();
	}

	private void Awake()
	{
		_postProcessingVolume.profile.TryGet(out _depthOfField);
		_toggle.isOn = _depthOfField.highQualitySampling.value;
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
	}
}
