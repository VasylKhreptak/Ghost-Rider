using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using Zenject;

public class VignetteToggle : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Toggle _toggle;
    [SerializeField] private Volume _postProcessingVolume;

    private SettingsProvider _settingsProvider;
    
    private Vignette _vignette;

    [Inject]
    private void Construct(SettingsProvider settingsProvider)
    {
        _settingsProvider = settingsProvider;
    }

    #region MonoBehaviour

    private void OnValidate()
    {
        _toggle ??= GetComponent<Toggle>();
    }

    private void Start()
    {
        _postProcessingVolume.profile.TryGet(out _vignette);
        _toggle.isOn = _settingsProvider.settings.vignetteEnabled;
        _toggle.interactable = _settingsProvider.settings.postProcessingEnabled;
        _vignette.active = _settingsProvider.settings.vignetteEnabled;
    }

    private void OnEnable()
    {
        _toggle.onValueChanged.AddListener(SetVignetteState);
    }

    private void OnDisable()
    {
        _toggle.onValueChanged.RemoveListener(SetVignetteState);
    }

    #endregion

    private void SetVignetteState(bool enabled)
    {
        _vignette.active = enabled;

        _settingsProvider.settings.vignetteEnabled = enabled;
    }
}
