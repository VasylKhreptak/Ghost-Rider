using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using Zenject;

public class RenderScaleSlider : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private Slider _slider;

	[Header("Preferences")]
	[SerializeField] private float _roundPrecision;
	
	private UniversalRenderPipelineAsset _renderAsset;

	private SettingsProvider _settingsProvider;

	[Inject]
	private void Construct(SettingsProvider settingsProvider)
	{
		_settingsProvider = settingsProvider;
	}
	
	#region MonoBehavoiur

	private void OnValidate()
	{
		_slider ??= GetComponent<Slider>();
	}

	private void Start()
	{
		_renderAsset = (UniversalRenderPipelineAsset)GraphicsSettings.currentRenderPipeline;
		_slider.value = _settingsProvider.settings.renderScale;
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
		float renderScale = Mathf.Round(value * _roundPrecision) / _roundPrecision;
		
		_renderAsset.renderScale = renderScale;

		_settingsProvider.settings.renderScale = renderScale;
	}
}
