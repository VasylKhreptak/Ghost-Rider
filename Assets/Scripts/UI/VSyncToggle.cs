using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class VSyncToggle : UIUpdatableItem
{
    [Header("References")]
    [SerializeField] private Toggle _toggle;
    [SerializeField] private GameFramerate _gameFramerate;

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
        UpdateValue();
    }

    private void OnEnable()
    {
        _toggle.onValueChanged.AddListener(OnValueChanged);
    }

    private void OnDisable()
    {
        _toggle.onValueChanged.RemoveListener(OnValueChanged);
    }

    #endregion

    public override void UpdateValue()
    {
        _toggle.isOn = _settingsProvider.settings.vSyncEnabled;
        QualitySettings.vSyncCount = _settingsProvider.settings.vSyncEnabled ? 1 : 0;
    }

    private void OnValueChanged(bool value)
    {
        int vSyncCount = value ? 1 : 0;

        QualitySettings.vSyncCount = vSyncCount;

        _settingsProvider.settings.vSyncEnabled = value;

        if (_settingsProvider.settings.maxFramerateEnabled)
        {
            _gameFramerate.Set(int.MaxValue);
        }
    }
}
