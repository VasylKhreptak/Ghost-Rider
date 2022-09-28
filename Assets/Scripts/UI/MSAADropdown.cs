using ModestTree;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Zenject;

public class MSAADropdown : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TMP_Dropdown _dropdown;

    private UniversalRenderPipelineAsset _renderAsset;

    private SettingsProvider _settingsProvider;
    
    private int[] msaaIndexes = { 1, 2, 4, 8 };

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
        _dropdown.value = msaaIndexes.IndexOf(_settingsProvider.settings.msaaSampleCount);
    }

    private void OnEnable()
    {
        _dropdown.onValueChanged.AddListener(SetAntiAliasing);
    }

    private void OnDisable()
    {
        _dropdown.onValueChanged.RemoveListener(SetAntiAliasing);
    }

    #endregion

    private void SetAntiAliasing(int index)
    {
        _renderAsset.msaaSampleCount = msaaIndexes[index];

        _settingsProvider.settings.msaaSampleCount = msaaIndexes[index];
    }
}
