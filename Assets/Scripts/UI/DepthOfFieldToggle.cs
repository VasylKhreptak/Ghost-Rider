using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using Zenject;

public class DepthOfFieldToggle : UIUpdatableItem
{
    [Header("References")]
    [SerializeField] private Toggle _toggle;
    [SerializeField] private Volume _postProcessingVolume;

    private SettingsProvider _settingsProvider;

    private DepthOfField _depthOfField;

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
        _postProcessingVolume.profile.TryGet(out _depthOfField);

        UpdateValue();
    }

    private void OnEnable()
    {
        _toggle.onValueChanged.AddListener(SetDepthOfFieldState);
    }

    private void OnDisable()
    {
        _toggle.onValueChanged.RemoveListener(SetDepthOfFieldState);
    }

    #endregion

    public override void UpdateValue()
    {
        _toggle.isOn = _settingsProvider.settings.depthOfFieldEnabled;
        _toggle.interactable = _settingsProvider.settings.postProcessingEnabled;
        _depthOfField.active = _settingsProvider.settings.depthOfFieldEnabled;
        _toggle.onValueChanged?.Invoke(_toggle.isOn);
    }

    private void SetDepthOfFieldState(bool enabled)
    {
        _depthOfField.active = enabled;

        _settingsProvider.settings.depthOfFieldEnabled = enabled;
    }
}
