using System;
using UnityEngine;

public class OnEventApplySettings : MonoBehaviour
{
    [Header("Event")]
    [SerializeField] private MonoEvent _monoEvent;

    [Header("References")]
    [SerializeField] private SettingsLoader _settingsloader;

    [Header("Preferences")]
    [SerializeField] private bool _applyVolume = true;
    [SerializeField] private bool _applyInput = true;
    [SerializeField] private bool _applyScreen = true;
    [SerializeField] private bool _applyGraphics = true;

    [Header("Settings")]
    [SerializeField] private Settings _settings;

    public Action onApply;

    #region MonoBehaviour

    private void OnValidate()
    {
        _monoEvent ??= GetComponent<MonoEvent>();
    }

    private void OnEnable()
    {
        _monoEvent.onMonoCall += TryToApplySettings;
    }

    private void OnDisable()
    {
        _monoEvent.onMonoCall -= TryToApplySettings;
    }

    #endregion

    private void TryToApplySettings()
    {
        if (_applyVolume)
        {
            _settingsloader.ApplyVolumeSettings(_settings);
        }

        if (_applyInput)
        {
            _settingsloader.ApplyInputSettings(_settings);
        }

        if (_applyScreen)
        {
            _settingsloader.ApplyScreenSettings(_settings);
        }
        
        if (_applyGraphics)
        {
            _settingsloader.ApplyGraphicsSettings(_settings);
        }

        onApply?.Invoke();
    }
}
