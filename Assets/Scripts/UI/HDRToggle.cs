using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class HDRToggle : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private Toggle _toggle;
	[SerializeField] private Volume _volume;

	private UniversalRenderPipelineAsset _renderAsset;

	#region MonoBaheviour

	private void OnValidate()
	{
		_toggle ??= GetComponent<Toggle>();
	}

	private void Awake()
	{
		_renderAsset = (UniversalRenderPipelineAsset)GraphicsSettings.currentRenderPipeline;
		_toggle.isOn = _renderAsset.supportsHDR;
		_toggle.interactable = Mathf.Approximately(_volume.weight, 0);
	}

	private void OnEnable()
	{
		_toggle.onValueChanged.AddListener(SetHDR);
	}

	private void OnDisable()
	{
		_toggle.onValueChanged.RemoveListener(SetHDR);
	}

	#endregion

	private void SetHDR(bool enabled)
	{
		_renderAsset.supportsHDR = enabled;
	}
}
