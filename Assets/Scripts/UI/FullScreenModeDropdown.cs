using ModestTree;
using TMPro;
using UnityEngine;
using Zenject;

public class FullScreenModeDropdown : UIUpdatableItem
{
    [Header("References")]
    [SerializeField] private TMP_Dropdown _dropdown;

    private SettingsProvider _settingsProvider;

    private readonly FullScreenMode[] _screenModes =
    {
        FullScreenMode.Windowed,
        FullScreenMode.ExclusiveFullScreen,
        FullScreenMode.FullScreenWindow
    };

    [Inject]
    private void Construct(SettingsProvider settingsProvider)
    {
        _settingsProvider = settingsProvider;
    }

    #region MonoBehaviour

    private void Start()
    {
        Screen.fullScreenMode = _settingsProvider.settings.fullScreenMode;
        
        UpdateValue();
    }

    public override void UpdateValue()
    {
        _dropdown.value = _screenModes.IndexOf(_settingsProvider.settings.fullScreenMode);
        Screen.fullScreenMode = _screenModes[_dropdown.value];
    }

    private void OnValidate()
    {
        _dropdown ??= GetComponent<TMP_Dropdown>();
    }

    private void OnEnable()
    {
        _dropdown.onValueChanged.AddListener(SetFullScreenMode);
    }

    private void OnDisable()
    {
        _dropdown.onValueChanged.RemoveListener(SetFullScreenMode);
    }

    #endregion

    private void SetFullScreenMode(int index)
    {
        Screen.fullScreenMode = _screenModes[index];

        _settingsProvider.settings.fullScreenMode = _screenModes[index];
    }
}
