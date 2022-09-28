using ModestTree;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Zenject;

public class UpscalingFilterDropdown : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TMP_Dropdown _dropdown;

    private UniversalRenderPipelineAsset _renderAsset;

    private SettingsProvider _settingsProvider;

    private UpscalingFilterSelection[] _upscalingFilters =
    {
        UpscalingFilterSelection.Auto,
        UpscalingFilterSelection.Linear,
        UpscalingFilterSelection.Point,
        UpscalingFilterSelection.FSR
    };

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
        _renderAsset = (UniversalRenderPipelineAsset)GraphicsSettings.currentRenderPipeline;
        _dropdown.value = _upscalingFilters.IndexOf(_settingsProvider.settings.upscalingFilter);
    }

    private void OnEnable()
    {
        _dropdown.onValueChanged.AddListener(SetUpscaleFilter);
    }

    private void OnDisable()
    {
        _dropdown.onValueChanged.RemoveListener(SetUpscaleFilter);
    }

    #endregion

    private void SetUpscaleFilter(int index)
    {
        _renderAsset.upscalingFilter = _upscalingFilters[index];

        _settingsProvider.settings.upscalingFilter = _upscalingFilters[index];
    }
}
