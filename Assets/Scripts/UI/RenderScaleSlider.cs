using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class RenderScaleSlider : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private Slider _slider;

	[Header("Preferences")]
	[SerializeField] private float _roundPrecision;
	
	private UniversalRenderPipelineAsset _renderAsset;	

	#region MonoBehavoiur

	private void OnValidate()
	{
		_slider ??= GetComponent<Slider>();
	}

	private void Awake()
	{
		_renderAsset = (UniversalRenderPipelineAsset)GraphicsSettings.currentRenderPipeline;
		_slider.value = _renderAsset.renderScale;
	}

	private void OnEnable()
	{
		_slider.onValueChanged.AddListener(SetRenderScale);
	}

	private void OnDisable()
	{
		_slider.onValueChanged.RemoveListener(SetRenderScale);
	}

	#endregion

	private void SetRenderScale(float value)
	{
		_renderAsset.renderScale = Mathf.Round(value * _roundPrecision) / _roundPrecision;
	}
}
