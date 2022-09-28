using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using Zenject;

public class PostProcessingToggle : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Volume _volume;
    [SerializeField] private Toggle _toggle;

    private SettingsProvider _settingsProvider;

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
        _toggle.isOn = _settingsProvider.settings.postProcessingEnabled;
        _volume.weight = _settingsProvider.settings.postProcessingEnabled ? 1 : 0;
    }

    private void OnEnable()
    {
        _toggle.onValueChanged.AddListener(SetPostProcessingState);
    }

    private void OnDisable()
    {
        _toggle.onValueChanged.RemoveListener(SetPostProcessingState);
    }

    #endregion

    private void SetPostProcessingState(bool enabled)
    {
        _volume.weight = enabled ? 1 : 0;

        _settingsProvider.settings.postProcessingEnabled = enabled;
    }
}
