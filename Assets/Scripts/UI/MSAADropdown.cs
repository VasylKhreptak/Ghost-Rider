using ModestTree;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class MSAADropdown : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TMP_Dropdown _dropdown;

    private UniversalRenderPipelineAsset _renderAsset;

    private int[] msaaIndexes = { 1, 2, 4, 8 };
    
    #region MonoBehaviour

    private void OnValidate()
    {
        _dropdown ??= GetComponent<TMP_Dropdown>();
    }

    private void Awake()
    {
        _renderAsset = (UniversalRenderPipelineAsset)GraphicsSettings.currentRenderPipeline;
        _dropdown.value = msaaIndexes.IndexOf(_renderAsset.msaaSampleCount);
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
    }
}
