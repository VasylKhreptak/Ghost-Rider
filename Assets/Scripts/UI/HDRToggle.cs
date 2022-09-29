using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using Zenject;

public class HDRToggle : UIUpdatableItem
{
    [Header("References")]
    [SerializeField] private Toggle _toggle;
    [SerializeField] private Volume _volume;

    private SettingsProvider _settingsProvider;

    private UniversalRenderPipelineAsset _renderAsset;

    [Inject]
    private void Construct(SettingsProvider settingsProvider)
    {
        _settingsProvider = settingsProvider;
    }

    #region MonoBaheviour

    private void OnValidate()
    {
        _toggle ??= GetComponent<Toggle>();
    }

    private void Start()
    {
        _renderAsset = (UniversalRenderPipelineAsset)GraphicsSettings.currentRenderPipeline;

        UpdateValue();
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

    public override void UpdateValue()
    {
        _toggle.isOn = _settingsProvider.settings.hdrEnabled;
        _toggle.interactable = _settingsProvider.settings.postProcessingEnabled;
        _renderAsset.supportsHDR = _settingsProvider.settings.hdrEnabled;
    }

    private void SetHDR(bool enabled)
    {
        _renderAsset.supportsHDR = enabled;

        _settingsProvider.settings.hdrEnabled = enabled;
    }
}
