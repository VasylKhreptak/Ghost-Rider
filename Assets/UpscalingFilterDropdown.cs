using ModestTree;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class UpscalingFilterDropdown : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TMP_Dropdown _dropdown;

    private UniversalRenderPipelineAsset _renderAsset;

    private UpscalingFilterSelection[] _upscalingFilters =
    {
        UpscalingFilterSelection.Auto,
        UpscalingFilterSelection.Linear,
        UpscalingFilterSelection.Point,
        UpscalingFilterSelection.FSR
    };

    #region MonoBehaviour

    private void OnValidate()
    {
        _dropdown ??= GetComponent<TMP_Dropdown>();
    }

    private void Awake()
    {
        _renderAsset = (UniversalRenderPipelineAsset)GraphicsSettings.currentRenderPipeline;
        _dropdown.value = _upscalingFilters.IndexOf(_renderAsset.upscalingFilter);
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
    }
}
